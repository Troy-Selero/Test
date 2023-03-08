using System;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DataTierGenerator
{
	public partial class Main : Form
	{
		#region Variables

		private const string Author = "Data Tier Generator";
		private readonly string _userFile = "DataTierGenerator.dat";

		#endregion Variables

		#region Properties

		#endregion Properties

		#region Constructors

		public Main()
		{
			InitializeComponent();

			Generator.DatabaseCounted += new CountUpdate(Generator_DatabaseCounted);
			Generator.TableCounted += new CountUpdate(Generator_TableCounted);

			_SetDefaults();
			_ReadUserData();
			_EnableGenerateButton();
			_SetToolTips();
		}

		#endregion Constructors

		#region Methods

		#region Public

		#endregion Public

		#region Private

		private void _EnableGenerateButton()
		{
			if (ServerTextBox.Text.Length == 0) {
				GenerateButton.Enabled = false;
				return;
			}

			if (DatabaseTextBox.Text.Length == 0) {
				GenerateButton.Enabled = false;
				return;
			}

			if (SQLServerAuthenticationRadioButton.Checked) {
				if (LoginNameTextBox.Text.Length == 0) {
					GenerateButton.Enabled = false;
					return;
				}
			}

			if (OutputPathTextBox.Text.Length == 0) {
				GenerateButton.Enabled = false;
				return;
			}

			GenerateButton.Enabled = true;
		}

		private void _ReadUserData()
		{
			IsolatedStorageFileStream userDataFile = null;
			StreamReader readStream = null;

			try {
				// Open the file in isolated storage (%LocalAppData%\IsolatedStorage)
				userDataFile = new IsolatedStorageFileStream(_userFile, FileMode.Open);

				readStream = new StreamReader(userDataFile);

				while (!readStream.EndOfStream) {
					string line = readStream.ReadLine();

					if (line.StartsWith("server="))
						ServerTextBox.Text = line.Replace("server=", string.Empty);
					else if (line.StartsWith("database="))
						DatabaseTextBox.Text = line.Replace("database=", string.Empty);
					else if (line.StartsWith("donotmodifyonly=")) {
						int.TryParse(line.Replace("donotmodifyonly=", string.Empty), out int doNotModifyOnly);

						if (doNotModifyOnly == 1)
							DoNotModifyCodeOnlyCheckBox.Checked = true;
					}
					else if (line.StartsWith("authenticationtype=")) {
						int.TryParse(line.Replace("authenticationtype=", string.Empty), out int authType);

						if (authType == 1)
							WindowsAuthenticationRadioButton.Checked = true;
						else
							SQLServerAuthenticationRadioButton.Checked = true;
					}
					else if (line.StartsWith("login="))
						LoginNameTextBox.Text = line.Replace("login=", string.Empty);
					else if (line.StartsWith("password="))
						PasswordTextBox.Text = line.Replace("password=", string.Empty);
					else if (line.StartsWith("defaulttypes=")) {
						string defaultsType = line.Replace("defaulttypes=", string.Empty);

						if (defaultsType.ToUpper() == "DATABASE")
							DefaultTypeDatabaseRadioButton.Checked = true;
						else
							DefaultTypeSystemRadioButton.Checked = true;
					}
					else if (line.StartsWith("grantuser="))
						GrantUserTextBox.Text = line.Replace("grantuser=", string.Empty);
					else if (line.StartsWith("storedprocedureprefix="))
						StoredProcedurePrefixTextBox.Text = line.Replace("storedprocedureprefix=", string.Empty);
					else if (line.StartsWith("createmultiplefiles=")) {
						int.TryParse(line.Replace("createmultiplefiles=", string.Empty), out int multipleFiles);

						if (multipleFiles == 1)
							MultipleFilesCheckBox.Checked = true;
					}
					else if (line.StartsWith("includedrop=")) {
						int.TryParse(line.Replace("includedrop=", string.Empty), out int includeDrop);

						if (includeDrop == 1)
							DropCheckBox.Checked = true;
					}
					else if (line.StartsWith("dirtyreads=")) {
						int.TryParse(line.Replace("dirtyreads=", string.Empty), out int dirtyReads);

						if (dirtyReads == 1)
							DirtyReadsCheckBox.Checked = true;
					}
					else if (line.StartsWith("outputpath="))
						OutputPathTextBox.Text = line.Replace("outputpath=", string.Empty);
					else if (line.StartsWith("generatedpath="))
						GeneratedFullPathLabel.Text = line.Replace("generatedpath=", string.Empty);
				}

				readStream.Close();
				userDataFile.Close();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
			finally {
				if (userDataFile != null)
					userDataFile.Close();
				if (readStream != null)
					readStream.Close();
			}
		}

		private void _SetDefaults()
		{
			ServerTextBox.Text = "localhost";
			DatabaseTextBox.Text = "Selero";
			GrantUserTextBox.Text = "public";
			StoredProcedurePrefixTextBox.Text = "csp_";
			MultipleFilesCheckBox.Checked = true;
			DropCheckBox.Checked = true;
			DirtyReadsCheckBox.Checked = true;
		}

		private void _SetToolTips()
		{
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(this.GrantUserTextBox, "grantUserTextBox");
			toolTip.SetToolTip(this.StoredProcedurePrefixTextBox, "storedProcedurePrefixTextBox");
		}

		private void _WriteUserData()
		{
			try {
				// Create the file in isolated storage (%LocalAppData%\IsolatedStorage)
				IsolatedStorageFileStream userDataFile = new IsolatedStorageFileStream(_userFile, FileMode.Create);

				// Write strings to the isolated storage file
				StreamWriter writeStream = new StreamWriter(userDataFile);
				writeStream.WriteLine(string.Format("author={0}", "Data Tier Generator"));
				writeStream.WriteLine(string.Format("server={0}", ServerTextBox.Text));
				writeStream.WriteLine(string.Format("database={0}", DatabaseTextBox.Text));
				writeStream.WriteLine(string.Format("donotmodifyonly={0}", DoNotModifyCodeOnlyCheckBox.Checked ? "1" : "0"));
				writeStream.WriteLine(string.Format("authenticationtype={0}", WindowsAuthenticationRadioButton.Checked ? "1" : "0"));
				writeStream.WriteLine(string.Format("login={0}", LoginNameTextBox.Text));
				writeStream.WriteLine(string.Format("password={0}", PasswordTextBox.Text));
				writeStream.WriteLine(string.Format("defaulttypes={0}", DefaultTypeDatabaseRadioButton.Checked ? "DATABASE" : "SYSTEM"));
				writeStream.WriteLine(string.Format("grantuser={0}", GrantUserTextBox.Text));
				writeStream.WriteLine(string.Format("storedprocedureprefix={0}", StoredProcedurePrefixTextBox.Text));
				writeStream.WriteLine(string.Format("createmultiplefiles={0}", MultipleFilesCheckBox.Checked ? "1" : "0"));
				writeStream.WriteLine(string.Format("includedrop={0}", DropCheckBox.Checked ? "1" : "0"));
				writeStream.WriteLine(string.Format("dirtyreads={0}", DirtyReadsCheckBox.Checked ? "1" : "0"));
				writeStream.WriteLine(string.Format("outputpath={0}", OutputPathTextBox.Text));
				writeStream.WriteLine(string.Format("generatedpath={0}", GeneratedFullPathLabel.Text));

				writeStream.Flush();
				writeStream.Close();
				userDataFile.Close();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		#endregion Private

		#endregion Methods

		#region Events

		private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return) {
				_EnableGenerateButton();
			}
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			try {
				GenerateButton.Enabled = false;

				// Build the connection string
				string connectionString;
				if (WindowsAuthenticationRadioButton.Checked)
					connectionString = "Server=" + ServerTextBox.Text + ";Database=" + DatabaseTextBox.Text + ";Trusted_Connection=true;";
				else
					connectionString = "Server=" + ServerTextBox.Text + ";Database=" + DatabaseTextBox.Text + ";User ID=" + LoginNameTextBox.Text + ";Password=" + PasswordTextBox.Text + ";";

				string defaultsType = DefaultTypeDatabaseRadioButton.Checked ? "DATABASE" : "SYSTEM";

				// Generate the SQL and C# code
				Generator.Generate(GeneratedFullPathLabel.Text, connectionString, GrantUserTextBox.Text, StoredProcedurePrefixTextBox.Text, MultipleFilesCheckBox.Checked, DropCheckBox.Checked, Author, DirtyReadsCheckBox.Checked, defaultsType, DoNotModifyCodeOnlyCheckBox.Checked, ProgressBar);

				// Save the current state 
				_WriteUserData();

				ProgressBar.Value = 0;
				MessageBox.Show("C# Classes and Stored Procedures Generated Successfully.");
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
			finally {
				GenerateButton.Enabled = true;
			}
		}

		private void DatabaseTextBox_TextChanged(object sender, EventArgs e)
		{
			_EnableGenerateButton();
		}

		private void ServerTextBox_TextChanged(object sender, System.EventArgs e)
		{
			_EnableGenerateButton();
		}

		private void Generator_DatabaseCounted(object sender, CountEventArgs e)
		{
			ProgressBar.Maximum = e.Count;
		}

		private void Generator_TableCounted(object sender, CountEventArgs e)
		{
			ProgressBar.Value = e.Count;
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			_WriteUserData();
		}

		private void OutputPathButton_Click(object sender, EventArgs e)
		{
			OutputPathDialog.SelectedPath = string.IsNullOrEmpty(OutputPathTextBox.Text) ? "C:\\" : OutputPathTextBox.Text;
			OutputPathDialog.Description = "Select the Folder to Place Generated Code";

			if (OutputPathDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel) {
				OutputPathTextBox.Text = OutputPathDialog.SelectedPath;

				GeneratedFullPathLabel.Text = string.Format(@"{0}\{1}", OutputPathTextBox.Text, DatabaseTextBox.Text);

				DirectoryInfo directory = new DirectoryInfo(GeneratedFullPathLabel.Text);

				if (!directory.Exists)
					directory.Create();
			}
		}

		private void OutputPathTextBox_TextChanged(object sender, EventArgs e)
		{
			_EnableGenerateButton();
		}

		private void WindowsAuthenticationRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			LoginNameLabel.Enabled = false;
			LoginNameTextBox.Enabled = false;
			LoginNameTextBox.BackColor = SystemColors.InactiveBorder;
			LoginNameTextBox.Text = string.Empty;

			PasswordLabel.Enabled = false;
			PasswordTextBox.Enabled = false;
			PasswordTextBox.BackColor = SystemColors.InactiveBorder;
			PasswordTextBox.Text = string.Empty;

			_EnableGenerateButton();
		}

		private void SQLServerAuthenticationRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			LoginNameLabel.Enabled = true;
			LoginNameTextBox.Enabled = true;
			LoginNameTextBox.BackColor = SystemColors.Window;

			PasswordLabel.Enabled = true;
			PasswordTextBox.Enabled = true;
			PasswordTextBox.BackColor = SystemColors.Window;

			_EnableGenerateButton();
		}

		#endregion Events
	}
}