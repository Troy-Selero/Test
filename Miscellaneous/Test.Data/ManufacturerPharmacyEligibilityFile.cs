using System.Transactions;
using Selero.Selena;

namespace Test.Data
{
	/// <summary>
	///
	/// </summary>
	/// <history>
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// Date		Author					Description
	/// ==========	=================		=======================================================
	/// 02/27/2023	Data Tier Generator		Initial Creation
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// </history>
	public class ManufacturerPharmacyEligibilityFile : BaseData
	{
		#region Variables

		private ManufacturerPharmacyEligibilityFileStatus _eligibilityFileStatus = new ManufacturerPharmacyEligibilityFileStatus();

		#endregion Variables

		#region Properties

		public ManufacturerPharmacyEligibilityFileStatus PharmacyMfrEligibilityFileStatus
		{
			get { return _eligibilityFileStatus; }
			set { _eligibilityFileStatus = value; }
		}

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		public override bool Add(string updateUser, string updateApp)
		{
			bool returnValue = false;

			using (TransactionScope scope = new TransactionScope()) {
				bool successful = false;

				try {
					_eligibilityFileStatus.SetData(DBServer, DBDatabase, DBUsername, DBPassword);
					_eligibilityFileStatus.RetailStatus = this.RetailStatus;

					if (_eligibilityFileStatus.ManufacturerPharmacyEligibilityFileID > 0) {
						// Add a new version
						_eligibilityFileStatus.CurrentVersion = ++this.Version;

						successful = _eligibilityFileStatus.Update(updateUser, updateApp);
					}
					else {
						// Add initial version
						this.Version = 0;

						successful = _eligibilityFileStatus.Add(updateUser, updateApp);
					}

					if (successful) {
						_manufacturerPharmacyEligibilityFileID = _eligibilityFileStatus.ManufacturerPharmacyEligibilityFileID;

						successful = base.Add(updateUser, updateApp);
					}
				}
				catch {
					successful = false;
				}
				finally {
					if (successful) {
						scope.Complete();

						returnValue = true;
					}
				}
			}

			return returnValue;
		}

		public bool Find(string manufacturer, string pharmacyID)
		{
			return Find(manufacturer, pharmacyID, false);
		}

		public bool Find(string manufacturer, string pharmacyID, bool recursive)
		{
			bool returnValue = false;

			try {
				long manufacturerPharmacyEligibilityFileUID = -1;

				// Define the input parameters
				Dictionary<string, object> inputParameters = new Dictionary<string, object>
				{
					{ "Manufacturer", manufacturer },
					{ "PharmacyID", pharmacyID }
				};

				// Define the output parameters
				Dictionary<string, object> outputParameters = new Dictionary<string, object>
				{
					{ "ManufacturerPharmacyEligibilityFileUID", manufacturerPharmacyEligibilityFileUID }
				};

				// Perform the operation
				if (base.ExecuteOutputStoredProcedure("csp_ManufacturerPharmacyEligibilityFile_Find_Manufacturer", inputParameters, outputParameters)) {
					manufacturerPharmacyEligibilityFileUID = (long)outputParameters["ManufacturerPharmacyEligibilityFileUID"];

					// Perform the base object find in order to populate its properties
					if (manufacturerPharmacyEligibilityFileUID > 0) {
						if (this.Find(manufacturerPharmacyEligibilityFileUID, recursive))
							returnValue = true;
					}
				}
			}
			catch (Exception) {
				throw;
			}

			return returnValue;
		}

		public bool Find(long manufacturerPharmacyEligibilityFileUID, bool recursive)
		{
			bool returnValue;

			try {
				returnValue = base.Find(manufacturerPharmacyEligibilityFileUID);

				if (returnValue && recursive)
					returnValue = _FindChildren();
			}
			catch (Exception) {
				throw;
			}

			return returnValue;
		}

		#endregion Public

		#region Private

