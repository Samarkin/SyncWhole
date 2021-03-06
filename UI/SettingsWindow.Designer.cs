﻿namespace SyncWhole.UI
{
	partial class SettingsWindow
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
			if (disposing && (components != null))
			{
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
			System.Windows.Forms.Button btnCancel;
			System.Windows.Forms.Button btnOk;
			System.Windows.Forms.GroupBox grpSettings;
			System.Windows.Forms.Label lblDestination;
			System.Windows.Forms.Label lblSource;
			System.Windows.Forms.Label lblTimeout;
			System.Windows.Forms.Label lblNote;
			System.Windows.Forms.GroupBox grpLogging;
			System.Windows.Forms.Label lblLogLevel;
			this._cmbDestination = new System.Windows.Forms.ComboBox();
			this._cmbSource = new System.Windows.Forms.ComboBox();
			this._numTimeout = new System.Windows.Forms.NumericUpDown();
			this._btnForce = new System.Windows.Forms.Button();
			this._cmbLogLevel = new System.Windows.Forms.ComboBox();
			btnCancel = new System.Windows.Forms.Button();
			btnOk = new System.Windows.Forms.Button();
			grpSettings = new System.Windows.Forms.GroupBox();
			lblDestination = new System.Windows.Forms.Label();
			lblSource = new System.Windows.Forms.Label();
			lblTimeout = new System.Windows.Forms.Label();
			lblNote = new System.Windows.Forms.Label();
			grpLogging = new System.Windows.Forms.GroupBox();
			lblLogLevel = new System.Windows.Forms.Label();
			grpSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._numTimeout)).BeginInit();
			grpLogging.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			btnCancel.Location = new System.Drawing.Point(238, 297);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 23);
			btnCancel.TabIndex = 0;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += new System.EventHandler(this.CancelClick);
			// 
			// btnOk
			// 
			btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			btnOk.Location = new System.Drawing.Point(157, 297);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(75, 23);
			btnOk.TabIndex = 1;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += new System.EventHandler(this.OkClick);
			// 
			// grpSettings
			// 
			grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			grpSettings.Controls.Add(lblDestination);
			grpSettings.Controls.Add(this._cmbDestination);
			grpSettings.Controls.Add(this._cmbSource);
			grpSettings.Controls.Add(lblSource);
			grpSettings.Controls.Add(lblTimeout);
			grpSettings.Controls.Add(this._numTimeout);
			grpSettings.Location = new System.Drawing.Point(12, 43);
			grpSettings.Name = "grpSettings";
			grpSettings.Size = new System.Drawing.Size(301, 156);
			grpSettings.TabIndex = 3;
			grpSettings.TabStop = false;
			grpSettings.Text = "General";
			// 
			// lblDestination
			// 
			lblDestination.AutoSize = true;
			lblDestination.Location = new System.Drawing.Point(6, 49);
			lblDestination.Name = "lblDestination";
			lblDestination.Size = new System.Drawing.Size(63, 13);
			lblDestination.TabIndex = 8;
			lblDestination.Text = "Destination:";
			// 
			// _cmbDestination
			// 
			this._cmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbDestination.FormattingEnabled = true;
			this._cmbDestination.Location = new System.Drawing.Point(139, 46);
			this._cmbDestination.Name = "_cmbDestination";
			this._cmbDestination.Size = new System.Drawing.Size(156, 21);
			this._cmbDestination.TabIndex = 7;
			// 
			// _cmbSource
			// 
			this._cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbSource.FormattingEnabled = true;
			this._cmbSource.Location = new System.Drawing.Point(139, 19);
			this._cmbSource.Name = "_cmbSource";
			this._cmbSource.Size = new System.Drawing.Size(156, 21);
			this._cmbSource.TabIndex = 6;
			// 
			// lblSource
			// 
			lblSource.AutoSize = true;
			lblSource.Location = new System.Drawing.Point(6, 22);
			lblSource.Name = "lblSource";
			lblSource.Size = new System.Drawing.Size(44, 13);
			lblSource.TabIndex = 5;
			lblSource.Text = "Source:";
			// 
			// lblTimeout
			// 
			lblTimeout.AutoSize = true;
			lblTimeout.Location = new System.Drawing.Point(6, 75);
			lblTimeout.Name = "lblTimeout";
			lblTimeout.Size = new System.Drawing.Size(127, 13);
			lblTimeout.TabIndex = 4;
			lblTimeout.Text = "Update interval (minutes):";
			// 
			// _numTimeout
			// 
			this._numTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._numTimeout.Location = new System.Drawing.Point(139, 73);
			this._numTimeout.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
			this._numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._numTimeout.Name = "_numTimeout";
			this._numTimeout.Size = new System.Drawing.Size(156, 20);
			this._numTimeout.TabIndex = 3;
			this._numTimeout.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			// 
			// lblNote
			// 
			lblNote.Dock = System.Windows.Forms.DockStyle.Top;
			lblNote.ForeColor = System.Drawing.Color.DarkRed;
			lblNote.Location = new System.Drawing.Point(0, 0);
			lblNote.Name = "lblNote";
			lblNote.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			lblNote.Size = new System.Drawing.Size(325, 40);
			lblNote.TabIndex = 4;
			lblNote.Text = "NOTE: Synchronization is paused\r\nwhile this window is open";
			lblNote.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// _btnForce
			// 
			this._btnForce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnForce.Location = new System.Drawing.Point(12, 297);
			this._btnForce.Name = "_btnForce";
			this._btnForce.Size = new System.Drawing.Size(91, 23);
			this._btnForce.TabIndex = 6;
			this._btnForce.Text = "Force re-sync";
			this._btnForce.UseVisualStyleBackColor = true;
			this._btnForce.Click += new System.EventHandler(this.ForceClick);
			// 
			// grpLogging
			// 
			grpLogging.Controls.Add(lblLogLevel);
			grpLogging.Controls.Add(this._cmbLogLevel);
			grpLogging.Location = new System.Drawing.Point(12, 205);
			grpLogging.Name = "grpLogging";
			grpLogging.Size = new System.Drawing.Size(301, 86);
			grpLogging.TabIndex = 10;
			grpLogging.TabStop = false;
			grpLogging.Text = "Logging";
			// 
			// _cmbLogLevel
			// 
			this._cmbLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbLogLevel.FormattingEnabled = true;
			this._cmbLogLevel.Location = new System.Drawing.Point(139, 19);
			this._cmbLogLevel.Name = "_cmbLogLevel";
			this._cmbLogLevel.Size = new System.Drawing.Size(156, 21);
			this._cmbLogLevel.TabIndex = 10;
			// 
			// lblLogLevel
			// 
			lblLogLevel.AutoSize = true;
			lblLogLevel.Location = new System.Drawing.Point(6, 22);
			lblLogLevel.Name = "lblLogLevel";
			lblLogLevel.Size = new System.Drawing.Size(55, 13);
			lblLogLevel.TabIndex = 11;
			lblLogLevel.Text = "Max level:";
			// 
			// SettingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 332);
			this.Controls.Add(grpLogging);
			this.Controls.Add(this._btnForce);
			this.Controls.Add(lblNote);
			this.Controls.Add(grpSettings);
			this.Controls.Add(btnOk);
			this.Controls.Add(btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SettingsWindow";
			this.Text = "Settings";
			this.VisibleChanged += new System.EventHandler(this.VisibilityChanged);
			grpSettings.ResumeLayout(false);
			grpSettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._numTimeout)).EndInit();
			grpLogging.ResumeLayout(false);
			grpLogging.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown _numTimeout;
		private System.Windows.Forms.Button _btnForce;
		private System.Windows.Forms.ComboBox _cmbDestination;
		private System.Windows.Forms.ComboBox _cmbSource;
		private System.Windows.Forms.ComboBox _cmbLogLevel;
	}
}