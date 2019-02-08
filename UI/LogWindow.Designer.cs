namespace SyncWhole.UI
{
	partial class LogWindow
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
			this.btnExport = new System.Windows.Forms.Button();
			this.clmLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnClose = new System.Windows.Forms.Button();
			this.clmMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this._lstLog = new System.Windows.Forms.ListView();
			this.btnClear = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExport.Location = new System.Drawing.Point(93, 266);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 1;
			this.btnExport.Text = "Export to file";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Visible = false;
			// 
			// clmLevel
			// 
			this.clmLevel.Text = "";
			this.clmLevel.Width = 24;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(515, 266);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.CloseClick);
			// 
			// clmMessage
			// 
			this.clmMessage.Text = "Message";
			this.clmMessage.Width = 395;
			// 
			// clmDateTime
			// 
			this.clmDateTime.Text = "Time";
			this.clmDateTime.Width = 134;
			// 
			// _lstLog
			// 
			this._lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmLevel,
            this.clmDateTime,
            this.clmMessage});
			this._lstLog.FullRowSelect = true;
			this._lstLog.Location = new System.Drawing.Point(12, 12);
			this._lstLog.Name = "_lstLog";
			this._lstLog.Size = new System.Drawing.Size(578, 248);
			this._lstLog.SmallImageList = this.imageList1;
			this._lstLog.TabIndex = 0;
			this._lstLog.UseCompatibleStateImageBehavior = false;
			this._lstLog.View = System.Windows.Forms.View.Details;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(12, 266);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.ClearClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "error");
			this.imageList1.Images.SetKeyName(1, "warning");
			this.imageList1.Images.SetKeyName(2, "info");
			this.imageList1.Images.SetKeyName(3, "debug");
			// 
			// LogWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(602, 301);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this._lstLog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(196, 196);
			this.Name = "LogWindow";
			this.Text = "Log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView _lstLog;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.ColumnHeader clmLevel;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ColumnHeader clmMessage;
		private System.Windows.Forms.ColumnHeader clmDateTime;
		private System.Windows.Forms.ImageList imageList1;
	}
}