namespace Texture64
{
   partial class TestForm
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
         this.comboBoxTest = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.buttonRun = new System.Windows.Forms.Button();
         this.textBoxLog = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // comboBoxTest
         // 
         this.comboBoxTest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTest.FormattingEnabled = true;
         this.comboBoxTest.Items.AddRange(new object[] {
            "All",
            "RGBA16",
            "RGBA32",
            "IA16",
            "IA8",
            "IA4",
            "I8",
            "I4",
            "CI8",
            "CI4",
            "1bpp"});
         this.comboBoxTest.Location = new System.Drawing.Point(50, 10);
         this.comboBoxTest.Name = "comboBoxTest";
         this.comboBoxTest.Size = new System.Drawing.Size(121, 21);
         this.comboBoxTest.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(13, 13);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(31, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "Test:";
         // 
         // buttonRun
         // 
         this.buttonRun.Location = new System.Drawing.Point(175, 8);
         this.buttonRun.Name = "buttonRun";
         this.buttonRun.Size = new System.Drawing.Size(75, 23);
         this.buttonRun.TabIndex = 2;
         this.buttonRun.Text = "Run";
         this.buttonRun.UseVisualStyleBackColor = true;
         this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
         // 
         // textBoxLog
         // 
         this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxLog.Location = new System.Drawing.Point(13, 124);
         this.textBoxLog.Multiline = true;
         this.textBoxLog.Name = "textBoxLog";
         this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxLog.Size = new System.Drawing.Size(377, 198);
         this.textBoxLog.TabIndex = 3;
         // 
         // TestForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(400, 334);
         this.Controls.Add(this.textBoxLog);
         this.Controls.Add(this.buttonRun);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.comboBoxTest);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "TestForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.Text = "Texture64 Tests";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ComboBox comboBoxTest;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button buttonRun;
      private System.Windows.Forms.TextBox textBoxLog;
   }
}