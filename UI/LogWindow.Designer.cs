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
			System.Windows.Forms.ImageList imgList;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
			System.Windows.Forms.Button btnExport;
			System.Windows.Forms.ColumnHeader clmLevel;
			System.Windows.Forms.Button btnClose;
			System.Windows.Forms.ColumnHeader clmMessage;
			System.Windows.Forms.ColumnHeader clmDateTime;
			System.Windows.Forms.Button btnClear;
			this._lstLog = new System.Windows.Forms.ListView();
			imgList = new System.Windows.Forms.ImageList(this.components);
			btnExport = new System.Windows.Forms.Button();
			clmLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			btnClose = new System.Windows.Forms.Button();
			clmMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			clmDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// imgList
			// 
			imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
			imgList.TransparentColor = System.Drawing.Color.Transparent;
			imgList.Images.SetKeyName(0, "error");
			imgList.Images.SetKeyName(1, "warning");
			imgList.Images.SetKeyName(2, "info");
			imgList.Images.SetKeyName(3, "verbose");
			// 
			// btnExport
			// 
			btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			btnExport.Location = new System.Drawing.Point(93, 266);
			btnExport.Name = "btnExport";
			btnExport.Size = new System.Drawing.Size(75, 23);
			btnExport.TabIndex = 1;
			btnExport.Text = "Export to file";
			btnExport.UseVisualStyleBackColor = true;
			btnExport.Visible = false;
			// 
			// clmLevel
			// 
			clmLevel.Text = "";
			clmLevel.Width = 24;
			// 
			// btnClose
			// 
			btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			btnClose.Location = new System.Drawing.Point(515, 266);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(75, 23);
			btnClose.TabIndex = 2;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += new System.EventHandler(this.CloseClick);
			// 
			// clmMessage
			// 
			clmMessage.Text = "Message";
			clmMessage.Width = 395;
			// 
			// clmDateTime
			// 
			clmDateTime.Text = "Time";
			clmDateTime.Width = 134;
			// 
			// _lstLog
			// 
			this._lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clmLevel,
            clmDateTime,
            clmMessage});
			this._lstLog.FullRowSelect = true;
			this._lstLog.Location = new System.Drawing.Point(12, 12);
			this._lstLog.Name = "_lstLog";
			this._lstLog.Size = new System.Drawing.Size(578, 248);
			this._lstLog.SmallImageList = imgList;
			this._lstLog.TabIndex = 0;
			this._lstLog.UseCompatibleStateImageBehavior = false;
			this._lstLog.View = System.Windows.Forms.View.Details;
			// 
			// btnClear
			// 
			btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			btnClear.Location = new System.Drawing.Point(12, 266);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 23);
			btnClear.TabIndex = 3;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = true;
			btnClear.Click += new System.EventHandler(this.ClearClick);
			// 
			// LogWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(602, 301);
			this.Controls.Add(btnClear);
			this.Controls.Add(btnClose);
			this.Controls.Add(btnExport);
			this.Controls.Add(this._lstLog);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(196, 196);
			this.Name = "LogWindow";
			this.Text = "Log";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView _lstLog;
	}
}