		private bool _FindChildren()
		{
			bool returnValue = false;

			try {
				_eligibilityFileStatus = new ManufacturerPharmacyEligibilityFileStatus(DBServer, DBDatabase, DBUsername, DBPassword);

				if (_eligibilityFileStatus.Find(_manufacturerPharmacyEligibilityFileID))
					returnValue = true;
			}
			catch {
				returnValue = false;
			}

			return returnValue;
		}

		#endregion Private

		#endregion Methods

		#region DTG Code - DO NOT MODIFY

		#region Generated Variables

		private long _manufacturerPharmacyEligibilityFileUID;
		private long _manufacturerPharmacyEligibilityBatchUID;
		private long _manufacturerPharmacyEligibilityFileID;
		private short _version;
		private string _manufacturer;
		private string _id340b;
		private string _pharmacyID;
		private string _dea;
		private string _pharmacyName;
		private string _pharmacyAddress1;
		private string _pharmacyAddress2;
		private string _pharmacyAddress3;
		private string _pharmacyCity;
		private string _pharmacyState;
		private string _pharmacyZip;
		private string _retailStatus;
		private DateTime? _retailTransmissionDate;
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
		/// Gets/Sets the ManufacturerPharmacyEligibilityFileUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "ManufacturerPharmacyEligibilityFileUID")]
		public long ManufacturerPharmacyEligibilityFileUID
		{
			get { return _manufacturerPharmacyEligibilityFileUID; }
			set { _manufacturerPharmacyEligibilityFileUID = value; }
		}

		/// <summary>
		/// Gets/Sets the ManufacturerPharmacyEligibilityBatchUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "ManufacturerPharmacyEligibilityBatchUID")]
		public long ManufacturerPharmacyEligibilityBatchUID
		{
			get { return _manufacturerPharmacyEligibilityBatchUID; }
			set { _manufacturerPharmacyEligibilityBatchUID = value; }
		}

		/// <summary>
		/// Gets/Sets the ManufacturerPharmacyEligibilityFileID property.
		/// </summary>
		[ColumnProperty(ColumnName = "ManufacturerPharmacyEligibilityFileID")]
		public long ManufacturerPharmacyEligibilityFileID
		{
			get { return _manufacturerPharmacyEligibilityFileID; }
			set { _manufacturerPharmacyEligibilityFileID = value; }
		}

		/// <summary>
		/// Gets/Sets the Version property.
		/// </summary>
		[ColumnProperty(ColumnName = "Version")]
		public short Version
		{
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// Gets/Sets the Manufacturer property.
		/// </summary>
		[ColumnProperty(ColumnName = "Manufacturer")]
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { _manufacturer = value; }
		}

