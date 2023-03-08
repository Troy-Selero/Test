using System;
using System.IO;
using System.Text;

namespace DataTierGenerator
{
	/// <summary>
	/// Generates C# data access and data transfer classes.
	/// </summary>
	internal static class CsGenerator
	{
		private static bool Deleted(Column column)
		{
			if (column.Name == "Deleted")
				return true;
			else
				return false;
		}

		private static bool DeletedApp(Column column)
		{
			if (column.Name == "DeletedApp")
				return true;
			else
				return false;
		}

		private static bool DeletedMachine(Column column)
		{
			if (column.Name == "DeletedMachine")
				return true;
			else
				return false;
		}

		private static bool DeletedDate(Column column)
		{
			if (column.Name == "DeletedDate")
				return true;
			else
				return false;
		}

		private static bool DeletedDateUTC(Column column)
		{
			if (column.Name == "DeletedDateUTC")
				return true;
			else
				return false;
		}

		private static bool DeletedBy(Column column)
		{
			if (column.Name == "DeletedBy")
				return true;
			else
				return false;
		}

		private static bool UpdatedApp(Column column)
		{
			if (column.Name == "UpdatedApp")
				return true;
			else
				return false;
		}

		private static bool UpdatedMachine(Column column)
		{
			if (column.Name == "UpdatedMachine")
				return true;
			else
				return false;
		}

		private static bool UpdatedDate(Column column)
		{
			if (column.Name == "UpdatedDate")
				return true;
			else
				return false;
		}

		private static bool UpdatedDateUTC(Column column)
		{
			if (column.Name == "UpdatedDateUTC")
				return true;
			else
				return false;
		}

		private static bool UpdatedBy(Column column)
		{
			if (column.Name == "UpdatedBy")
				return true;
			else
				return false;
		}

