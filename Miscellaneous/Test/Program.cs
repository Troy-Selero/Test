using System.Net;
using System.Reflection;
using System.Text.Json;
using OfficeOpenXml;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Selero.Core;
using Test.Data;

namespace Miscellaneous
{
	internal class Program
	{
		#region Variables

		private static Routines _routines = new Routines();
		private static string _updateApp = "TestApp";
		private static string _updateUser = Environment.UserName;

		#endregion Variables

		static void Main(string[] args)
		{
			// Place called method here
			LoadManufacturerPharmacyEligibilityFile();
		}

		#region Methods

		#region Public

		public static void LoadManufacturerPharmacyEligibilityFile()
		{
			try {
				List<string> messages = new List<string>();
				FileInfo fileInfo = new FileInfo(@"C:\Data\Temp\340B_ESP_Eligibility_Deidentified1.xlsx");

				if (fileInfo.Exists) {
					ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

					using (ExcelPackage excelPackage = new ExcelPackage(fileInfo)) {
						if (excelPackage != null && excelPackage.Workbook != null && excelPackage.Workbook.Worksheets.Count > 0) {
							ManufacturerPharmacyEligibilityBatch eligibilityBatch = new ManufacturerPharmacyEligibilityBatch("localhost", "Selena", string.Empty, string.Empty)
							{
								UploadFile = fileInfo.Name
							};

							if (eligibilityBatch.Add(_updateUser, _updateApp)) {
								foreach (ExcelWorksheet excelWorksheet in excelPackage.Workbook.Worksheets) {
									List<ManufacturerFileFormatColumn> formatColumns = _GetFileFormatColumns(excelWorksheet.Name, out char delimiter, out Dictionary<string, int> header);

									if (formatColumns != null) {
										bool valid = true;

										// Validate row 1 with the expected header
										foreach (KeyValuePair<string, int> kvp in header) {
											string value = kvp.Key;
											int column = kvp.Value;

											if (value.ToUpper() != _routines.GetCellValue<string>(excelWorksheet, 1, column + 1).ToUpper()) {
												valid = false;
												break;
											}
										}

										if (valid) {
											Dictionary<int, Dictionary<string, string>> pharmacies = _ReadData(2, excelWorksheet, formatColumns, header);

											if (pharmacies.Count > 0)
												_ProcessPharmacies(excelWorksheet.Name, eligibilityBatch.ManufacturerPharmacyEligibilityBatchUID, pharmacies, messages);
										}
										else
											messages.Add(string.Format("File {0} has an invalid header in worksheet {1}.", fileInfo.FullName, excelWorksheet.Name));
									}
								}
							}
							else
								messages.Add(string.Format("Failed to create batch for file {0}.", fileInfo.Name));
						}
						else
							messages.Add(string.Format("File {0} is invalid.", fileInfo.FullName));
					}
				}
				else
					messages.Add(string.Format("File {0} does not exist.", fileInfo.FullName));

				if (messages.Count > 0) {
					foreach (string message in messages)
						Console.WriteLine(message);
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				Console.ReadLine();
			}
		}

		/// <summary>
		/// Use the openFDA drug NDC directory endpoint API to search for a drug using the brand name, product NDC, or package NDC.
		/// Each drug is identified by a 10 digit, 3 segment number.
		/// 
		/// 1. Labeler - Any firm that manufactures or distributes the drug.
		/// 2. Product Code - Identifies a specific strength, dosage form, and formulation of a drug for a particular firm.
		/// 3. Trade Package Size - Identifies package sizes and types.
		/// 
		/// The NDC will be in one of the following configurations: 4-4-2, 5-3-2, or 5-4-1
		/// </summary>
		public static void NDCSearch()
		{
			StreamWriter? sw = null;

			try {
				// Drug NDC Directory endpoint
				string requestUri = "https://api.fda.gov/drug/ndc.json";

				NDCSearchBy searchBy = new NDCSearchBy();
				//searchBy = NDCSearchBy.BrandName;
				searchBy = NDCSearchBy.ProductNdc;
				//searchBy = NDCSearchBy.PackageNdc;

				string brandName = "Vimpat";
				string productNDC = "0131-2479";
				string packageNDC = "0131-2479-35";

				//brandName = "Lacosamide";
				//productNDC = "65162-926";
				//packageNDC = "65162-926-06";

				//brandName = "Venlafaxine Hydrochloride";
				//productNDC = "65841-675";
				//packageNDC = "65841-675-01";

				switch (searchBy) {
					case NDCSearchBy.BrandName:
						requestUri = string.Format("{0}?search=brand_name:\"{1}\"", requestUri, brandName);
						break;

					case NDCSearchBy.ProductNdc:
						requestUri = string.Format("{0}?search=product_ndc:\"{1}\"", requestUri, productNDC);
						break;

					case NDCSearchBy.PackageNdc:
						requestUri = string.Format("{0}?search=packaging.package_ndc:\"{1}\"", requestUri, packageNDC);
						break;
				}

				// If no limit, the query returns one result
				int? limit = null;

				// The largest allowed value is 1000
				//limit = 1000;

				if (limit.HasValue)
					requestUri = string.Format("{0}&limit={1}", requestUri, limit.Value);

				HttpClient client = new HttpClient();

				using (HttpResponseMessage response = client.GetAsync(requestUri).Result) {
					if (response.StatusCode == HttpStatusCode.OK) {
						using (HttpContent content = response.Content) {
							string json = content.ReadAsStringAsync().Result;

							sw = new StreamWriter(string.Format(@"C:\Data\Temp\{0}.json", brandName));
							sw.Write(json);

							// Parse the JSON into the type OpenFDAQueryModel
							Models.FDA.OpenFDAQueryModel? queryModel = JsonSerializer.Deserialize<Models.FDA.OpenFDAQueryModel>(json);

							if (queryModel != null && queryModel.results.Length > 0) {
								foreach (Models.FDA.Result result in queryModel.results) {
									Drug drug = new Drug("localhost", "Selena", string.Empty, string.Empty);

									if (!drug.Find(result.product_ndc, true)) {
										drug.ProductNDC = result.product_ndc;
										drug.BrandName = result.brand_name;
										drug.GenericName = result.generic_name;
										drug.DEASchedule = !string.IsNullOrEmpty(result.dea_schedule) ? result.dea_schedule : "Non-Scheduled";
										drug.Manufacturer = result.openfda.manufacturer_name[0];

										// Indicate generic if the first word in the brand name and the generic name are the same
										if (result.brand_name.StartsWith(result.generic_name))
											drug.Generic = true;
										else
											drug.Generic = false;

										foreach (string name in result.pharm_class) {
											PharmClass pharm_class = new PharmClass
											{
												Name = name
											};

											drug.PharmClasses.Add(pharm_class);
										}

										if(!drug.Add(_updateUser, _updateApp)) {
										}
									}
								}
							}
						}
					}
					else
						throw new ApplicationException("Response Error");
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			finally {
				if (sw != null) {
					sw.Flush();
					sw.Close();
				}
			}
		}

		/// <summary>
		/// Use the NPI Registry API to search for a prescriber using the individual or organization NPI.
		/// </summary>
		public static void NPIRegistrySearch()
		{
			StreamWriter? sw = null;

			try {
				bool individual = true;
				string npi = "1225068620";
				//npi = "1598780777";

				//individual= false;
				//npi = "1154435824";

				string requestUri = string.Format("https://npiregistry.cms.hhs.gov/api/?number={0}&version=2.1", npi);

				HttpClient client = new HttpClient();

				using (HttpResponseMessage response = client.GetAsync(requestUri).Result) {
					if (response.StatusCode == HttpStatusCode.OK) {
						using (HttpContent content = response.Content) {
							string json = content.ReadAsStringAsync().Result;

							// Parse the JSON into the type NPIRegistryQueryModel
							Models.NPI.NPIRegistryQueryModel? lookupModel = JsonSerializer.Deserialize<Models.NPI.NPIRegistryQueryModel>(json);

							if (lookupModel != null && lookupModel.results.Length == 1) {
								Models.NPI.Result result = lookupModel.results[0];

								if (individual) {
									string path = string.Format(@"C:\Data\Temp\{0} {1}.json", result.basic.first_name, result.basic.last_name);

									sw = new StreamWriter(path);
									sw.Write(json);

									Prescriber prescriber = _GetPrescriber(result);
									EntityPrescriber entityPrescriber = _GetEntityPrescriber("uabh", result);
								}
							}
						}
					}
					else
						throw new ApplicationException("Response Error");
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			finally {
				if (sw != null) {
					sw.Flush();
					sw.Close();
				}
			}
		}

		/// <summary>
		/// Use the debug profile named FileManager: C:\Data\Development\Test\Miscellaneous\Settings\filemanager.xml compressfiles
		/// 
		/// Use the debug profile named ServiceBroker: C:\Data\Development\Test\Miscellaneous\Settings\servicebroker.xml prescriber
		///	The stored procedures and triggers, along with the Service Broker objects will need created. Use the script named NonQualifiedPrescription_BrokerObjects.sql
		///	
		/// </summary>
		/// <param name="args"></param>
		public static void ProcessAppSettings(string[] args)
		{
			try {
				if (args.Length >= 2) {
					string settings = args[0];
					string application = args[1];

					AppSettings appSettings = new AppSettings(settings, application);

					if (appSettings != null && appSettings.ErrorMessage == string.Empty) {
						// Get the assembly name
						Assembly assembly;
						try {
							assembly = Assembly.Load(appSettings.AssemblyName);
						}
						catch {
							assembly = null;
						}

						if (assembly != null) {
							string objectName = string.Format("{0}.{1}", appSettings.AssemblyName, appSettings.ObjectName);

							// Get the type of object
							Type? type = null;
							try {
								type = assembly.GetType(objectName);
							}
							catch {
								type = null;
							}

							if (type != null) {
								object obj = (object)Activator.CreateInstance(type);

								object[] arguments;
								arguments = new object[1];
								arguments[0] = appSettings.Settings;

								try {
									type.InvokeMember(appSettings.ObjectEntryPoint, BindingFlags.InvokeMethod, null, obj, arguments);
								}
								catch {
									_LogError(string.Format("Could Not Invoke Method '{0}' on Object '{1}'.  Arguments: '{2}', {3}", appSettings.ObjectEntryPoint, objectName, args.Length > 0 ? args[0] : string.Empty, args.Length > 1 ? args[1] : string.Empty));
								}
							}
							else
								_LogError(string.Format("Object '{0}' could not be found. Arguments: '{1}', {2}", objectName, args.Length > 0 ? args[0] : string.Empty, args.Length > 1 ? args[1] : string.Empty));
						}
						else
							_LogError(string.Format("Assembly '{0}' could not be found. Arguments: '{1}', {2}", appSettings.AssemblyName, args.Length > 0 ? args[0] : string.Empty, args.Length > 1 ? args[1] : string.Empty));
					}
					else if (appSettings != null)
						_LogError(appSettings.ErrorMessage);
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}

		public static void SFtpDownload()
		{
			try {
				// The address test.rebex.net is used for testing Rebex components (https://www.rebex.net). It is read only.
				SftpClient sftpClient = new SftpClient("test.rebex.net", "demo", "password");

				sftpClient.Connect();

				if (sftpClient.IsConnected) {
					IEnumerable<SftpFile> files = sftpClient.ListDirectory("/");

					foreach (SftpFile file in files) {
						if (!file.IsDirectory) {
							string path = string.Format(@"C:\Data\Temp\{0}", file.Name);

							using (FileStream stream = new FileStream(path, FileMode.Create)) {
								sftpClient.DownloadFile(file.FullName, stream);
							}
						}
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}

		#endregion Public

		#region Private

		private static char _GetDelimiterChar(string delimiter)
		{
			char returnValue;

			switch (delimiter) {
				case "comma":
				case ",":
					returnValue = Convert.ToChar(",");
					break;

				case "tab":
				case "\t":
					returnValue = Convert.ToChar("\t");
					break;

				default:
					returnValue = Convert.ToChar(delimiter);
					break;
			}

			return returnValue;
		}

		private static EntityPrescriber _GetEntityPrescriber(string entityID, Models.NPI.Result result)
		{
			EntityPrescriber entityPrescriber = null;

			try {
				Entity entity = new Entity("localhost", "Selena", string.Empty, string.Empty);

				if (entity.Find(entityID, true)) {
					// Does a prescriber exist for the entity and NPI?
					entityPrescriber = entity.EntityPrescribers.Items.FirstOrDefault(x => x.PrescriberNPI == result.number);

					if (entityPrescriber == null) {
						entityPrescriber = new EntityPrescriber("localhost", "Selena", string.Empty, string.Empty)
						{
							EntityUID = entity.EntityUID,
							PrescriberNPI = result.number,
							PrescriberFirstName = result.basic.first_name,
							PrescriberLastName = result.basic.last_name,
							PrescriberTaxonomyDescription = result.taxonomies[0].desc
						};

						if (!entityPrescriber.Add(_updateUser, _updateApp)) {
						}
					}
				}
			}
			catch (Exception) {
				throw;
			}

			return entityPrescriber;
		}

		private static List<ManufacturerFileFormatColumn> _GetFileFormatColumns(string name, out char delimiter, out Dictionary<string, int> header)
		{
			List<ManufacturerFileFormatColumn> formatColumns = null;
			delimiter = ' ';
			header = new Dictionary<string, int>();

			try {
				ManufacturerFileFormat fileFormat = new ManufacturerFileFormat("localhost", "Selena", string.Empty, string.Empty);

				if (fileFormat.Find(name, true)) {
					delimiter = _GetDelimiterChar(fileFormat.Delimiter);

					string[] fields = Array.Empty<string>();

					// Split the header into fields using the FileFormat delimiter
					switch (delimiter) {
						case ',':
							//TODO: Add split csv
							break;

						default:
							fields = fileFormat.Header.Split(delimiter);
							break;
					}

					for (int i = 0; i < fields.Length; i++)
						header.Add(fields[i], i);

					if (fileFormat.ManufacturerFileFormatColumns != null)
						formatColumns = fileFormat.ManufacturerFileFormatColumns.Items.Where(c => !string.IsNullOrEmpty(c.OutputColumnName)).OrderBy(f => f.Sequence).ToList();
				}
			}
			catch (Exception) {
				throw;
			}

			return formatColumns;
		}

		private static Prescriber _GetPrescriber(Models.NPI.Result result)
		{
			Prescriber prescriber = new Prescriber("localhost", "Selena", string.Empty, string.Empty);

			try {
				if (!prescriber.Find(result.number)) {
					prescriber.NPI = result.number;
					prescriber.FirstName = result.basic.first_name;
					prescriber.LastName = result.basic.last_name;

					if (!prescriber.Add(_updateUser, _updateApp)) {
					}
				}
			}
			catch (Exception) {
				throw;
			}

			return prescriber;
		}

		private static void _LogError(string msg)
		{
			FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
			string logFile = fileInfo.FullName.Replace(fileInfo.Extension, ".err");

			_routines.LogMessage(logFile, msg);
		}

		private static void _ProcessPharmacies(string manufacturer, long eligibilityBatchUID, Dictionary<int, Dictionary<string, string>> pharmacies, List<string> messages)
		{
			try {
				bool successful = false;
				string status = "add";

				foreach (KeyValuePair<int, Dictionary<string, string>> pharmacy in pharmacies) {
					ManufacturerPharmacyEligibilityFile eligibilityFile = new ManufacturerPharmacyEligibilityFile("localhost", "Selena", string.Empty, string.Empty)
					{
						ManufacturerPharmacyEligibilityBatchUID = eligibilityBatchUID,
						Manufacturer = manufacturer,
						ID340B = pharmacy.Value.FirstOrDefault(s => s.Key == "ID340B").Value,
						PharmacyID = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyID").Value,
						DEA = pharmacy.Value.FirstOrDefault(s => s.Key == "DEA").Value,
						PharmacyName = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyName").Value,
						PharmacyAddress1 = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyAddress1").Value,
						PharmacyAddress2 = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyAddress2").Value,
						PharmacyAddress3 = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyAddress3").Value,
						PharmacyCity = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyCity").Value,
						PharmacyState = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyState").Value,
						PharmacyZip = pharmacy.Value.FirstOrDefault(s => s.Key == "PharmacyZip").Value,
						RetailStatus = pharmacy.Value.FirstOrDefault(s => s.Key == "RetailStatus").Value,
						RetailTransmissionDate = _routines.ConvertString<DateTime?>(pharmacy.Value.FirstOrDefault(s => s.Key == "RetailTransmissionDate").Value)
					};

					// Search for an existing pharmacy by the manufacturer
					ManufacturerPharmacyEligibilityFile existingEligibilityFile = new ManufacturerPharmacyEligibilityFile("localhost", "Selena", string.Empty, string.Empty);

					if (existingEligibilityFile.Find(manufacturer, eligibilityFile.PharmacyID, true)) {
						// Do not compare the following list of properties
						List<string> exclude = new List<string>
						{
							"ManufacturerPharmacyEligibilityFileUID",
							"ManufacturerPharmacyEligibilityBatchUID",
							"ManufacturerPharmacyEligibilityFileID",
							"Version",
							"CreatedMachine",
							"CreatedApp",
							"CreatedDate",
							"CreatedBy",
							"UpdatedMachine",
							"UpdatedApp",
							"UpdatedDate",
							"UpdatedBy"
						};

						// Compare properties to see if different
						if (!existingEligibilityFile.PropertiesEqual(eligibilityFile, exclude)) {
							status = "update";

							// Copy values received in the current file into the existing object, and assign the new batch uid
							if (existingEligibilityFile.CopyProperties(eligibilityFile, exclude)) {
								existingEligibilityFile.ManufacturerPharmacyEligibilityBatchUID = eligibilityBatchUID;

								successful = existingEligibilityFile.Add(_updateUser, _updateApp);
							}
						}
						else
							successful = true;
					}
					else
						successful = eligibilityFile.Add(_updateUser, _updateApp);

					if (!successful)
						messages.Add(string.Format("Failed to {0} Manufacturer \"{1}\" Pharmacy Eligibility \"{2}\".", status, manufacturer, eligibilityFile.PharmacyName));
				}
			}
			catch (Exception) {
				throw;
			}
		}

		private static Dictionary<int, Dictionary<string, string>> _ReadData(int row, ExcelWorksheet excelWorksheet, List<ManufacturerFileFormatColumn> formatColumns, Dictionary<string, int> header)
		{
			Dictionary<int, Dictionary<string, string>> pharmacies = new Dictionary<int, Dictionary<string, string>>();

			try {
				int key = 0;
				char[] trimChars = { ' ' };

				// Read all rows in the worksheet until the value in the first column is empty
				while (!string.IsNullOrEmpty(_routines.GetCellValue<string>(excelWorksheet, row, 1, trimChars))) {
					Dictionary<string, string> pharmacy = new Dictionary<string, string>();

					foreach (ManufacturerFileFormatColumn formatColumn in formatColumns) {
						if (header.ContainsKey(formatColumn.InputColumnName)) {
							// Begin at top left cell and locate column where the value is equal to the input column name
							int column = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString() == formatColumn.InputColumnName).Start.Column;
							string value = _routines.GetCellValue<string>(excelWorksheet, row, column, trimChars);

							pharmacy.Add(formatColumn.OutputColumnName, value);
						}
					}

					if (pharmacy.Count > 0) {
						pharmacies.Add(key, pharmacy);
						key++;
					}

					// Move to next row in the worksheet
					row++;
				}
			}
			catch {
				pharmacies = null;
			}

			return pharmacies;
		}

		#endregion Private

		#endregion Methods
	}

	enum NDCSearchBy
	{
		BrandName,
		ProductNdc,
		PackageNdc
	}
}