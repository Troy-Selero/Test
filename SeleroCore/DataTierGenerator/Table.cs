using System;
using System.Collections.Generic;

namespace DataTierGenerator
{
	/// <summary>
	/// Class that stores information for tables in a database.
	/// </summary>
	public class Table
	{
		private string name;
		private List<Column> columns;
		private List<Column> primaryKeys;
		private Dictionary<string, List<Column>> foreignKeys;
		private List<ExtendedProperty> extendedProperties;

		/// <summary>
		/// Default constructor.  All collections are initialized.
		/// </summary>
		public Table()
		{
			columns = new List<Column>();
			primaryKeys = new List<Column>();
			foreignKeys = new Dictionary<string, List<Column>>();
			extendedProperties = new List<ExtendedProperty>();
		}

		/// <summary>
		/// Contains the list of Column instances that define the table.
		/// </summary>
		public List<Column> Columns
		{
			get { return columns; }
		}

		/// <summary>
		/// Contains the list of Column instances that define the table.
		/// </summary>
		public List<ExtendedProperty> ExtendedProperties
		{
			get { return extendedProperties; }
		}
		/// <summary>
		/// Contains the list of Column instances that define the table.  The Dictionary returned 
		/// is keyed on the foreign key name, and the value associated with the key is an 
		/// List of Column instances that compose the foreign key.
		/// </summary>
		public Dictionary<string, List<Column>> ForeignKeys
		{
			get { return foreignKeys; }
		}

		/// <summary>
		/// Name of the table.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Contains the list of primary key Column instances that define the table.
		/// </summary>
		public List<Column> PrimaryKeys
		{
			get { return primaryKeys; }
		}
	}
}
