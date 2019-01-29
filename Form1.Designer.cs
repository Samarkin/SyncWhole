namespace SyncWhole
{
	partial class Form1
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
			this._btnLoad = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this._lblDetails = new System.Windows.Forms.Label();
			this._cmbCalendars = new System.Windows.Forms.ComboBox();
			this._btnSync = new System.Windows.Forms.Button();
			this._btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _btnLoad
			// 
			this._btnLoad.Location = new System.Drawing.Point(289, 12);
			this._btnLoad.Name = "_btnLoad";
			this._btnLoad.Size = new System.Drawing.Size(75, 23);
			this._btnLoad.TabIndex = 0;
			this._btnLoad.Text = "Load";
			this._btnLoad.UseVisualStyleBackColor = true;
			this._btnLoad.Click += new System.EventHandler(this.ButtonClick);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(12, 41);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(352, 381);
			this.listBox1.TabIndex = 1;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBoxSelectionChanged);
			// 
			// _lblDetails
			// 
			this._lblDetails.AutoSize = true;
			this._lblDetails.Location = new System.Drawing.Point(370, 41);
			this._lblDetails.Name = "_lblDetails";
			this._lblDetails.Size = new System.Drawing.Size(16, 13);
			this._lblDetails.TabIndex = 2;
			this._lblDetails.Text = "...";
			// 
			// _cmbCalendars
			// 
			this._cmbCalendars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbCalendars.FormattingEnabled = true;
			this._cmbCalendars.Location = new System.Drawing.Point(12, 14);
			this._cmbCalendars.Name = "_cmbCalendars";
			this._cmbCalendars.Size = new System.Drawing.Size(271, 21);
			this._cmbCalendars.TabIndex = 3;
			// 
			// _btnSync
			// 
			this._btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnSync.Location = new System.Drawing.Point(713, 12);
			this._btnSync.Name = "_btnSync";
			this._btnSync.Size = new System.Drawing.Size(75, 23);
			this._btnSync.TabIndex = 4;
			this._btnSync.Text = "Sync!";
			this._btnSync.UseVisualStyleBackColor = true;
			this._btnSync.Click += new System.EventHandler(this.SyncClick);
			// 
			// _btnClear
			// 
			this._btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnClear.Location = new System.Drawing.Point(632, 12);
			this._btnClear.Name = "_btnClear";
			this._btnClear.Size = new System.Drawing.Size(75, 23);
			this._btnClear.TabIndex = 5;
			this._btnClear.Text = "Clear";
			this._btnClear.UseVisualStyleBackColor = true;
			this._btnClear.Click += new System.EventHandler(this.ClearClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this._btnClear);
			this.Controls.Add(this._btnSync);
			this.Controls.Add(this._cmbCalendars);
			this.Controls.Add(this._lblDetails);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this._btnLoad);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1Closing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label _lblDetails;
		private System.Windows.Forms.Button _btnLoad;
		private System.Windows.Forms.ComboBox _cmbCalendars;
		private System.Windows.Forms.Button _btnSync;
		private System.Windows.Forms.Button _btnClear;
	}
}

