namespace SyncWhole.UI
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
			System.Windows.Forms.Label lblTimeout;
			System.Windows.Forms.Label lblNote;
			this._numTimeout = new System.Windows.Forms.NumericUpDown();
			this._btnForce = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			btnOk = new System.Windows.Forms.Button();
			grpSettings = new System.Windows.Forms.GroupBox();
			lblTimeout = new System.Windows.Forms.Label();
			lblNote = new System.Windows.Forms.Label();
			grpSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._numTimeout)).BeginInit();
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
			grpSettings.Controls.Add(lblTimeout);
			grpSettings.Controls.Add(this._numTimeout);
			grpSettings.Location = new System.Drawing.Point(12, 43);
			grpSettings.Name = "grpSettings";
			grpSettings.Size = new System.Drawing.Size(301, 248);
			grpSettings.TabIndex = 3;
			grpSettings.TabStop = false;
			grpSettings.Text = "General";
			// 
			// lblTimeout
			// 
			lblTimeout.AutoSize = true;
			lblTimeout.Location = new System.Drawing.Point(6, 21);
			lblTimeout.Name = "lblTimeout";
			lblTimeout.Size = new System.Drawing.Size(127, 13);
			lblTimeout.TabIndex = 4;
			lblTimeout.Text = "Update interval (minutes):";
			// 
			// _numTimeout
			// 
			this._numTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._numTimeout.Location = new System.Drawing.Point(139, 19);
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
			// SettingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 332);
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown _numTimeout;
		private System.Windows.Forms.Button _btnForce;
	}
}