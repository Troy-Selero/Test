namespace DataTierGenerator
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.StoredProcedurePrefixLabel = new System.Windows.Forms.Label();
			this.GrantUserTextBox = new System.Windows.Forms.TextBox();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.StoredProcedurePrefixTextBox = new System.Windows.Forms.TextBox();
			this.GrantUserLabel = new System.Windows.Forms.Label();
			this.MultipleFilesCheckBox = new System.Windows.Forms.CheckBox();
			this.CsGroupBox = new System.Windows.Forms.GroupBox();
			this.DoNotModifyCodeOnlyCheckBox = new System.Windows.Forms.CheckBox();
			this.SqlGroupBox = new System.Windows.Forms.GroupBox();
			this.DirtyReadsCheckBox = new System.Windows.Forms.CheckBox();
			this.DropCheckBox = new System.Windows.Forms.CheckBox();
			this.GenerateButton = new System.Windows.Forms.Button();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.LoginNameTextBox = new System.Windows.Forms.TextBox();
			this.ServerTextBox = new System.Windows.Forms.TextBox();
			this.AuthenticationGroupBox = new System.Windows.Forms.GroupBox();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.LoginNameLabel = new System.Windows.Forms.Label();
			this.SQLServerAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
			this.WindowsAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
			this.DatabaseLabel = new System.Windows.Forms.Label();
			this.DatabaseTextBox = new System.Windows.Forms.TextBox();
			this.ServerLabel = new System.Windows.Forms.Label();
			this.DefaultsGroupBox = new System.Windows.Forms.GroupBox();
			this.DefaultTypeSystemRadioButton = new System.Windows.Forms.RadioButton();
			this.DefaultTypeDatabaseRadioButton = new System.Windows.Forms.RadioButton();
			this.OutputPathDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.OutputPathLabel = new System.Windows.Forms.Label();
			this.OutputPathTextBox = new System.Windows.Forms.TextBox();
			this.OutputPathButton = new System.Windows.Forms.Button();
			this.GeneratedPathLabel = new System.Windows.Forms.Label();
			this.GeneratedFullPathLabel = new System.Windows.Forms.Label();
			this.CsGroupBox.SuspendLayout();
			this.SqlGroupBox.SuspendLayout();
			this.AuthenticationGroupBox.SuspendLayout();
			this.DefaultsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// StoredProcedurePrefixLabel
			// 
			this.StoredProcedurePrefixLabel.Location = new System.Drawing.Point(20, 94);
			this.StoredProcedurePrefixLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.StoredProcedurePrefixLabel.Name = "StoredProcedurePrefixLabel";
			this.StoredProcedurePrefixLabel.Size = new System.Drawing.Size(110, 44);
			this.StoredProcedurePrefixLabel.TabIndex = 10;
			this.StoredProcedurePrefixLabel.Text = "Prefix:";
			this.StoredProcedurePrefixLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// GrantUserTextBox
			// 
			this.GrantUserTextBox.Location = new System.Drawing.Point(130, 46);
			this.GrantUserTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.GrantUserTextBox.Name = "GrantUserTextBox";
			this.GrantUserTextBox.Size = new System.Drawing.Size(337, 31);
			this.GrantUserTextBox.TabIndex = 6;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(30, 624);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(868, 44);
			this.ProgressBar.TabIndex = 20;
			// 
			// StoredProcedurePrefixTextBox
			// 
			this.StoredProcedurePrefixTextBox.Location = new System.Drawing.Point(130, 94);
			this.StoredProcedurePrefixTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.StoredProcedurePrefixTextBox.Name = "StoredProcedurePrefixTextBox";
			this.StoredProcedurePrefixTextBox.Size = new System.Drawing.Size(337, 31);
			this.StoredProcedurePrefixTextBox.TabIndex = 7;
			// 
			// GrantUserLabel
			// 
			this.GrantUserLabel.Location = new System.Drawing.Point(20, 46);
			this.GrantUserLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.GrantUserLabel.Name = "GrantUserLabel";
			this.GrantUserLabel.Size = new System.Drawing.Size(110, 44);
			this.GrantUserLabel.TabIndex = 8;
			this.GrantUserLabel.Text = "Grant User:";
			this.GrantUserLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// MultipleFilesCheckBox
			// 
			this.MultipleFilesCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.MultipleFilesCheckBox.Location = new System.Drawing.Point(24, 142);
			this.MultipleFilesCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.MultipleFilesCheckBox.Name = "MultipleFilesCheckBox";
			this.MultipleFilesCheckBox.Size = new System.Drawing.Size(360, 46);
			this.MultipleFilesCheckBox.TabIndex = 8;
			this.MultipleFilesCheckBox.Text = "Create multiple files for stored procedures";
			// 
			// CsGroupBox
			// 
			this.CsGroupBox.Controls.Add(this.DoNotModifyCodeOnlyCheckBox);
			this.CsGroupBox.Location = new System.Drawing.Point(9, 120);
			this.CsGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.CsGroupBox.Name = "CsGroupBox";
			this.CsGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.CsGroupBox.Size = new System.Drawing.Size(524, 98);
			this.CsGroupBox.TabIndex = 19;
			this.CsGroupBox.TabStop = false;
			this.CsGroupBox.Text = "C#";
			// 
			// DoNotModifyCodeOnlyCheckBox
			// 
			this.DoNotModifyCodeOnlyCheckBox.AutoSize = true;
			this.DoNotModifyCodeOnlyCheckBox.Location = new System.Drawing.Point(27, 48);
			this.DoNotModifyCodeOnlyCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DoNotModifyCodeOnlyCheckBox.Name = "DoNotModifyCodeOnlyCheckBox";
			this.DoNotModifyCodeOnlyCheckBox.Size = new System.Drawing.Size(324, 29);
			this.DoNotModifyCodeOnlyCheckBox.TabIndex = 17;
			this.DoNotModifyCodeOnlyCheckBox.Text = "Include \"Do Not Modify\" Code Only";
			this.DoNotModifyCodeOnlyCheckBox.UseVisualStyleBackColor = true;
			// 
			// SqlGroupBox
			// 
			this.SqlGroupBox.Controls.Add(this.DirtyReadsCheckBox);
			this.SqlGroupBox.Controls.Add(this.DropCheckBox);
			this.SqlGroupBox.Controls.Add(this.StoredProcedurePrefixTextBox);
			this.SqlGroupBox.Controls.Add(this.StoredProcedurePrefixLabel);
			this.SqlGroupBox.Controls.Add(this.GrantUserTextBox);
			this.SqlGroupBox.Controls.Add(this.GrantUserLabel);
			this.SqlGroupBox.Controls.Add(this.MultipleFilesCheckBox);
			this.SqlGroupBox.Location = new System.Drawing.Point(190, 240);
			this.SqlGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.SqlGroupBox.Name = "SqlGroupBox";
			this.SqlGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.SqlGroupBox.Size = new System.Drawing.Size(848, 279);
			this.SqlGroupBox.TabIndex = 17;
			this.SqlGroupBox.TabStop = false;
			this.SqlGroupBox.Text = "SQL";
			// 
			// DirtyReadsCheckBox
			// 
			this.DirtyReadsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.DirtyReadsCheckBox.Location = new System.Drawing.Point(24, 221);
			this.DirtyReadsCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.DirtyReadsCheckBox.Name = "DirtyReadsCheckBox";
			this.DirtyReadsCheckBox.Size = new System.Drawing.Size(360, 46);
			this.DirtyReadsCheckBox.TabIndex = 12;
			this.DirtyReadsCheckBox.Text = "Allow \"Dirty\" Reads";
			// 
			// DropCheckBox
			// 
			this.DropCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.DropCheckBox.Location = new System.Drawing.Point(24, 180);
			this.DropCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.DropCheckBox.Name = "DropCheckBox";
			this.DropCheckBox.Size = new System.Drawing.Size(360, 46);
			this.DropCheckBox.TabIndex = 11;
			this.DropCheckBox.Text = "Include dropping of objects";
			// 
			// GenerateButton
			// 
			this.GenerateButton.Enabled = false;
			this.GenerateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GenerateButton.Location = new System.Drawing.Point(922, 624);
			this.GenerateButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(116, 44);
			this.GenerateButton.TabIndex = 18;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.PasswordTextBox.Enabled = false;
			this.PasswordTextBox.Location = new System.Drawing.Point(170, 185);
			this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.PasswordChar = '*';
			this.PasswordTextBox.Size = new System.Drawing.Size(217, 31);
			this.PasswordTextBox.TabIndex = 5;
			this.PasswordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
			// 
			// LoginNameTextBox
			// 
			this.LoginNameTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.LoginNameTextBox.Enabled = false;
			this.LoginNameTextBox.Location = new System.Drawing.Point(170, 140);
			this.LoginNameTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.LoginNameTextBox.Name = "LoginNameTextBox";
			this.LoginNameTextBox.Size = new System.Drawing.Size(217, 31);
			this.LoginNameTextBox.TabIndex = 4;
			// 
			// ServerTextBox
			// 
			this.ServerTextBox.Location = new System.Drawing.Point(109, 22);
			this.ServerTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.ServerTextBox.Name = "ServerTextBox";
			this.ServerTextBox.Size = new System.Drawing.Size(424, 31);
			this.ServerTextBox.TabIndex = 13;
			this.ServerTextBox.TextChanged += new System.EventHandler(this.ServerTextBox_TextChanged);
			// 
			// AuthenticationGroupBox
			// 
			this.AuthenticationGroupBox.Controls.Add(this.PasswordTextBox);
			this.AuthenticationGroupBox.Controls.Add(this.LoginNameTextBox);
			this.AuthenticationGroupBox.Controls.Add(this.PasswordLabel);
			this.AuthenticationGroupBox.Controls.Add(this.LoginNameLabel);
			this.AuthenticationGroupBox.Controls.Add(this.SQLServerAuthenticationRadioButton);
			this.AuthenticationGroupBox.Controls.Add(this.WindowsAuthenticationRadioButton);
			this.AuthenticationGroupBox.Location = new System.Drawing.Point(558, 0);
			this.AuthenticationGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.AuthenticationGroupBox.Name = "AuthenticationGroupBox";
			this.AuthenticationGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.AuthenticationGroupBox.Size = new System.Drawing.Size(480, 240);
			this.AuthenticationGroupBox.TabIndex = 16;
			this.AuthenticationGroupBox.TabStop = false;
			this.AuthenticationGroupBox.Text = "Authentication";
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.Enabled = false;
			this.PasswordLabel.Location = new System.Drawing.Point(50, 185);
			this.PasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(120, 44);
			this.PasswordLabel.TabIndex = 3;
			this.PasswordLabel.Text = "Password:";
			// 
			// LoginNameLabel
			// 
			this.LoginNameLabel.Enabled = false;
			this.LoginNameLabel.Location = new System.Drawing.Point(50, 140);
			this.LoginNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LoginNameLabel.Name = "LoginNameLabel";
			this.LoginNameLabel.Size = new System.Drawing.Size(120, 44);
			this.LoginNameLabel.TabIndex = 2;
			this.LoginNameLabel.Text = "Login Name:";
			// 
			// SQLServerAuthenticationRadioButton
			// 
			this.SQLServerAuthenticationRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SQLServerAuthenticationRadioButton.Location = new System.Drawing.Point(20, 86);
			this.SQLServerAuthenticationRadioButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.SQLServerAuthenticationRadioButton.Name = "SQLServerAuthenticationRadioButton";
			this.SQLServerAuthenticationRadioButton.Size = new System.Drawing.Size(260, 46);
			this.SQLServerAuthenticationRadioButton.TabIndex = 3;
			this.SQLServerAuthenticationRadioButton.Text = "SQL Server Authentication";
			this.SQLServerAuthenticationRadioButton.CheckedChanged += new System.EventHandler(this.SQLServerAuthenticationRadioButton_CheckedChanged);
			// 
			// WindowsAuthenticationRadioButton
			// 
			this.WindowsAuthenticationRadioButton.Checked = true;
			this.WindowsAuthenticationRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.WindowsAuthenticationRadioButton.Location = new System.Drawing.Point(20, 46);
			this.WindowsAuthenticationRadioButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.WindowsAuthenticationRadioButton.Name = "WindowsAuthenticationRadioButton";
			this.WindowsAuthenticationRadioButton.Size = new System.Drawing.Size(260, 46);
			this.WindowsAuthenticationRadioButton.TabIndex = 2;
			this.WindowsAuthenticationRadioButton.TabStop = true;
			this.WindowsAuthenticationRadioButton.Text = "Windows Authentication";
			this.WindowsAuthenticationRadioButton.CheckedChanged += new System.EventHandler(this.WindowsAuthenticationRadioButton_CheckedChanged);
			// 
			// DatabaseLabel
			// 
			this.DatabaseLabel.Location = new System.Drawing.Point(9, 69);
			this.DatabaseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DatabaseLabel.Name = "DatabaseLabel";
			this.DatabaseLabel.Size = new System.Drawing.Size(100, 44);
			this.DatabaseLabel.TabIndex = 14;
			this.DatabaseLabel.Text = "Database:";
			this.DatabaseLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// DatabaseTextBox
			// 
			this.DatabaseTextBox.Location = new System.Drawing.Point(109, 69);
			this.DatabaseTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.DatabaseTextBox.Name = "DatabaseTextBox";
			this.DatabaseTextBox.Size = new System.Drawing.Size(424, 31);
			this.DatabaseTextBox.TabIndex = 15;
			this.DatabaseTextBox.TextChanged += new System.EventHandler(this.DatabaseTextBox_TextChanged);
			// 
			// ServerLabel
			// 
			this.ServerLabel.Location = new System.Drawing.Point(9, 22);
			this.ServerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServerLabel.Name = "ServerLabel";
			this.ServerLabel.Size = new System.Drawing.Size(100, 44);
			this.ServerLabel.TabIndex = 12;
			this.ServerLabel.Text = "Server:";
			this.ServerLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// DefaultsGroupBox
			// 
			this.DefaultsGroupBox.Controls.Add(this.DefaultTypeSystemRadioButton);
			this.DefaultsGroupBox.Controls.Add(this.DefaultTypeDatabaseRadioButton);
			this.DefaultsGroupBox.Location = new System.Drawing.Point(13, 240);
			this.DefaultsGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DefaultsGroupBox.Name = "DefaultsGroupBox";
			this.DefaultsGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DefaultsGroupBox.Size = new System.Drawing.Size(151, 129);
			this.DefaultsGroupBox.TabIndex = 21;
			this.DefaultsGroupBox.TabStop = false;
			this.DefaultsGroupBox.Text = "Default Types";
			// 
			// DefaultTypeSystemRadioButton
			// 
			this.DefaultTypeSystemRadioButton.AutoSize = true;
			this.DefaultTypeSystemRadioButton.Location = new System.Drawing.Point(20, 75);
			this.DefaultTypeSystemRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DefaultTypeSystemRadioButton.Name = "DefaultTypeSystemRadioButton";
			this.DefaultTypeSystemRadioButton.Size = new System.Drawing.Size(94, 29);
			this.DefaultTypeSystemRadioButton.TabIndex = 2;
			this.DefaultTypeSystemRadioButton.Text = "System";
			this.DefaultTypeSystemRadioButton.UseVisualStyleBackColor = true;
			// 
			// DefaultTypeDatabaseRadioButton
			// 
			this.DefaultTypeDatabaseRadioButton.AutoSize = true;
			this.DefaultTypeDatabaseRadioButton.Checked = true;
			this.DefaultTypeDatabaseRadioButton.Location = new System.Drawing.Point(17, 32);
			this.DefaultTypeDatabaseRadioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DefaultTypeDatabaseRadioButton.Name = "DefaultTypeDatabaseRadioButton";
			this.DefaultTypeDatabaseRadioButton.Size = new System.Drawing.Size(111, 29);
			this.DefaultTypeDatabaseRadioButton.TabIndex = 0;
			this.DefaultTypeDatabaseRadioButton.TabStop = true;
			this.DefaultTypeDatabaseRadioButton.Text = "Database";
			this.DefaultTypeDatabaseRadioButton.UseVisualStyleBackColor = true;
			// 
			// OutputPathLabel
			// 
			this.OutputPathLabel.AutoSize = true;
			this.OutputPathLabel.Location = new System.Drawing.Point(51, 529);
			this.OutputPathLabel.Name = "OutputPathLabel";
			this.OutputPathLabel.Size = new System.Drawing.Size(112, 25);
			this.OutputPathLabel.TabIndex = 30;
			this.OutputPathLabel.Text = "Output Path:";
			this.OutputPathLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// OutputPathTextBox
			// 
			this.OutputPathTextBox.Location = new System.Drawing.Point(168, 529);
			this.OutputPathTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.OutputPathTextBox.Name = "OutputPathTextBox";
			this.OutputPathTextBox.Size = new System.Drawing.Size(730, 31);
			this.OutputPathTextBox.TabIndex = 31;
			this.OutputPathTextBox.TextChanged += new System.EventHandler(this.OutputPathTextBox_TextChanged);
			// 
			// OutputPathButton
			// 
			this.OutputPathButton.Location = new System.Drawing.Point(922, 531);
			this.OutputPathButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.OutputPathButton.Name = "OutputPathButton";
			this.OutputPathButton.Size = new System.Drawing.Size(47, 42);
			this.OutputPathButton.TabIndex = 32;
			this.OutputPathButton.Text = "...";
			this.OutputPathButton.UseVisualStyleBackColor = true;
			this.OutputPathButton.Click += new System.EventHandler(this.OutputPathButton_Click);
			// 
			// GeneratedPathLabel
			// 
			this.GeneratedPathLabel.AutoSize = true;
			this.GeneratedPathLabel.Location = new System.Drawing.Point(20, 579);
			this.GeneratedPathLabel.Name = "GeneratedPathLabel";
			this.GeneratedPathLabel.Size = new System.Drawing.Size(136, 25);
			this.GeneratedPathLabel.TabIndex = 33;
			this.GeneratedPathLabel.Text = "Generated Path:";
			// 
			// GeneratedFullPathLabel
			// 
			this.GeneratedFullPathLabel.AutoSize = true;
			this.GeneratedFullPathLabel.ForeColor = System.Drawing.Color.Red;
			this.GeneratedFullPathLabel.Location = new System.Drawing.Point(167, 579);
			this.GeneratedFullPathLabel.Name = "GeneratedFullPathLabel";
			this.GeneratedFullPathLabel.Size = new System.Drawing.Size(46, 25);
			this.GeneratedFullPathLabel.TabIndex = 34;
			this.GeneratedFullPathLabel.Text = "Path";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1069, 685);
			this.Controls.Add(this.GeneratedFullPathLabel);
			this.Controls.Add(this.GeneratedPathLabel);
			this.Controls.Add(this.OutputPathButton);
			this.Controls.Add(this.OutputPathTextBox);
			this.Controls.Add(this.OutputPathLabel);
			this.Controls.Add(this.DefaultsGroupBox);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.CsGroupBox);
			this.Controls.Add(this.SqlGroupBox);
			this.Controls.Add(this.GenerateButton);
			this.Controls.Add(this.ServerTextBox);
			this.Controls.Add(this.AuthenticationGroupBox);
			this.Controls.Add(this.DatabaseLabel);
			this.Controls.Add(this.DatabaseTextBox);
			this.Controls.Add(this.ServerLabel);
			this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.Name = "Main";
			this.Text = "Data Tier Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.CsGroupBox.ResumeLayout(false);
			this.CsGroupBox.PerformLayout();
			this.SqlGroupBox.ResumeLayout(false);
			this.SqlGroupBox.PerformLayout();
			this.AuthenticationGroupBox.ResumeLayout(false);
			this.AuthenticationGroupBox.PerformLayout();
			this.DefaultsGroupBox.ResumeLayout(false);
			this.DefaultsGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		private void PasswordTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		private System.Windows.Forms.Label StoredProcedurePrefixLabel;
        private System.Windows.Forms.TextBox GrantUserTextBox;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.TextBox StoredProcedurePrefixTextBox;
        private System.Windows.Forms.Label GrantUserLabel;
        private System.Windows.Forms.CheckBox MultipleFilesCheckBox;
        private System.Windows.Forms.GroupBox CsGroupBox;
        private System.Windows.Forms.GroupBox SqlGroupBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox LoginNameTextBox;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.GroupBox AuthenticationGroupBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label LoginNameLabel;
        private System.Windows.Forms.RadioButton SQLServerAuthenticationRadioButton;
        private System.Windows.Forms.RadioButton WindowsAuthenticationRadioButton;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.CheckBox DropCheckBox;
        private System.Windows.Forms.CheckBox DirtyReadsCheckBox;
        private System.Windows.Forms.GroupBox DefaultsGroupBox;
        private System.Windows.Forms.RadioButton DefaultTypeSystemRadioButton;
        private System.Windows.Forms.RadioButton DefaultTypeDatabaseRadioButton;
        private System.Windows.Forms.CheckBox DoNotModifyCodeOnlyCheckBox;
		private System.Windows.Forms.FolderBrowserDialog OutputPathDialog;
		private System.Windows.Forms.Label OutputPathLabel;
		private System.Windows.Forms.TextBox OutputPathTextBox;
		private System.Windows.Forms.Button OutputPathButton;
		private System.Windows.Forms.Label GeneratedPathLabel;
		private System.Windows.Forms.Label GeneratedFullPathLabel;
	}
}