		/// <summary>
		/// Creates a C# data access class for all of the table's stored procedures.
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
		/// <param name="targetNamespace">The namespace that the generated C# classes should contained in.</param>
		/// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
		/// <param name="path">Path where the class should be created.</param>
		internal static void CreateTableClass(Table table, string targetNamespace, string storedProcedurePrefix, string path, bool createCollectionClass, string baseTableName, string author, string defaultsType, bool doNotModifyCodeOnly)
		{
			string className = Utility.FormatClassName(table.Name);

			using (StreamWriter sw = new StreamWriter(Path.Combine(path, className + ".cs"))) {
				// Create the header for the class
				#region Header

				if (!doNotModifyCodeOnly) {
					sw.WriteLine("using Selero.Selena;");
					sw.WriteLine();
					sw.WriteLine("namespace " + targetNamespace);
					sw.WriteLine("{");
					sw.WriteLine("\t/// <summary>");
					sw.WriteLine("\t///");
					sw.WriteLine("\t/// </summary>");
					sw.WriteLine("\t/// <history>");
					sw.WriteLine("\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					sw.WriteLine("\t/// Date\t\tAuthor\t\t\t\t\tDescription");
					sw.WriteLine("\t/// ==========\t=================\t\t=======================================================");
					sw.WriteLine("\t/// {0}\t{1}\t\tInitial Creation", DateTime.Today.ToString("MM/dd/yyyy"), author);
					sw.WriteLine("\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					sw.WriteLine("\t/// </history>");

					sw.WriteLine("\tpublic class " + className + " : BaseData");
					sw.WriteLine("\t{");

					sw.WriteLine("\t\t#region Variables");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#endregion Variables");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#region Properties");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#endregion Properties");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#region Constructors");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#endregion Constructors");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#region Methods");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#region Public");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#endregion Public");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#region Private");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#endregion Private");
					sw.WriteLine(string.Empty);
					sw.WriteLine("\t\t#endregion Methods");
					sw.WriteLine(string.Empty);
				}

				#endregion Header

				#region Variables/Properties

				// Append the private members
				sw.WriteLine("\t\t#region DTG Code - DO NOT MODIFY");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#region Generated Variables");
				sw.WriteLine(string.Empty);
				foreach (Column column in table.Columns)
					sw.WriteLine("\t\tprivate " + Utility.CreateMethodParameter(column, "_") + ";");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Variables");
				sw.WriteLine(string.Empty);

				// Append the public properties
				sw.WriteLine("\t\t#region Generated Properties");
				sw.WriteLine(string.Empty);

				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = (Column)table.Columns[i];

					string parameter = Utility.CreateMethodParameter(column, "_");
					string type = parameter.Split(' ')[0];
					string name = parameter.Split(' ')[1];

					sw.WriteLine("\t\t/// <summary>");
					sw.WriteLine("\t\t/// Gets/Sets the " + column.Name + " property.");
					sw.WriteLine("\t\t/// </summary>");

					string columnProperty = string.Format("\t\t[ColumnProperty(ColumnName = \"{0}\"", column.Name);

					if (defaultsType.ToUpper() == "DATABASE" && !column.AddOnly && !column.DeleteOnly && !column.IsDefaultUser && !column.IsDefaultDate && !column.IsDefaultDateUTC && !column.IsDefaultGuid && !column.IsDefaultApp && !column.IsDefaultMachine)
						columnProperty += ")]";
					else {
						if (column.AddOnly)
							columnProperty += ", AddOnly = true";

						if (column.DeleteOnly)
							columnProperty += ", DeleteOnly = true";

						if (column.IsDefaultUser)
							columnProperty += ", DefaultUser = true";

						if (column.IsDefaultDate)
							columnProperty += ", DefaultDate = true";

						if (column.IsDefaultDateUTC)
							columnProperty += ", DefaultDateUTC = true";

						if (column.IsDefaultGuid)
							columnProperty += ", DefaultGUID = true";

						if (column.IsEncrypted)
							columnProperty += ", Encrypted = true";

						if (column.IsDefaultApp)
							columnProperty += ", DefaultApp = true";

						if (column.IsDefaultMachine)
							columnProperty += ", DefaultMachine = true";

						columnProperty += ")]";
					}
					sw.WriteLine(columnProperty);
					sw.WriteLine("\t\tpublic " + type + " " + column.Name);
					sw.WriteLine("\t\t{");

					sw.WriteLine("\t\t\tget { return " + name + "; }");
					sw.WriteLine("\t\t\tset { " + name + " = value; }");
					sw.WriteLine("\t\t}");

					if (i < (table.Columns.Count - 1))
						sw.WriteLine(string.Empty);
				}
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Properties");
				sw.WriteLine(string.Empty);

				#endregion Variables/Properties

				#region Constructors

				sw.WriteLine("\t\t#region Generated Constructors");
				sw.WriteLine(string.Empty);

				// Create an explicit public constructor
				sw.WriteLine("\t\t/// <inheritdoc/>");
				sw.WriteLine("\t\tpublic " + className + "() : base() { }");
				sw.WriteLine(string.Empty);

				// Create an explicit public constructor
				sw.WriteLine("\t\t/// <inheritdoc/>");
				sw.WriteLine("\t\tpublic " + className + "(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword) { }");
				sw.WriteLine(string.Empty);

				// Create an explicit public constructor with command timeout
				sw.WriteLine("\t\t/// <inheritdoc/>");
				sw.WriteLine("\t\tpublic " + className + "(string dbServer, string dbDatabase, string dbUsername, string dbPassword, int commandTimeout) : base(dbServer, dbDatabase, dbUsername, dbPassword, commandTimeout) { }");
				sw.WriteLine(string.Empty);

				// Create the "partial" constructor
				int parameterCount = 0;
				sw.WriteLine("\t\t/// <summary>");
				sw.WriteLine("\t\t/// Initializes a new instance of the " + className + " class.");
				sw.WriteLine("\t\t/// </summary>");
				sw.WriteLine("\t\t/// <param name=\"dbServer\">Name/IP Address of database server</param>");
				sw.WriteLine("\t\t/// <param name=\"dbDatabase\">Name of the database</param>");
				sw.WriteLine("\t\t/// <param name=\"dbUsername\">Username used to connect (leave blank for Trusted Connections)</param>");
				sw.WriteLine("\t\t/// <param name=\"dbPassword\">Password used to connect (leave blank for Trusted Connections)</param>");
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = (Column)table.Columns[i];

					if (!column.IsIdentity)
						sw.WriteLine("\t\t/// <param name=\"" + Utility.FormatCamel(column.Name) + "\">" + string.Empty + "</param>");
				}
				sw.Write("\t\tpublic " + className + "(string dbServer, string dbDatabase, string dbUsername, string dbPassword, ");
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = (Column)table.Columns[i];
					if (column.IsIdentity == false) {
						if (parameterCount > 0)
							sw.Write(", ");

						sw.Write(Utility.CreateMethodParameter(column, "_").Replace("_", string.Empty));

						parameterCount++;
					}
				}
				sw.WriteLine(") : base(dbServer, dbDatabase, dbUsername, dbPassword)");
				sw.WriteLine("\t\t{");

				foreach (Column column in table.Columns) {
					if (column.IsIdentity == false) {
						sw.WriteLine("\t\t\t_" + Utility.FormatCamel(column.Name) + " = " + Utility.FormatCamel(column.Name) + ";");
					}
				}
				sw.WriteLine("\t\t}");

				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Constructors");

				#endregion Constructors

				#region Methods

				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#region Generated Methods");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#region Generated Public");
				sw.WriteLine(string.Empty);

				string deleteMethod = CreateDeleteMethod(table, author);
				if (!string.IsNullOrEmpty(deleteMethod)) {
					sw.Write(deleteMethod);
					sw.WriteLine(string.Empty);
				}

				sw.Write(CreateFindMethod(table, author));
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Public");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#region Generated Protected");
				sw.WriteLine(string.Empty);
				sw.Write(CreateSetupDatabaseMethod(table, storedProcedurePrefix, author, defaultsType));
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Protected");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#region Generated Private");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Private");
				sw.WriteLine(string.Empty);
				sw.WriteLine("\t\t#endregion Generated Methods");
				sw.WriteLine(string.Empty);

