using System.Xml;
using Selero.Core;

namespace Test.Apps
{
	public class FileManager
	{
		#region Variables

		private Routines _routines = new Routines();

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Public

		public void CompressDirectory(XmlNode settings)
		{
			try {
				XmlNode processes = settings.SelectSingleNode("processes");

				foreach (XmlNode process in processes.SelectNodes("process")) {
					if (process.NodeType != XmlNodeType.Comment) {
						XmlNode directoriesNode = process.SelectSingleNode("directories");

						if (directoriesNode != null && directoriesNode.HasChildNodes) {
							foreach (XmlNode directory in directoriesNode.ChildNodes) {
								if (directory.NodeType != XmlNodeType.Comment) {
									bool successful = true;
									List<string> messages = new List<string>();

									string sourceDirectoryName = _routines.GetAttribute<string>(directory, "sourcedirectoryname");
									int compressLevel = _routines.GetAttribute<int>(directory, "compresslevel");
									string archiveFileName = _routines.GetAttribute<string>(directory, "archivefilename");
									string minimumAgeInterval = _routines.GetAttribute<string>(directory, "minimumageinterval");
									int minimumAge = _routines.GetAttribute<int>(directory, "minimumage");
									bool verify = _routines.GetAttribute<bool>(directory, "verify");
									bool deleteSource = _routines.GetAttribute<bool>(directory, "deletesource");

									if (compressLevel > 0)
										successful = successful = _Compress(sourceDirectoryName, 1, compressLevel, archiveFileName, minimumAgeInterval, minimumAge, verify, deleteSource, ref messages);
									else {
										messages.Add(string.Format("Compress level {0} is invalid.", compressLevel));
										successful = false;
									}
								}
							}
						}
					}
				}
			}
			catch { }
		}

		#endregion Public

		#region Private

		private bool _Compress(string sourceDirectoryName, int currentLevel, int compressLevel, string archiveFileName, string minimumAgeInterval, int minimumAge, bool verify, bool deleteSource, ref List<string> messages)
		{
			bool returnValue = true;

			try {
				// Get all directories and files from the source directory
				List<DirectoryInfo> directories = new DirectoryInfo(sourceDirectoryName).GetDirectories().ToList();
				FileInfo[] files = new DirectoryInfo(sourceDirectoryName).GetFiles();

				if (currentLevel == compressLevel) {
					// Compress all directories
					foreach (DirectoryInfo directory in directories) {
						string sourceDirectory = string.Format(@"{0}\{1}", sourceDirectoryName, directory.Name);
						string destinationArchiveName = string.Format(@"{0}\{1}", sourceDirectoryName, Path.GetFileName(archiveFileName).Replace("[base]", directory.Name));

						if (_routines.CompressDirectory(sourceDirectory, destinationArchiveName, minimumAgeInterval, minimumAge, verify, deleteSource, ref messages))
							continue;
						else
							messages.Add(string.Format("Failed to compress directory '{0}' (Method: {1}, Compress Level: {2})", sourceDirectory, System.Reflection.MethodBase.GetCurrentMethod().Name, compressLevel));
					}

					// Compress all files
					foreach (FileInfo file in files) {
						string sourceFileName = string.Format(@"{0}\{1}", sourceDirectoryName, file.Name);
						string destinationArchiveName = archiveFileName.Replace("[base]", file.Name);

						if (_routines.CompressFile(sourceFileName, destinationArchiveName, minimumAgeInterval, minimumAge, deleteSource, ref messages))
							continue;
						else
							messages.Add(string.Format("Failed to compress file '{0}' (Method: {1}, Compress Level: {2})", sourceFileName, System.Reflection.MethodBase.GetCurrentMethod().Name, compressLevel));
					}
				}
				else {
					currentLevel++;

					foreach (DirectoryInfo directory in directories) {
						string sourceDirectory = string.Format(@"{0}\{1}", sourceDirectoryName, directory.Name);
						string destinationArchiveName = string.Format(@"{0}\{1}", sourceDirectory, Path.GetFileName(archiveFileName));

						if (_Compress(sourceDirectory, currentLevel, compressLevel, destinationArchiveName, minimumAgeInterval, minimumAge, verify, deleteSource, ref messages))
							continue;
						else {
							messages.Add(string.Format("Failed to compress directory '{0}' (Method: {1}, Compress Level: {2})", sourceDirectory, System.Reflection.MethodBase.GetCurrentMethod().Name, compressLevel));

							returnValue = false;
						}
					}
				}
			}
			catch (Exception ex) {
				messages.Add(ex.Message);

				returnValue = false;
			}

			return returnValue;
		}

		#endregion Private

		#endregion Methods
	}
}