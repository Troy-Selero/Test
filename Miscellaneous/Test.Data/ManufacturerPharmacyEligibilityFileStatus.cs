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
	public class ManufacturerPharmacyEligibilityFileStatus : BaseData
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

		private long _manufacturerPharmacyEligibilityFileID;
		private short _currentVersion;
		private string _retailStatus;
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
		/// Gets/Sets the ManufacturerPharmacyEligibilityFileID property.
		/// </summary>
		[ColumnProperty(ColumnName = "ManufacturerPharmacyEligibilityFileID")]
		public long ManufacturerPharmacyEligibilityFileID
		{
			get { return _manufacturerPharmacyEligibilityFileID; }
			set { _manufacturerPharmacyEligibilityFileID = value; }
		}

		/// <summary>
		/// Gets/Sets the CurrentVersion property.
		/// </summary>
		[ColumnProperty(ColumnName = "CurrentVersion")]
		public short CurrentVersion
		{
			get { return _currentVersion; }
			set { _currentVersion = value; }
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
		public ManufacturerPharmacyEligibilityFileStatus() : base() { }

		/// <inheritdoc/>
		public ManufacturerPharmacyEligibilityFileStatus(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public ManufacturerPharmacyEligibilityFileStatus(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the ManufacturerPharmacyEligibilityFileStatus class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="currentVersion"></param>
		/// <param name="retailStatus"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public ManufacturerPharmacyEligibilityFileStatus(string dbServer, string dbDatabase, string dbUsername, string dbPassword, short currentVersion, string retailStatus, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_currentVersion = currentVersion;
			_retailStatus = retailStatus;
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
		/// Returns the record for the specified ManufacturerPharmacyEligibilityFileID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long manufacturerPharmacyEligibilityFileID)
		{
			try {
				return base.Find(manufacturerPharmacyEligibilityFileID);
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

			_cspAdd = "csp_ManufacturerPharmacyEligibilityFileStatus_Add";
			_cspUpdate = "csp_ManufacturerPharmacyEligibilityFileStatus_Update";
			_cspFind = "csp_ManufacturerPharmacyEligibilityFileStatus_Find";
			_cspDelete = "csp_ManufacturerPharmacyEligibilityFileStatus_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class ManufacturerPharmacyEligibilityFileStatuses : BaseDataCollection<ManufacturerPharmacyEligibilityFileStatus>
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

		public ManufacturerPharmacyEligibilityFileStatuses()
		{
			_SetupDatabase();
		}

		public ManufacturerPharmacyEligibilityFileStatuses(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_ManufacturerPharmacyEligibilityFileStatuses_Find";
			_cspFindParameterNames.Clear();
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