		/// <summary>
		/// Gets/Sets the ID340B property.
		/// </summary>
		[ColumnProperty(ColumnName = "ID340B")]
		public string ID340B
		{
			get { return _id340b; }
			set { _id340b = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyID property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyID")]
		public string PharmacyID
		{
			get { return _pharmacyID; }
			set { _pharmacyID = value; }
		}

		/// <summary>
		/// Gets/Sets the DEA property.
		/// </summary>
		[ColumnProperty(ColumnName = "DEA")]
		public string DEA
		{
			get { return _dea; }
			set { _dea = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyName property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyName")]
		public string PharmacyName
		{
			get { return _pharmacyName; }
			set { _pharmacyName = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyAddress1 property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyAddress1")]
		public string PharmacyAddress1
		{
			get { return _pharmacyAddress1; }
			set { _pharmacyAddress1 = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyAddress2 property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyAddress2")]
		public string PharmacyAddress2
		{
			get { return _pharmacyAddress2; }
			set { _pharmacyAddress2 = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyAddress3 property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyAddress3")]
		public string PharmacyAddress3
		{
			get { return _pharmacyAddress3; }
			set { _pharmacyAddress3 = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyCity property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyCity")]
		public string PharmacyCity
		{
			get { return _pharmacyCity; }
			set { _pharmacyCity = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyState property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyState")]
		public string PharmacyState
		{
			get { return _pharmacyState; }
			set { _pharmacyState = value; }
		}

		/// <summary>
		/// Gets/Sets the PharmacyZip property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyZip")]
		public string PharmacyZip
		{
			get { return _pharmacyZip; }
			set { _pharmacyZip = value; }
		}

		/// <summary>
		/// Gets/Sets the RetailStatus property.
		/// </summary>
		[ColumnProperty(ColumnName = "RetailStatus")]
		public string RetailStatus
		{
			get { return _retailStatus; }
			set { _retailStatus = value; }
		}

		/// <summary>
		/// Gets/Sets the RetailTransmissionDate property.
		/// </summary>
		[ColumnProperty(ColumnName = "RetailTransmissionDate")]
		public DateTime? RetailTransmissionDate
		{
			get { return _retailTransmissionDate; }
			set { _retailTransmissionDate = value; }
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
		public ManufacturerPharmacyEligibilityFile() : base() { }

		/// <inheritdoc/>
		public ManufacturerPharmacyEligibilityFile(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public ManufacturerPharmacyEligibilityFile(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the ManufacturerPharmacyEligibilityFile class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="manufacturerPharmacyEligibilityBatchUID"></param>
		/// <param name="manufacturerPharmacyEligibilityFileID"></param>
		/// <param name="version"></param>
		/// <param name="manufacturer"></param>
		/// <param name="id340b"></param>
		/// <param name="pharmacyID"></param>
		/// <param name="dea"></param>
		/// <param name="pharmacyName"></param>
		/// <param name="pharmacyAddress1"></param>
		/// <param name="pharmacyAddress2"></param>
		/// <param name="pharmacyAddress3"></param>
		/// <param name="pharmacyCity"></param>
		/// <param name="pharmacyState"></param>
		/// <param name="pharmacyZip"></param>
		/// <param name="retailStatus"></param>
		/// <param name="retailTransmissionDate"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public ManufacturerPharmacyEligibilityFile(string dbServer, string dbDatabase, string dbUsername, string dbPassword, long manufacturerPharmacyEligibilityBatchUID, long manufacturerPharmacyEligibilityFileID, short version, string manufacturer, string id340b, string pharmacyID, string dea, string pharmacyName, string pharmacyAddress1, string pharmacyAddress2, string pharmacyAddress3, string pharmacyCity, string pharmacyState, string pharmacyZip, string retailStatus, DateTime? retailTransmissionDate, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_manufacturerPharmacyEligibilityBatchUID = manufacturerPharmacyEligibilityBatchUID;
			_manufacturerPharmacyEligibilityFileID = manufacturerPharmacyEligibilityFileID;
			_version = version;
			_manufacturer = manufacturer;
			_id340b = id340b;
			_pharmacyID = pharmacyID;
			_dea = dea;
			_pharmacyName = pharmacyName;
			_pharmacyAddress1 = pharmacyAddress1;
			_pharmacyAddress2 = pharmacyAddress2;
			_pharmacyAddress3 = pharmacyAddress3;
			_pharmacyCity = pharmacyCity;
			_pharmacyState = pharmacyState;
			_pharmacyZip = pharmacyZip;
			_retailStatus = retailStatus;
			_retailTransmissionDate = retailTransmissionDate;
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
		/// Returns the record for the specified ManufacturerPharmacyEligibilityFileUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long manufacturerPharmacyEligibilityFileUID)
		{
			try {
				return base.Find(manufacturerPharmacyEligibilityFileUID);
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

			_cspAdd = "csp_ManufacturerPharmacyEligibilityFile_Add";
			_cspUpdate = "csp_ManufacturerPharmacyEligibilityFile_Update";
			_cspFind = "csp_ManufacturerPharmacyEligibilityFile_Find";
			_cspDelete = "csp_ManufacturerPharmacyEligibilityFile_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class ManufacturerPharmacyEligibilityFiles : BaseDataCollection<ManufacturerPharmacyEligibilityFile>
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

		public ManufacturerPharmacyEligibilityFiles()
		{
			_SetupDatabase();
		}

		public ManufacturerPharmacyEligibilityFiles(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_ManufacturerPharmacyEligibilityFiles_Find";
			_cspFindParameterNames.Clear();
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
