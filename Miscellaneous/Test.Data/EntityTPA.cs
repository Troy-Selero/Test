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
	/// 03/08/2023	Data Tier Generator		Initial Creation
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// </history>
	public class EntityTPA : BaseData
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

		private long _entityTPAUID;
		private string _entityTPAID;
		private long _entityUID;
		private string _name;
		private decimal _procFeeBrand;
		private decimal _procFeeGeneric;
		private decimal _procFeeSpecialty;
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
		/// Gets/Sets the EntityTPAUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "EntityTPAUID")]
		public long EntityTPAUID
		{
			get { return _entityTPAUID; }
			set { _entityTPAUID = value; }
		}

		/// <summary>
		/// Gets/Sets the EntityTPAID property.
		/// </summary>
		[ColumnProperty(ColumnName = "EntityTPAID")]
		public string EntityTPAID
		{
			get { return _entityTPAID; }
			set { _entityTPAID = value; }
		}

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
		/// Gets/Sets the Name property.
		/// </summary>
		[ColumnProperty(ColumnName = "Name")]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets/Sets the ProcFeeBrand property.
		/// </summary>
		[ColumnProperty(ColumnName = "ProcFeeBrand")]
		public decimal ProcFeeBrand
		{
			get { return _procFeeBrand; }
			set { _procFeeBrand = value; }
		}

		/// <summary>
		/// Gets/Sets the ProcFeeGeneric property.
		/// </summary>
		[ColumnProperty(ColumnName = "ProcFeeGeneric")]
		public decimal ProcFeeGeneric
		{
			get { return _procFeeGeneric; }
			set { _procFeeGeneric = value; }
		}

		/// <summary>
		/// Gets/Sets the ProcFeeSpecialty property.
		/// </summary>
		[ColumnProperty(ColumnName = "ProcFeeSpecialty")]
		public decimal ProcFeeSpecialty
		{
			get { return _procFeeSpecialty; }
			set { _procFeeSpecialty = value; }
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
		public EntityTPA() : base() { }

		/// <inheritdoc/>
		public EntityTPA(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public EntityTPA(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the EntityTPA class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="entityTPAID"></param>
		/// <param name="entityUID"></param>
		/// <param name="name"></param>
		/// <param name="procFeeBrand"></param>
		/// <param name="procFeeGeneric"></param>
		/// <param name="procFeeSpecialty"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public EntityTPA(string dbServer, string dbDatabase, string dbUsername, string dbPassword, string entityTPAID, long entityUID, string name, decimal procFeeBrand, decimal procFeeGeneric, decimal procFeeSpecialty, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_entityTPAID = entityTPAID;
			_entityUID = entityUID;
			_name = name;
			_procFeeBrand = procFeeBrand;
			_procFeeGeneric = procFeeGeneric;
			_procFeeSpecialty = procFeeSpecialty;
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
		/// Returns the record for the specified EntityTPAUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long entityTPAUID)
		{
			try {
				return base.Find(entityTPAUID);
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

			_cspAdd = "csp_EntityTPA_Add";
			_cspUpdate = "csp_EntityTPA_Update";
			_cspFind = "csp_EntityTPA_Find";
			_cspDelete = "csp_EntityTPA_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class EntityTPAs : BaseDataCollection<EntityTPA>
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

		public EntityTPAs()
		{
			_SetupDatabase();
		}

		public EntityTPAs(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_SetupDatabase();
		}

		#endregion Generated Constructors

		#region Generated Methods

		#region Generated Public

		public bool Find(long entityUID)
		{
			return Find(entityUID, false);
		}

		public bool Find(long entityUID, bool deleted)
		{
			return base.Find(entityUID, deleted);
		}

		#endregion Generated Public

		#region Generated Private

		private void _SetupDatabase()
		{
			_cspFind = "csp_EntityTPAs_Find";
			_cspFindParameterNames.Clear();
			_cspFindParameterNames.Add("EntityUID");
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
