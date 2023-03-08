using System.Net;
using System.Text;
using System.Xml;

namespace Selero.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class AppSettings
	{
		#region Variables

		private string _application = string.Empty;
		private string _assemblyName = string.Empty;
		private XmlDocument _document;
		private string _errorMessage = string.Empty;
		private string _location = string.Empty;
		private string _objectEntryPoint = string.Empty;
		private string _objectName = string.Empty;
		private string _path = string.Empty;
		private Routines _routines = new Routines();
		private XmlNode _settings;
		private string _specification = string.Empty;

		#endregion Variables

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public string Application
		{
			get { return _application; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string AssemblyName
		{
			get { return _assemblyName; }
		}

		/// <summary>
		/// 
		/// </summary>
		public XmlDocument Document
		{
			get { return _document; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ErrorMessage
		{
			get { return _errorMessage; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Location
		{
			get { return _location; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ObjectEntryPoint
		{
			get { return _objectEntryPoint; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ObjectName
		{
			get { return _objectName; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Path
		{
			get { return _path; }
		}

		/// <summary>
		/// 
		/// </summary>
		public XmlNode Settings
		{
			get { return _settings; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Specification
		{
			get { return _specification; }
		}

		#endregion Properties

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="specification"></param>
		/// <param name="application"></param>
		/// <exception cref="Exception"></exception>
		public AppSettings(string specification, string application)
		{
			_specification = specification;
			_application = application;
			_document = _LoadFromFileSystem(specification);

			if (_document != null) {
				if (_IsDocumentValid()) {
					if (_CreateSettingsNode()) {
						_assemblyName = _routines.GetAttribute<string>(_settings.SelectSingleNode("assembly"), "name");
						_objectName = _routines.GetAttribute<string>(_settings.SelectSingleNode("object"), "name");
						_objectEntryPoint = _routines.GetAttribute<string>(_settings.SelectSingleNode("object"), "entrypoint");
					}
				}
			}
			else
				throw new Exception("Settings file could not be loaded.");
		}

		#endregion Constructors

		#region Methods

		#region Public

		#endregion Public

		#region Private

		private bool _CreateSettingsNode()
		{
			bool returnValue = false;

			try {
				// Get the application and common node
				XmlNode common = _document.GetElementsByTagName("common")[0];
				XmlNode application = _document.GetElementsByTagName(_application)[0];

				// Process each child node of the common node a put into application node if the node doesn't exist in the application node already.
				// The "variables" node will have special processing            
				foreach (XmlNode node in common.ChildNodes) {
					if (node.Name == "variables") {
						XmlNode appVariables = application.SelectSingleNode("variables");
						if (appVariables == null)
							application.AppendChild(node);
						else {
							foreach (XmlNode varNode in node.ChildNodes) {
								string varName = _routines.GetAttribute<string>(varNode, "name");
								string varValue = _routines.GetAttribute<string>(varNode, "value");

								//Determine if this variable exists at the application level or not
								bool exist = false;
								foreach (XmlNode appVarNode in appVariables.ChildNodes) {
									string appVarName = _routines.GetAttribute<string>(appVarNode, "name");

									if (appVarName == varName) {
										exist = true;
										break;
									}
								}
								if (!exist) {
									XmlNode newNode = varNode.CloneNode(true);
									application.SelectSingleNode("variables").AppendChild(newNode);
								}
							}
						}
					}
					else {
						// Determine if the node already exists in the application node
						if (application.SelectSingleNode(node.Name) == null) {
							XmlNode newNode = node.CloneNode(true);
							application.AppendChild(newNode);
						}
					}

				}

				// Replace all of the variables and remove that node
				string xml = application.OuterXml;
				XmlNode variables = application.SelectSingleNode("variables");

				foreach (XmlNode repNode in variables.ChildNodes) {
					string repName = _routines.GetAttribute<string>(repNode, "name");
					string repValue = WebUtility.HtmlEncode(_routines.GetAttribute<string>(repNode, "value"));

					xml = xml.Replace(string.Concat("[", repName, "]"), repValue);
				}

				xml = xml.Replace(_application, "settings");

				// Use the application 
				XmlDocument newDocument = new XmlDocument();
				newDocument.LoadXml(xml);

				XmlNode newVariables = newDocument.DocumentElement.SelectSingleNode("variables");
				newDocument.DocumentElement.RemoveChild(newVariables);

				_settings = (XmlNode)newDocument.DocumentElement;

				returnValue = true;
			}
			catch (Exception ex) {
				_errorMessage = ex.Message;
				_settings = null;
			}

			return returnValue;
		}

		private bool _IsDocumentValid()
		{
			bool returnValue = false;

			try {
				StringBuilder sb = new StringBuilder();

				// Determine the number of occurrences for nodes that are needed
				int settings = _document.GetElementsByTagName("settings").Count;
				int common = _document.GetElementsByTagName("common").Count;
				int app = _document.GetElementsByTagName(_application).Count;
				int assembly = _document.GetElementsByTagName("assembly").Count;
				int appobj = _document.GetElementsByTagName("object").Count;

				// Validate the number of occurrences
				if (settings == 0)
					sb.AppendLine("'settings' Node not found");
				else if (settings > 1)
					sb.AppendLine("Multiple 'settings' Nodes found");

				if (common == 0)
					sb.AppendLine("'common' Node not found");
				else if (common > 1)
					sb.AppendLine("Multiple 'common' Nodes found");

				if (app == 0)
					sb.AppendFormat("'{0}' Node not found\r\n", _application);
				else if (app > 1)
					sb.AppendFormat("Multiple '{0}' Nodes found\r\n", _application);

				if (assembly == 0)
					sb.AppendFormat("'assembly' Node not found\r\n", _application);

				if (appobj == 0)
					sb.AppendFormat("'object' Node not found\r\n", _application);

				// Make sure the document only has one "settings" node
				if (sb.Length > 0)
					_errorMessage = sb.ToString();
				else
					returnValue = true;
			}
			catch (Exception ex) {
				_errorMessage = ex.Message;
			}

			return returnValue;
		}

		private XmlDocument _LoadFromFileSystem(string file)
		{
			XmlDocument xmlDoc = new XmlDocument();

			try {
				FileInfo fi = new FileInfo(file);

				if (fi.Exists) {
					xmlDoc.Load(fi.FullName);

					_location = "LOCAL";
					_path = fi.FullName;
				}
				else
					xmlDoc = null;
			}
			catch {
				xmlDoc = null;
			}

			return xmlDoc;
		}

		#endregion Private

		#endregion Methods
	}
}