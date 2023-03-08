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
	public class ManufacturerFileFormat : BaseData
	{
		#region Variables

		private ManufacturerFileFormatColumns _manufacturerFileFormatColumns = new ManufacturerFileFormatColumns();

		#endregion Variables

		#region Properties

		public ManufacturerFileFormatColumns ManufacturerFileFormatColumns
		{
			get { return _manufacturerFileFormatColumns; }
			set { _manufacturerFileFormatColumns = value; }
		}

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		public bool Find(string name, bool recursive)
		{
			bool returnValue = false;

			try {
				long manufacturerFileFormatUID = -1;

				// Define the input parameters
				Dictionary<string, object> inputParameters = new Dictionary<string, object>
				{
					{ "Name", name }
				};

				// Define the output parameters
				Dictionary<string, object> outputParameters = new Dictionary<string, object>
				{
					{ "ManufacturerFileFormatUID", manufacturerFileFormatUID }
				};

				// Perform the operation
				if (base.ExecuteOutputStoredProcedure("csp_ManufacturerFileFormat_Find_Name", inputParameters, outputParameters)) {
					manufacturerFileFormatUID = (long)outputParameters["ManufacturerFileFormatUID"];

					// Perform the base object find in order to populate its properties
					if (manufacturerFileFormatUID > 0) {
						if (this.Find(manufacturerFileFormatUID, recursive))
							returnValue = true;
					}
				}
			}
			catch (Exception) {
				throw;
			}

			return returnValue;
		}

		public bool Find(long manufacturerFileFormatUID, bool recursive)
		{
			bool returnValue;

			try {
				returnValue = base.Find(manufacturerFileFormatUID);

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
			bool returnValue;

			try {
				_manufacturerFileFormatColumns = new ManufacturerFileFormatColumns(DBServer, DBDatabase, DBUsername, DBPassword);

				returnValue = _manufacturerFileFormatColumns.Find(this.ManufacturerFileFormatUID);
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

		private long _manufacturerFileFormatUID;
		private string _name;
		private bool _active;
		private string _delimiter;
		private string _header;
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
		/// Gets/Sets the ManufacturerFileFormatUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "ManufacturerFileFormatUID")]
		public long ManufacturerFileFormatUID
		{
			get { return _manufacturerFileFormatUID; }
			set { _manufacturerFileFormatUID = value; }
		}

		/// <summary>
		/// Gets/Sets the Name property.
		/// </summary>
		[ColumnProperty(ColumnName = "Name")]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets/Sets the Active property.
		/// </summary>
		[ColumnProperty(ColumnName = "Active")]
		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		/// <summary>
		/// Gets/Sets the Delimiter property.
		/// </summary>
		[ColumnProperty(ColumnName = "Delimiter")]
		public string Delimiter
		{
			get { return _delimiter; }
			set { _delimiter = value; }
		}

		/// <summary>
		/// Gets/Sets the Header property.
		/// </summary>
		[ColumnProperty(ColumnName = "Header")]
		public string Header
		{
			get { return _header; }
			set { _header = value; }
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
		public ManufacturerFileFormat() : base() { }

		/// <inheritdoc/>
		public ManufacturerFileFormat(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public ManufacturerFileFormat(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the ManufacturerFileFormat class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="name"></param>
		/// <param name="active"></param>
		/// <param name="delimiter"></param>
		/// <param name="header"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public ManufacturerFileFormat(string dbServer, string dbDatabase, string dbUsername, string dbPassword, string name, bool active, string delimiter, string header, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_name = name;
			_active = active;
			_delimiter = delimiter;
			_header = header;
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
		/// Returns the record for the specified ManufacturerFileFormatUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long manufacturerFileFormatUID)
		{
			try {
				return base.Find(manufacturerFileFormatUID);
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

			_cspAdd = "csp_ManufacturerFileFormat_Add";
			_cspUpdate = "csp_ManufacturerFileFormat_Update";
			_cspFind = "csp_ManufacturerFileFormat_Find";
			_cspDelete = "csp_ManufacturerFileFormat_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class ManufacturerFileFormats : BaseDataCollection<ManufacturerFileFormat>
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

		public ManufacturerFileFormats()
		{
			_SetupDatabase();
		}

		public ManufacturerFileFormats(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_ManufacturerFileFormats_Find";
			_cspFindParameterNames.Clear();
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
