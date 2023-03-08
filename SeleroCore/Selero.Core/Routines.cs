using System.Data;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Xml;
using OfficeOpenXml;

namespace Selero.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class Routines
	{
		#region Variables

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		/// <summary>
		/// Creates a zip archive that contains the files and directories from the specified directory.
		/// </summary>
		/// <param name="sourceDirectory">The path to the directory to be archived.</param>
		/// <param name="destinationArchiveName">The path to the archive to be created.</param>
		/// <param name="minimumAgeInterval">The number of months (MONTHS), days (DAYS), hours (HOURS), or minutes (MINUTES).</param>
		/// <param name="minimumAge">The interval required to have passed since the last write time to the source directory.</param>
		/// <param name="verify">If true, the files in the source directory are verified they exist in the archive.</param>
		/// <param name="deleteSource">If true, delete the source directory and all its subdirectories and files.</param>
		/// <param name="messages">List of messages describing the result of the compression.</param>/// 
		/// <returns>True if the directory has been successfully archived; False otherwise.</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 01/15/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool CompressDirectory(string sourceDirectory, string destinationArchiveName, string minimumAgeInterval, int minimumAge, bool verify, bool deleteSource, ref List<string> messages)
		{
			bool returnValue = true;

			try {
				// Check to see if the source directory exists
				if (Directory.Exists(sourceDirectory)) {
					// Get the most recent file written to the source directory, including subdirectories
					FileInfo file = new DirectoryInfo(sourceDirectory).GetFiles("*", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

					// Check to see if the archived file exists
					if (!File.Exists(destinationArchiveName)) {
						DateTime minimumDate = DateTime.MaxValue;

						switch (minimumAgeInterval.ToUpper()) {
							case "DAYS":
								minimumDate = DateTime.Now.AddDays(-1 * minimumAge).Date;
								break;

							case "HOURS":
								minimumDate = DateTime.Now.AddHours(-1 * minimumAge);
								break;

							case "MINUTES":
								minimumDate = DateTime.Now.AddMinutes(-1 * minimumAge);
								break;

							case "MONTHS":
								minimumDate = DateTime.Now.AddMonths(-1 * minimumAge).Date;
								break;
						}

						// Make sure the source directory meets the specified threshold
						if (minimumAge == 0 || file.LastWriteTime < minimumDate) {
							ZipFile.CreateFromDirectory(sourceDirectory, destinationArchiveName);

							messages.Add(string.Format("Created archive '{0}'.", destinationArchiveName));

							if (verify) {
								returnValue = _VerifyArchive(sourceDirectory, destinationArchiveName, ref messages);

								if (returnValue == true)
									messages.Add(string.Format("Verified archive '{0}'.", destinationArchiveName));
								else
									messages.Add(string.Format("Failed to verify archive '{0}'.", destinationArchiveName));
							}

							// If specified, delete the source directory and all subdirectories and files
							if (returnValue == true && deleteSource) {
								Directory.Delete(sourceDirectory, true);

								messages.Add(string.Format("Deleted directory '{0}'.", sourceDirectory));
							}
						}
						else
							messages.Add(string.Format("Directory '{0}' is not older than {1} {2}.", sourceDirectory, minimumAge, minimumAgeInterval.ToLower()));
					}
					else {
						messages.Add(string.Format("Archive '{0}' already exist.", destinationArchiveName));

						returnValue = _VerifyArchive(sourceDirectory, destinationArchiveName, ref messages);

						if (returnValue == true)
							messages.Add(string.Format("Verified archive '{0}'.", destinationArchiveName));
						else
							messages.Add(string.Format("Failed to verify archive '{0}'.", destinationArchiveName));

						// If specified, delete the source directory and all subdirectories and files
						if (returnValue == true && deleteSource) {
							Directory.Delete(sourceDirectory, true);

							messages.Add(string.Format("Deleted directory '{0}'.", sourceDirectory));
						}
					}
				}
				else
					messages.Add(string.Format("Directory '{0}' does not exist.", sourceDirectory));
			}
			catch (Exception ex) {
				messages.Add(ex.Message);
				messages.Add(ex.StackTrace);

				returnValue = false;
			}

			return returnValue;
		}

		/// <summary>
		/// Creates a compressed file in the zip archive format.
		/// </summary>
		/// <param name="sourceFileName">The path to the file to be compressed.</param>
		/// <param name="destinationArchiveName">The name of the entry to create in the zip archive.</param>
		/// <param name="messages">List of messages describing the result of the compression.</param>
		/// <param name="minimumAgeInterval">The number of months (MONTHS), days (DAYS), hours (HOURS), or minutes (MINUTES).</param>
		/// <param name="minimumAge">The interval required to have passed since the last write time to the file.</param>
		/// <param name="deleteSource">If true, delete the source file.</param>
		/// <returns>True if the file has been successfully archived; False otherwise.</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 01/15/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool CompressFile(string sourceFileName, string destinationArchiveName, string minimumAgeInterval, int minimumAge, bool deleteSource, ref List<string> messages)
		{
			bool returnValue = true;

			try {
				FileInfo sourceFile = new FileInfo(sourceFileName);
				FileInfo destinationFile = new FileInfo(destinationArchiveName);

				if (sourceFile.Exists) {
					if (!destinationFile.Exists) {
						if (!_IsCompressedFile(sourceFile.FullName, ref messages)) {
							DateTime minimumDate = DateTime.MaxValue;

							switch (minimumAgeInterval.ToUpper()) {
								case "DAYS":
									minimumDate = DateTime.Now.AddDays(-1 * minimumAge).Date;
									break;

								case "HOURS":
									minimumDate = DateTime.Now.AddHours(-1 * minimumAge);
									break;

								case "MINUTES":
									minimumDate = DateTime.Now.AddMinutes(-1 * minimumAge);
									break;

								case "MONTHS":
									minimumDate = DateTime.Now.AddMonths(-1 * minimumAge).Date;
									break;
							}

							// Make sure the source file meets the specified threshold
							if (minimumAge == 0 || sourceFile.LastWriteTime < minimumDate) {
								using (ZipArchive zipArchive = ZipFile.Open(destinationArchiveName, ZipArchiveMode.Create)) {
									zipArchive.CreateEntryFromFile(sourceFileName, Path.GetFileName(sourceFileName));

									messages.Add(string.Format("Created archive '{0}'.", destinationArchiveName));

									if (deleteSource) {
										sourceFile.Delete();

										messages.Add(string.Format("Deleted file '{0}'.", sourceFileName));
									}
								}
							}
							else
								messages.Add(string.Format("File '{0}' is not older than {1} {2}.", sourceFileName, minimumAge, minimumAgeInterval.ToLower()));
						}
					}
					else
						messages.Add(string.Format("Archive '{0}' already exist.", destinationArchiveName));
				}
				else
					messages.Add(string.Format("File '{0}' does not exist.", sourceFileName));
			}
			catch (Exception ex) {
				messages.Add(ex.Message);
				messages.Add(ex.StackTrace);

				returnValue = false;
			}

			return returnValue;
		}

		/// <summary>
		/// Creates a SqlParameter object from the specified name and value as in input only
		/// parameter
		/// </summary>
		/// <param name="name">The parameter name</param>
		/// <param name="value">The parameter value</param>
		/// <param name="type"></param>
		/// <returns>SqlParameter object</returns>
		public SqlParameter CreateParameter(string name, object value, Type type)
		{
			return CreateParameter(name, value, type, false);
		}

		/// <summary>
		/// Creates a SqlParameter object from the specified name and value with the direction
		/// determined by the specified flags.
		/// parameter
		/// </summary>
		/// <param name="name">The parameter name</param>
		/// <param name="value">The parameter value</param>
		/// <param name="type"></param>
		/// <param name="isOutput">Flag indicating whether or not the parameter is an Output parameter</param>
		/// <returns>SqlParameter object</returns>
		public SqlParameter CreateParameter(string name, object value, Type type, bool isOutput)
		{
			SqlParameter parameter;

			try {
				// Determine the corresponding SQL Database type
				SqlDbType dbType = SqlDbType.Variant;

				if (type == typeof(byte) || type == typeof(byte?))
					dbType = SqlDbType.TinyInt;
				else if (type == typeof(bool) || type == typeof(bool?))
					dbType = SqlDbType.Bit;
				else if (type == typeof(long) || type == typeof(Int64) || type == typeof(long?) || type == typeof(Int64?))
					dbType = SqlDbType.BigInt;
				else if (type == typeof(int) || type == typeof(Int32) || type == typeof(int?) || type == typeof(Int32?))
					dbType = SqlDbType.Int;
				else if (type == typeof(short) || type == typeof(Int16) || type == typeof(short?) || type == typeof(Int16?))
					dbType = SqlDbType.SmallInt;
				else if (type == typeof(decimal) || type == typeof(decimal?))
					dbType = SqlDbType.Decimal;
				else if (type == typeof(double) || type == typeof(double?))
					dbType = SqlDbType.Decimal;
				else if (type == typeof(string))
					dbType = SqlDbType.Text;
				else if (type == typeof(DateTime) || type == typeof(DateTime?))
					dbType = SqlDbType.DateTime;
				else if (type == typeof(System.Guid) || type == typeof(System.Guid?))
					dbType = SqlDbType.UniqueIdentifier;
				else if (type == typeof(List<long>) || type == typeof(List<string>) || type == typeof(List<object>)) {
					dbType = SqlDbType.Structured;
					isOutput = false;
				}
				else if (type == typeof(byte[]))
					dbType = SqlDbType.VarBinary;
				else
					dbType = SqlDbType.Variant;

				parameter = new SqlParameter(name, dbType);

				if (value != null && !value.Equals(DBNull.Value))
					parameter.Value = value;
				else
					parameter.Value = DBNull.Value;

				if (isOutput && type == typeof(string)) {
					parameter.SqlDbType = SqlDbType.VarChar;
					parameter.Size = 8000;
				}

				// Set the direction of the parameter
				if (isOutput)
					parameter.Direction = ParameterDirection.InputOutput;
				else
					parameter.Direction = ParameterDirection.Input;
			}
			catch (Exception ex) {
				throw ex;
			}

			return parameter;
		}

		/// <summary>
		/// Determines the value of the attribute within the specified node.
		/// </summary>
		/// <param name="node">Node that contains the value.</param>
		/// <param name="attribute">Name of the attribute that contains the value.</param>
		/// <returns>Value of node.</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 01/15/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public T GetAttribute<T>(XmlNode node, string attribute)
		{
			T returnValue = default(T);
			object returnObject = null;

			if (typeof(T) == typeof(bool))
				returnObject = _GetAttributeBool(node, attribute);
			else if (typeof(T) == typeof(char))
				returnObject = _GetAttributeChar(node, attribute);
			else if (typeof(T) == typeof(DateTime))
				returnObject = _GetAttributeDate(node, attribute);
			else if (typeof(T) == typeof(DateTime?))
				returnObject = _GetAttributeDateNullable(node, attribute);
			else if (typeof(T) == typeof(decimal))
				returnObject = _GetAttributeDecimal(node, attribute);
			else if (typeof(T) == typeof(decimal?))
				returnObject = _GetAttributeDecimalNullable(node, attribute);
			else if (typeof(T) == typeof(double))
				returnObject = _GetAttributeDouble(node, attribute);
			else if (typeof(T) == typeof(float))
				returnObject = _GetAttributeFloat(node, attribute);
			else if (typeof(T) == typeof(int))
				returnObject = _GetAttributeInt(node, attribute);
			else if (typeof(T) == typeof(long))
				returnObject = _GetAttributeLong(node, attribute);
			else if (typeof(T) == typeof(short))
				returnObject = _GetAttributeShort(node, attribute);
			else if (typeof(T) == typeof(string))
				returnObject = _GetAttributeString(node, attribute);

			if ((returnObject == null && typeof(T).GetProperty("HasValue") != null) || typeof(T) == returnObject.GetType() ||
				(typeof(T).GetProperty("HasValue") != null && typeof(T).GetProperty("Value").PropertyType == returnObject.GetType())) {

				returnValue = (T)returnObject;
			}

			return returnValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="worksheet"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public T GetCellValue<T>(ExcelWorksheet worksheet, int row, int col)
		{
			char[] trimChars = new char[] { };

			return GetCellValue<T>(worksheet, row, col, trimChars);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="worksheet"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="trimChars"></param>
		/// <returns></returns>
		public T GetCellValue<T>(ExcelWorksheet worksheet, int row, int col, char[] trimChars)
		{
			string cellValue = string.Empty;

			try {
				cellValue = worksheet.Cells[row, col].Value == null ? string.Empty : worksheet.Cells[row, col].Value.ToString().TrimEnd(trimChars).TrimStart(trimChars);
			}
			catch {
				cellValue = string.Empty;
			}

			return ConvertString<T>(cellValue);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public T ConvertString<T>(string value)
		{
			T returnValue = default(T);
			object returnObject = null;

			if (typeof(T) == typeof(bool))
				returnObject = _StringToBool(value);
			if (typeof(T) == typeof(bool?))
				returnObject = _StringToBoolNullable(value);
			else if (typeof(T) == typeof(char))
				returnObject = _StringToChar(value);
			else if (typeof(T) == typeof(DateTime))
				returnObject = _StringToDate(value);
			else if (typeof(T) == typeof(DateTime?))
				returnObject = _StringToDateNullable(value);
			else if (typeof(T) == typeof(decimal))
				returnObject = _StringToDecimal(value);
			else if (typeof(T) == typeof(decimal?))
				returnObject = _StringToDecimalNullable(value);
			else if (typeof(T) == typeof(DirectoryInfo))
				returnObject = _StringToDirectoryInfo(value);
			else if (typeof(T) == typeof(double))
				returnObject = _StringToDouble(value);
			else if (typeof(T) == typeof(float))
				returnObject = _StringToFloat(value);
			else if (typeof(T) == typeof(int))
				returnObject = _StringToInt(value);
			else if (typeof(T) == typeof(int?))
				returnObject = _StringToIntNullable(value);
			else if (typeof(T) == typeof(long))
				returnObject = _StringToLong(value);
			else if (typeof(T) == typeof(short))
				returnObject = _StringToShort(value);
			else if (typeof(T) == typeof(string))
				returnObject = value;

			if ((returnObject == null && typeof(T).GetProperty("HasValue") != null) || typeof(T) == returnObject.GetType() ||
				(typeof(T).GetProperty("HasValue") != null && typeof(T).GetProperty("Value").PropertyType == returnObject.GetType())) {

				returnValue = (T)returnObject;
			}

			return returnValue;
		}

		/// <summary>
		/// Gets the fully qualified current user name from enviromment variables.
		/// </summary>
		/// <returns></returns>
		public string GetCurrentUser()
		{
			try {
				string userName = Environment.UserName;
				string domainName = Environment.UserDomainName;

				return string.Format(@"{0}{1}{2}", domainName, string.IsNullOrEmpty(domainName) ? string.Empty : "\\", userName).Trim();
			}
			catch {
				return "Unkown User";
			}
		}

		/// <summary>
		/// Determines whether the specified Type can be assigned a null value.
		/// </summary>
		/// <param name="type">System.Type object to investigate</param>
		/// <returns></returns>
		public bool IsNullable(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logFile"></param>
		/// <param name="msg"></param>
		public void LogMessage(string logFile, string msg)
		{
			if (logFile != "") {
				string fileName = logFile.Replace("[YYYY]", DateTime.Now.ToString("yyyy")).Replace("[MM]", DateTime.Now.ToString("MM")).Replace("[DD]", DateTime.Now.ToString("dd"));

				FileInfo fi = new FileInfo(fileName);
				LogMessage(fi, msg);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fiLog"></param>
		/// <param name="msg"></param>
		public void LogMessage(FileInfo fiLog, string msg)
		{
			if (!fiLog.Directory.Exists) {
				fiLog.Directory.Create();
				fiLog.Directory.Refresh();
			}

			if (fiLog.Directory.Exists) {
				StreamWriter sw = null;

				try {
					sw = new StreamWriter(fiLog.FullName, true);
					sw.WriteLine(string.Format("{0:MM/dd/yyyy hh:mm:ss} | {1}", DateTime.Now, msg));
				}
				catch { }
				finally {
					if (sw != null) {
						sw.Flush();
						sw.Close();
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <param name="value"></param>
		public void SetAttribute(XmlNode node, string attribute, string value)
		{
			if (node != null)
				((XmlElement)node).SetAttribute(attribute, value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <param name="date"></param>
		/// <param name="format"></param>
		public void SetAttribute(XmlNode node, string attribute, DateTime date, string format)
		{
			if (date == null || date == DateTime.MinValue || date == DateTime.MaxValue)
				SetAttribute(node, attribute, string.Empty);
			else
				SetAttribute(node, attribute, date.ToString(format));
		}


		#endregion Public

		#region Private

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		private bool _GetAttributeBool(XmlNode node, string attribute)
		{
			bool returnValue = false;

			try {
				string value = _GetAttributeString(node, attribute);

				if (_IsStringTrue(value))
					returnValue = true;
			}
			catch {
				returnValue = false;
			}

			return returnValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		private char _GetAttributeChar(XmlNode node, string attribute)
		{
			char returnValue = '\0';

			try {
				string value = _GetAttributeString(node, attribute);

				if (!char.TryParse(value, out returnValue))
					returnValue = '\0';
			}
			catch {
				returnValue = '\0';
			}

			return returnValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		private DateTime _GetAttributeDate(XmlNode node, string attribute)
		{
			DateTime returnValue = DateTime.MinValue;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!DateTime.TryParse(value, out returnValue))
					returnValue = DateTime.MinValue;
			}
			catch {
				returnValue = DateTime.MinValue;
			}

			return returnValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		private DateTime? _GetAttributeDateNullable(XmlNode node, string attribute)
		{
			DateTime? returnValue = null;

			try {
				string value = _GetAttributeString(node, attribute);
				DateTime dateValue = DateTime.MinValue;

				if (!DateTime.TryParse(value, out dateValue))
					returnValue = null;
				else
					returnValue = dateValue;
			}
			catch {
				returnValue = null;
			}

			return returnValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		private decimal _GetAttributeDecimal(XmlNode node, string attribute)
		{
			decimal returnValue = 0;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!decimal.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}

			return returnValue;
		}

		private decimal? _GetAttributeDecimalNullable(XmlNode node, string attribute)
		{
			try {
				decimal returnValue;
				string value = _GetAttributeString(node, attribute);

				if (decimal.TryParse(value, out returnValue))
					return returnValue;
				else
					return null;
			}
			catch {
				return null;
			}
		}

		private double _GetAttributeDouble(XmlNode node, string attribute)
		{
			double returnValue = 0;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!double.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}

			return returnValue;
		}

		private float _GetAttributeFloat(XmlNode node, string attribute)
		{
			float returnValue = 0;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!float.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}

			return returnValue;
		}

		private int _GetAttributeInt(XmlNode node, string attribute)
		{
			int returnValue = 0;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!int.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}

			return returnValue;
		}

		private long _GetAttributeLong(XmlNode node, string attribute)
		{
			long returnValue = 0;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!long.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}

			return returnValue;
		}

		private short _GetAttributeShort(XmlNode node, string attribute)
		{
			short returnValue = 0;

			try {
				string value = _GetAttributeString(node, attribute);

				if (!short.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}

			return returnValue;
		}

		/// <summary>
		/// Determines the value of the attribute within the specified node.
		/// </summary>
		/// <param name="node">Node that contains the value.</param>
		/// <param name="attribute">Name of the attribute that contains the value.</param>
		/// <returns>Value of node as a string.</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 01/15/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private string _GetAttributeString(XmlNode node, string attribute)
		{
			string returnValue = string.Empty;

			try {
				if (node != null && node.Attributes != null && node.Attributes.GetNamedItem(attribute) != null)
					returnValue = node.Attributes.GetNamedItem(attribute).Value;
			}
			catch {
				returnValue = string.Empty;
			}

			return returnValue;
		}

		/// <summary>
		/// Opens a zip archive for reading at the specified path.
		/// </summary>
		/// <param name="destinationArchiveFileName">The path to the created archive.</param>
		/// <param name="messages">List of messages describing the result of reading the archive.</param>
		/// <returns>True if successfully reads the archive; False otherwise.</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 01/26/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private bool _IsCompressedFile(string destinationArchiveFileName, ref List<string> messages)
		{
			bool returnValue = false;

			try {
				if (File.Exists(destinationArchiveFileName)) {
					using (ZipArchive archive = ZipFile.OpenRead(destinationArchiveFileName)) {
						returnValue = true;
					}
				}
				else
					messages.Add(string.Format("Archive '{0}' does not exist.", destinationArchiveFileName));
			}
			catch (InvalidDataException) { }    // Zip file is invalid
			catch (Exception ex) {
				messages.Add(ex.Message);
				messages.Add(ex.StackTrace);

				returnValue = false;
			}

			return returnValue;
		}

		private bool _IsStringTrue(string value)
		{
			bool returnValue = false;

			if (value.Trim().Length > 0) {
				if (value.ToUpper().Trim() == "1")
					returnValue = true;
				else if (value.ToUpper().Trim() == "Y")
					returnValue = true;
				else if (value.ToUpper().Trim() == "YES")
					returnValue = true;
				else if (value.ToUpper().Trim() == "TRUE")
					returnValue = true;
				else if (value.ToUpper().Trim() == "T")
					returnValue = true;
			}

			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as bool.
		/// True = 1, Y, YES, TRUE, T
		/// False = Everything else
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Bool representation of the value.  Returns false in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private bool _StringToBool(string value)
		{
			bool returnValue = false;
			try {
				if (_IsStringTrue(value))
					returnValue = true;
			}
			catch {
				returnValue = false;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as bool.
		/// True = 1, Y, YES, TRUE, T
		/// False = Everything else
		/// Note:  If the string is blank or there is an error parsing then null is returned
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Bool representation of the value.  Returns false in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private bool? _StringToBoolNullable(string value)
		{
			bool? returnValue = null;
			try {
				if (string.IsNullOrEmpty(value)) {
					if (_IsStringTrue(value))
						returnValue = true;
					else
						returnValue = false;
				}
			}
			catch {
				returnValue = null;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as char.  If the
		/// string is longer than one character it will use only the first character.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Char representation of the value.  Returns '\0' in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private char _StringToChar(string value)
		{
			char returnValue = '\0';
			try {
				if (!char.TryParse(value, out returnValue))
					returnValue = '\0';
			}
			catch {
				returnValue = '\0';
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a date.  If 
		/// the value can't be converted to a date, a Date.MinValue is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Date representation of the value.  Returns null in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private DateTime _StringToDate(string value)
		{
			DateTime returnValue = DateTime.MinValue;
			try {
				if (!DateTime.TryParse(value, out returnValue))
					returnValue = DateTime.MinValue;
			}
			catch {
				returnValue = DateTime.MinValue;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a date.  If 
		/// the value can't be converted to a date, a null is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Date representation of the value.  Returns null in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private DateTime? _StringToDateNullable(string value)
		{
			DateTime? returnValue = null;
			try {
				DateTime dateValue = DateTime.MinValue;
				if (!DateTime.TryParse(value, out dateValue))
					returnValue = null;
				else {
					returnValue = dateValue;
				}
			}
			catch {
				returnValue = null;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a decimal.  If 
		/// the value can't be converted to a date, a 0 is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Date representation of the value.  Returns 0 (zero) in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private decimal _StringToDecimal(string value)
		{
			decimal returnValue = 0;
			try {
				if (!decimal.TryParse(value.Replace("$", "").Replace(",", ""), out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a decimal.  If 
		/// the value can't be converted to a date or it is blank, a null is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Date representation of the value.  Returns 0 (zero) in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private decimal? _StringToDecimalNullable(string value)
		{
			decimal? returnValue = null;
			try {
				if (!string.IsNullOrEmpty(value)) {
					decimal amount;
					if (decimal.TryParse(value.Replace("$", "").Replace(",", ""), out amount))
						returnValue = amount;
				}
			}
			catch {
				returnValue = null;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a DirectoryInfo
		/// object.  If the value of the object is blank a null is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>DirectoryInfo object representation of the value.  Returns null if blank</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private DirectoryInfo _StringToDirectoryInfo(string value)
		{
			DirectoryInfo returnValue = null;
			try {
				if (value.Trim() == "")
					returnValue = null;
				else {
					returnValue = new DirectoryInfo(value.Trim());
				}
			}
			catch {
				returnValue = null;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a double.  If 
		/// the value can't be converted to a double, a 0 is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Date representation of the value.  Returns 0 (zero) in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private double _StringToDouble(string value)
		{
			double returnValue = 0;
			try {
				if (!double.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a float.  If 
		/// the value can't be converted to a float, a 0 is returned.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Date representation of the value.  Returns 0 (zero) in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private float _StringToFloat(string value)
		{
			float returnValue = 0;
			try {
				if (!float.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as an 
		/// int (System.Int32) variable.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>int(System.Int32) representation of the value.  Returns 0 in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private int _StringToInt(string value)
		{
			int returnValue = 0;
			try {
				if (!int.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as an 
		/// int (System.Int32) variable. If the value is blank or can't be parsed a null is returned
		/// </summary>
		/// <param name="value"></param>
		/// <returns>int(System.Int32) representation of the value.  Returns 0 in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private int? _StringToIntNullable(string value)
		{
			int? returnValue = null;
			try {
				if (!string.IsNullOrEmpty(value)) {
					int number;
					if (int.TryParse(value, out number))
						returnValue = number;
				}
			}
			catch {
				returnValue = null;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as a
		/// long (System.Int64) variable.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>int(System.Int64) representation of the value.  Returns 0 in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private long _StringToLong(string value)
		{
			long returnValue = 0;
			try {
				if (!long.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}
			return returnValue;
		}

		/// <summary>
		/// Determines the value of the specified attribute within the specified node as an 
		/// int (System.Int16) variable.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>int(System.Int16) representation of the value.  Returns 0 in the event of failure</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 02/06/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private short _StringToShort(string value)
		{
			short returnValue = 0;
			try {
				if (!short.TryParse(value, out returnValue))
					returnValue = 0;
			}
			catch {
				returnValue = 0;
			}
			return returnValue;
		}

		/// <summary>
		/// Verify files in the source directory exist in the archive.
		/// </summary>
		/// <param name="sourceDirectory">The path to the directory that has been archived.</param>
		/// <param name="destinationArchiveFileName">The path to the created archive.</param>
		/// <param name="messages">List of messages describing the result of verifying the archive.</param>
		/// <returns>True if the number of files in the directory exist in the archived file, and all files in the directory are found in the archived file; False otherwise.</returns>
		/// <history>
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	=================		=========================================================
		/// 01/15/2023	Troy Edmonson			Initial Creation
		/// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private bool _VerifyArchive(string sourceDirectory, string destinationArchiveFileName, ref List<string> messages)
		{
			bool returnValue = false;

			try {
				DirectoryInfo directory = new DirectoryInfo(sourceDirectory);

				if (directory.Exists) {
					if (File.Exists(destinationArchiveFileName)) {
						List<FileInfo> files = directory.GetFiles("*", SearchOption.AllDirectories).ToList();

						if (files.Count > 0) {
							bool fileFound = false;

							if (_IsCompressedFile(destinationArchiveFileName, ref messages)) {
								using (ZipArchive archive = ZipFile.OpenRead(destinationArchiveFileName)) {
									// Verify number of files contained in the source and archive are the same
									if (files.Count == archive.Entries.Count) {
										foreach (ZipArchiveEntry entry in archive.Entries) {
											// Verify the file in the source is found in the archive
											fileFound = files.Any(file => file.Name == entry.Name);

											if (!fileFound)
												break;
										}
									}
								}

								returnValue = fileFound;
							}
							else
								messages.Add(string.Format("Archive '{0}' is not a valid compressed file.", destinationArchiveFileName));
						}
						else
							messages.Add(string.Format("Directory '{0}' does not contain files to verify.", sourceDirectory));
					}
					else
						messages.Add(string.Format("Archive '{0}' does not exist.", destinationArchiveFileName));
				}
				else
					messages.Add(string.Format("Directory '{0}' does not exist.", sourceDirectory));
			}
			catch (Exception ex) {
				messages.Add(ex.Message);
				messages.Add(ex.StackTrace);

				returnValue = false;
			}

			return returnValue;
		}

		#endregion Private

		#endregion Methods
	}
}