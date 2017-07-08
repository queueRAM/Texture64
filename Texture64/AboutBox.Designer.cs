namespace Texture64
{
   partial class AboutBox
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
         this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
         this.logoPictureBox = new System.Windows.Forms.PictureBox();
         this.labelProductName = new System.Windows.Forms.Label();
         this.labelVersion = new System.Windows.Forms.Label();
         this.labelCopyright = new System.Windows.Forms.Label();
         this.labelDescription = new System.Windows.Forms.Label();
         this.okButton = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.timerAnimate = new System.Windows.Forms.Timer(this.components);
         this.tableLayoutPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // tableLayoutPanel
         // 
         this.tableLayoutPanel.ColumnCount = 2;
         this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
         this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
         this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
         this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
         this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
         this.tableLayoutPanel.Controls.Add(this.labelDescription, 1, 3);
         this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
         this.tableLayoutPanel.Controls.Add(this.textBox1, 1, 4);
         this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
         this.tableLayoutPanel.Name = "tableLayoutPanel";
         this.tableLayoutPanel.RowCount = 6;
         this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel.Size = new System.Drawing.Size(446, 269);
         this.tableLayoutPanel.TabIndex = 0;
         // 
         // logoPictureBox
         // 
         this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
         this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
         this.logoPictureBox.Name = "logoPictureBox";
         this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
         this.logoPictureBox.Size = new System.Drawing.Size(34, 263);
         this.logoPictureBox.TabIndex = 12;
         this.logoPictureBox.TabStop = false;
         // 
         // labelProductName
         // 
         this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
         this.labelProductName.Location = new System.Drawing.Point(46, 0);
         this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
         this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
         this.labelProductName.Name = "labelProductName";
         this.labelProductName.Size = new System.Drawing.Size(397, 17);
         this.labelProductName.TabIndex = 19;
         this.labelProductName.Text = "Product Name";
         this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelVersion
         // 
         this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
         this.labelVersion.Location = new System.Drawing.Point(46, 17);
         this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
         this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
         this.labelVersion.Name = "labelVersion";
         this.labelVersion.Size = new System.Drawing.Size(397, 17);
         this.labelVersion.TabIndex = 0;
         this.labelVersion.Text = "Version";
         this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCopyright
         // 
         this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
         this.labelCopyright.Location = new System.Drawing.Point(46, 34);
         this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
         this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
         this.labelCopyright.Name = "labelCopyright";
         this.labelCopyright.Size = new System.Drawing.Size(397, 17);
         this.labelCopyright.TabIndex = 21;
         this.labelCopyright.Text = "Copyright";
         this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDescription
         // 
         this.labelDescription.AutoSize = true;
         this.labelDescription.Location = new System.Drawing.Point(43, 51);
         this.labelDescription.Name = "labelDescription";
         this.labelDescription.Size = new System.Drawing.Size(60, 13);
         this.labelDescription.TabIndex = 25;
         this.labelDescription.Text = "Description";
         // 
         // okButton
         // 
         this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.okButton.Location = new System.Drawing.Point(368, 243);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(75, 23);
         this.okButton.TabIndex = 24;
         this.okButton.Text = "&OK";
         // 
         // textBox1
         // 
         this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBox1.Location = new System.Drawing.Point(43, 67);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.ReadOnly = true;
         this.textBox1.Size = new System.Drawing.Size(400, 168);
         this.textBox1.TabIndex = 26;
         this.textBox1.Text = resources.GetString("textBox1.Text");
         // 
         // timerAnimate
         // 
         this.timerAnimate.Enabled = true;
         this.timerAnimate.Interval = 200;
         this.timerAnimate.Tick += new System.EventHandler(this.timerAnimate_Tick);
         // 
         // AboutBox
         // 
         this.AcceptButton = this.okButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(464, 287);
         this.Controls.Add(this.tableLayoutPanel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "AboutBox";
         this.Padding = new System.Windows.Forms.Padding(9);
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "AboutBox";
         this.tableLayoutPanel.ResumeLayout(false);
         this.tableLayoutPanel.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
      private System.Windows.Forms.PictureBox logoPictureBox;
      private System.Windows.Forms.Label labelProductName;
      private System.Windows.Forms.Label labelVersion;
      private System.Windows.Forms.Label labelCopyright;
      private System.Windows.Forms.Button okButton;
      private System.Windows.Forms.Timer timerAnimate;
      private System.Windows.Forms.Label labelDescription;
      private System.Windows.Forms.TextBox textBox1;
   }
}
