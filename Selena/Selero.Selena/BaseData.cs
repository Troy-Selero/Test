using System;

namespace Selero.Selena
{
	public class BaseData : Core.BaseData
	{
		#region Variables

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		/// <inheritdoc/>
		public BaseData() : base() { }

		/// <inheritdoc/>
		public BaseData(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public BaseData(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }

		#endregion Constructors

		#region Methods

		#region Public
		
		/// <summary>
		/// Takes the properties of the associated object and creates an entry into the corresponding table in the database using the public and
		/// instance binding flags and the specified user to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="updateUser">Username to use to update fields associated with the properites property DefaultUser attribute is true</param>
		/// <param name="updateApp">Application Name to use to update fields associated with the properites property DefaultApp attribute is true</param>
		/// <returns>Boolean indicating whether or not this add was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the _cspAddParameterNamesOutput list</returns>
		public virtual bool Add(string updateUser, string updateApp)
		{
			// Update the default values dictionary
			if (_defaultValues.ContainsKey("updateApp"))
				_defaultValues["updateApp"] = updateApp;
			else
				_defaultValues.Add("updateApp", updateApp);

			return base.Add(updateUser);
		}

		/// <summary>
		/// Deletes the data in the database that is assoicated with an instance of the object found using the "Delete" stored procedure along with the specified parameter values.
		/// </summary>
		/// <param name="updateUser">The name of the user that is performing this Delete.</param>
		/// <param name="updateApp">Application Name to use to update fields associated with the properites property DefaultApp attribute is true.</param>
		/// <param name="parameterValues">values to be assigned to the parameters of the "Delete" stored 
		/// procedure</param>
		/// <returns>Flag indicating whether or not this was successful</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public virtual bool Delete(string updateUser, string updateApp)
		{
			return Delete(updateUser, updateApp, null);
		}

		/// <summary>
		/// Takes the properties of the associated object and updates an entry into the corresponding table in the database using the public and instance binding flags
		/// and the specified user to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="updateUser">Username to use to update fields associated with the properites property DefaultUser attribute is true.</param>
		/// <returns>Boolean indicating whether or not this updates was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the _cspUpdateParameterNamesOutput list</returns>
		public virtual bool Update(string updateUser, string updateApp)
		{
			// Update the default values dictionary
			if (_defaultValues.ContainsKey("updateApp"))
				_defaultValues["updateApp"] = updateApp;
			else
				_defaultValues.Add("updateApp", updateApp);

			return base.Update(updateUser);
		}

		#endregion Public

		#region Protected

		/// <summary>
		/// Deletes the data in the database that is associated with an instance of the object found using the "Delete" stored procedure along with the specified parameter values.
		/// </summary>
		/// <param name="updateUser">The name of the user that is performing this Delete.</param>
		/// <param name="updateApp">Application Name to use to update fields associated with the properites property DefaultApp attribute is true.</param>
		/// <param name="parameterValues">values to be assigned to the parameters of the "Delete" stored 
		/// procedure</param>
		/// <returns>Flag indicating whether or not this was successful</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool Delete(string updateUser, string updateApp, params object[] parameterValues)
		{
			// Update the default values dictionary
			if (_defaultValues.ContainsKey("updateApp"))
				_defaultValues["updateApp"] = updateApp;
			else
				_defaultValues.Add("updateApp", updateApp);

			return base.Delete(updateUser, parameterValues);
		}

		/// <inheritdoc/>
		protected override void SetupDatabase()
		{
			base.SetupDatabase();

			if (!_defaultValues.ContainsKey("updateApp"))
				_defaultValues.Add("updateApp", null);
		}

		/// <inheritdoc/>
		protected override void UpdateDefaultValue(ref object value, object columnProperty)
		{
			base.UpdateDefaultValue(ref value, columnProperty);

			string updateApp = _defaultValues.ContainsKey("updateApp") ? _defaultValues["updateApp"].ToString() : string.Empty;

			ColumnProperty localColumnProperty = (ColumnProperty)columnProperty;
			if (localColumnProperty.DefaultType == Core.ColumnProperty.DefaultTypes.Database) {
				if (localColumnProperty.DefaultApp) {
					if (updateApp != null && updateApp.Trim() == string.Empty)
						value = null;
					else
						value = updateApp;
				}
				else if (localColumnProperty.DefaultMachine)
					value = Environment.MachineName;
			}
			else if (localColumnProperty.DefaultType == Core.ColumnProperty.DefaultTypes.System) {
				if (localColumnProperty.DefaultApp) {
					if (updateApp != null && updateApp.Trim() == string.Empty)
						value = this.GetType().Assembly.GetName();
					else
						value = updateApp;
				}
				else if (localColumnProperty.DefaultMachine)
					value = Environment.MachineName;
			}
		}

		#endregion Protected

		#region Private

		#endregion Private

		#endregion Methods
	}

