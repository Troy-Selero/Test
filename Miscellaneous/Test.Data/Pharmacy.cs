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
	public class Pharmacy : BaseData
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

		private long _pharmacyUID;
		private string _pharmacyID;
		private long _entityUID;
		private string _name;
		private string _npi;
		private string _addressStreet1;
		private string _addressStreet2;
		private string _addressCity;
		private string _addressState;
		private string _addressZip;
		private string _tPAAccountID;
		private string _wholesalerAccountID;
		private string _revenueModel340B;
		private bool _includeGenerics;
		private string _scheduleStatus;
		private string _invoiceGroupID;
		private decimal? _dispFeeSG30;
		private decimal? _dispFeeSG60;
		private decimal? _dispFeeSG61;
		private decimal? _dispFeeSB30;
		private decimal? _dispFeeSB60;
		private decimal? _dispFeeSB61;
		private decimal? _dispFeeSS30;
		private decimal? _dispFeeSS60;
		private decimal? _dispFeeSS61;
		private decimal? _dispFeeVG30;
		private decimal? _dispFeeVG60;
		private decimal? _dispFeeVG61;
		private decimal? _dispFeeVB30;
		private decimal? _dispFeeVB60;
		private decimal? _dispFeeVB61;
		private decimal? _dispFeeVS30;
		private decimal? _dispFeeVS60;
		private decimal? _dispFeeVS61;
		private decimal? _dispFeeMin;
		private decimal? _dispFeeMax;
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
		/// Gets/Sets the PharmacyUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "PharmacyUID")]
		public long PharmacyUID
		{
			get { return _pharmacyUID; }
			set { _pharmacyUID = value; }
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
		/// Gets/Sets the NPI property.
		/// </summary>
		[ColumnProperty(ColumnName = "NPI")]
		public string NPI
		{
			get { return _npi; }
			set { _npi = value; }
		}

		/// <summary>
		/// Gets/Sets the AddressStreet1 property.
		/// </summary>
		[ColumnProperty(ColumnName = "AddressStreet1")]
		public string AddressStreet1
		{
			get { return _addressStreet1; }
			set { _addressStreet1 = value; }
		}

		/// <summary>
		/// Gets/Sets the AddressStreet2 property.
		/// </summary>
		[ColumnProperty(ColumnName = "AddressStreet2")]
		public string AddressStreet2
		{
			get { return _addressStreet2; }
			set { _addressStreet2 = value; }
		}

		/// <summary>
		/// Gets/Sets the AddressCity property.
		/// </summary>
		[ColumnProperty(ColumnName = "AddressCity")]
		public string AddressCity
		{
			get { return _addressCity; }
			set { _addressCity = value; }
		}

		/// <summary>
		/// Gets/Sets the AddressState property.
		/// </summary>
		[ColumnProperty(ColumnName = "AddressState")]
		public string AddressState
		{
			get { return _addressState; }
			set { _addressState = value; }
		}

		/// <summary>
		/// Gets/Sets the AddressZip property.
		/// </summary>
		[ColumnProperty(ColumnName = "AddressZip")]
		public string AddressZip
		{
			get { return _addressZip; }
			set { _addressZip = value; }
		}

		/// <summary>
		/// Gets/Sets the TPAAccountID property.
		/// </summary>
		[ColumnProperty(ColumnName = "TPAAccountID")]
		public string TPAAccountID
		{
			get { return _tPAAccountID; }
			set { _tPAAccountID = value; }
		}

		/// <summary>
		/// Gets/Sets the WholesalerAccountID property.
		/// </summary>
		[ColumnProperty(ColumnName = "WholesalerAccountID")]
		public string WholesalerAccountID
		{
			get { return _wholesalerAccountID; }
			set { _wholesalerAccountID = value; }
		}

		/// <summary>
		/// Gets/Sets the RevenueModel340B property.
		/// </summary>
		[ColumnProperty(ColumnName = "RevenueModel340B")]
		public string RevenueModel340B
		{
			get { return _revenueModel340B; }
			set { _revenueModel340B = value; }
		}

		/// <summary>
		/// Gets/Sets the IncludeGenerics property.
		/// </summary>
		[ColumnProperty(ColumnName = "IncludeGenerics")]
		public bool IncludeGenerics
		{
			get { return _includeGenerics; }
			set { _includeGenerics = value; }
		}

		/// <summary>
		/// Gets/Sets the ScheduleStatus property.
		/// </summary>
		[ColumnProperty(ColumnName = "ScheduleStatus")]
		public string ScheduleStatus
		{
			get { return _scheduleStatus; }
			set { _scheduleStatus = value; }
		}

		/// <summary>
		/// Gets/Sets the InvoiceGroupID property.
		/// </summary>
		[ColumnProperty(ColumnName = "InvoiceGroupID")]
		public string InvoiceGroupID
		{
			get { return _invoiceGroupID; }
			set { _invoiceGroupID = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSG30 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSG30")]
		public decimal? DispFeeSG30
		{
			get { return _dispFeeSG30; }
			set { _dispFeeSG30 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSG60 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSG60")]
		public decimal? DispFeeSG60
		{
			get { return _dispFeeSG60; }
			set { _dispFeeSG60 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSG61 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSG61")]
		public decimal? DispFeeSG61
		{
			get { return _dispFeeSG61; }
			set { _dispFeeSG61 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSB30 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSB30")]
		public decimal? DispFeeSB30
		{
			get { return _dispFeeSB30; }
			set { _dispFeeSB30 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSB60 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSB60")]
		public decimal? DispFeeSB60
		{
			get { return _dispFeeSB60; }
			set { _dispFeeSB60 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSB61 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSB61")]
		public decimal? DispFeeSB61
		{
			get { return _dispFeeSB61; }
			set { _dispFeeSB61 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSS30 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSS30")]
		public decimal? DispFeeSS30
		{
			get { return _dispFeeSS30; }
			set { _dispFeeSS30 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSS60 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSS60")]
		public decimal? DispFeeSS60
		{
			get { return _dispFeeSS60; }
			set { _dispFeeSS60 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeSS61 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeSS61")]
		public decimal? DispFeeSS61
		{
			get { return _dispFeeSS61; }
			set { _dispFeeSS61 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVG30 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVG30")]
		public decimal? DispFeeVG30
		{
			get { return _dispFeeVG30; }
			set { _dispFeeVG30 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVG60 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVG60")]
		public decimal? DispFeeVG60
		{
			get { return _dispFeeVG60; }
			set { _dispFeeVG60 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVG61 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVG61")]
		public decimal? DispFeeVG61
		{
			get { return _dispFeeVG61; }
			set { _dispFeeVG61 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVB30 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVB30")]
		public decimal? DispFeeVB30
		{
			get { return _dispFeeVB30; }
			set { _dispFeeVB30 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVB60 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVB60")]
		public decimal? DispFeeVB60
		{
			get { return _dispFeeVB60; }
			set { _dispFeeVB60 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVB61 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVB61")]
		public decimal? DispFeeVB61
		{
			get { return _dispFeeVB61; }
			set { _dispFeeVB61 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVS30 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVS30")]
		public decimal? DispFeeVS30
		{
			get { return _dispFeeVS30; }
			set { _dispFeeVS30 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVS60 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVS60")]
		public decimal? DispFeeVS60
		{
			get { return _dispFeeVS60; }
			set { _dispFeeVS60 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeVS61 property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeVS61")]
		public decimal? DispFeeVS61
		{
			get { return _dispFeeVS61; }
			set { _dispFeeVS61 = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeMin property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeMin")]
		public decimal? DispFeeMin
		{
			get { return _dispFeeMin; }
			set { _dispFeeMin = value; }
		}

		/// <summary>
		/// Gets/Sets the DispFeeMax property.
		/// </summary>
		[ColumnProperty(ColumnName = "DispFeeMax")]
		public decimal? DispFeeMax
		{
			get { return _dispFeeMax; }
			set { _dispFeeMax = value; }
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
		public Pharmacy() : base() { }

		/// <inheritdoc/>
		public Pharmacy(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public Pharmacy(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the Pharmacy class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="pharmacyID"></param>
		/// <param name="entityUID"></param>
		/// <param name="name"></param>
		/// <param name="npi"></param>
		/// <param name="addressStreet1"></param>
		/// <param name="addressStreet2"></param>
		/// <param name="addressCity"></param>
		/// <param name="addressState"></param>
		/// <param name="addressZip"></param>
		/// <param name="tPAAccountID"></param>
		/// <param name="wholesalerAccountID"></param>
		/// <param name="revenueModel340B"></param>
		/// <param name="includeGenerics"></param>
		/// <param name="scheduleStatus"></param>
		/// <param name="invoiceGroupID"></param>
		/// <param name="dispFeeSG30"></param>
		/// <param name="dispFeeSG60"></param>
		/// <param name="dispFeeSG61"></param>
		/// <param name="dispFeeSB30"></param>
		/// <param name="dispFeeSB60"></param>
		/// <param name="dispFeeSB61"></param>
		/// <param name="dispFeeSS30"></param>
		/// <param name="dispFeeSS60"></param>
		/// <param name="dispFeeSS61"></param>
		/// <param name="dispFeeVG30"></param>
		/// <param name="dispFeeVG60"></param>
		/// <param name="dispFeeVG61"></param>
		/// <param name="dispFeeVB30"></param>
		/// <param name="dispFeeVB60"></param>
		/// <param name="dispFeeVB61"></param>
		/// <param name="dispFeeVS30"></param>
		/// <param name="dispFeeVS60"></param>
		/// <param name="dispFeeVS61"></param>
		/// <param name="dispFeeMin"></param>
		/// <param name="dispFeeMax"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public Pharmacy(string dbServer, string dbDatabase, string dbUsername, string dbPassword, string pharmacyID, long entityUID, string name, string npi, string addressStreet1, string addressStreet2, string addressCity, string addressState, string addressZip, string tPAAccountID, string wholesalerAccountID, string revenueModel340B, bool includeGenerics, string scheduleStatus, string invoiceGroupID, decimal? dispFeeSG30, decimal? dispFeeSG60, decimal? dispFeeSG61, decimal? dispFeeSB30, decimal? dispFeeSB60, decimal? dispFeeSB61, decimal? dispFeeSS30, decimal? dispFeeSS60, decimal? dispFeeSS61, decimal? dispFeeVG30, decimal? dispFeeVG60, decimal? dispFeeVG61, decimal? dispFeeVB30, decimal? dispFeeVB60, decimal? dispFeeVB61, decimal? dispFeeVS30, decimal? dispFeeVS60, decimal? dispFeeVS61, decimal? dispFeeMin, decimal? dispFeeMax, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_pharmacyID = pharmacyID;
			_entityUID = entityUID;
			_name = name;
			_npi = npi;
			_addressStreet1 = addressStreet1;
			_addressStreet2 = addressStreet2;
			_addressCity = addressCity;
			_addressState = addressState;
			_addressZip = addressZip;
			_tPAAccountID = tPAAccountID;
			_wholesalerAccountID = wholesalerAccountID;
			_revenueModel340B = revenueModel340B;
			_includeGenerics = includeGenerics;
			_scheduleStatus = scheduleStatus;
			_invoiceGroupID = invoiceGroupID;
			_dispFeeSG30 = dispFeeSG30;
			_dispFeeSG60 = dispFeeSG60;
			_dispFeeSG61 = dispFeeSG61;
			_dispFeeSB30 = dispFeeSB30;
			_dispFeeSB60 = dispFeeSB60;
			_dispFeeSB61 = dispFeeSB61;
			_dispFeeSS30 = dispFeeSS30;
			_dispFeeSS60 = dispFeeSS60;
			_dispFeeSS61 = dispFeeSS61;
			_dispFeeVG30 = dispFeeVG30;
			_dispFeeVG60 = dispFeeVG60;
			_dispFeeVG61 = dispFeeVG61;
			_dispFeeVB30 = dispFeeVB30;
			_dispFeeVB60 = dispFeeVB60;
			_dispFeeVB61 = dispFeeVB61;
			_dispFeeVS30 = dispFeeVS30;
			_dispFeeVS60 = dispFeeVS60;
			_dispFeeVS61 = dispFeeVS61;
			_dispFeeMin = dispFeeMin;
			_dispFeeMax = dispFeeMax;
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
		/// Returns the record for the specified PharmacyUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long pharmacyUID)
		{
			try {
				return base.Find(pharmacyUID);
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

			_cspAdd = "csp_Pharmacy_Add";
			_cspUpdate = "csp_Pharmacy_Update";
			_cspFind = "csp_Pharmacy_Find";
			_cspDelete = "csp_Pharmacy_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class Pharmacies : BaseDataCollection<Pharmacy>
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

		public Pharmacies()
		{
			_SetupDatabase();
		}

		public Pharmacies(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_Pharmacies_Find";
			_cspFindParameterNames.Clear();
			_cspFindParameterNames.Add("EntityUID");
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
