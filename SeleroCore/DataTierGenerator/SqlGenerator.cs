using System;
using System.IO;
using System.Text;

namespace DataTierGenerator
{
	/// <summary>
	/// Generates SQL Server stored procedures for a database.
	/// </summary>
	internal static class SqlGenerator
	{
		/// <summary>
		/// Creates an insert stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
		/// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
		internal static void CreateAddStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles, bool includeDrop, string author, string defaultsType)
		{
			// Create the stored procedure name
			string procedureName = storedProcedurePrefix + Utility.FormatClassName(table.Name) + "_Add";
			string fileName;

			// Determine the file name to be used
			if (createMultipleFiles)
				fileName = Path.Combine(path, procedureName + ".sql");
			else
				fileName = Path.Combine(path, "StoredProcedures.sql");

			using (StreamWriter sw = new StreamWriter(fileName, true)) {
				// Create drop statment if necessary
				if (includeDrop) {
					sw.WriteLine("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[" + procedureName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
					sw.WriteLine("\tDROP PROCEDURE [dbo].[" + procedureName + "]");
					sw.WriteLine("GO");
					sw.WriteLine();
				}

				// Create heading
				sw.WriteLine("-- =========================================================================");
				sw.WriteLine("-- Purpose:  Add entry to the " + table.Name + " table.");
				sw.WriteLine("--");
				sw.WriteLine("-- Date        Name                 Description");
				sw.WriteLine("-- ----------  -------------------  ----------------------------------------");
				sw.WriteLine("-- " + DateTime.Today.ToString("MM/dd/yyyy") + "  " + author + "  " + "Initial Creation");
				sw.WriteLine("-- =========================================================================");

				// Create the SQL for the stored procedure
				sw.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
				sw.WriteLine("(");

				// Create the parameter list
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = table.Columns[i];
					bool setInProcedure = Utility.ValueSetInStoredProcedure(true, column, defaultsType);

					sw.Write("\t" + Utility.CreateParameterString(column, true, setInProcedure));

					if (i < (table.Columns.Count - 1))
						sw.Write(",");
					sw.WriteLine();
				}
				sw.WriteLine(")");
				sw.WriteLine("AS");
				sw.WriteLine();

				// Initialize all columns
				bool headingAdded = false;
				foreach (Column column in table.Columns) {
					bool setInProcedure = Utility.ValueSetInStoredProcedure(true, column, defaultsType);

					if (!headingAdded && setInProcedure) {
						sw.WriteLine("-- Set needed values");
						headingAdded = true;
					}

					if (setInProcedure) {
						if (column.IsDefaultGuid)
							sw.WriteLine("SET @p" + column.Name + "=NEWID()");
						else if (column.IsDefaultUser)
							sw.WriteLine("SET @p" + column.Name + "=ISNULL(@p" + column.Name + ",SYSTEM_USER)");
						else if (column.IsDefaultDate)
							sw.WriteLine("SET @p" + column.Name + "=GETDATE()");
						else if (column.IsDefaultDateUTC)
							sw.WriteLine("SET @p" + column.Name + "=GETUTCDATE()");
						else if (column.IsDefaultApp)
							sw.WriteLine("SET @p" + column.Name + "=ISNULL(@p" + column.Name + ",'" + procedureName + "')");
						else if (column.IsDefaultMachine)
							sw.WriteLine("SET @p" + column.Name + "=ISNULL(@p" + column.Name + ",HOST_NAME())");
					}
				}
				if (headingAdded)
					sw.WriteLine(string.Empty);

				sw.WriteLine("INSERT INTO [" + table.Name + "]");
				sw.WriteLine("(");

				// Create the parameter list
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = table.Columns[i];

					// Ignore any identity columns
					if (column.IsIdentity == false) {
						// Append the column name as a parameter of the insert statement
						if (i < (table.Columns.Count - 1))
							sw.WriteLine("\t[" + column.Name + "],");
						else
							sw.WriteLine("\t[" + column.Name + "]");
					}
				}

				sw.WriteLine(")");
				sw.WriteLine("VALUES");
				sw.WriteLine("(");

				// Create the values list
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = table.Columns[i];

					// Is the current column an identity column?
					if (column.IsIdentity == false) {
						// Append the necessary line breaks and commas
						if (i < (table.Columns.Count - 1))
							sw.WriteLine("\t@p" + column.Name + ",");
						else
							sw.WriteLine("\t@p" + column.Name);
					}
				}