				#endregion Methods

				sw.WriteLine("\t\t#endregion DTG Code - DO NOT MODIFY");

				if (!doNotModifyCodeOnly)
					sw.WriteLine("\t}");

				// Create collection class
				if (createCollectionClass)
					sw.Write(CreateTableClassCollection(table, className, storedProcedurePrefix, baseTableName, doNotModifyCodeOnly));

				if (!doNotModifyCodeOnly)
					sw.WriteLine("}");
			}
		}

		private static string CreateSetupDatabaseMethod(Table table, string storedProcedurePrefix, string author, string defaultsType)
		{
			StringBuilder returnValue = new StringBuilder();
			string className = Utility.FormatClassName(table.Name);
			string variableName = Utility.FormatCamel(className);

			string identityColumnName = string.Empty;
			foreach (Column column in table.Columns) {
				if (column.IsIdentity) {
					identityColumnName = column.Name;
					break;
				}
			}

			// Append the method header
			returnValue.AppendLine("\t\t/// <summary>");
			returnValue.AppendLine("\t\t/// Sets variables to be used in base data class.");
			returnValue.AppendLine("\t\t/// </summary>");
			returnValue.AppendLine("\t\t/// <history>");
			returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
			returnValue.AppendLine("\t\t/// Date\t\tAuthor\t\t\t\t\tDescription");
			returnValue.AppendLine("\t\t/// ========\t==================\t\t=====================================================");
			returnValue.AppendFormat("\t\t/// {0}\t{1}\t\tInitial Creation", DateTime.Today.ToString("MM/dd/yyyy"), author).AppendLine();
			returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
			returnValue.AppendLine("\t\t/// </history>");
			returnValue.AppendLine("\t\tprotected override void SetupDatabase()");
			returnValue.AppendLine("\t\t{");
			returnValue.AppendLine("\t\t\tbase.SetupDatabase();");
			returnValue.AppendLine(string.Empty);

			// Create Add/Update/Find/Delete stored procedure names
			returnValue.AppendLine("\t\t\t_cspAdd = \"" + storedProcedurePrefix + className + "_Add\";");
			returnValue.AppendLine("\t\t\t_cspUpdate = \"" + storedProcedurePrefix + className + "_Update\";");
			returnValue.AppendLine("\t\t\t_cspFind = \"" + storedProcedurePrefix + className + "_Find\";");
			returnValue.AppendLine("\t\t\t_cspDelete = \"" + storedProcedurePrefix + className + "_Delete\";");
			returnValue.AppendLine("\t\t}");

			return returnValue.ToString();
		}

		private static string CreateFindMethod(Table table, string author)
		{
			// Determine if table contains deleted columns
			Column deletedColumn = table.Columns.Find(Deleted);
			Column deletedDateColumn = table.Columns.Find(DeletedDate);
			Column deletedDateUTCColumn = table.Columns.Find(DeletedDateUTC);
			bool includeDeleted = (deletedColumn != null || deletedDateColumn != null || deletedDateUTCColumn != null);

			string identityColumnName = string.Empty;
			string identityColumnTypeName = string.Empty;

			if (table.PrimaryKeys.Count > 0) {
				identityColumnName = table.PrimaryKeys[0].Name;
				identityColumnTypeName = Utility.CreateMethodParameter(table.PrimaryKeys[0], string.Empty);
			}

			// Append the method header
			StringBuilder returnValue = new StringBuilder();
			returnValue.AppendLine("\t\t/// <summary>");
			returnValue.AppendLine("\t\t/// Returns the record for the specified " + identityColumnName + " from the database.");
			returnValue.AppendLine("\t\t/// </summary>");
			returnValue.AppendLine("\t\t/// <history>");
			returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
			returnValue.AppendLine("\t\t/// Date\t\tAuthor\t\t\t\t\tDescription");
			returnValue.AppendLine("\t\t/// ========\t==================\t\t=====================================================");
			returnValue.AppendFormat("\t\t/// {0}\t{1}\t\tInitial Creation", DateTime.Today.ToString("MM/dd/yyyy"), author).AppendLine();
			returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
			returnValue.AppendLine("\t\t/// </history>");
			returnValue.AppendLine("\t\tpublic bool Find(" + identityColumnTypeName + ")");
			returnValue.AppendLine("\t\t{");

			if (includeDeleted) {
				returnValue.AppendLine("\t\t\treturn Find(" + Utility.FormatCamel(identityColumnName) + ", false);");
				returnValue.AppendLine("\t\t}");

				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t/// <summary>");
				returnValue.AppendLine("\t\t/// Returns the record for the specified " + identityColumnName + " from the database.");
				returnValue.AppendLine("\t\t/// </summary>");
				returnValue.AppendLine("\t\t/// <history>");
				returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
				returnValue.AppendLine("\t\t/// Date\t\tAuthor\t\t\t\t\tDescription");
				returnValue.AppendLine("\t\t/// ========\t==================\t\t=====================================================");
				returnValue.AppendFormat("\t\t/// {0}\t{1}\t\tInitial Creation", DateTime.Today.ToString("MM/dd/yyyy"), author).AppendLine();
				returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
				returnValue.AppendLine("\t\t/// </history>");
				returnValue.AppendLine("\t\tpublic bool Find(" + identityColumnTypeName + ", bool deleted)");
				returnValue.AppendLine("\t\t{");
				returnValue.AppendLine("\t\t\ttry {");
				returnValue.AppendLine("\t\t\t\treturn base.Find(" + Utility.FormatCamel(identityColumnName) + ", deleted);");
				returnValue.AppendLine("\t\t\t}");
				returnValue.AppendLine("\t\t\tcatch (Exception) {");
				returnValue.AppendLine("\t\t\t\tthrow;");
				returnValue.AppendLine("\t\t\t}");
				returnValue.AppendLine("\t\t}");
			}
			else {
				returnValue.AppendLine("\t\t\ttry {");
				returnValue.AppendLine("\t\t\t\treturn base.Find(" + Utility.FormatCamel(identityColumnName) + ");");
				returnValue.AppendLine("\t\t\t}");
				returnValue.AppendLine("\t\t\tcatch (Exception) {");
				returnValue.AppendLine("\t\t\t\tthrow;");
				returnValue.AppendLine("\t\t\t}");
				returnValue.AppendLine("\t\t}");
			}

			return returnValue.ToString();
		}

		private static string CreateTableClassCollection(Table table, string className, string storedProcedurePrefix, string baseTableName, bool doNotModifyCodeOnly)
		{
			// Determine if table contains deleted columns
			Column deletedColumn = table.Columns.Find(Deleted);
			Column deletedDateColumn = table.Columns.Find(DeletedDate);
			Column deletedDateUTCColumn = table.Columns.Find(DeletedDateUTC);
			bool includeDeleted = (deletedColumn != null || deletedDateColumn != null || deletedDateUTCColumn != null);

			StringBuilder returnValue = new StringBuilder();
			bool baseTableUIDExist = false;
			bool baseTableIDExist = false;
			string tableID = "UID";

			for (int i = 0; i < table.Columns.Count; i++) {
				Column column = (Column)table.Columns[i];
				if (column.Name.ToUpper() == string.Format("{0}UID", baseTableName).ToUpper())
					baseTableUIDExist = true;
				if (column.Name.ToUpper() == string.Format("{0}ID", baseTableName).ToUpper())
					baseTableIDExist = true;
			}

			if (!baseTableUIDExist && baseTableIDExist)
				tableID = "ID";

			if (!doNotModifyCodeOnly) {
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\tpublic class " + Utility.MakePlural(className) + " : BaseDataCollection<" + className + ">");
				returnValue.AppendLine("\t{");

				returnValue.AppendLine("\t\t#region Variables");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#endregion Variables");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#region Properties");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#endregion Properties");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#region Constructors");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#endregion Constructors");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#region Methods");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#region Public");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#endregion Public");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#region Private");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#endregion Private");
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("\t\t#endregion Methods");
				returnValue.AppendLine(string.Empty);
			}
			else {
				returnValue.AppendLine(string.Empty);
				returnValue.AppendLine("***************COLLECTION CODE****************");
				returnValue.AppendLine(string.Empty);
			}
			returnValue.AppendLine("\t\t#region DTG Code - DO NOT MODIFY");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#region Generated Variables");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#endregion Generated Variables");
			returnValue.AppendLine(string.Empty);

			returnValue.AppendLine("\t\t#region Generated Properties");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#endregion Generated Properties");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#region Generated Constructors");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\tpublic " + Utility.MakePlural(className) + "()");
			returnValue.AppendLine("\t\t{");
			returnValue.AppendLine("\t\t\t_SetupDatabase();");
			returnValue.AppendLine("\t\t}");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\tpublic " + Utility.MakePlural(className) + "(string dbServer, string dbDatabase, string dbUsername, string dbPassword) : base(dbServer, dbDatabase, dbUsername, dbPassword)");
			returnValue.AppendLine("\t\t{");
			returnValue.AppendLine("\t\t\t_SetupDatabase();");
			returnValue.AppendLine("\t\t}");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#endregion Generated Constructors");
			returnValue.AppendLine(string.Empty);

			returnValue.AppendLine("\t\t#region Generated Methods");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#region Generated Public");
			returnValue.AppendLine(string.Empty);

			if (baseTableName != string.Empty) {
				if (includeDeleted) {
					returnValue.AppendFormat("\t\tpublic bool Find(long {0}{1})", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t{");
					returnValue.AppendFormat("\t\t\treturn Find({0}{1}, false);", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t}");
					returnValue.AppendLine();

					returnValue.AppendFormat("\t\tpublic bool Find(long {0}{1}, bool deleted)", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t{");
					returnValue.AppendFormat("\t\t\treturn base.Find({0}{1}, deleted);", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t}");
				}
				else {
					returnValue.AppendFormat("\t\tpublic bool Find(long {0}{1})", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t{");
					returnValue.AppendFormat("\t\t\treturn Find({0}{1}, false);", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t}");
					returnValue.AppendLine();

					returnValue.AppendFormat("\t\tpublic bool Find(long {0}{1}, bool deleted)", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t{");
					returnValue.AppendFormat("\t\t\treturn base.Find({0}{1}, deleted);", Utility.FormatCamel(baseTableName), tableID);
					returnValue.AppendLine();
					returnValue.AppendLine("\t\t}");
				}
			}
			else {
				returnValue.AppendLine("\t\tpublic bool Find()");
				returnValue.AppendLine("\t\t{");
				returnValue.AppendLine("\t\t\treturn Find(false);");
				returnValue.AppendLine("\t\t}");
				returnValue.AppendLine();

				returnValue.AppendLine("\t\tpublic bool Find(bool deleted)");
				returnValue.AppendLine("\t\t{");
				returnValue.AppendLine("\t\t\treturn base.Find(deleted);");
				returnValue.AppendLine("\t\t}");
			}
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#endregion Generated Public");
			returnValue.AppendLine(string.Empty);

			returnValue.AppendLine("\t\t#region Generated Private");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\tprivate void _SetupDatabase()");
			returnValue.AppendLine("\t\t{");
			returnValue.AppendLine("\t\t\t_cspFind = \"" + storedProcedurePrefix + Utility.MakePlural(className) + "_Find\";");
			returnValue.AppendLine("\t\t\t_cspFindParameterNames.Clear();");
			if (baseTableName != string.Empty) {
				returnValue.AppendLine("\t\t\t_cspFindParameterNames.Add(\"" + baseTableName + tableID + "\");");
				if (includeDeleted) {
					returnValue.AppendLine("\t\t\t_cspFindParameterNames.Add(\"Deleted\");");
				}
			}
			returnValue.AppendLine("\t\t}");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#endregion Generated Private");
			returnValue.AppendLine(string.Empty);
			returnValue.AppendLine("\t\t#endregion Generated Methods");
			returnValue.AppendLine(string.Empty);

			returnValue.AppendLine("\t\t#endregion DTG Code - DO NOT MODIFY");
			if (!doNotModifyCodeOnly) {
				returnValue.AppendLine("\t}");
			}
			return returnValue.ToString();
		}

		private static string CreateDeleteMethod(Table table, string author)
		{
			StringBuilder returnValue = new StringBuilder();

			if (table.Columns.Find(Deleted) != null || table.Columns.Find(DeletedDate) != null || table.Columns.Find(DeletedDateUTC) != null) {
				string identityColumnName = string.Empty;
				string identityColumnTypeName = string.Empty;

				if (table.PrimaryKeys.Count > 0) {
					identityColumnName = table.PrimaryKeys[0].Name;
					identityColumnTypeName = Utility.CreateMethodParameter(table.PrimaryKeys[0], string.Empty);
				}

				// Append the method header
				returnValue.AppendLine("\t\t/// <summary>");
				returnValue.AppendLine("\t\t/// Deletes the record for the specified " + identityColumnName + " from the database.");
				returnValue.AppendLine("\t\t/// </summary>");
				returnValue.AppendLine("\t\t/// <history>");
				returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
				returnValue.AppendLine("\t\t/// Date\t\tAuthor\t\t\t\t\tDescription");
				returnValue.AppendLine("\t\t/// ========\t==================\t\t=====================================================");
				returnValue.AppendFormat("\t\t/// {0}\t{1}\t\tInitial Creation", DateTime.Today.ToString("MM/dd/yyyy"), author).AppendLine();
				returnValue.AppendLine("\t\t///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
				returnValue.AppendLine("\t\t/// </history>");
				returnValue.AppendLine("\t\tpublic bool Delete(string updateUser, string updateApp, " + identityColumnTypeName + ")");
				returnValue.AppendLine("\t\t{");
				returnValue.AppendLine("\t\t\ttry {");
				returnValue.AppendLine("\t\t\t\treturn base.Delete(updateUser, updateApp, " + Utility.FormatCamel(identityColumnName) + ");");
				returnValue.AppendLine("\t\t\t}");
				returnValue.AppendLine("\t\t\tcatch (Exception) {");
				returnValue.AppendLine("\t\t\t\tthrow;");
				returnValue.AppendLine("\t\t\t}");
				returnValue.AppendLine("\t\t}");
			}

			return returnValue.ToString();
		}
	}
}