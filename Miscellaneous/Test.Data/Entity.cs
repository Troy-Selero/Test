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
	public class Entity : BaseData
	{
		#region Variables

		EntityPrescribers _entityPrescribers = new EntityPrescribers();

		#endregion Variables

		#region Properties

		public EntityPrescribers EntityPrescribers
		{
			get { return _entityPrescribers; }
			set { _entityPrescribers = value; }
		}

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		public bool Find(string entityID, bool recursive)
		{
			bool returnValue = false;

			try {
				long entityUID = -1;

				// Define the input parameters
				Dictionary<string, object> inputParameters = new Dictionary<string, object>
				{
					{ "EntityID", entityID }
				};

				// Define the output parameters
				Dictionary<string, object> outputParameters = new Dictionary<string, object>
				{
					{ "EntityUID", entityUID }
				};

				// Perform the operation
				if (base.ExecuteOutputStoredProcedure("csp_Entity_Find_EntityID", inputParameters, outputParameters)) {
					entityUID = (long)outputParameters["EntityUID"];

					// Perform the base object find in order to populate its properties
					if (entityUID > 0) {
						if (Find(entityUID)) {
							if (recursive)
								returnValue = _FindChildren();
							else
								returnValue = true;
						}
					}
				}
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
				_entityPrescribers = new EntityPrescribers(DBServer, DBDatabase, DBUsername, DBPassword);

				returnValue = _entityPrescribers.Find(this.EntityUID);
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

		private long _entityUID;
		private string _entityID;
		private string _hrsaID;
		private string _name;
		private string _nameAbbrev;
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
		/// Gets/Sets the EntityUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "EntityUID")]
		public long EntityUID
		{
			get { return _entityUID; }
			set { _entityUID = value; }
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
		/// Gets/Sets the HrsaID property.
		/// </summary>
		[ColumnProperty(ColumnName = "HrsaID")]
		public string HrsaID
		{
			get { return _hrsaID; }
			set { _hrsaID = value; }
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
		/// Gets/Sets the NameAbbrev property.
		/// </summary>
		[ColumnProperty(ColumnName = "NameAbbrev")]
		public string NameAbbrev
		{
			get { return _nameAbbrev; }
			set { _nameAbbrev = value; }
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
		public Entity() : base() { }

		/// <inheritdoc/>
		public Entity(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public Entity(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the Entity class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="entityID"></param>
		/// <param name="hrsaID"></param>
		/// <param name="name"></param>
		/// <param name="nameAbbrev"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public Entity(string dbServer, string dbDatabase, string dbUsername, string dbPassword, string entityID, string hrsaID, string name, string nameAbbrev, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_entityID = entityID;
			_hrsaID = hrsaID;
			_name = name;
			_nameAbbrev = nameAbbrev;
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
		/// Returns the record for the specified EntityUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long entityUID)
		{
			try {
				return base.Find(entityUID);
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

			_cspAdd = "csp_Entity_Add";
			_cspUpdate = "csp_Entity_Update";
			_cspFind = "csp_Entity_Find";
			_cspDelete = "csp_Entity_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class Entities : BaseDataCollection<Entity>
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

		public Entities()
		{
			_SetupDatabase();
		}

		public Entities(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_Entities_Find";
			_cspFindParameterNames.Clear();
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
