using Selero.Selena;
using System.Reflection;

namespace Test.Data
{
	/// <summary>
	///
	/// </summary>
	/// <history>
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// Date		Author					Description
	/// ==========	=================		=======================================================
	/// 03/03/2023	Data Tier Generator		Initial Creation
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// </history>
	public class NonQualifiedPrescription : BaseData
	{
		#region Variables

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		public bool ParseServiceBroker(string data)
		{
			bool returnValue = true;

			try {
				string[] pairs = data.Split('|');

				foreach (string pair in pairs) {
					string[] pieces = pair.Split(':');

					if (pieces.Length > 0) {
						PropertyInfo pi = this.GetType().GetProperty(pieces[0]);
						pi.SetValue(this, Convert.ChangeType(pieces[1], pi.PropertyType));
					}
				}
			}
			catch {
				returnValue = false;
			}

			return returnValue;
		}

		public bool Update(long nonQualifiedPrescriptionUID, string status, string updatedBy, string updatedApp, string updatedMachine)
		{
			bool returnValue = false;

			try {
				// Define the input parameters
				Dictionary<string, object> inputParameters = new Dictionary<string, object>
				{
					{ "NonQualifiedPrescriptionUID", nonQualifiedPrescriptionUID },
					{ "Status", status },
					{ "UpdatedBy", updatedBy },
					{ "UpdatedApp", updatedApp },
					{ "UpdatedMachine", updatedMachine }
				};

				returnValue = base.ExecuteStoredProcedure("csp_NonQualifiedPrescription_UpdateStatus", inputParameters);
			}
			catch (Exception) {
				throw;
			}

			return returnValue;
		}

		#endregion Public

		#region Private

		#endregion Private

		#endregion Methods

		#region DTG Code - DO NOT MODIFY

		#region Generated Variables

		private long _nonQualifiedPrescriptionUID;
		private string _entityID;
		private string _rxProductNDC;
		private string _rxPrescriberNPI;
		private string _status;
		private string _statusMachine;
		private string _statusApp;
		private DateTime _statusDate;
		private string _statusBy;
		private string _createdMachine;
		private string _createdApp;
		private DateTime _createdDate;
		private string _createdBy;
		private string _updatedMachine;
		private string _updatedApp;
		private DateTime _updatedDate;
		private string _updatedBy;

		#endregion Generated Variables

		#region Generated Properties

		/// <summary>
		/// Gets/Sets the NonQualifiedPrescriptionUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "NonQualifiedPrescriptionUID")]
		public long NonQualifiedPrescriptionUID
		{
			get { return _nonQualifiedPrescriptionUID; }
			set { _nonQualifiedPrescriptionUID = value; }
		}

		/// <summary>
		/// Gets/Sets the EntityID property.
		/// </summary>
		[ColumnProperty(ColumnName = "EntityID")]
		public string EntityID
		{
			get { return _entityID; }
			set { _entityID = value; }
		}

		/// <summary>
		/// Gets/Sets the RxProductNDC property.
		/// </summary>
		[ColumnProperty(ColumnName = "RxProductNDC")]
		public string RxProductNDC
		{
			get { return _rxProductNDC; }
			set { _rxProductNDC = value; }
		}

		/// <summary>
		/// Gets/Sets the RxPrescriberNPI property.
		/// </summary>
		[ColumnProperty(ColumnName = "RxPrescriberNPI")]
		public string RxPrescriberNPI
		{
			get { return _rxPrescriberNPI; }
			set { _rxPrescriberNPI = value; }
		}

		/// <summary>
		/// Gets/Sets the Status property.
		/// </summary>
		[ColumnProperty(ColumnName = "Status")]
		public string Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// Gets/Sets the StatusMachine property.
		/// </summary>
		[ColumnProperty(ColumnName = "StatusMachine", AddOnly = true, DefaultMachine = true)]
		public string StatusMachine
		{
			get { return _statusMachine; }
			set { _statusMachine = value; }
		}

		/// <summary>
		/// Gets/Sets the StatusApp property.
		/// </summary>
		[ColumnProperty(ColumnName = "StatusApp", AddOnly = true, DefaultApp = true)]
		public string StatusApp
		{
			get { return _statusApp; }
			set { _statusApp = value; }
		}

		/// <summary>
		/// Gets/Sets the StatusDate property.
		/// </summary>
		[ColumnProperty(ColumnName = "StatusDate", AddOnly = true, DefaultDate = true)]
		public DateTime StatusDate
		{
			get { return _statusDate; }
			set { _statusDate = value; }
		}

		/// <summary>
		/// Gets/Sets the StatusBy property.
		/// </summary>
		[ColumnProperty(ColumnName = "StatusBy", AddOnly = true, DefaultUser = true)]
		public string StatusBy
		{
			get { return _statusBy; }
			set { _statusBy = value; }
		}

		/// <summary>
		/// Gets/Sets the CreatedMachine property.
		/// </summary>
		[ColumnProperty(ColumnName = "CreatedMachine", AddOnly = true, DefaultMachine = true)]
		public string CreatedMachine
		{
			get { return _createdMachine; }
			set { _createdMachine = value; }
		}

