using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DataTierGenerator
{
	internal delegate void CountUpdate(object sender, CountEventArgs e);

	/// <summary>
	/// Generates C# and SQL code for accessing a database.
	/// </summary>
	internal static class Generator
	{
		public static event CountUpdate DatabaseCounted;
		public static event CountUpdate TableCounted;

		/// <summary>
		/// Generates the SQL and C# code for the specified database.
		/// </summary>
		/// <param name="outputDirectory">The directory where the C# and SQL code should be created.</param>
		/// <param name="connectionString">The connection string to be used to connect the to the database.</param>
		/// <param name="grantLoginName">The SQL Server login name that should be granted execute rights on the generated stored procedures.</param>
		/// <param name="storedProcedurePrefix">The prefix that should be used when creating stored procedures.</param>
		/// <param name="createMultipleFiles">A flag indicating if the generated stored procedures should be created in one file or separate files.</param>
		public static void Generate(string outputDirectory, string connectionString, string grantLoginName, string storedProcedurePrefix, bool createMultipleFiles, bool includeDrop, string author, bool allowDirtyReads, string defaultsType, bool doNotModifyCodeOnly, System.Windows.Forms.ProgressBar progressBar)
		{
			List<Table> tableList = new List<Table>();
			string databaseName;
			string sqlPath;
			string csPath;

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				databaseName = Utility.FormatPascal(connection.Database);
				sqlPath = Path.Combine(outputDirectory, "SQL");
				csPath = Path.Combine(outputDirectory, "CS");

				connection.Open();

				// Get list of tables in the database
				DataTable dataTable = new DataTable();
				SqlDataAdapter dataAdapter = new SqlDataAdapter(Utility.GetTableQuery(connection.Database), connection);
				dataAdapter.Fill(dataTable);

				progressBar.Maximum = dataTable.Rows.Count;

				// Process each table
				foreach (DataRow dataRow in dataTable.Rows) {
					Table table = new Table
					{
						Name = (string)dataRow["TABLE_NAME"]
					};

					QueryTable(connection, table);
					tableList.Add(table);

					progressBar.Value = tableList.Count;
				}
			}

			DatabaseCounted(null, new CountEventArgs(tableList.Count));

			// Generate the necessary SQL and C# code for each table
			int count = 0;
			if (tableList.Count > 0) {
				// Create the necessary directories
				Utility.CreateSubDirectory(sqlPath, true);
				Utility.CreateSubDirectory(csPath, true);

				// Create the CRUD stored procedures and data access code for each table
				foreach (Table table in tableList) {
					string nameSpace = string.Empty;
					string rootNameSpace = string.Empty;
					string baseTable = string.Empty;

					// Get table Extended Properties
					if (table.ExtendedProperties.Count > 0) {
						ExtendedProperty property = table.ExtendedProperties.Find(p => p.PropertyName == "DTG_Data_Namespace");
						if (property != null)
							nameSpace = property.PropertyValue;

						property = table.ExtendedProperties.Find(p => p.PropertyName == "DTG_Data_RootNamespace");
						if (property != null)
							rootNameSpace = property.PropertyValue;

						property = table.ExtendedProperties.Find(p => p.PropertyName == "DTG_BaseTable");
						if (property != null)
							baseTable = property.PropertyValue;

						// Create stored procedures
						SqlGenerator.CreateAddStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles, includeDrop, author, defaultsType);
						SqlGenerator.CreateDeleteStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles, includeDrop, author, defaultsType);
						SqlGenerator.CreateFindStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles, includeDrop, author, allowDirtyReads);
						SqlGenerator.CreateFindCollectionStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles, includeDrop, baseTable, author, allowDirtyReads);
						SqlGenerator.CreateUpdateStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles, includeDrop, author, defaultsType);

						// CS Methods
						if (!string.IsNullOrEmpty(nameSpace) && !string.IsNullOrEmpty(rootNameSpace))
							CsGenerator.CreateTableClass(table, nameSpace, storedProcedurePrefix, csPath, true, baseTable, author, defaultsType, doNotModifyCodeOnly);

						count++;
						TableCounted(null, new CountEventArgs(count));
					}
				}
			}
		}

		/// <summary>
		/// To determine if the string represents a "True" value.
		/// </summary>
		/// <param name="value">String being analyzed</param>
		/// <returns>Boolean indicating whether or not the string represents a "True" value</returns>
		private static bool IsStringTrue(string value)
		{
			bool successful = false;

			if (!string.IsNullOrEmpty(value) && value.Trim().Length > 0) {
				if (value.ToUpper().Trim() == "1")
					successful = true;
				else if (value.ToUpper().Trim() == "Y")
					successful = true;
				else if (value.ToUpper().Trim() == "YES")
					successful = true;
				else if (value.ToUpper().Trim() == "TRUE")
					successful = true;
				else if (value.ToUpper().Trim() == "T")
					successful = true;
			}

			return successful;
		}

		/// <summary>
		/// Retrieves the column, primary key, and foreign key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="table">The table instance that information should be retrieved for.</param>
		private static void QueryTable(SqlConnection connection, Table table)
		{
			// Get a list of the columns in the table
			DataTable dataTable = new DataTable();
			SqlDataAdapter dataAdapter = new SqlDataAdapter(Utility.GetColumnQuery(table.Name), connection);
			dataAdapter.Fill(dataTable);

			// Process each column
			foreach (DataRow columnRow in dataTable.Rows) {
				Column column = new Column();
				column.Name = columnRow["COLUMN_NAME"].ToString();
				column.Type = columnRow["DATA_TYPE"].ToString();
				column.Precision = columnRow["NUMERIC_PRECISION"].ToString();
				column.Scale = columnRow["NUMERIC_SCALE"].ToString();

				// Determine the column's length
				if (columnRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
					column.Length = columnRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
				else
					column.Length = columnRow["COLUMN_LENGTH"].ToString();

				// Is the column an Identity column?
				if (columnRow["IS_IDENTITY"].ToString() == "1")
					column.IsIdentity = true;

				// Is columnRow column a computed column?
				if (columnRow["IS_COMPUTED"].ToString() == "1")
					column.IsComputed = true;

				if (columnRow["IS_NULLABLE"].ToString().ToUpper() == "YES")
					column.AllowsNulls = true;

				if (columnRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
					column.CharacterMaxLength = Convert.ToInt32(columnRow["CHARACTER_MAXIMUM_LENGTH"]);
				else
					column.CharacterMaxLength = null;

				// Get column extended properties
				DataTable columnDataTable = new DataTable();
				SqlDataAdapter columnDataAdapter = new SqlDataAdapter(Utility.GetColumnExtendedPropertiesQuery(table.Name, column.Name), connection);
				columnDataAdapter.Fill(columnDataTable);

				if (columnDataTable != null && columnDataTable.Rows != null && columnDataTable.Rows.Count > 0) {
					foreach (DataRow row in columnDataTable.Rows) {
						ExtendedProperty extendedProperty = new ExtendedProperty();

						extendedProperty.PropertyName = row["COLUMN_PROPERTY_NAME"].ToString();
						extendedProperty.PropertyValue = row["COLUMN_PROPERTY_VALUE"].ToString();

						column.ExtendedProperties.Add(extendedProperty);

						if (extendedProperty.PropertyName == "DTG_AddOnly" && IsStringTrue(extendedProperty.PropertyValue))
							column.AddOnly = true;
						if (extendedProperty.PropertyName == "DTG_DeleteOnly" && IsStringTrue(extendedProperty.PropertyValue))
							column.DeleteOnly = true;
						if (extendedProperty.PropertyName == "DTG_DefaultUser" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsDefaultUser = true;
						if (extendedProperty.PropertyName == "DTG_DefaultDate" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsDefaultDate = true;
						if (extendedProperty.PropertyName == "DTG_DefaultDateUTC" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsDefaultDateUTC = true;
						if (extendedProperty.PropertyName == "DTG_DefaultGUID" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsDefaultGuid = true;
						if (extendedProperty.PropertyName == "DTG_Encrypted" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsEncrypted = true;
						if (extendedProperty.PropertyName == "DTG_DefaultApp" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsDefaultApp = true;
						if (extendedProperty.PropertyName == "DTG_DefaultMachine" && IsStringTrue(extendedProperty.PropertyValue))
							column.IsDefaultMachine = true;
					}
				}

				table.Columns.Add(column);
			}

			// Get the list of primary keys
			DataTable primaryKeyTable = Utility.GetPrimaryKeyList(connection, table.Name);
			foreach (DataRow primaryKeyRow in primaryKeyTable.Rows) {
				string primaryKeyName = primaryKeyRow["COLUMN_NAME"].ToString();

				foreach (Column column in table.Columns) {
					if (column.Name == primaryKeyName) {
						table.PrimaryKeys.Add(column);

						break;
					}
				}
			}

			// Get the list of foreign keys
			DataTable foreignKeyTable = Utility.GetForeignKeyList(connection, table.Name);
			foreach (DataRow foreignKeyRow in foreignKeyTable.Rows) {
				string name = foreignKeyRow["FK_NAME"].ToString();
				string columnName = foreignKeyRow["FKCOLUMN_NAME"].ToString();

				if (table.ForeignKeys.ContainsKey(name) == false)
					table.ForeignKeys.Add(name, new List<Column>());

				List<Column> foreignKeys = table.ForeignKeys[name];

				foreach (Column column in table.Columns) {
					if (column.Name == columnName) {
						foreignKeys.Add(column);
						break;
					}
				}
			}

			// Get the list of extended properties
			dataTable = new DataTable();
			dataAdapter = new SqlDataAdapter(Utility.GetExtendedPropertiesQuery(table.Name), connection);
			dataAdapter.Fill(dataTable);

			foreach (DataRow row in dataTable.Rows) {
				ExtendedProperty extendedProperty = new ExtendedProperty();

				extendedProperty.PropertyName = row["PROPERTY_NAME"].ToString();
				extendedProperty.PropertyValue = row["PROPERTY_VALUE"].ToString();

				table.ExtendedProperties.Add(extendedProperty);
			}
		}
	}
}