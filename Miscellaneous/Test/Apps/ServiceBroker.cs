using System.Data.SqlClient;
using System.Text;
using System.Xml;
using Selero.Core;

namespace Test.Apps
{
	public class ServiceBroker
	{
		#region Variables

		private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
		private string _dbDatabase = string.Empty;
		private string _dbPassword = string.Empty;
		private string _dbServer = string.Empty;
		private string _dbUsername = string.Empty;
		private string _endConvCommand = string.Empty;
		private string _endDialogType = string.Empty;
		private string _errorType = string.Empty;
		private Routines _routines = new Routines();
		private static string _updateApp = "TestApp";
		private static string _updateMachine = Environment.MachineName;
		private static string _updateUser = Environment.UserName;
		private string _waitForCommand = string.Empty;

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		public void Process(XmlNode settings)
		{
			try {
				if (_GetSettings(settings)) {
					_StartService();

					Console.WriteLine("Service started");
					Console.WriteLine("--- Press ENTER to stop ---");
					Console.ReadLine();

					_StopService();
				}
			}
			catch { }
		}

		#endregion Public

		#region Private

		private bool _BuildSqlConnection(out string connectionString)
		{
			bool returnValue = false;
			connectionString = string.Empty;

			try {
				if (!string.IsNullOrEmpty(_dbServer) && !string.IsNullOrEmpty(_dbDatabase)) {
					connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};{4}", _dbServer, _dbDatabase, _dbUsername, _dbPassword,
						(string.IsNullOrEmpty(_dbUsername) && string.IsNullOrEmpty(_dbPassword)) ? "Trusted_Connection=Yes" : "");

					returnValue = true;
				}
			}
			catch { }

			return returnValue;
		}

		private bool _GetSettings(XmlNode settings)
		{
			bool returnValue = false;

			try {
				XmlNode database = settings.SelectSingleNode("database");
				_dbServer = _routines.GetAttribute<string>(database, "server");
				_dbDatabase = _routines.GetAttribute<string>(database, "name");
				_dbUsername = _routines.GetAttribute<string>(database, "username");
				_dbPassword = _routines.GetAttribute<string>(database, "password");

				XmlNode process = settings.SelectSingleNode("process");
				_waitForCommand = _routines.GetAttribute<string>(process, "waitforcommand");
				_endConvCommand = _routines.GetAttribute<string>(process, "endconvcommand");
				_endDialogType = _routines.GetAttribute<string>(process, "enddialogtype");
				_errorType = _routines.GetAttribute<string>(process, "errortype");

				returnValue = true;
			}
			catch { }

			return returnValue;
		}

		private async void _ReadMessages()
		{
			if (_BuildSqlConnection(out string connectionString)) {
				bool cancelled = false;
				bool endConversation = false;
				bool readerHasData = false;
				string request = string.Empty;

				System.Data.SqlTypes.SqlGuid conversationHandle = new System.Data.SqlTypes.SqlGuid();

				using (SqlConnection connection = new SqlConnection(connectionString)) {
					do {
						try {
							await connection.OpenAsync(_cancellationToken.Token);

							while (true) {
								using (SqlCommand sqlCommand = new SqlCommand(_waitForCommand, connection)) {
									using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync(_cancellationToken.Token)) {
										if (sqlDataReader.Read()) {
											readerHasData = true;

											if (sqlDataReader.GetString(2) != _endDialogType && sqlDataReader.GetString(2) != _errorType) {
												byte[] bytes = sqlDataReader.GetSqlBytes(1).Value;

												// Get received request message
												request = Encoding.Unicode.GetString(bytes);

												conversationHandle = sqlDataReader.GetSqlGuid(0);
											}
											else
												endConversation = true;
										}
									}
								}

								if (readerHasData && !endConversation) {
									if (_ProcessRequest(request, out string message)) {
										using (SqlCommand endConvCommand = new SqlCommand(string.Format(_endConvCommand, conversationHandle), connection)) {
											try {
												endConvCommand.ExecuteNonQuery();
												readerHasData = false;
											}
											catch (Exception) {
											}
										}

										if (!string.IsNullOrEmpty(message)) {

										}
									}
								}
							}
						}
						catch (Exception ex) {
							if (ex is OperationCanceledException)
								cancelled = true;
							else {
								if (connection.State == System.Data.ConnectionState.Open)
									connection.Close();
							}
						}
					} while (!cancelled);
				}
			}
		}

		private bool _ProcessRequest(string request, out string message)
		{
			bool returnValue = false;
			message = string.Empty;

			try {
				Data.NonQualifiedPrescription nonQualifiedPrescription = new Data.NonQualifiedPrescription(_dbServer, _dbDatabase, _dbUsername, _dbPassword);

				if (nonQualifiedPrescription.ParseServiceBroker(request)) {
					long uid = nonQualifiedPrescription.NonQualifiedPrescriptionUID;

					if (nonQualifiedPrescription.Update(uid, "Processed", _updateUser, _updateApp, _updateMachine))
						returnValue = true;
				}
			}
			catch (Exception) {
				returnValue = false;
			}

			return returnValue;
		}

		private void _StartService()
		{
			Task mainTask = new Task(() => _ReadMessages(), _cancellationToken.Token);
			mainTask.Start();
		}

		private void _StopService()
		{
			_cancellationToken.Cancel();
		}

		#endregion Private

		#endregion Methods
	}
}
