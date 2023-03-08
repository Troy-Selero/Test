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
	public class EntityPrescriber : BaseData
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

		private long _entityPrescriberUID;
		private long _entityUID;
		private string _prescriberNPI;
		private string _prescriberFirstName;
		private string _prescriberLastName;
		private string _prescriberTaxonomyDescription;
		private string _prescriber340BStatus;
		private DateTime? _prescriber340BEligibleDate;
		private DateTime? _prescriber340BTerminationDate;
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
		/// Gets/Sets the EntityPrescriberUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "EntityPrescriberUID")]
		public long EntityPrescriberUID
		{
			get { return _entityPrescriberUID; }
			set { _entityPrescriberUID = value; }
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
		/// Gets/Sets the PrescriberNPI property.
		/// </summary>
		[ColumnProperty(ColumnName = "PrescriberNPI")]
		public string PrescriberNPI
		{
			get { return _prescriberNPI; }
			set { _prescriberNPI = value; }
		}

		/// <summary>
		/// Gets/Sets the PrescriberFirstName property.
		/// </summary>
		[ColumnProperty(ColumnName = "PrescriberFirstName")]
		public string PrescriberFirstName
		{
			get { return _prescriberFirstName; }
			set { _prescriberFirstName = value; }
		}

		/// <summary>
		/// Gets/Sets the PrescriberLastName property.
		/// </summary>
		[ColumnProperty(ColumnName = "PrescriberLastName")]
		public string PrescriberLastName
		{
			get { return _prescriberLastName; }
			set { _prescriberLastName = value; }
		}

		/// <summary>
		/// Gets/Sets the PrescriberTaxonomyDescription property.
		/// </summary>
		[ColumnProperty(ColumnName = "PrescriberTaxonomyDescription")]
		public string PrescriberTaxonomyDescription
		{
			get { return _prescriberTaxonomyDescription; }
			set { _prescriberTaxonomyDescription = value; }
		}

		/// <summary>
		/// Gets/Sets the Prescriber340BStatus property.
		/// </summary>
		[ColumnProperty(ColumnName = "Prescriber340BStatus")]
		public string Prescriber340BStatus
		{
			get { return _prescriber340BStatus; }
			set { _prescriber340BStatus = value; }
		}

		/// <summary>
		/// Gets/Sets the Prescriber340BEligibleDate property.
		/// </summary>
		[ColumnProperty(ColumnName = "Prescriber340BEligibleDate")]
		public DateTime? Prescriber340BEligibleDate
		{
			get { return _prescriber340BEligibleDate; }
			set { _prescriber340BEligibleDate = value; }
		}

		/// <summary>
		/// Gets/Sets the Prescriber340BTerminationDate property.
		/// </summary>
		[ColumnProperty(ColumnName = "Prescriber340BTerminationDate")]
		public DateTime? Prescriber340BTerminationDate
		{
			get { return _prescriber340BTerminationDate; }
			set { _prescriber340BTerminationDate = value; }
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
		public EntityPrescriber() : base() { }

		/// <inheritdoc/>
		public EntityPrescriber(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public EntityPrescriber(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the EntityPrescriber class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="entityUID"></param>
		/// <param name="prescriberNPI"></param>
		/// <param name="prescriberFirstName"></param>
		/// <param name="prescriberLastName"></param>
		/// <param name="prescriberTaxonomyDescription"></param>
		/// <param name="prescriber340BStatus"></param>
		/// <param name="prescriber340BEligibleDate"></param>
		/// <param name="prescriber340BTerminationDate"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public EntityPrescriber(string dbServer, string dbDatabase, string dbUsername, string dbPassword, long entityUID, string prescriberNPI, string prescriberFirstName, string prescriberLastName, string prescriberTaxonomyDescription, string prescriber340BStatus, DateTime? prescriber340BEligibleDate, DateTime? prescriber340BTerminationDate, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_entityUID = entityUID;
			_prescriberNPI = prescriberNPI;
			_prescriberFirstName = prescriberFirstName;
			_prescriberLastName = prescriberLastName;
			_prescriberTaxonomyDescription = prescriberTaxonomyDescription;
			_prescriber340BStatus = prescriber340BStatus;
			_prescriber340BEligibleDate = prescriber340BEligibleDate;
			_prescriber340BTerminationDate = prescriber340BTerminationDate;
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
		/// Returns the record for the specified EntityPrescriberUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long entityPrescriberUID)
		{
			try {
				return base.Find(entityPrescriberUID);
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

			_cspAdd = "csp_EntityPrescriber_Add";
			_cspUpdate = "csp_EntityPrescriber_Update";
			_cspFind = "csp_EntityPrescriber_Find";
			_cspDelete = "csp_EntityPrescriber_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class EntityPrescribers : BaseDataCollection<EntityPrescriber>
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

		public EntityPrescribers()
		{
			_SetupDatabase();
		}

		public EntityPrescribers(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_EntityPrescribers_Find";
			_cspFindParameterNames.Clear();
			_cspFindParameterNames.Add("EntityUID");
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