	public class BaseDataCollection<T> : Core.BaseDataCollection<T> where T : BaseData, new()
	{
		#region Variables

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		/// <inheritdoc/>
		public BaseDataCollection() : base() { }

		/// <inheritdoc/>
		public BaseDataCollection(string dbServer, string dbDatase, string dbUsername, string dbPassword) : base(dbServer, dbDatase, dbUsername, dbPassword) { }

		/// <inheritdoc/>
		public BaseDataCollection(string dbServer, string dbDatase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatase, dbUsername, dbPassword, commandTimeout) { }
		
		#endregion Constructors

		#region Methods

		#region Public

		#endregion Public

		#region Protected

		#endregion Protected

		#region Private

		#endregion Private

		#endregion Methods
	}

	/// <inheritdoc/>
	public class ColumnProperty : Core.ColumnProperty
	{
		#region Variables
		
		private bool _defaultApp = false;
		private bool _defaultMachine = false;

		#endregion Variables

		#region Properties

		/// <summary>
		/// Indicates whether value should be updated with current Assembly Name (or updateApp parameter
		/// of the operation Method) during Add/Update/Delete operations.
		/// Default: false
		/// </summary>
		public bool DefaultApp
		{
			get { return _defaultApp; }
			set { _defaultApp = value; }
		}

		/// <summary>
		/// Indicates whether value should be update with the current machine name during 
		/// Add/Update/Delete operations.
		/// Default: false
		/// </summary>
		public bool DefaultMachine
		{
			get { return _defaultMachine; }
			set { _defaultMachine = value; }
		}

		#endregion Properties

		#region Constructors

		/// <inheritdoc/>
		public ColumnProperty() : base()
		{
			_defaultMachine = false;
			_defaultApp = false;
		}

		/// <inheritdoc/>
		public ColumnProperty(string columnName) : base(columnName)
		{
			_defaultMachine = false;
			_defaultApp = false;
		}

		/// <summary>
		/// Creates a ColumnProperty object by specifying all of the needed attributes.
		/// </summary>
		/// <param name="columnName">Name of column to create the object for</param>
		/// <param name="defaultType">The type of default to use.  (Database or System)</param>
		/// <param name="addOnly">Indicates that this column should be updated with defaults on Adds Only</param>
		/// <param name="deleteOnly">Indicates that this column should be updated with defaults on Deletes Only</param>
		/// <param name="defaultUser">Indicates that the column should be updated with the default (Database or System) user (or updateUser parameter
		/// of the operation Method)</param>
		/// <param name="defaultDate">Indicates that the column should be updated with the current (Database or System) local date/time</param>
		/// <param name="deleteOnly">Indicates that this column should be updated with defaults on Deletes Only</param>
		/// <param name="defaultApp">Indicates that the column should be updated with the current Assembly name (or updateApp parameter
		/// of the operation Method)</param>
		/// <param name="defaultMachine">Indicates that the column should be updated with the default (Database or System) machine name</param>
		public ColumnProperty(string columnName, DefaultTypes defaultType, bool addOnly, bool deleteOnly, bool defaultUser, bool defaultDate, bool defaultApp, bool defaultMachine)
			: base(columnName, defaultType, addOnly, deleteOnly, defaultUser, defaultDate, defaultApp, defaultMachine)
		{
			_defaultApp = defaultApp;
			_defaultMachine = defaultMachine;
		}

		#endregion Constructors

		#region Methods

		#region Public

		#endregion Public

		#region Protected

		#endregion Protected

		#region Private

		#endregion Private

		#endregion Methods
	}
}