		/// <summary>
		/// Gets/Sets the CreatedApp property.
		/// </summary>
		[ColumnProperty(ColumnName = "CreatedApp", AddOnly = true, DefaultApp = true)]
		public string CreatedApp
		{
			get { return _createdApp; }
			set { _createdApp = value; }
		}

		/// <summary>
		/// Gets/Sets the CreatedDate property.
		/// </summary>
		[ColumnProperty(ColumnName = "CreatedDate", AddOnly = true, DefaultDate = true)]
		public DateTime CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}

		/// <summary>
		/// Gets/Sets the CreatedBy property.
		/// </summary>
		[ColumnProperty(ColumnName = "CreatedBy", AddOnly = true, DefaultUser = true)]
		public string CreatedBy
		{
			get { return _createdBy; }
			set { _createdBy = value; }
		}

		/// <summary>
		/// Gets/Sets the UpdatedMachine property.
		/// </summary>
		[ColumnProperty(ColumnName = "UpdatedMachine", DefaultMachine = true)]
		public string UpdatedMachine
		{
			get { return _updatedMachine; }
			set { _updatedMachine = value; }
		}

		/// <summary>
		/// Gets/Sets the UpdatedApp property.
		/// </summary>
		[ColumnProperty(ColumnName = "UpdatedApp", DefaultApp = true)]
		public string UpdatedApp
		{
			get { return _updatedApp; }
			set { _updatedApp = value; }
		}

		/// <summary>
		/// Gets/Sets the UpdatedDate property.
		/// </summary>
		[ColumnProperty(ColumnName = "UpdatedDate", DefaultDate = true)]
		public DateTime UpdatedDate
		{
			get { return _updatedDate; }
			set { _updatedDate = value; }
		}

		/// <summary>
		/// Gets/Sets the UpdatedBy property.
		/// </summary>
		[ColumnProperty(ColumnName = "UpdatedBy", DefaultUser = true)]
		public string UpdatedBy
		{
			get { return _updatedBy; }
			set { _updatedBy = value; }
		}

		#endregion Generated Properties

		#region Generated Constructors

		/// <inheritdoc/>
		public NonQualifiedPrescription() : base() { }

		/// <inheritdoc/>
		public NonQualifiedPrescription(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public NonQualifiedPrescription(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the NonQualifiedPrescription class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="entityID"></param>
		/// <param name="rxProductNDC"></param>
		/// <param name="rxPrescriberNPI"></param>
		/// <param name="status"></param>
		/// <param name="statusMachine"></param>
		/// <param name="statusApp"></param>
		/// <param name="statusDate"></param>
		/// <param name="statusBy"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public NonQualifiedPrescription(string dbServer, string dbDatabase, string dbUsername, string dbPassword, string entityID, string rxProductNDC, string rxPrescriberNPI, string status, string statusMachine, string statusApp, DateTime statusDate, string statusBy, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_entityID = entityID;
			_rxProductNDC = rxProductNDC;
			_rxPrescriberNPI = rxPrescriberNPI;
			_status = status;
			_statusMachine = statusMachine;
			_statusApp = statusApp;
			_statusDate = statusDate;
			_statusBy = statusBy;
			_createdMachine = createdMachine;
			_createdApp = createdApp;
			_createdDate = createdDate;
			_createdBy = createdBy;
			_updatedMachine = updatedMachine;
			_updatedApp = updatedApp;
			_updatedDate = updatedDate;
			_updatedBy = updatedBy;
		}

		#endregion Generated Constructors

		#region Generated Methods

		#region Generated Public

		/// <summary>
		/// Returns the record for the specified NonQualifiedPrescriptionUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long nonQualifiedPrescriptionUID)
		{
			try {
				return base.Find(nonQualifiedPrescriptionUID);
			}
			catch (Exception) {
				throw;
			}
		}

		#endregion Generated Public

		#region Generated Protected

		/// <summary>
		/// Sets variables to be used in base data class.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected override void SetupDatabase()
		{
			base.SetupDatabase();

			_cspAdd = "csp_NonQualifiedPrescription_Add";
			_cspUpdate = "csp_NonQualifiedPrescription_Update";
			_cspFind = "csp_NonQualifiedPrescription_Find";
			_cspDelete = "csp_NonQualifiedPrescription_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class NonQualifiedPrescriptions : BaseDataCollection<NonQualifiedPrescription>
	{
		#region Variables

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		#endregion Public

		#region Private

		#endregion Private

		#endregion Methods

		#region DTG Code - DO NOT MODIFY

		#region Generated Variables

		#endregion Generated Variables

		#region Generated Properties

		#endregion Generated Properties

		#region Generated Constructors

		public NonQualifiedPrescriptions()
		{
			_SetupDatabase();
		}

		public NonQualifiedPrescriptions(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_SetupDatabase();
		}

		#endregion Generated Constructors

		#region Generated Methods

		#region Generated Public

		public bool Find()
		{
			return Find(false);
		}

		public bool Find(bool deleted)
		{
			return base.Find(deleted);
		}

		#endregion Generated Public

		#region Generated Private

		private void _SetupDatabase()
		{
			_cspFind = "csp_NonQualifiedPrescriptions_Find";
			_cspFindParameterNames.Clear();
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
