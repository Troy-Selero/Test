using System.Collections.Generic;

namespace DataTierGenerator
{
	/// <summary>
	/// Class that stores information for columns in a database table.
	/// </summary>
	public class Column
	{
		#region Variables

		// Private variable used to hold the property values
		private string name;
		private string type;
		private string length;
		private string precision;
		private string scale;
		private bool isRowGuidCol;
		private bool isIdentity;
		private bool isComputed;
		private bool allowsNulls;
		private int? characterMaxLength;
		private List<ExtendedProperty> extendedProperties;
		private bool addOnly;
		private bool deleteOnly;
		private bool isDefaultUser;
		private bool isDefaultDate;
		private bool isDefaultDateUTC;
		private bool isDefaultGuid;
		private bool isEncrypted;
		private bool isDefaultApp;
		private bool isDefaultMachine;

		#endregion Variables

		#region Properties

		/// <summary>
		/// Name of the column.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Data type of the column.
		/// </summary>
		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// Length in bytes of the column.
		/// </summary>
		public string Length
		{
			get { return length; }
			set { length = value; }
		}

		/// <summary>
		/// Precision of the column.  Applicable to decimal, float, and numeric data types only.
		/// </summary>
		public string Precision
		{
			get { return precision; }
			set { precision = value; }
		}

		/// <summary>
		/// Scale of the column.  Applicable to decimal, and numeric data types only.
		/// </summary>
		public string Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		/// <summary>
		/// Flags the column as a uniqueidentifier column.
		/// </summary>
		public bool IsRowGuidCol
		{
			get { return isRowGuidCol; }
			set { isRowGuidCol = value; }
		}

		/// <summary>
		/// Flags the column as an identity column.
		/// </summary>
		public bool IsIdentity
		{
			get { return isIdentity; }
			set { isIdentity = value; }
		}

		/// <summary>
		/// Flags the column as being computed.
		/// </summary>
		public bool IsComputed
		{
			get { return isComputed; }
			set { isComputed = value; }
		}

		public bool AllowsNulls
		{
			get { return allowsNulls; }
			set { allowsNulls = value; }
		}

		public int? CharacterMaxLength
		{
			get { return characterMaxLength; }
			set { characterMaxLength = value; }
		}

		/// <summary>
		/// Contains the list of Column instances that define the table.
		/// </summary>
		public List<ExtendedProperty> ExtendedProperties
		{
			get { return extendedProperties; }
		}

		/// <summary>
		/// Flags the column as a AddOnly column.
		/// </summary>
		public bool AddOnly
		{
			get { return addOnly; }
			set { addOnly = value; }
		}

		/// <summary>
		/// Flags the column as a DeleteOnly column.
		/// </summary>
		public bool DeleteOnly
		{
			get { return deleteOnly; }
			set { deleteOnly = value; }
		}

		/// <summary>
		/// Flags the column as a IsDefaultUser column.
		/// </summary>
		public bool IsDefaultUser
		{
			get { return isDefaultUser; }
			set { isDefaultUser = value; }
		}

		/// <summary>
		/// Flags the column as a IsDefaultDate column.
		/// </summary>
		public bool IsDefaultDate
		{
			get { return isDefaultDate; }
			set { isDefaultDate = value; }
		}

		/// <summary>
		/// Flags the column as a IsDefaultDateUTC column.
		/// </summary>
		public bool IsDefaultDateUTC
		{
			get { return isDefaultDateUTC; }
			set { isDefaultDateUTC = value; }
		}

		/// <summary>
		/// Flags the column as a IsDefaultGuid column.
		/// </summary>
		public bool IsDefaultGuid
		{
			get { return isDefaultGuid; }
			set { isDefaultGuid = value; }
		}

		/// <summary>
		/// Flags the column as a IsEncrypted column.
		/// </summary>
		public bool IsEncrypted
		{
			get { return isEncrypted; }
			set { isEncrypted = value; }
		}

		/// <summary>
		/// Flags the column as a IsDefaultApp column.
		/// </summary>
		public bool IsDefaultApp
		{
			get { return isDefaultApp; }
			set { isDefaultApp = value; }
		}

		/// <summary>
		/// Flags the column as a IsDefaultMachine column.
		/// </summary>
		public bool IsDefaultMachine
		{
			get { return isDefaultMachine; }
			set { isDefaultMachine = value; }
		}

		#endregion Properties

		#region Constructors

		public Column()
		{
			extendedProperties = new List<ExtendedProperty>();
		}

		#endregion Constructors
	}
}