				sw.WriteLine(")");

				// Should we include a line for returning the identity?
				foreach (Column column in table.Columns) {
					// Is the current column an identity column?
					if (column.IsIdentity) {
						sw.WriteLine();
						sw.WriteLine("-- Set the return parameter for created identity");
						sw.WriteLine("SELECT @p" + column.Name + "=SCOPE_IDENTITY()");
					}
				}

				sw.WriteLine();
				sw.WriteLine("GO");

				// Create the grant statement, if a user was specified
				if (grantLoginName.Length > 0) {
					sw.WriteLine();
					sw.WriteLine("-- Grant permissions");
					sw.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
					sw.WriteLine("GO");
				}
				sw.WriteLine();
			}
		}

		/// <summary>
		/// Creates an update stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
		/// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
		internal static void CreateUpdateStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles, bool includeDrop, string author, string defaultsType)
		{
			// Create the stored procedure name
			string procedureName = storedProcedurePrefix + Utility.FormatClassName(table.Name) + "_Update";
			string fileName;

			// Determine the file name to be used
			if (createMultipleFiles)
				fileName = Path.Combine(path, procedureName + ".sql");
			else
				fileName = Path.Combine(path, "StoredProcedures.sql");

			using (StreamWriter sw = new StreamWriter(fileName, true)) {
				// Create the drop statment if necessary
				if (includeDrop) {
					sw.WriteLine("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[" + procedureName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
					sw.WriteLine("\tDROP PROCEDURE [dbo].[" + procedureName + "]");
					sw.WriteLine("GO");
					sw.WriteLine();
				}

				// Create the heading
				sw.WriteLine("-- =========================================================================");
				sw.WriteLine("-- Purpose:  Update entry in the " + table.Name + " table.");
				sw.WriteLine("--");
				sw.WriteLine("-- Date        Name                 Description");
				sw.WriteLine("-- ----------  -------------------  ----------------------------------------");
				sw.WriteLine("-- " + DateTime.Today.ToString("MM/dd/yyyy") + "  " + author + "  " + "Initial Creation");
				sw.WriteLine("-- =========================================================================");

				// Create the SQL for the stored procedure
				sw.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
				sw.WriteLine("(");

				// Create the parameter list
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = table.Columns[i];
					bool setInProcedure = Utility.ValueSetInStoredProcedure(column, defaultsType);

					sw.Write("\t" + Utility.CreateParameterString(column, false, setInProcedure));

					if (i < (table.Columns.Count - 1))
						sw.Write(",");
					sw.WriteLine();
				}
				sw.WriteLine(")");
				sw.WriteLine("AS");
				sw.WriteLine();

				// Initialize all RowGuidCol columns
				bool headingAdded = false;
				foreach (Column column in table.Columns) {
					bool setInProcedure = Utility.ValueSetInStoredProcedure(column, defaultsType);

					if (!headingAdded && setInProcedure) {
						sw.WriteLine("-- Set needed values");
						headingAdded = true;
					}

					if (setInProcedure && !column.AddOnly && !column.DeleteOnly) {
						if (column.IsDefaultGuid)
							sw.WriteLine("SET @p" + column.Name + "=NEWID()");
						else if (column.IsDefaultUser)
							sw.WriteLine("SET @p" + column.Name + "=ISNULL(@p" + column.Name + ",SYSTEM_USER)");
						else if (column.IsDefaultDate)
							sw.WriteLine("SET @p" + column.Name + "=GETDATE()");
						else if (column.IsDefaultDateUTC)
							sw.WriteLine("SET @p" + column.Name + "=GETUTCDATE()");
						else if (column.IsDefaultApp)
							sw.WriteLine("SET @p" + column.Name + "=ISNULL(@p" + column.Name + ",'" + procedureName + "')");
						else if (column.IsDefaultMachine)
							sw.WriteLine("SET @p" + column.Name + "=ISNULL(@p" + column.Name + ",HOST_NAME())");
					}
				}
				if (headingAdded)
					sw.WriteLine(string.Empty);

				sw.WriteLine("UPDATE [" + table.Name + "]");
				sw.WriteLine("SET");

				// Create the parameter list
				for (int i = 0; i < table.Columns.Count; i++) {
					Column column = table.Columns[i];

					// Ignore any identity columns
					if (column.IsIdentity == false && (table.PrimaryKeys.Contains(column) == false || table.PrimaryKeys.Count == table.Columns.Count)) {
						// Append the column name as a parameter of the insert statement
						if (i < (table.Columns.Count - 1))
							sw.WriteLine("\t[" + column.Name + "]=@p" + column.Name + ",");
						else
							sw.WriteLine("\t[" + column.Name + "]=@p" + column.Name);
					}
				}

				sw.WriteLine("WHERE");

				// Create the where clause
				for (int i = 0; i < table.PrimaryKeys.Count; i++) {
					Column column = table.PrimaryKeys[i];

					if (i == 0)
						sw.WriteLine("\t[" + column.Name + "]=@p" + column.Name);
					else
						sw.WriteLine("\tAND [" + column.Name + "]=@p" + column.Name);
				}

				sw.WriteLine();
				sw.WriteLine("GO");

				// Create the grant statement, if a user was specified
				if (grantLoginName.Length > 0) {
					sw.WriteLine();
					sw.WriteLine("-- Grant permissions");
					sw.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
					sw.WriteLine("GO");
				}
				sw.WriteLine();
			}
		}

		/// <summary>
		/// Creates an select stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
		/// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
		internal static void CreateFindStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles, bool includeDrop, string author, bool allowDirtyReads)
		{
			if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count) {
				// Determine if table contains deleted columns
				Column deletedColumn = table.Columns.Find(Deleted);
				Column deletedDateColumn = table.Columns.Find(DeletedDate);
				Column deletedDateUTCColumn = table.Columns.Find(DeletedDateUTC);
				bool includeDeleted = (deletedColumn != null || deletedDateColumn != null || deletedDateUTCColumn != null);

				// Create the stored procedure name
				string procedureName = storedProcedurePrefix + Utility.FormatClassName(table.Name) + "_Find";
				string fileName;

				// Determine the file name to be used
				if (createMultipleFiles)
					fileName = Path.Combine(path, procedureName + ".sql");
				else
					fileName = Path.Combine(path, "StoredProcedures.sql");

				using (StreamWriter sw = new StreamWriter(fileName, true)) {
					// Create the drop statment if necessary
					if (includeDrop) {
						sw.WriteLine("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[" + procedureName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
						sw.WriteLine("\tDROP PROCEDURE [dbo].[" + procedureName + "]");
						sw.WriteLine("GO");
						sw.WriteLine();
					}

					// Create the heading
					sw.WriteLine("-- =========================================================================");
					sw.WriteLine("-- Purpose:  Find entry in the " + table.Name + " table.");
					sw.WriteLine("--");
					sw.WriteLine("-- Date        Name                 Description");
					sw.WriteLine("-- ----------  -------------------  ----------------------------------------");
					sw.WriteLine("-- " + DateTime.Today.ToString("MM/dd/yyyy") + "  " + author + "  " + "Initial Creation");
					sw.WriteLine("-- =========================================================================");

					// Create the SQL for the stored procedure
					sw.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
					sw.WriteLine("(");

					// Create the parameter list
					for (int i = 0; i < table.PrimaryKeys.Count; i++) {
						Column column = table.PrimaryKeys[i];

						if (i == (table.PrimaryKeys.Count - 1))
							sw.WriteLine(string.Format("\t{0}{1}", Utility.CreateParameterString(column, false, false), includeDeleted ? "," : string.Empty));
						else
							sw.WriteLine("\t" + Utility.CreateParameterString(column, false, false) + ",");
					}
					if (includeDeleted)
						sw.WriteLine("\t@pDeleted bit=0");
					sw.WriteLine(")");
					sw.WriteLine("AS");
					sw.WriteLine();

					sw.WriteLine("-- Set row counters off");
					sw.WriteLine("SET NOCOUNT ON");
					sw.WriteLine();
					sw.WriteLine("SELECT");

					// Create the list of columns
					for (int i = 0; i < table.Columns.Count; i++) {
						Column column = (Column)table.Columns[i];

						if (i < (table.Columns.Count - 1))
							sw.WriteLine("\t[" + column.Name + "],");
						else
							sw.WriteLine("\t[" + column.Name + "]");
					}

					sw.WriteLine("FROM");
					sw.WriteLine("\t[" + table.Name + "] " + (allowDirtyReads ? "WITH (NOLOCK)" : string.Empty));
					sw.WriteLine("WHERE");

					// Create the where clause
					for (int i = 0; i < table.PrimaryKeys.Count; i++) {
						Column column = (Column)table.PrimaryKeys[i];

						if (i > 0)
							sw.WriteLine("\tAND " + column.Name + "=@p" + column.Name);
						else
							sw.WriteLine("\t" + column.Name + "=@p" + column.Name);
					}

					if (deletedColumn != null || deletedDateColumn != null || deletedDateUTCColumn != null) {
						if (table.PrimaryKeys.Count > 0)
							sw.WriteLine("\tAND (@pDeleted=1 OR Deleted=0)");
						else
							sw.WriteLine("\t@pDeleted=1 OR Deleted=0");
					}

					sw.WriteLine();
					sw.WriteLine("-- Set row counters on");
					sw.WriteLine("SET NOCOUNT OFF");

					sw.WriteLine();
					sw.WriteLine("GO");

					// Create the grant statement, if a user was specified
					if (grantLoginName.Length > 0) {
						sw.WriteLine();
						sw.WriteLine("-- Grant permissions");
						sw.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
						sw.WriteLine("GO");
					}
					sw.WriteLine();
				}
			}
		}

		/// <summary>
		/// Creates an select all stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
		/// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
		internal static void CreateFindCollectionStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles, bool includeDrop, string baseTable, string author, bool allowDirtyReads)
		{
			bool baseTableUIDExist = false;
			bool baseTableIDExist = false;
			bool baseTableMnemonicExist = false;
			string tableID = "UID";

			//Determine if current table has the needed column to make the procedure
			bool hasField = false;
			if (baseTable == string.Empty)
				hasField = true;
			else {
				for (int i = 0; i < table.Columns.Count; i++) {
					if (table.Columns[i].Name.ToUpper() == String.Concat(baseTable.ToUpper(), "UID"))
						baseTableUIDExist = true;
					if (table.Columns[i].Name.ToUpper() == String.Concat(baseTable.ToUpper(), "ID"))
						baseTableIDExist = true;
					if (table.Columns[i].Name.ToUpper() == String.Concat(baseTable.ToUpper(), "MNEMONIC"))
						baseTableMnemonicExist = true;

				}
				if (!baseTableUIDExist) {
					if (baseTableIDExist)
						tableID = "ID";
					else if (baseTableMnemonicExist)
						tableID = "Mnemonic";
				}

				if (baseTableUIDExist || baseTableIDExist || baseTableMnemonicExist)
					hasField = true;
			}

			if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count && hasField && table.Name.ToUpper() != baseTable.ToUpper()) {
				// Determine if table contains deleted columns
				Column deletedColumn = table.Columns.Find(Deleted);
				Column deletedDateColumn = table.Columns.Find(DeletedDate);
				Column deletedDateUTCColumn = table.Columns.Find(DeletedDateUTC);
				bool includeDeleted = (deletedColumn != null || deletedDateColumn != null || deletedDateUTCColumn != null);

				// Create the stored procedure name
				string procedureName = storedProcedurePrefix + Utility.MakePlural(Utility.FormatClassName(table.Name)) + "_Find";
				string fileName;

				// Determine the file name to be used
				if (createMultipleFiles)
					fileName = Path.Combine(path, procedureName + ".sql");
				else
					fileName = Path.Combine(path, "StoredProcedures.sql");

				using (StreamWriter sw = new StreamWriter(fileName, true)) {
					// Create the drop statment if necessary
					if (includeDrop) {
						sw.WriteLine("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[" + procedureName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
						sw.WriteLine("\tDROP PROCEDURE [dbo].[" + procedureName + "]");
						sw.WriteLine("GO");
						sw.WriteLine();
					}

					// Create the heading
					sw.WriteLine("-- =========================================================================");
					sw.WriteLine("-- Purpose:  Find list of " + Utility.MakePlural(table.Name) + '.');
					sw.WriteLine("--");
					sw.WriteLine("-- Date        Name                 Description");
					sw.WriteLine("-- ----------  -------------------  ----------------------------------------");
					sw.WriteLine("-- " + DateTime.Today.ToString("MM/dd/yyyy") + "  " + author + "  " + "Initial Creation");
					sw.WriteLine("-- =========================================================================");

					// Create the SQL for the stored procedure
					sw.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");

					if (!string.IsNullOrEmpty(baseTable)) {
						// Create the parameter list
						sw.WriteLine("(");
						sw.WriteLine(string.Format("\t@p{0}{1} bigint{2}", baseTable, tableID, includeDeleted ? "," : string.Empty));
						if (includeDeleted)
							sw.WriteLine("\t@pDeleted bit=0");
						sw.WriteLine(")");
					}
					else if (includeDeleted) {
						sw.WriteLine("(");
						sw.WriteLine("\t@pDeleted bit=0");
						sw.WriteLine(")");
					}
					sw.WriteLine("AS");
					sw.WriteLine();

					sw.WriteLine("-- Set row counters off");
					sw.WriteLine("SET NOCOUNT ON");
					sw.WriteLine();
					sw.WriteLine("SELECT");

					// Create the list of columns
					for (int i = 0; i < table.Columns.Count; i++) {
						Column column = (Column)table.Columns[i];

						if (i < (table.Columns.Count - 1))
							sw.WriteLine("\t[" + column.Name + "],");
						else
							sw.WriteLine("\t[" + column.Name + "]");
					}

					sw.WriteLine("FROM");
					sw.WriteLine("\t[" + table.Name + "] " + (allowDirtyReads ? "WITH (NOLOCK)" : string.Empty));

					if (baseTable != string.Empty || includeDeleted) {
						if (baseTable != string.Empty) {
							sw.WriteLine("WHERE");
							sw.WriteLine("\t{0}{1}=@p{0}{1}", baseTable, tableID);
							if (includeDeleted)
								sw.WriteLine("\tAND (@pDeleted=1 OR Deleted=0)");
						}
						else {
							if (includeDeleted) {
								sw.WriteLine("WHERE");
								sw.WriteLine("\t(@pDeleted=1 OR Deleted=0)");
							}
						}
					}

					sw.WriteLine();
					sw.WriteLine("-- Set row counters on");
					sw.WriteLine("SET NOCOUNT OFF");
					sw.WriteLine();

					sw.WriteLine("GO");

					// Create the grant statement, if a user was specified
					if (grantLoginName.Length > 0) {
						sw.WriteLine();
						sw.WriteLine("-- Grant permissions");
						sw.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
						sw.WriteLine("GO");
					}
				}
			}
		}

		/// <summary>
		/// Creates an delete stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
		/// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
		internal static void CreateDeleteStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles, bool includeDrop, string author, string defaultsType)
		{
			if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count) {
				Column deletedMachineColumn = table.Columns.Find(DeletedMachine);
				Column deletedAppColumn = table.Columns.Find(DeletedApp);
				Column deletedDateColumn = table.Columns.Find(DeletedDate);
				Column deletedDateUTCColumn = table.Columns.Find(DeletedDateUTC);
				Column deletedByColumn = table.Columns.Find(DeletedBy);
				Column deletedColumn = table.Columns.Find(Deleted);

				Column updatedMachineColumn = table.Columns.Find(UpdatedMachine);
				Column updatedAppColumn = table.Columns.Find(UpdatedApp);
				Column updatedDateColumn = table.Columns.Find(UpdatedDate);
				Column updatedDateUTCColumn = table.Columns.Find(UpdatedDateUTC);
				Column updatedByColumn = table.Columns.Find(UpdatedBy);

				bool useDeletedFlag = false;
				if (table.Columns.Find(Deleted) != null || table.Columns.Find(DeletedDate) != null || table.Columns.Find(DeletedDateUTC) != null)
					useDeletedFlag = true;

				// Create the stored procedure name
				string procedureName = storedProcedurePrefix + Utility.FormatClassName(table.Name) + "_Delete";
				string fileName;

				// Determine the file name to be used
				if (createMultipleFiles)
					fileName = Path.Combine(path, procedureName + ".sql");
				else
					fileName = Path.Combine(path, "StoredProcedures.sql");

				using (StreamWriter sw = new StreamWriter(fileName, true)) {
					// Create the drop statment if necessary
					if (includeDrop) {
						sw.WriteLine("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[" + procedureName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
						sw.WriteLine("\tDROP PROCEDURE [dbo].[" + procedureName + "]");
						sw.WriteLine("GO");
						sw.WriteLine();
					}

					// Create the heading
					sw.WriteLine("-- =========================================================================");
					sw.WriteLine("-- Purpose:  Delete entry from " + table.Name + " table.");
					sw.WriteLine("--");
					sw.WriteLine("-- Date        Name                 Description");
					sw.WriteLine("-- ----------  -------------------  ----------------------------------------");
					sw.WriteLine("-- " + DateTime.Today.ToString("MM/dd/yyyy") + "  " + author + "  " + "Initial Creation");
					sw.WriteLine("-- =========================================================================");

					// Create the SQL for the stored procedure
					sw.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
					sw.WriteLine("(");

					// Create the parameter list
					for (int i = 0; i < table.PrimaryKeys.Count; i++) {
						Column column = (Column)table.PrimaryKeys[i];

						if (useDeletedFlag)
							sw.WriteLine("\t" + Utility.CreateParameterString(column, false, false) + ",");
						else
							sw.WriteLine("\t" + Utility.CreateParameterString(column, false, false));
					}

					if (useDeletedFlag) {
						bool setInProcedure;

						if (updatedMachineColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(updatedMachineColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(updatedMachineColumn, true, setInProcedure) + ",");
						}

						if (updatedAppColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(updatedAppColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(updatedAppColumn, true, setInProcedure) + ",");
						}

						if (updatedDateColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(updatedDateColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(updatedDateColumn, true, setInProcedure) + ",");
						}
						else if (updatedDateUTCColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(updatedDateUTCColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(updatedDateUTCColumn, true, setInProcedure) + ",");
						}

						if (updatedByColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(updatedByColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(updatedByColumn, true, setInProcedure) + ",");
						}

						if (deletedMachineColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(deletedMachineColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(deletedMachineColumn, true, setInProcedure) + ",");
						}

						if (updatedAppColumn != null) {
							setInProcedure = Utility.ValueSetInStoredProcedure(deletedAppColumn, defaultsType);
							sw.WriteLine("\t" + Utility.CreateParameterString(deletedAppColumn, true, setInProcedure) + ",");
						}

						if (deletedDateColumn != null)
							sw.WriteLine("\t" + Utility.CreateParameterString(deletedDateColumn, true, true) + ",");
						else if (deletedDateUTCColumn != null)
							sw.WriteLine("\t" + Utility.CreateParameterString(deletedDateUTCColumn, true, true) + ",");

						if (deletedByColumn != null)
							sw.WriteLine("\t" + Utility.CreateParameterString(deletedByColumn, true, true) + ",");

						if (deletedColumn != null)
							sw.WriteLine("\t" + Utility.CreateParameterString(deletedColumn, true, true));
					}
					sw.WriteLine(")");
					sw.WriteLine("AS");
					sw.WriteLine();

					if (!useDeletedFlag) {
						sw.WriteLine("DELETE");

						sw.WriteLine("FROM");
						sw.WriteLine("\t[" + table.Name + "] ");
					}
					else {
						sw.WriteLine("-- Set needed values");
						if (updatedMachineColumn != null)
							sw.WriteLine("SET @pUpdatedMachine=ISNULL(@p" + updatedMachineColumn.Name + ",HOST_NAME())");

						if (updatedAppColumn != null)
							sw.WriteLine("SET @pUpdatedApp=ISNULL(@p" + updatedAppColumn.Name + ",'" + procedureName + "')");

						if (updatedDateUTCColumn != null)
							sw.WriteLine("SET @pUpdatedDateUTC=GETUTCDATE()");
						else
							sw.WriteLine("SET @pUpdatedDate=GETDATE()");
						sw.WriteLine("SET @pUpdatedBy=ISNULL(@pUpdatedBy,SYSTEM_USER)");

						if (deletedMachineColumn != null)
							sw.WriteLine("SET @pDeletedMachine=ISNULL(@p" + deletedMachineColumn.Name + ",HOST_NAME())");

						if (deletedAppColumn != null)
							sw.WriteLine("SET @pDeletedApp=ISNULL(@p" + deletedAppColumn.Name + ",'" + procedureName + "')");

						if (deletedDateUTCColumn != null)
							sw.WriteLine("SET @pDeletedDateUTC=GETUTCDATE()");
						else
							sw.WriteLine("SET @pDeletedDate=GETDATE()");
						sw.WriteLine("SET @pDeletedBy=ISNULL(@pDeletedBy,SYSTEM_USER)");

						if (deletedColumn != null)
							sw.WriteLine("SET @pDeleted=1");
						sw.WriteLine();

						sw.WriteLine("UPDATE [" + table.Name + "]");
						sw.WriteLine("SET");
						sw.WriteLine("\t[DeletedMachine]=@pUpdatedMachine,");
						sw.WriteLine("\t[DeletedApp]=@pUpdatedApp,");
						if (deletedDateUTCColumn != null)
							sw.WriteLine("\t[DeletedDateUTC]=@pUpdatedDateUTC,");
						else
							sw.WriteLine("\t[DeletedDate]=@pUpdatedDate,");
						sw.WriteLine("\t[DeletedBy]=@pUpdatedBy,");
						sw.WriteLine("\t[Deleted]=1,");
						sw.WriteLine("\t[UpdatedMachine]=@pUpdatedMachine,");
						sw.WriteLine("\t[UpdatedApp]=@pUpdatedApp,");

						if (updatedDateUTCColumn != null)
							sw.WriteLine("\t[UpdatedDateUTC]=@pUpdatedDateUTC,");
						else
							sw.WriteLine("\t[UpdatedDate]=@pUpdatedDate,");
						sw.WriteLine("\t[UpdatedBy]=@pUpdatedBy");
					}
					sw.WriteLine("WHERE");

					// Create the where clause
					for (int i = 0; i < table.PrimaryKeys.Count; i++) {
						Column column = (Column)table.PrimaryKeys[i];

						if (i > 0)
							sw.WriteLine("\tAND " + column.Name + "=@p" + column.Name);
						else
							sw.WriteLine("\t" + column.Name + "=@p" + column.Name);
					}

					if (useDeletedFlag)
						sw.WriteLine("\tAND Deleted=0");

					sw.WriteLine();
					sw.WriteLine("GO");

					// Create the grant statement, if a user was specified
					if (grantLoginName.Length > 0) {
						sw.WriteLine();
						sw.WriteLine("-- Grant permissions");
						sw.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
						sw.WriteLine("GO");
					}
					sw.WriteLine();
				}
			}
		}

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
	}
}
