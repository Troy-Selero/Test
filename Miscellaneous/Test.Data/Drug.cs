using Selero.Selena;
using System.Transactions;

namespace Test.Data
{
	/// <summary>
	///
	/// </summary>
	/// <history>
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// Date		Author					Description
	/// ==========	=================		=======================================================
	/// 03/07/2023	Data Tier Generator		Initial Creation
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// </history>
	public class Drug : BaseData
	{
		#region Variables

		PharmClasses _pharmClasses = new PharmClasses();

		#endregion Variables

		#region Properties

		public PharmClasses PharmClasses
		{
			get { return _pharmClasses; }
			set { _pharmClasses = value; }
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
					if (base.Add(updateUser, updateApp)) {
						successful = true;

						foreach (PharmClass pharmClass in this.PharmClasses) {
							pharmClass.SetData(DBServer, DBDatabase, DBUsername, DBPassword);
							pharmClass.DrugUID = this.DrugUID;

							if (!pharmClass.Add(updateUser, updateApp)) {
								successful = false;
								break;
							}
						}
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

		public bool Find(string productNDC, bool recursive)
		{
			bool returnValue = false;

			try {
				long drugUID = -1;

				// Define the input parameters
				Dictionary<string, object> inputParameters = new Dictionary<string, object>
				{
					{ "ProductNDC", productNDC }
				};

				// Define the output parameters
				Dictionary<string, object> outputParameters = new Dictionary<string, object>
				{
					{ "DrugUID", drugUID }
				};

				// Perform the operation
				if (base.ExecuteOutputStoredProcedure("csp_Drug_Find_ProductNDC", inputParameters, outputParameters)) {
					drugUID = (long)outputParameters["DrugUID"];

					// Perform the base object find in order to populate its properties
					if (drugUID > 0) {
						if (this.Find(drugUID, recursive))
							returnValue = true;
					}
				}
			}
			catch (Exception) {
				throw;
			}

			return returnValue;
		}

		public bool Find(long drugUID, bool recursive)
		{
			bool returnValue;

			try {
				returnValue = base.Find(drugUID);

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
				_pharmClasses = new PharmClasses(DBServer, DBDatabase, DBUsername, DBPassword);

				returnValue = _pharmClasses.Find(this.DrugUID);
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

		private long _drugUID;
		private string _productNDC;
		private string _brandName;
		private string _genericName;
		private string _dEASchedule;
		private string _manufacturer;
		private bool _generic;
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
		/// Gets/Sets the DrugUID property.
		/// </summary>
		[ColumnProperty(ColumnName = "DrugUID")]
		public long DrugUID
		{
			get { return _drugUID; }
			set { _drugUID = value; }
		}

		/// <summary>
		/// Gets/Sets the ProductNDC property.
		/// </summary>
		[ColumnProperty(ColumnName = "ProductNDC")]
		public string ProductNDC
		{
			get { return _productNDC; }
			set { _productNDC = value; }
		}

		/// <summary>
		/// Gets/Sets the BrandName property.
		/// </summary>
		[ColumnProperty(ColumnName = "BrandName")]
		public string BrandName
		{
			get { return _brandName; }
			set { _brandName = value; }
		}

		/// <summary>
		/// Gets/Sets the GenericName property.
		/// </summary>
		[ColumnProperty(ColumnName = "GenericName")]
		public string GenericName
		{
			get { return _genericName; }
			set { _genericName = value; }
		}

		/// <summary>
		/// Gets/Sets the DEASchedule property.
		/// </summary>
		[ColumnProperty(ColumnName = "DEASchedule")]
		public string DEASchedule
		{
			get { return _dEASchedule; }
			set { _dEASchedule = value; }
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
		/// Gets/Sets the Generic property.
		/// </summary>
		[ColumnProperty(ColumnName = "Generic")]
		public bool Generic
		{
			get { return _generic; }
			set { _generic = value; }
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
		public Drug() : base() { }

		/// <inheritdoc/>
		public Drug(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public Drug(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		/// <summary>
		/// Initializes a new instance of the Drug class.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="productNDC"></param>
		/// <param name="brandName"></param>
		/// <param name="genericName"></param>
		/// <param name="dEASchedule"></param>
		/// <param name="manufacturer"></param>
		/// <param name="generic"></param>
		/// <param name="createdMachine"></param>
		/// <param name="createdApp"></param>
		/// <param name="createdDate"></param>
		/// <param name="createdBy"></param>
		/// <param name="updatedMachine"></param>
		/// <param name="updatedApp"></param>
		/// <param name="updatedDate"></param>
		/// <param name="updatedBy"></param>
		public Drug(string dbServer, string dbDatabase, string dbUsername, string dbPassword, string productNDC, string brandName, string genericName, string dEASchedule, string manufacturer, bool generic, string createdMachine, string createdApp, DateTime createdDate, string createdBy, string updatedMachine, string updatedApp, DateTime updatedDate, string updatedBy) : base(dbServer, dbDatabase, dbUsername, dbPassword)
		{
			_productNDC = productNDC;
			_brandName = brandName;
			_genericName = genericName;
			_dEASchedule = dEASchedule;
			_manufacturer = manufacturer;
			_generic = generic;
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
		/// Returns the record for the specified DrugUID from the database.
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ========	==================		=====================================================
		/// 03/08/2023	Data Tier Generator		Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public bool Find(long drugUID)
		{
			try {
				return base.Find(drugUID);
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

			_cspAdd = "csp_Drug_Add";
			_cspUpdate = "csp_Drug_Update";
			_cspFind = "csp_Drug_Find";
			_cspDelete = "csp_Drug_Delete";
		}

		#endregion Generated Protected

		#region Generated Private

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}

	public class Drugs : BaseDataCollection<Drug>
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

		public Drugs()
		{
			_SetupDatabase();
		}

		public Drugs(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)
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
			_cspFind = "csp_Drugs_Find";
			_cspFindParameterNames.Clear();
		}

		#endregion Generated Private

		#endregion Generated Methods

		#endregion DTG Code - DO NOT MODIFY
	}
}
