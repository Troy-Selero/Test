using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DataTierGenerator
{
	/// <summary>
	/// Provides utility functions for the data tier generator.
	/// </summary>
	internal sealed class Utility
	{
		private Utility() { }

		/// <summary>
		/// Creates the specified sub-directory, if it doesn't exist.
		/// </summary>
		/// <param name="name">The name of the sub-directory to be created.</param>
		internal static void CreateSubDirectory(string name)
		{
			if (Directory.Exists(name) == false)
				Directory.CreateDirectory(name);
		}

		/// <summary>
		/// Creates the specified sub-directory, if it doesn't exist.
		/// </summary>
		/// <param name="name">The name of the sub-directory to be created.</param>
		/// <param name="deleteIfExists">Indicates if the directory should be deleted if it exists.</param>
		internal static void CreateSubDirectory(string name, bool deleteIfExists)
		{
			if (Directory.Exists(name))
				Directory.Delete(name, true);

			Directory.CreateDirectory(name);
		}

		/// <summary>
		/// Returns the query that should be used for retrieving the list of tables for the specified database.
		/// </summary>
		/// <param name="databaseName">The database to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
		internal static string GetTableQuery(string databaseName)
		{
			StringBuilder query = new StringBuilder();
			query.AppendLine("SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE");
			query.AppendLine("FROM INFORMATION_SCHEMA.TABLES");
			query.AppendLine(String.Format("WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME !='dtProperties' AND TABLE_CATALOG='{0}'", databaseName));
			query.AppendLine("ORDER BY TABLE_NAME");

			return query.ToString();
		}

		/// <summary>
		/// Returns the query that should be used for retrieving the list of columns for the specified table.
		/// </summary>
		/// <param name="databaseName">The table to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of columns for the specified table.</returns>
		internal static string GetColumnQuery(string tableName)
		{
			StringBuilder query = new StringBuilder();
			query.AppendLine("SELECT");
			query.AppendLine(String.Format("\tINFORMATION_SCHEMA.COLUMNS.*,"));
			query.AppendLine(String.Format("\tCOL_LENGTH('{0}', INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS COLUMN_LENGTH,", tableName));
			query.AppendLine(String.Format("\tCOLUMNPROPERTY(OBJECT_ID('{0}'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed') AS IS_COMPUTED,", tableName));
			query.AppendLine(String.Format("\tCOLUMNPROPERTY(OBJECT_ID('{0}'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IS_IDENTITY,", tableName));
			query.AppendLine(String.Format("\tCOLUMNPROPERTY(OBJECT_ID('{0}'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IS_ROWGUIDCOL", tableName));
			query.AppendLine("FROM");
			query.AppendLine("INFORMATION_SCHEMA.COLUMNS");
			query.AppendLine("WHERE");
			query.AppendLine(String.Format("\tINFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '{0}'", tableName));

			return query.ToString();
		}

		/// <summary>
		/// Returns the query that should be used for retrieving the list of extended properties for the specified table.
		/// </summary>
		/// <param name="databaseName">The table to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of columns for the specified table.</returns>
		internal static string GetExtendedPropertiesQuery(string tableName)
		{
			StringBuilder query = new StringBuilder();
			query.AppendLine("SELECT");
			query.AppendLine("\to.name as TABLE_NAME");
			query.AppendLine("\t,ep.name as PROPERTY_NAME");
			query.AppendLine("\t,ep.value as PROPERTY_VALUE");
			query.AppendLine("\tFROM sys.objects o ");
			query.AppendLine("\tINNER JOIN sys.extended_properties ep ON ep.major_id = o.object_id");
			query.AppendLine("\tWHERE type = 'U'");
			query.AppendLine(String.Format("\tAND o.name = '{0}'", tableName));

			return query.ToString();
		}

		/// <summary>
		/// Returns the query that should be used for retrieving the list of extended properties for the specified table.
		/// </summary>
		/// <param name="tableName">The table to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of columns for the specified table.</returns>
		internal static string GetColumnExtendedPropertiesQuery(string tableName, string columnName)
		{
			StringBuilder query = new StringBuilder();
			query.AppendLine("SELECT");
			query.AppendLine("\to.name as TABLE_NAME");
			query.AppendLine("\t, c.name as COLUMN_NAME");
			query.AppendLine("\t, ep.name as COLUMN_PROPERTY_NAME");
			query.AppendLine("\t, ep.value as COLUMN_PROPERTY_VALUE");
			query.AppendLine("\tFROM sys.objects o");
			query.AppendLine("\tINNER JOIN sys.columns c on c.object_id = o.object_id");
			query.AppendLine("\tINNER JOIN sys.extended_properties ep ON ep.major_id = o.object_id and ep.minor_id = c.column_id");
			query.AppendLine("\tWHERE type = 'U'");
			query.AppendLine(String.Format("\tAND o.name = '{0}'", tableName));
			query.AppendLine(String.Format("\tAND c.name = '{0}'", columnName));

			return query.ToString();
		}

		/// <summary>
		/// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
		/// </summary>
		/// <param name="name">Name of the resource to retrieve.</param>
		/// <param name="databaseName">The name of the database to be used.</param>
		/// <param name="grantLoginName">The name of the user to be used.</param>
		/// <returns>The queries that should be used to create the specified database login.</returns>
		internal static string GetUserQueries(string databaseName, string grantLoginName)
		{
			//USE #DatabaseName#

			///******************************************************************************************
			//Create the #UserName# login.
			//******************************************************************************************/
			//IF NOT EXISTS(SELECT * FROM master..syslogins WHERE name = '#UserName#')
			//    EXEC sp_addlogin '#UserName#', '', '#DatabaseName#'
			//GO

			///******************************************************************************************
			//Grant the #UserName# login access to the #DatabaseName# database.
			//******************************************************************************************/
			//IF NOT EXISTS (SELECT * FROM [dbo].sysusers WHERE NAME = N'#UserName#' AND uid < 16382)
			//    EXEC sp_grantdbaccess N'#UserName#', N'#UserName#'
			//GO

			StringBuilder query = new StringBuilder();
			query.AppendLine(String.Format("USE {0}", databaseName));
			query.AppendLine(string.Empty);
			query.AppendLine("/******************************************************************************************");
			query.AppendLine("Create the #UserName# login.");
			query.AppendLine("******************************************************************************************/");
			query.AppendLine(String.Format("IF NOT EXISTS(SELECT * FROM master..syslogins WHERE name = '{0}')", grantLoginName));
			query.AppendLine(String.Format("\tEXEC sp_addlogin '{0}', '', '{1}'", grantLoginName, databaseName));
			query.AppendLine("GO");
			query.AppendLine(string.Empty);
			query.AppendLine("/******************************************************************************************");
			query.AppendLine(String.Format("Grant the {0} login access to the #DatabaseName# database.", grantLoginName));
			query.AppendLine("******************************************************************************************/");
			query.AppendLine(String.Format("IF NOT EXISTS (SELECT * FROM [dbo].sysusers WHERE NAME = N'{0}' AND uid < 16382)", grantLoginName));
			query.AppendLine(String.Format("\tEXEC sp_grantdbaccess N'{0}', N'{0}'", grantLoginName));
			query.AppendLine("GO");

			return query.ToString();
		}

		/// <summary>
		/// Returns the query that should be used for retrieving the list of tables for the specified database.
		/// </summary>
		/// <param name="databaseName">The database to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
		internal static string Get(string databaseName)
		{
			return GetTableQuery(databaseName);
		}

		/// <summary>
		/// Retrieves the foreign key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="tableName">Name of the table that foreign keys should be checked for.</param>
		/// <returns>DataReader containing the foreign key information for the specified table.</returns>
		internal static DataTable GetForeignKeyList(SqlConnection connection, string tableName)
		{
			SqlParameter parameter;

			using (SqlCommand command = new SqlCommand("sp_fkeys", connection)) {
				command.CommandType = CommandType.StoredProcedure;

				parameter = new SqlParameter("@pktable_name", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_name", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@pktable_owner", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_owner", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@pktable_qualifier", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_qualifier", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@fktable_name", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_name", DataRowVersion.Current, tableName);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@fktable_owner", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_owner", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@fktable_qualifier", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_qualifier", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);

				SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				return dataTable;
			}
		}

		/// <summary>
		/// Retrieves the primary key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="tableName">Name of the table that primary keys should be checked for.</param>
		/// <returns>DataReader containing the primary key information for the specified table.</returns>
		internal static DataTable GetPrimaryKeyList(SqlConnection connection, string tableName)
		{
			SqlParameter parameter;

			using (SqlCommand command = new SqlCommand("sp_pkeys", connection)) {
				command.CommandType = CommandType.StoredProcedure;

				parameter = new SqlParameter("@table_name", SqlDbType.NVarChar, 128, ParameterDirection.Input, false, 0, 0, "table_name", DataRowVersion.Current, tableName);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@table_owner", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "table_owner", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);
				parameter = new SqlParameter("@table_qualifier", SqlDbType.NVarChar, 128, ParameterDirection.Input, true, 0, 0, "table_qualifier", DataRowVersion.Current, DBNull.Value);
				command.Parameters.Add(parameter);

				SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				return dataTable;
			}
		}

		/// <summary>
		/// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
		/// </summary>
		/// <param name="column">Object that stores the information for the column the parameter represents.</param>
		/// <param name="checkForOutputParameter">Indicates if the created parameter should be checked to see if it should be created as an output parameter.</param>
		/// <param name="setInProcedure">Is this value set/updated in stored procedure</param>
		/// <returns>String containing parameter information of the specified column for a stored procedure.</returns>
		internal static string CreateParameterString(Column column, bool checkForOutputParameter, bool setInProcedure)
		{
			return CreateParameterString(column, checkForOutputParameter, setInProcedure, false);
		}

		/// <summary>
		/// Returns parameter string
		/// </summary>
		/// <param name="column"></param>
		/// <param name="checkForOutputParameter">Appends OUTPUT if true, and the column is an Identity.</param>
		/// <param name="setInProcedure"></param>
		/// <param name="requireParameter"></param>
		/// <returns></returns>
		internal static string CreateParameterString(Column column, bool checkForOutputParameter, bool setInProcedure, bool requireParameter)
		{
			string parameter;

			switch (column.Type.ToLower()) {
				case "binary":
					parameter = "@p" + column.Name + " " + column.Type + "(" + column.Length + ")";
					break;

				case "bigint":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "bit":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "char":
					parameter = "@p" + column.Name + " " + column.Type + "(" + column.Length + ")";
					break;

				case "date":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "datetime":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "decimal":
					if (column.Scale.Length == 0)
						parameter = "@p" + column.Name + " " + column.Type + "(" + column.Precision + ")";
					else
						parameter = "@p" + column.Name + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
					break;

				case "float":
					parameter = "@p" + column.Name + " " + column.Type + "(" + column.Precision + ")";
					break;

				case "image":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "int":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "money":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "nchar":
					parameter = "@p" + column.Name + " " + column.Type + "(" + column.Length + ")";
					break;

				case "ntext":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "nvarchar":
					parameter = "@p" + column.Name + " " + column.Type + "(" + (column.Length == "-1" ? "max" : column.Length) + ")";
					break;

				case "numeric":
					if (column.Scale.Length == 0)
						parameter = "@p" + column.Name + " " + column.Type + "(" + column.Precision + ")";
					else
						parameter = "@p" + column.Name + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
					break;

				case "real":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "smalldatetime":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "smallint":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "smallmoney":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "sql_variant":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "sysname":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "text":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "timestamp":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "tinyint":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				case "varbinary":
					if (int.Parse(column.Length) == -1)
						parameter = "@p" + column.Name + " " + column.Type + "(MAX)";
					else
						parameter = "@p" + column.Name + " " + column.Type + "(" + column.Length + ")";
					break;

				case "varchar":
					if (int.Parse(column.Length) == -1)
						parameter = "@p" + column.Name + " " + column.Type + "(MAX)";
					else
						parameter = "@p" + column.Name + " " + column.Type + "(" + column.Length + ")";
					break;

				case "uniqueidentifier":
					parameter = "@p" + column.Name + " " + column.Type;
					break;

				default:  // Unknow data type
					throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
			}

			// Return the new parameter string
			if ((column.AllowsNulls && !requireParameter) || setInProcedure)
				parameter += "=NULL";

			if ((checkForOutputParameter && column.IsIdentity) || setInProcedure)
				return parameter + " OUTPUT";
			else
				return parameter;
		}

		/// <summary>
		/// Creates a string for a method parameter representing the specified column.
		/// </summary>
		/// <param name="column">Object that stores the information for the column the parameter represents.</param>
		/// <returns>String containing parameter information of the specified column for a method call.</returns>
		internal static string CreateMethodParameter(Column column, string prefix)
		{
			StringBuilder parameter = new StringBuilder();
			parameter.Append(GetCsType(column));

			if (column.AllowsNulls && column.CharacterMaxLength == null)
				parameter.Append("?");
			parameter.Append(" " + prefix + FormatCamel(column.Name));

			return parameter.ToString();
		}

		/// <summary>
		/// Creates the name of the method to call on a SqlDataReader for the specified column.
		/// </summary>
		/// <param name="column">The column to retrieve data for.</param>
		/// <returns>The name of the method to call on a SqlDataReader for the specified column.</returns>
		internal static string GetCsType(Column column)
		{
			switch (column.Type.ToLower()) {
				case "binary":
					return "byte[]";

				case "bigint":
					return "long";

				case "bit":
					return "bool";

				case "char":
					return "string";

				case "date":
					return "DateTime";

				case "datetime":
					return "DateTime";

				case "decimal":
					return "decimal";

				case "float":
					return "float";

				case "image":
					return "byte[]";

				case "int":
					return "int";

				case "money":
					return "decimal";

				case "nchar":
					return "string";

				case "ntext":
					return "string";

				case "nvarchar":
					return "string";

				case "numeric":
					return "decimal";

				case "real":
					return "decimal";

				case "smalldatetime":
					return "DateTime";

				case "smallint":
					return "short";

				case "smallmoney":
					return "float";

				case "sql_variant":
					return "byte[]";

				case "sysname":
					return "string";

				case "text":
					return "string";

				case "timestamp":
					return "DateTime";

				case "tinyint":
					return "byte";

				case "varbinary":
					return "byte[]";

				case "varchar":
					return "string";

				case "uniqueidentifier":
					return "Guid";

				default:  // Unknow data type
					throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
			}
		}

		/// <summary>
		/// Formats a string in Camel case (the first letter is in lower case).
		/// </summary>
		/// <param name="original">A string to be formatted.</param>
		/// <returns>A string in Camel case.</returns>
		internal static string FormatCamel(string original)
		{
			if (original == original.ToUpper())
				return original.ToLower();
			else if (original.Length > 0)
				return Char.ToLower(original[0]) + original.Substring(1);
			else
				return String.Empty;
		}

		/// <summary>
		/// Formats a string in Pascal case (the first letter is in upper case).
		/// </summary>
		/// <param name="original">A string to be formatted.</param>
		/// <returns>A string in Pascal case.</returns>
		internal static string FormatPascal(string original)
		{
			if (original.ToLower() == "ttl")
				return original.ToUpper();
			else if (original.Length > 0)
				return Char.ToUpper(original[0]) + original.Substring(1);
			else
				return String.Empty;
		}

		/// <summary>
		/// Formats the table name for use as a data transfer object.
		/// </summary>
		/// <param name="tableName">The name of the table to format.</param>
		/// <returns>The table name, formatted for use as a data transfer object.</returns>
		internal static string FormatClassName(string tableName)
		{
			string className;

			if (Char.IsUpper(tableName[0]))
				className = tableName;
			else
				className = FormatPascal(tableName);

			return className;
		}

		/// <summary>
		/// Matches a SQL Server data type to a SqlClient.SqlDbType.
		/// </summary>
		/// <param name="sqlDbType">A string representing a SQL Server data type.</param>
		/// <returns>A string representing a SqlClient.SqlDbType.</returns>
		internal static string GetSqlDbType(string sqlDbType)
		{
			switch (sqlDbType.ToLower()) {
				case "binary":
					return "Binary";
				case "bigint":
					return "BigInt";
				case "bit":
					return "Bit";
				case "char":
					return "Char";
				case "datetime":
					return "DateTime";
				case "decimal":
					return "Decimal";
				case "float":
					return "Float";
				case "image":
					return "Image";
				case "int":
					return "Int";
				case "money":
					return "Money";
				case "nchar":
					return "NChar";
				case "ntext":
					return "NText";
				case "nvarchar":
					return "NVarChar";
				case "numeric":
					return "Decimal";
				case "real":
					return "Real";
				case "smalldatetime":
					return "SmallDateTime";
				case "smallint":
					return "SmallInt";
				case "smallmoney":
					return "SmallMoney";
				case "sql_variant":
					return "Variant";
				case "sysname":
					return "VarChar";
				case "text":
					return "Text";
				case "timestamp":
					return "Timestamp";
				case "tinyint":
					return "TinyInt";
				case "varbinary":
					return "VarBinary";
				case "varchar":
					return "VarChar";
				case "uniqueidentifier":
					return "UniqueIdentifier";
				default:  // Unknow data type
					throw (new Exception("Invalid SQL Server data type specified: " + sqlDbType));
			}
		}

		/// <summary>
		/// Creates a string for a SqlParameter representing the specified column.
		/// </summary>
		/// <param name="column">Object that stores the information for the column the parameter represents.</param>
		/// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
		internal static string CreateSqlParameter(Table table, Column column)
		{
			string className = Utility.FormatClassName(table.Name);
			string variableName = Utility.FormatCamel(className);

			return "new SqlParameter(\"@" + column.Name + "\", " + variableName + "." + FormatPascal(column.Name) + ")";
		}

		internal static string MakePlural(string inputString)
		{

			string outputString = inputString.Trim();
			if (inputString.EndsWith("s") || inputString.EndsWith("S") || inputString.EndsWith("tch"))
				outputString = string.Format("{0}es", inputString);
			else if (inputString.EndsWith("y"))
				outputString = string.Format("{0}ies", inputString.Remove(inputString.Length - 1));
			else
				outputString = string.Format("{0}s", inputString);

			return outputString;
		}

		internal static bool ValueSetInStoredProcedure(Column column, string defaultsType)
		{
			return ValueSetInStoredProcedure(false, column, defaultsType);
		}

		internal static bool ValueSetInStoredProcedure(bool isAdd, Column column, string defaultsType)
		{
			if (defaultsType.ToUpper() == "DATABASE") {
				if (isAdd) {
					if (!column.DeleteOnly && (column.IsDefaultUser || column.IsDefaultDate || column.IsDefaultDateUTC || column.IsDefaultGuid || column.IsDefaultMachine || column.IsDefaultApp))
						return true;
				}
				else if (column.IsDefaultUser || column.IsDefaultDate || column.IsDefaultDateUTC || column.IsDefaultGuid || column.IsDefaultMachine || column.IsDefaultApp) {
					if (column.DeleteOnly)
						return false;
					else
						return true;
				}
			}

			return false;
		}
	}
}
