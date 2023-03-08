using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Selero.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class BaseData
	{
		#region Variables

		private int _commandTimeout = 120;
		private string _dbServer = string.Empty;
		private string _dbDatabase = string.Empty;
		private string _dbUsername = string.Empty;
		private string _dbPassword = string.Empty;
		private Routines _routines = new Routines();

		/// <summary>
		/// The name of the stored procedure used to perform an add in the database.
		/// </summary>
		protected string _cspAdd = string.Empty;

		/// <summary>
		/// The name of the stored procedure used to perform an update in the database.
		/// </summary>
		protected string _cspUpdate = string.Empty;

		/// <summary>
		/// The name of the stored procedure used to perform a find in the database.
		/// </summary>
		protected string _cspFind = string.Empty;

		/// <summary>
		/// The name of the stored procedure used to perform a delete in the database.
		/// </summary>
		protected string _cspDelete = string.Empty;

		/// <summary>
		/// A dictionary of any default values used during an add, update, or delete in the database.
		/// </summary>
		protected Dictionary<string, object> _defaultValues = new Dictionary<string, object>();

		#endregion Variables

		#region Properties

		/// <summary>
		/// Gets/Sets the wait time (in seconds) before terminating the attempt to execute a command and generating an error.
		/// The default value is 120 seconds.
		/// </summary>
		public int CommandTimeout
		{
			get { return _commandTimeout; }
			set { _commandTimeout = value; }
		}

		/// <summary>
		/// Gets/Sets the name or ip address of the database server.
		/// </summary>
		public string DBServer
		{
			get { return _dbServer; }
			set { _dbServer = value; }
		}

		/// <summary>
		/// Gets/Sets the name of the database.
		/// </summary>
		public string DBDatabase
		{
			get { return _dbDatabase; }
			set { _dbDatabase = value; }
		}

		/// <summary>
		/// Gets/Sets the user name used to connect to the database. Leave blank for a trusted connection.
		/// </summary>
		public string DBUsername
		{
			get { return _dbUsername; }
			set { _dbUsername = value; }
		}

		/// <summary>
		/// Gets/Sets the password used to connect to the database. Leave blank for a trusted connection.
		/// </summary>
		public string DBPassword
		{
			get { return _dbPassword; }
			set { _dbPassword = value; }
		}

		#endregion Properties

		#region Constructors

		/// <summary>
		/// Create an instance of the object without data properties.
		/// </summary>
		public BaseData()
		{
			SetupDatabase();
		}

		/// <summary>
		/// Create an instance of the object using the specified arguments to create the data properties necessary to connect to the database.
		/// </summary>
		/// <param name="dbServer">Name or ip address of database server.</param>
		/// <param name="dbDatabase">Name of the database.</param>
		/// <param name="dbUsername">User name used to connect to the database. Leave blank for a trusted connection.</param>
		/// <param name="dbPassword">Password used to connect to the database. Leave blank for a trusted connections.</param>
		public BaseData(string dbServer, string dbDatabase, string dbUsername, string dbPassword)
		{
			SetupDatabase();
			SetData(dbServer, dbDatabase, dbUsername, dbPassword);
		}

		/// <summary>
		/// Create an instance of the object using the specified arguments to create the data properties necessary to connect to the database.
		/// </summary>
		/// <param name="dbServer">Name or ip address of database server.</param>
		/// <param name="dbDatabase">Name of the database.</param>
		/// <param name="dbUsername">User name used to connect to the database. Leave blank for a trusted connection.</param>
		/// <param name="dbPassword">Password used to connect to the database. Leave blank for a trusted connections.</param>
		/// <param name="commandTimeout">Wait time (in seconds) before terminating the attempt to execute a command and generating an error. The default value is 120 seconds.</param>
		public BaseData(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout)
		{
			SetupDatabase();
			SetData(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout);
		}

		#endregion Constructors

		#region Methods

		#region Public

		/// <summary>
		/// Copy all properties defined as Column Properties from the source object to the current object.
		/// </summary>
		/// <param name="source">The source object.</param>
		/// <returns>True if successful; False otherwise.</returns>
		public virtual bool CopyProperties(object source)
		{
			List<string> defaultIgnore = new List<string>();

			return CopyProperties(source, defaultIgnore);
		}

		/// <summary>
		/// Copy all properties defined as Column Properties from the source object to the current object.
		/// </summary>
		/// <param name="source">The source object.</param>
		/// <param name="ignoreList">The list of properties to ignore when copying.</param>
		/// <returns>True if successful; False otherwise.</returns>
		public virtual bool CopyProperties(object source, List<string> ignoreList)
		{
			bool returnValue = false;

			try {
				if (source.GetType() == this.GetType()) {
					PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

					foreach (PropertyInfo property in properties) {
						if (property.IsDefined(typeof(ColumnProperty), true) && !ignoreList.Contains(property.Name)) {
							object value = property.GetValue(this, null);
							object compareValue = property.GetValue(source, null);

							if (value != null || compareValue != null) {
								if (value == null || !value.Equals(compareValue))
									property.SetValue(this, compareValue, null);
							}
						}
					}

					returnValue = true;
				}
			}
			catch {
				returnValue = false;
			}

			return returnValue;
		}

		/// <summary>
		/// Compare the current object to the specified object based on the properties defined as Column Properties.
		/// </summary>
		/// <param name="source"></param>
		/// <returns>True if properties are the same; False otherwise.</returns>
		public virtual bool PropertiesEqual(object source)
		{
			List<string> defaultIgnore = new List<string>();

			return PropertiesEqual(source, defaultIgnore);
		}

		/// <summary>
		/// Compare the current object to the specified object based on the properties defined as Column Properties.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="ignoreList">The list of properties to ignore when comparing.</param>
		/// <returns>True if properties are the same; False otherwise.</returns>
		public virtual bool PropertiesEqual(object source, List<string> ignoreList)
		{
			bool returnValue = true;

			try {
				if (source.GetType() == this.GetType()) {
					PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

					foreach (PropertyInfo property in properties) {
						if (property.IsDefined(typeof(ColumnProperty), true) && !ignoreList.Contains(property.Name)) {
							object value = property.GetValue(this, null);
							object compareValue = property.GetValue(source, null);

							if (value != null && compareValue != null) {
								if (!value.Equals(compareValue)) {
									returnValue = false;

									break;
								}
							}
							else if ((value == null && compareValue != null) || (value != null && compareValue == null)) {
								returnValue = false;

								break;
							}
						}
					}
				}
				else
					returnValue = false;
			}
			catch {
				returnValue = false;
			}

			return returnValue;
		}

		/// <summary>
		/// Set the data properties necessary to make a connection to a data source.
		/// </summary>
		/// <param name="dbServer">Name or ip address of database server.</param>
		/// <param name="dbDatabase">Name of the database.</param>
		/// <param name="dbUsername">User name used to connect to the database. Leave blank for a trusted connection.</param>
		/// <param name="dbPassword">Password used to connect to the database. Leave blank for a trusted connections.</param>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public void SetData(string dbServer, string dbDatabase, string dbUsername, string dbPassword)
		{
			SetData(dbServer, dbDatabase, dbUsername, dbPassword, 120);
		}

		/// <summary>
		/// Sets the data properties necessary to make a connection to a data source
		/// </summary>
		/// <param name="dbServer">Name or ip address of database server.</param>
		/// <param name="dbDatabase">Name of the database.</param>
		/// <param name="dbUsername">User name used to connect to the database. Leave blank for a trusted connection.</param>
		/// <param name="dbPassword">Password used to connect to the database. Leave blank for a trusted connections.</param>
		/// <param name="commandTimeout">Wait time (in seconds) before terminating the attempt to execute a command and generating an error. The default value is 120 seconds.</param>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public void SetData(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout)
		{
			_dbServer = dbServer;
			_dbDatabase = dbDatabase;
			_dbUsername = dbUsername;
			_dbPassword = dbPassword;
			_commandTimeout = commandTimeout;
		}

		#endregion Public

		#region Protected

		/// <summary>
		/// Takes the properties of the associated object and creates an entry into the corresponding
		/// table in the database using the Public and Instance Binding Flags and using the Database
		/// Server's System User or the current Environment.UserName (depending on the property's DefaultType
		/// attribute) to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <returns>Boolean indicating whether or not this add was successful.
		/// Also, it sets the value(s) of any output parameters specified in the _cspAddParameterNamesOutput list</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool Add()
		{
			return Add(null, BindingFlags.Public | BindingFlags.Instance);
		}

		/// <summary>
		/// Takes the properties of the associated object and creates an entry into the corresponding
		/// table in the database using the Public and Instance Binding Flags and the specified user 
		/// to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="updateUser">Username to use to update fields associated with the properites property DefaultUser attribute is true</param>
		/// <returns>Boolean indicating whether or not this add was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the _cspAddParameterNamesOutput list</returns>
		protected virtual bool Add(string updateUser)
		{
			return Add(updateUser, BindingFlags.Public | BindingFlags.Instance);
		}

		/// <summary>
		/// Takes the properties of the associated object and creates an entry into the corresponding
		/// table in the database using the specified Binding Flags and using the Database
		/// Server's System User or the current Environment.UserName (depending on the property's DefaultType
		/// attribute) to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="flags">Specifies the System.Reflection.BindingFlags to use when retrieving the objects properties to use to update the database.</param>
		/// <returns>Boolean indicating whether or not this add was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the _cspAddParameterNamesOutput list</returns>
		protected virtual bool Add(BindingFlags flags)
		{
			return Add(string.Empty, flags);
		}

		/// <summary>
		/// Takes the properties of the associated object and creates an entry into the corresponding
		/// table in the database using the specified Binding Flags and the specified user to update 
		/// fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="updateUser">Username to use to update fields associated with the properites property DefaultUser attribute is true.</param>
		/// <param name="flags">Specifies the System.Reflection.BindingFlags to use when retrieving
		/// the objects properties to use to update the database.</param>
		/// <returns>Boolean indicating whether or not this add was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the _cspAddParameterNamesOutput list</returns>
		protected virtual bool Add(string updateUser, BindingFlags flags)
		{
			bool returnValue = false;

			if (_Initialized()) {
				try {
					// Update the default values dictionary
					if (_defaultValues.ContainsKey("updateUser"))
						_defaultValues["updateUser"] = updateUser;
					else
						_defaultValues.Add("updateUser", updateUser);

					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						// Open the connection to the database
						connection.Open();

						// Create the command object
						SqlCommand command = new SqlCommand(_cspAdd, connection)
						{
							CommandType = CommandType.StoredProcedure,
							CommandTimeout = _commandTimeout
						};

						// Retrieve parameter information from the stored procedure in the SqlCommand and populate the Parameters collection
						SqlCommandBuilder.DeriveParameters(command);

						// Get all properties for the current type
						PropertyInfo[] properties = this.GetType().GetProperties(flags);

						foreach (PropertyInfo property in properties) {
							// Process only if property has the ColumnProperty attribute
							if (property.IsDefined(typeof(ColumnProperty), true)) {
								ColumnProperty columnProperty = (ColumnProperty)property.GetCustomAttributes(typeof(ColumnProperty), true)[0];

								// Get the current value and name of the property
								object value = property.GetValue(this, null);
								string parameterName = string.Format("@p{0}", columnProperty.ColumnName);

								if (!columnProperty.DeleteOnly)
									UpdateDefaultValue(ref value, columnProperty);

								// Set the value of the parameter
								if (command.Parameters.Contains(parameterName))
									command.Parameters[parameterName].Value = value;
							}
						}

						// Execute the query
						int rowsAffected = command.ExecuteNonQuery();

						// Get the return value(s)
						if (rowsAffected >= 1) {
							returnValue = true;

							foreach (SqlParameter parameter in command.Parameters) {
								// Update the properties for the current type with the returned values
								if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput) {
									string columnName = parameter.ParameterName.Replace("@p", string.Empty);
									object columnValue = parameter.Value;
									PropertyInfo property = this.GetType().GetProperty(columnName);

									// Update the value if writable (has a set accessor)
									if (property != null && property.CanWrite) {
										if (!columnValue.Equals(DBNull.Value) || _routines.IsNullable(property.GetType()))
											this.GetType().GetProperty(columnName).SetValue(this, columnValue, null);
									}
								}
							}
						}

						// Close the connection to the database
						connection.Close();
					}
				}
				catch (Exception ex) {
					throw ex;
				}

				return returnValue;
			}
			else
				throw new Exception("Data class is not initialized");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		protected virtual bool Delete(params object[] parameterValues)
		{
			return Delete(null, parameterValues);
		}

		/// <summary>
		/// Deletes the data in the database that is assoicated with an instance of the object
		/// found using the "Delete" stored procedure along with the specified parameter values
		/// </summary>
		/// <param name="updateUser">The name of the user that is performing this Delete</param>
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
		protected virtual bool Delete(string updateUser, params object[] parameterValues)
		{
			return Delete(updateUser, out _, parameterValues);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rowsAffected"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		protected virtual bool Delete(out int rowsAffected, params object[] parameterValues)
		{
			return Delete(null, out rowsAffected, parameterValues);
		}

		/// <summary>
		/// Deletes the data in the database that is assoicated with an instance of the object
		/// found using the "Delete" stored procedure along with the specified parameter values
		/// and returns the number of rows affected by the delete
		/// </summary>
		/// <param name="updateUser">The name of the user that is performing this Delete</param>
		/// <param name="rowsAffected">Number of rows affected by the Delete</param>
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
		protected virtual bool Delete(string updateUser, out int rowsAffected, params object[] parameterValues)
		{
			bool returnValue = false;

			if (_Initialized()) {
				try {
					// Update the default values dictionary
					if (_defaultValues.ContainsKey("updateUser"))
						_defaultValues["updateUser"] = updateUser;
					else
						_defaultValues.Add("updateUser", updateUser);

					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						// Open the connection to the database
						connection.Open();

						// Create the command object
						SqlCommand command = new SqlCommand(_cspDelete, connection)
						{
							CommandType = CommandType.StoredProcedure,
							CommandTimeout = _commandTimeout
						};

						// Retrieve parameter information from the stored procedure in the SqlCommand and populate the Parameters collection
						SqlCommandBuilder.DeriveParameters(command);

						// Get all properties for the current type
						PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

						foreach (PropertyInfo property in properties) {
							// Process only if property has the ColumnProperty attribute
							if (property.IsDefined(typeof(ColumnProperty), true)) {
								ColumnProperty columnProperty = (ColumnProperty)property.GetCustomAttributes(typeof(ColumnProperty), true)[0];

								// Get the current value and name of the property
								object value = property.GetValue(this, null);
								string parameterName = string.Format("@p{0}", columnProperty.ColumnName);

								if (!columnProperty.AddOnly) {
									UpdateDefaultValue(ref value, columnProperty);
								}

								// Set the value of the parameter
								if (command.Parameters.Contains(parameterName))
									command.Parameters[parameterName].Value = value;
							}
						}

						// Set the value for each stored procedure parameter from the provided parameter values
						int index = 0;
						foreach (SqlParameter parameter in command.Parameters) {
							if (parameter.Direction != ParameterDirection.ReturnValue && parameter.Direction != ParameterDirection.Output && parameter.Direction != ParameterDirection.InputOutput) {
								if (parameterValues != null && parameterValues.Length > index)
									parameter.Value = parameterValues[index++];
							}
						}

						// Execute the query
						rowsAffected = command.ExecuteNonQuery();

						// Get the return value(s)
						if (rowsAffected >= 1) {
							returnValue = true;

							// Do not update current object properties if this delete is done using specific value(s)
							if (parameterValues == null) {
								foreach (SqlParameter parameter in command.Parameters) {
									// Update the properties for the current type with the returned values
									if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput) {
										string columnName = parameter.ParameterName.Replace("@p", string.Empty);
										object columnValue = parameter.Value;
										PropertyInfo property = this.GetType().GetProperty(columnName);

										// Update the value if writable (has a set accessor)
										if (property != null && property.CanWrite) {
											if (!columnValue.Equals(System.DBNull.Value) || _routines.IsNullable(property.GetType()))
												this.GetType().GetProperty(columnName).SetValue(this, columnValue, null);
										}
									}
								}
							}
						}

						// Close the connection to the database
						connection.Close();

						returnValue = true;
					}
				}
				catch (Exception ex) {
					throw ex;
				}

				return returnValue;
			}
			else
				throw new Exception("Data class is not initialized");
		}

		/// <summary>
		/// Executes the specified stored procedure using the specified input parameters and is expecting
		/// the stored procedure to return any needed values as output parameters.
		/// </summary>
		/// <param name="storedProcedure">Name of stored procedure to execute</param>
		/// <param name="inputParameters">A dictionary of objects to be used as input parameters to the
		/// stored procedure.  The key to the dictionary is the parameter name and the value is value to
		/// assign to the parameter</param>
		/// <param name="outputParameters">A dictionary of objects to be used as input parameters to the
		/// stored procedure.  The key to the dictionary is the parameter name and the value is value to
		/// assign to the parameter</param>
		/// <returns>Flag indicating whether or not the execution of the stored procedure was successful.
		/// Also, it returns the outputParameters dictionary in order to access the values returned as 
		/// output parameters.</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool ExecuteOutputStoredProcedure(string storedProcedure, Dictionary<string, object> inputParameters, Dictionary<string, object> outputParameters)
		{
			bool returnValue = false;

			if (_Initialized()) {
				try {
					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						// Open the connection to the database
						connection.Open();

						// Create the command object
						SqlCommand command = new SqlCommand(storedProcedure, connection)
						{
							CommandType = CommandType.StoredProcedure,
							CommandTimeout = _commandTimeout
						};

						// Retrieve parameter information from the stored procedure in the SqlCommand and populate the Parameters collection
						SqlCommandBuilder.DeriveParameters(command);

						// Add the SqlCommand input parameters
						foreach (KeyValuePair<string, object> kvp in inputParameters)
							_AddSQLParameters(kvp, command);

						// Add the SqlCommand output parameters
						foreach (KeyValuePair<string, object> kvp in outputParameters)
							_AddSQLParameters(kvp, command);

						// Execute the query
						command.ExecuteNonQuery();

						// Get the return value(s)
						foreach (SqlParameter parameter in command.Parameters) {
							if (outputParameters.ContainsKey(parameter.ParameterName.Replace("@p", string.Empty)))
								outputParameters[parameter.ParameterName.Replace("@p", string.Empty)] = parameter.Value;
						}

						returnValue = true;

						// Close the connection to the database
						connection.Close();
					}
				}
				catch {
					throw;
				}
			}
			else
				throw new Exception("Data class is not initialized");

			return returnValue;
		}

		/// <summary>
		/// Executes the specified stored procedure using the specified input parameters
		/// </summary>
		/// <param name="storedProcedure">Name of stored procedure to execute</param>
		/// <param name="inputParameters">A dictionary of objects to be used as input parameters to the
		/// stored procedure.  The key to the dictionary is the parameter name and the value is value to
		/// assign to the parameter</param>
		/// <returns>Flag indicating whether or not the execution of the stored procedure was successful.</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool ExecuteStoredProcedure(string storedProcedure, Dictionary<string, object> inputParameters)
		{
			int rowsAffected = 0;
			return ExecuteStoredProcedure(out rowsAffected, storedProcedure, inputParameters);
		}

		/// <summary>
		/// Executes the specified stored procedure using the specified input parameters and provides
		/// the number of affected rows as an output argument
		/// </summary>
		/// <param name="rowsAffected">The number of rows affected by executing the query</param>
		/// <param name="storedProcedure">Name of stored procedure to execute</param>
		/// <param name="inputParameters">A dictionary of objects to be used as input parameters to the
		/// stored procedure.  The key to the dictionary is the parameter name and the value is value to
		/// assign to the parameter</param>
		/// <returns>Flag indicating whether or not the execution of the stored procedure was successful.</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool ExecuteStoredProcedure(out int rowsAffected, string storedProcedure, Dictionary<string, object> inputParameters)
		{
			bool returnValue = false;

			if (_Initialized()) {
				try {
					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						// Open the connection to the database
						connection.Open();

						// Create the command object
						SqlCommand command = new SqlCommand(storedProcedure, connection)
						{
							CommandType = CommandType.StoredProcedure,
							CommandTimeout = _commandTimeout
						};

						// Add the SqlCommand input parameters
						foreach (KeyValuePair<string, object> kvp in inputParameters)
							command.Parameters.Add(_routines.CreateParameter(String.Format("@p{0}", kvp.Key), kvp.Value, kvp.Value.GetType(), false));

						// Execute the query
						rowsAffected = command.ExecuteNonQuery();

						returnValue = true;

						// Close the connection to the database
						connection.Close();
					}
				}
				catch (Exception ex) {
					throw ex;
				}
			}
			else
				throw new Exception("Data class is not initialized");

			return returnValue;
		}

		/// <summary>
		/// Finds the data in the database that is assoicated with an instance of the object
		/// found using the "Find" stored procedure along with the specified parameter values
		/// and populates the object with the information
		/// </summary>
		/// <param name="parameterValues">Values to be assigned to the parameters of the "Find" stored 
		/// procedure</param>
		/// <returns>Flag indicating whether or not this was successful</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool Find(params object[] parameterValues)
		{
			bool returnValue = false;

			if (_Initialized()) {
				try {
					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						// Open the connection to the database
						connection.Open();

						// Create the command object
						SqlCommand command = new SqlCommand(_cspFind, connection)
						{
							CommandType = CommandType.StoredProcedure,
							CommandTimeout = _commandTimeout
						};

						// Retrieve parameter information from the stored procedure in the SqlCommand and populate the Parameters collection
						SqlCommandBuilder.DeriveParameters(command);

						// Set the value for each stored procedure parameter from the provided parameter values
						int index = 0;
						foreach (SqlParameter parameter in command.Parameters) {
							if (parameter.Direction != ParameterDirection.ReturnValue)
								parameter.Value = parameterValues[index++];
						}

						// Execute the query
						SqlDataReader dr = command.ExecuteReader();

						// Process the first returned row
						if (dr.HasRows && dr.Read() && !dr.IsClosed) {
							// Update the properties for the current type with the returned values
							for (int i = 0; i < dr.FieldCount; i++) {
								string columnName = dr.GetName(i);
								object columnValue = dr.GetValue(i);
								PropertyInfo property = this.GetType().GetProperty(columnName);

								// Update the value if writable (has a set accessor)
								if (property != null && property.CanWrite) {
									if (!columnValue.Equals(DBNull.Value) || _routines.IsNullable(property.GetType()))
										this.GetType().GetProperty(columnName).SetValue(this, dr.GetValue(i), null);
								}
							}

							returnValue = true;
						}

						// Close the connection to the database
						connection.Close();
					}
				}
				catch (Exception ex) {
					throw ex;
				}

				return returnValue;
			}
			else
				throw new Exception("Data class is not initialized");
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void SetupDatabase()
		{
			_defaultValues.Clear();
			_defaultValues.Add("updateUser", null);
		}

		/// <summary>
		/// Takes the properties of the associated object and updates an entry into the corresponding
		/// table in the database using the Public and Instance Binding Flags and using the Database
		/// Server's System User or the current Environment.UserName (depending on the property's DefaultType
		/// attribute) to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <returns>Boolean indicating whether or not this updates was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the stored procedure specified by the protected variable _cspFind</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool Update()
		{
			return Update(BindingFlags.Public | BindingFlags.Instance);
		}

		/// <summary>
		/// Takes the properties of the associated object and updates an entry into the corresponding
		/// table in the database using the specified Binding Flags and using the Database
		/// Server's System User or the current Environment.UserName (depending on the property's DefaultType
		/// attribute) to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="flags">Specifies the System.Reflection.BindingFlags to use when retrieving
		/// the objects properties to use to update the database.</param>
		/// <returns>Boolean indicating whether or not this updates was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the stored procedure specified by the protected variable _cspFind</returns>
		protected virtual bool Update(BindingFlags flags)
		{
			return Update(string.Empty, flags);
		}

		/// <summary>
		/// Takes the properties of the associated object and updates an entry into the corresponding
		/// table in the database using the Public and Instance Binding Flags and the specified user 
		/// to update fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="updateUser">Username to use to update fields associated with the properites
		/// property DefaultUser attribute is true</param>
		/// <returns>Boolean indicating whether or not this updates was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the stored procedure specified by the protected variable _cspFind</returns>
		protected virtual bool Update(string updateUser)
		{
			return Update(updateUser, BindingFlags.Public | BindingFlags.Instance);
		}

		/// <summary>
		/// Takes the properties of the associated object and updates an entry into the corresponding
		/// table in the database using the specified Binding Flags and the specified user to update 
		/// fields whose property DefaultUser attribute is true.
		/// </summary>
		/// <param name="updateUser">Username to use to update fields associated with the properites
		/// property DefaultUser attribute is true.</param>
		/// <param name="flags">Specifies the System.Reflection.BindingFlags to use when retrieving
		/// the objects properties to use to update the database.</param>
		/// <returns>Boolean indicating whether or not this updates was successful.  Also, it sets the value(s) of
		/// any output parameters specified in the stored procedure specified by the protected variable _cspFind</returns>
		protected virtual bool Update(string updateUser, BindingFlags flags)
		{
			bool returnValue = false;

			if (_Initialized()) {
				try {
					// Update the default values dictionary
					if (_defaultValues.ContainsKey("updateUser"))
						_defaultValues["updateUser"] = updateUser;
					else
						_defaultValues.Add("updateUser", updateUser);

					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						// Open the connection to the database
						connection.Open();

						// Create the command object
						SqlCommand command = new SqlCommand(_cspUpdate, connection)
						{
							CommandType = CommandType.StoredProcedure,
							CommandTimeout = _commandTimeout
						};

						// Retrieve parameter information from the stored procedure in the SqlCommand and populate the Parameters collection
						SqlCommandBuilder.DeriveParameters(command);

						// Get all properties for the current type
						PropertyInfo[] properties = this.GetType().GetProperties(flags);

						foreach (PropertyInfo property in properties) {
							// Process only if property has the ColumnProperty attribute
							if (property.IsDefined(typeof(ColumnProperty), true)) {
								ColumnProperty columnProperty = (ColumnProperty)property.GetCustomAttributes(typeof(ColumnProperty), true)[0];

								// Get the current value and name of the property
								object value = property.GetValue(this, null);
								string parameterName = string.Format("@p{0}", columnProperty.ColumnName);

								if (!columnProperty.AddOnly && !columnProperty.DeleteOnly) {
									UpdateDefaultValue(ref value, columnProperty);
								}

								// Set the value of the parameter
								if (command.Parameters.Contains(parameterName))
									command.Parameters[parameterName].Value = value;
							}
						}

						// Execute the query
						int rowsAffected = command.ExecuteNonQuery();

						// Get the return value(s)
						if (rowsAffected >= 1) {
							returnValue = true;

							foreach (SqlParameter parameter in command.Parameters) {
								// Update the properties for the current type with the returned values
								if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput) {
									string columnName = parameter.ParameterName.Replace("@p", string.Empty);
									object columnValue = parameter.Value;
									PropertyInfo property = this.GetType().GetProperty(columnName);

									// Update the value if writable (has a set accessor)
									if (property != null && property.CanWrite) {
										if (!columnValue.Equals(DBNull.Value) || _routines.IsNullable(property.GetType()))
											this.GetType().GetProperty(columnName).SetValue(this, columnValue, null);
									}
								}
							}
						}

						// Close the connection to the database
						connection.Close();
					}
				}
				catch (Exception ex) {
					throw ex;
				}

				return returnValue;
			}
			else
				throw new Exception("Data class is not initialized");
		}

		/// <summary>
		/// Update the value of a column based upon the ColumnProperty attributes and the current defaultValues Dictionary.
		/// This base data object assumes that the first parameterValue is the updateUser.  Any classes derived from
		/// this can extend this list to include other values needed to do additional updating of default values.
		/// </summary>
		/// <param name="value">The value that is being updated</param>
		/// <param name="columnProperty">The ColumnProperty attributes associated with this value</param>
		protected virtual void UpdateDefaultValue(ref object value, object columnProperty)
		{
			string updateUser = _defaultValues.ContainsKey("updateUser") ? _defaultValues["updateUser"].ToString() : string.Empty;

			ColumnProperty localColumnProperty = (ColumnProperty)columnProperty;
			if (localColumnProperty.DefaultType == ColumnProperty.DefaultTypes.Database) {
				if (localColumnProperty.DefaultUser) {
					if (updateUser != null && updateUser.Trim() == string.Empty)
						value = null;
					else
						value = updateUser;
				}
				else if (localColumnProperty.DefaultDate)
					value = null;
			}
			else if (localColumnProperty.DefaultType == ColumnProperty.DefaultTypes.System) {
				if (localColumnProperty.DefaultUser) {
					if (updateUser != null && updateUser.Trim() == string.Empty)
						value = _routines.GetCurrentUser();
					else
						value = updateUser;
				}
				else if (localColumnProperty.DefaultDate)
					value = DateTime.Now;
			}
		}

		#endregion Protected

		#region Private

		private void _AddSQLParameters(KeyValuePair<string, object> kvp, SqlCommand command)
		{
			try {
				string value = string.Format("@p{0}", kvp.Key);

				// Set the value of the parameter
				if (command.Parameters.Contains(value)) {
					if (command.Parameters[value].SqlDbType == SqlDbType.Structured) {
						Type type = kvp.Value.GetType();

						if (type == typeof(List<long>)) {
							List<long> list = (List<long>)kvp.Value;

							DataTable parameterTable = new DataTable();
							parameterTable.Columns.Add("column_0", typeof(string));

							foreach (long item in list) {
								DataRow dataRow = parameterTable.NewRow();
								dataRow["column_0"] = item;

								parameterTable.Rows.Add(dataRow);
							}

							command.Parameters[value].Value = parameterTable;
							command.Parameters[value].TypeName = string.Empty;
						}
						else if (type == typeof(List<string>)) {
							List<string> list = (List<string>)kvp.Value;

							DataTable parameterTable = new DataTable();
							parameterTable.Columns.Add("column_0", typeof(string));

							foreach (string item in list) {
								DataRow dataRow = parameterTable.NewRow();
								dataRow["column_0"] = item;

								parameterTable.Rows.Add(dataRow);
							}

							command.Parameters[value].Value = parameterTable;
							command.Parameters[value].TypeName = string.Empty;
						}
						else if (type == typeof(List<object>)) {
							List<object> list = (List<object>)kvp.Value;

							DataTable parameterTable = new DataTable();

							// Get properties from first item in the list
							object firstitem = list.First();
							Type itemtype = firstitem.GetType();
							PropertyInfo[] propertyInfos = itemtype.GetProperties();

							// Add columns to the table 
							foreach (PropertyInfo propertyInfo in propertyInfos)
								parameterTable.Columns.Add(propertyInfo.Name, typeof(string));

							// Add rows to the table
							foreach (object item in list) {
								DataRow dataRow = parameterTable.NewRow();

								foreach (PropertyInfo propertyInfo in propertyInfos)
									dataRow[propertyInfo.Name] = propertyInfo.GetValue(item);

								parameterTable.Rows.Add(dataRow);
							}

							command.Parameters[value].Value = parameterTable;
							command.Parameters[value].TypeName = string.Empty;
						}
					}
					else
						command.Parameters[value].Value = kvp.Value;
				}
			}
			catch {
				throw;
			}
		}

		/// <summary>
		/// Creates the connection string to the database based on the properties of the object
		/// </summary>
		/// <returns></returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private string _ConnectString()
		{
			if (!_Initialized())
				return string.Empty;
			else {
				string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};{4}",
					_dbServer, _dbDatabase, _dbUsername, _dbPassword, (_dbUsername == string.Empty && _dbPassword == string.Empty) ? "Trusted_Connection=Yes" : string.Empty);

				return connectionString;
			}
		}

		/// <summary>
		/// Determines if the object has enough information to create a connection to the database
		/// </summary>
		/// <returns>Boolean indicating if the object has enough information to create a connection to the 
		/// database </returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private bool _Initialized()
		{
			if (!string.IsNullOrEmpty(_dbServer) && !string.IsNullOrEmpty(_dbDatabase))
				return true;
			else
				return false;
		}

		#endregion Private

		#endregion Methods
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BaseDataCollection<T> where T : BaseData, new()
	{
		#region Enums

		#endregion Enums

		#region Delegates

		#endregion Delegates

		#region Variables

		private int _commandTimeout = 120;
		private string _dbServer = string.Empty;
		private string _dbDatabase = string.Empty;
		private string _dbUsername = string.Empty;
		private string _dbPassword = string.Empty;
		private List<T> _items;
		private Routines _routines = new Routines();

		/// <summary>
		/// The name of the stored procedure used to perform a standard Find of the colleciton of objects from the database
		/// </summary>
		protected string _cspFind = string.Empty;

		/// <summary>
		/// The list of input parameters needed when performing a Find of the collection objects from the database
		/// </summary>
		protected ArrayList _cspFindParameterNames = new ArrayList();

		#endregion Variables

		#region Properties

		/// <summary>
		/// Define the indexer property for this collection
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public T this[int index]
		{
			get { return _items[index]; }
		}

		/// <summary>
		/// Gets/Sets the wait time (in seconds) before terminating the attempt to execute a command and generating an error.
		/// Default value: 120 seconds
		/// </summary>
		public int CommandTimeout
		{
			get { return _commandTimeout; }
			set { _commandTimeout = value; }
		}

		/// <summary>
		/// Gets/Sets the Name/IP Address of database server
		/// </summary>
		public string DBServer
		{
			get { return _dbServer; }
			set { _dbServer = value; }
		}

		/// <summary>
		/// Gets/Sets Name of the database
		/// </summary>
		public string DBDatabase
		{
			get { return _dbDatabase; }
			set { _dbDatabase = value; }
		}

		/// <summary>
		/// Gets/Sets Username used to connect (leave blank for Trusted Connections)
		/// </summary>
		public string DBUsername
		{
			get { return _dbUsername; }
			set { _dbUsername = value; }
		}

		/// <summary>
		/// Gets/Sets Password used to connect (leave blank for Trusted Connections)
		/// </summary>
		public string DBPassword
		{
			get { return _dbPassword; }
			set { _dbPassword = value; }
		}

		/// <summary>
		/// Provides access to the list of items contained in the collection
		/// </summary>
		public List<T> Items
		{
			get { return _items; }
		}

		#endregion Properties

		#region Constructors

		/// <summary>
		/// Create an instance of the object without the Data properties set
		/// </summary>
		public BaseDataCollection()
		{
			_items = new List<T>();
			SetupDatabase();
		}

		/// <summary>
		/// Creates an instance of the object using the specified arguments to create the
		/// data properties necessary to make connection to a data source.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		public BaseDataCollection(string dbServer, string dbDatabase, string dbUsername, string dbPassword)
		{
			_items = new List<T>();
			SetupDatabase();
			SetData(dbServer, dbDatabase, dbUsername, dbPassword);
		}

		/// <summary>
		/// Creates an instance of the object using the specified arguments to create the
		/// data properties necessary to make connection to a data source.
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="commandTimeout">The value of the timeout (in seconds) to use for executing commands</param>
		public BaseDataCollection(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout)
		{
			_items = new List<T>();
			SetupDatabase();
			SetData(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout);
		}

		#endregion Constructors

		#region Interface

		/// <summary>
		/// Define the IEnumerable Interface when using for each for this collection of objects
		/// </summary>
		/// <typeparam name="DataType"></typeparam>
		public interface IEnumerable<DataType>
		{
			/// <summary>
			/// Use the GetEnumerator for this enumeration
			/// </summary>
			/// <returns></returns>
			IEnumerator<DataType> GetEnumerator();
		}

		/// <summary>
		///  Define the IEnumerator Interface when using for each for this collection of objects
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			foreach (T obj in _items) {
				yield return obj;
			}
		}

		/// <summary>
		///  Define the ICollection Interface when using for each for this collection of objects
		/// </summary>
		public ICollection<T> List
		{
			get { return _items; }
		}

		#endregion Interface

		#region Methods

		#region Public

		/// <summary>
		/// Add an item to the list in the collection
		/// </summary>
		/// <param name="item">Item to add</param>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public virtual void Add(T item)
		{
			_items.Add(item);
		}

		/// <summary>
		/// Gives a count of the number of items in the current collection
		/// </summary>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public virtual int Count
		{
			get { return _items.Count; }
		}

		/// <summary>
		/// Removes the specified item from the collection
		/// </summary>
		/// <param name="item">Item to be removed</param>
		/// <returns>Flag indicating whter or not this was successful</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public virtual bool Remove(T item)
		{
			return _items.Remove(item);
		}

		/// <summary>
		/// Removes the items at the specified location from the collection
		/// </summary>
		/// <param name="index">Location of object within collection to be removed</param>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public virtual void RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}

		/// <summary>
		/// Sets the data properties necessary to make a connection to a data source
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public void SetData(string dbServer, string dbDatabase, string dbUsername, string dbPassword)
		{
			SetData(dbServer, dbDatabase, dbUsername, dbPassword, 120);
		}

		/// <summary>
		/// Sets the data properties necessary to make a connection to a data source
		/// </summary>
		/// <param name="dbServer">Name/IP Address of database server</param>
		/// <param name="dbDatabase">Name of the database</param>
		/// <param name="dbUsername">Username used to connect (leave blank for Trusted Connections)</param>
		/// <param name="dbPassword">Password used to connect (leave blank for Trusted Connections)</param>
		/// <param name="commandTimeout">the wait time (in seconds) before terminating the attempt to execute a command and generating an error.  (Default: 120 seconds)</param>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		public void SetData(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout)
		{
			_dbServer = dbServer;
			_dbDatabase = dbDatabase;
			_dbUsername = dbUsername;
			_dbPassword = dbPassword;
			_commandTimeout = commandTimeout;
		}

		#endregion Public

		#region Protected

		/// <summary>
		/// Finds the data in the database that is assoicated with collection of objects
		/// found using the "Find" stored procedure along with the specified parameter values
		/// and populates the object with the information
		/// </summary>
		/// <param name="parameterValues">values to be assigned to the parameters of the "Find" stored 
		/// procedure</param>
		/// <returns>Flag indicating whether or not this was successful</returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		protected virtual bool Find(params object[] parameterValues)
		{
			bool returnValue = false;
			_items = new List<T>();

			if (_Initialized()) {
				try {
					using (SqlConnection connection = new SqlConnection(_ConnectString())) {
						//Open the connection
						connection.Open();

						//Create the command object
						SqlCommand command = new SqlCommand(_cspFind, connection);
						command.CommandType = CommandType.StoredProcedure;
						command.CommandTimeout = _commandTimeout;

						//Add the command's parameters
						for (int i = 0; i < _cspFindParameterNames.Count; i++) {
							command.Parameters.Add(_routines.CreateParameter(String.Format("@p{0}", _cspFindParameterNames[i].ToString()), parameterValues[i], parameterValues[i].GetType()));
						}

						//Execute the query
						SqlDataReader dr = command.ExecuteReader();

						//Begin processing each returned row                        
						while (dr.Read()) {
							//T obj = System.Activator.CreateInstance<T>();

							T obj = new T();
							obj.SetData(_dbServer, _dbDatabase, _dbUsername, _dbPassword);

							//Set the properties of this object
							for (int i = 0; i < dr.FieldCount; i++) {
								string columnName = dr.GetName(i);
								object columnValue = dr.GetValue(i);
								PropertyInfo property = typeof(T).GetProperty(columnName);
								if (property != null && property.CanWrite) {
									if (!columnValue.Equals(System.DBNull.Value) || _routines.IsNullable(property.GetType())) {
										typeof(T).GetProperty(columnName).SetValue(obj, dr.GetValue(i), null);
									}
								}
							}

							this.Add(obj);
						}

						//Close the connection
						connection.Close();

						//Set the return value
						returnValue = true;
					}

				}
				catch (Exception ex) {

					throw ex;
				}

				return returnValue;
			}
			else
				throw new Exception("Data class is not initialized");
		}

		/// <summary>
		/// This method is used to set up the stored procedures and their parameters for performing the basic
		/// CRUD operations.
		/// </summary>
		protected virtual void SetupDatabase()
		{
		}

		#endregion Protected

		#region Private

		/// <summary>
		/// Creates the connection string to the database based on the properties of the object
		/// </summary>
		/// <returns></returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private string _ConnectString()
		{
			if (!_Initialized())
				return string.Empty;
			else {
				string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};{4}",
					_dbServer, _dbDatabase, _dbUsername, _dbPassword, (_dbUsername == string.Empty && _dbPassword == string.Empty) ? "Trusted_Connection=Yes" : string.Empty);

				return connectionString;
			}
		}

		/// <summary>
		/// Determines if the object has enough information to create a connection to the database
		/// </summary>
		/// <returns>Boolean indicating if the object has enough information to create a connection to the 
		/// database </returns>
		/// <history>
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// Date		Author					Description
		/// ==========	====================	=========================================================
		/// 02/09/2023	Troy Edmonson			Initial Creation
		///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/// </history>
		private bool _Initialized()
		{
			if (!string.IsNullOrEmpty(_dbServer) && !string.IsNullOrEmpty(_dbDatabase))
				return true;
			else
				return false;
		}

		#endregion Private

		#endregion Methods
	}

	/// <summary>
	/// This attribute class will be used to assoicate a property of an object with a column in
	/// the database.
	/// </summary>
	/// <history>
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// Date		Author					Description
	/// ==========	====================	=========================================================
	/// 02/09/2023	Troy Edmonson			Initial Creation
	///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// </history>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class ColumnProperty : Attribute
	{
		#region Enums

		/// <summary>
		/// Define the types of Defaults that can be defined for Dates, Users, and GUIDs
		/// </summary>
		public enum DefaultTypes
		{
			/// <summary>
			/// Get default information from the Database Server
			/// </summary>
			Database,
			/// <summary>
			/// Get default information from the Operating System
			/// </summary>
			System
		}

		#endregion Enums

		#region Delegates

		#endregion Delegates

		#region Variables

		private bool _addOnly = false;
		private string _columnName = string.Empty;
		private bool _defaultApp = false;
		private bool _defaultDate = false;
		private bool _defaultMachine = false;
		private DefaultTypes _defaultType = DefaultTypes.Database;
		private bool _defaultUser = false;
		private bool _deleteOnly = false;

		#endregion Variables

		#region Properties

		/// <summary>
		/// Indicates whether value should be updated only during the Add operation.
		/// Default: false
		/// </summary>
		public bool AddOnly
		{
			get { return _addOnly; }
			set { _addOnly = value; }
		}

		/// <summary>
		/// The name of the column in the database that this property is bound to.
		/// </summary>
		public string ColumnName
		{
			get { return _columnName; }
			set { _columnName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool DefaultApp
		{
			get { return _defaultApp; }
			set { _defaultApp = value; }
		}

		/// <summary>
		/// Indicates whether value should be updated with current local date/time during Add/Update/Delete operations.
		/// Default:  false
		/// </summary>
		public bool DefaultDate
		{
			get { return _defaultDate; }
			set { _defaultDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool DefaultMachine
		{
			get { return _defaultMachine; }
			set { _defaultMachine = value; }
		}

		/// <summary>
		/// Indicates the where to get the default values for those properties with DefaultDate and/or
		/// DefaultUser set to true and the value is not otherwise supplied
		/// Default:  false
		/// </summary>
		public DefaultTypes DefaultType
		{
			get { return _defaultType; }
			set { _defaultType = value; }
		}

		/// <summary>
		/// Indicates whether value should be update with the current user (or updateUser parameter
		/// of the operation method) during Add/Update/Delete operations
		/// Default: false
		/// </summary>
		public bool DefaultUser
		{
			get { return _defaultUser; }
			set { _defaultUser = value; }
		}

		/// <summary>
		/// Indicates whether value should be updated only during the Delete operation.
		/// Default: false
		/// </summary>
		public bool DeleteOnly
		{
			get { return _deleteOnly; }
			set { _deleteOnly = value; }
		}

		#endregion Properties

		#region Constructors

		/// <summary>
		/// Creates and empty ColumnProperty object
		/// </summary>
		public ColumnProperty() : this(string.Empty) { }

		/// <summary>
		/// Creates a ColumnProperty object for a specific column with the default values set for the other attributes.
		/// </summary>
		/// <param name="columnName">Name of column to create the object for</param>
		public ColumnProperty(string columnName) : this(columnName, DefaultTypes.Database, false, false, false, false, false, false) { }

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
		/// <param name="defaultApp">Indicates that the column should be updated with the current Assembly name (or updateApp parameter
		/// of the operation Method)</param>
		/// <param name="defaultMachine">Indicates that the column should be updated with the default (Database or System) machine name</param>
		public ColumnProperty(string columnName, DefaultTypes defaultType, bool addOnly, bool deleteOnly, bool defaultUser, bool defaultDate, bool defaultApp, bool defaultMachine)
		{
			_columnName = columnName;
			_defaultType = defaultType;
			_addOnly = addOnly;
			_deleteOnly = deleteOnly;
			_defaultUser = defaultUser;
			_defaultDate = defaultDate;
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
