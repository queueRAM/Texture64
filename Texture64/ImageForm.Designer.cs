namespace Texture64
{
    partial class ImageForm
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
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.statusStripFile = new System.Windows.Forms.ToolStripStatusLabel();
         this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
         this.toolStripCodec = new System.Windows.Forms.ToolStripComboBox();
         this.textHex = new System.Windows.Forms.TextBox();
         this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
         this.colorDialog1 = new System.Windows.Forms.ColorDialog();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.paletteFileLabel = new System.Windows.Forms.Label();
         this.splitOffsetText = new System.Windows.Forms.TextBox();
         this.splitLengthText = new System.Windows.Forms.TextBox();
         this.label6 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.splitPaletteCheck = new System.Windows.Forms.CheckBox();
         this.textPalette = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.fileListView = new System.Windows.Forms.ListView();
         this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
         this.statusStripLength = new System.Windows.Forms.ToolStripStatusLabel();
         this.prevFileButton = new System.Windows.Forms.Button();
         this.nextFileButton = new System.Windows.Forms.Button();
         this.offsetMinusButton = new System.Windows.Forms.Button();
         this.loadPaletteButton = new System.Windows.Forms.Button();
         this.splitMinusButton = new System.Windows.Forms.Button();
         this.palMinusButton = new System.Windows.Forms.Button();
         this.splitPlusButton = new System.Windows.Forms.Button();
         this.palPlusButton = new System.Windows.Forms.Button();
         this.offsetPlusButton = new System.Windows.Forms.Button();
         this.toolStripOpen = new System.Windows.Forms.ToolStripButton();
         this.bgColorButton = new System.Windows.Forms.ToolStripButton();
         this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
         this.gviewPalette = new Texture64.GraphicsViewer();
         this.graphicsViewerCustom = new Texture64.GraphicsViewer();
         this.graphicsViewerMap = new Texture64.GraphicsViewer();
         this.graphicsViewer8 = new Texture64.GraphicsViewer();
         this.graphicsViewer16 = new Texture64.GraphicsViewer();
         this.graphicsViewer32 = new Texture64.GraphicsViewer();
         this.graphicsViewer64 = new Texture64.GraphicsViewer();
         this.statusStrip1.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
         this.groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
         this.SuspendLayout();
         // 
         // statusStrip1
         // 
         this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripFile,
            this.toolStripStatusLabel1,
            this.statusStripLength});
         this.statusStrip1.Location = new System.Drawing.Point(0, 540);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(836, 22);
         this.statusStrip1.TabIndex = 0;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // statusStripFile
         // 
         this.statusStripFile.Name = "statusStripFile";
         this.statusStripFile.Size = new System.Drawing.Size(25, 17);
         this.statusStripFile.Text = "File";
         // 
         // vScrollBar1
         // 
         this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Left;
         this.vScrollBar1.Location = new System.Drawing.Point(0, 25);
         this.vScrollBar1.Maximum = 1024;
         this.vScrollBar1.Name = "vScrollBar1";
         this.vScrollBar1.Size = new System.Drawing.Size(17, 515);
         this.vScrollBar1.TabIndex = 2;
         this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
         // 
         // toolStrip1
         // 
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpen,
            this.toolStripLabel2,
            this.toolStripCodec,
            this.bgColorButton});
         this.toolStrip1.Location = new System.Drawing.Point(0, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(836, 25);
         this.toolStrip1.TabIndex = 1;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // toolStripLabel2
         // 
         this.toolStripLabel2.Name = "toolStripLabel2";
         this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
         this.toolStripLabel2.Text = "Codec:";
         // 
         // toolStripCodec
         // 
         this.toolStripCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.toolStripCodec.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.toolStripCodec.Items.AddRange(new object[] {
            "RGBA16",
            "RGBA32",
            "IA16",
            "IA8",
            "IA4",
            "I8",
            "I4",
            "CI8",
            "1bpp"});
         this.toolStripCodec.Name = "toolStripCodec";
         this.toolStripCodec.Size = new System.Drawing.Size(80, 25);
         this.toolStripCodec.SelectedIndexChanged += new System.EventHandler(this.toolStripCodec_SelectedIndexChanged);
         // 
         // textHex
         // 
         this.textHex.Location = new System.Drawing.Point(36, 29);
         this.textHex.Name = "textHex";
         this.textHex.Size = new System.Drawing.Size(112, 20);
         this.textHex.TabIndex = 8;
         this.textHex.Text = "000000";
         this.textHex.TextChanged += new System.EventHandler(this.textHex_TextChanged);
         // 
         // numericUpDownWidth
         // 
         this.numericUpDownWidth.Location = new System.Drawing.Point(199, 158);
         this.numericUpDownWidth.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
         this.numericUpDownWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
         this.numericUpDownWidth.Name = "numericUpDownWidth";
         this.numericUpDownWidth.Size = new System.Drawing.Size(44, 20);
         this.numericUpDownWidth.TabIndex = 10;
         this.numericUpDownWidth.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
         this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(155, 160);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(38, 13);
         this.label1.TabIndex = 11;
         this.label1.Text = "Width:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(249, 160);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(41, 13);
         this.label2.TabIndex = 12;
         this.label2.Text = "Height:";
         // 
         // numericUpDownHeight
         // 
         this.numericUpDownHeight.Location = new System.Drawing.Point(288, 158);
         this.numericUpDownHeight.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
         this.numericUpDownHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
         this.numericUpDownHeight.Name = "numericUpDownHeight";
         this.numericUpDownHeight.Size = new System.Drawing.Size(44, 20);
         this.numericUpDownHeight.TabIndex = 13;
         this.numericUpDownHeight.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
         this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.paletteFileLabel);
         this.groupBox1.Controls.Add(this.loadPaletteButton);
         this.groupBox1.Controls.Add(this.splitMinusButton);
         this.groupBox1.Controls.Add(this.palMinusButton);
         this.groupBox1.Controls.Add(this.splitPlusButton);
         this.groupBox1.Controls.Add(this.splitOffsetText);
         this.groupBox1.Controls.Add(this.splitLengthText);
         this.groupBox1.Controls.Add(this.label6);
         this.groupBox1.Controls.Add(this.label5);
         this.groupBox1.Controls.Add(this.splitPaletteCheck);
         this.groupBox1.Controls.Add(this.palPlusButton);
         this.groupBox1.Controls.Add(this.textPalette);
         this.groupBox1.Controls.Add(this.label4);
         this.groupBox1.Controls.Add(this.gviewPalette);
         this.groupBox1.Location = new System.Drawing.Point(425, 29);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(143, 357);
         this.groupBox1.TabIndex = 16;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "CI Palette";
         // 
         // paletteFileLabel
         // 
         this.paletteFileLabel.AutoSize = true;
         this.paletteFileLabel.Location = new System.Drawing.Point(7, 334);
         this.paletteFileLabel.Name = "paletteFileLabel";
         this.paletteFileLabel.Size = new System.Drawing.Size(60, 13);
         this.paletteFileLabel.TabIndex = 13;
         this.paletteFileLabel.Text = "Palette FIle";
         // 
         // splitOffsetText
         // 
         this.splitOffsetText.Enabled = false;
         this.splitOffsetText.Location = new System.Drawing.Point(67, 247);
         this.splitOffsetText.Name = "splitOffsetText";
         this.splitOffsetText.Size = new System.Drawing.Size(67, 20);
         this.splitOffsetText.TabIndex = 8;
         this.splitOffsetText.Text = "000000";
         this.splitOffsetText.TextChanged += new System.EventHandler(this.splitOffsetText_TextChanged);
         // 
         // splitLengthText
         // 
         this.splitLengthText.Enabled = false;
         this.splitLengthText.Location = new System.Drawing.Point(67, 224);
         this.splitLengthText.Name = "splitLengthText";
         this.splitLengthText.Size = new System.Drawing.Size(67, 20);
         this.splitLengthText.TabIndex = 7;
         this.splitLengthText.Text = "80";
         this.splitLengthText.TextChanged += new System.EventHandler(this.splitLengthText_TextChanged);
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(6, 228);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(57, 13);
         this.label6.TabIndex = 6;
         this.label6.Text = "Length: 0x";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(11, 250);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(52, 13);
         this.label5.TabIndex = 5;
         this.label5.Text = "Offset: 0x";
         // 
         // splitPaletteCheck
         // 
         this.splitPaletteCheck.AutoSize = true;
         this.splitPaletteCheck.Location = new System.Drawing.Point(6, 208);
         this.splitPaletteCheck.Name = "splitPaletteCheck";
         this.splitPaletteCheck.Size = new System.Drawing.Size(82, 17);
         this.splitPaletteCheck.TabIndex = 4;
         this.splitPaletteCheck.Text = "Split Palette";
         this.splitPaletteCheck.UseVisualStyleBackColor = true;
         this.splitPaletteCheck.CheckedChanged += new System.EventHandler(this.splitPaletteCheck_CheckedChanged);
         // 
         // textPalette
         // 
         this.textPalette.Location = new System.Drawing.Point(59, 17);
         this.textPalette.Name = "textPalette";
         this.textPalette.Size = new System.Drawing.Size(75, 20);
         this.textPalette.TabIndex = 2;
         this.textPalette.Text = "000000";
         this.textPalette.TextChanged += new System.EventHandler(this.textPalette_TextChanged);
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(7, 20);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(52, 13);
         this.label4.TabIndex = 1;
         this.label4.Text = "Offset: 0x";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(20, 32);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(18, 13);
         this.label3.TabIndex = 17;
         this.label3.Text = "0x";
         // 
         // fileListView
         // 
         this.fileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.fileListView.HideSelection = false;
         this.fileListView.Location = new System.Drawing.Point(575, 29);
         this.fileListView.Name = "fileListView";
         this.fileListView.Size = new System.Drawing.Size(249, 479);
         this.fileListView.TabIndex = 19;
         this.fileListView.UseCompatibleStateImageBehavior = false;
         this.fileListView.View = System.Windows.Forms.View.SmallIcon;
         this.fileListView.SelectedIndexChanged += new System.EventHandler(this.fileListView_SelectedIndexChanged);
         // 
         // toolStripStatusLabel1
         // 
         this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
         this.toolStripStatusLabel1.Size = new System.Drawing.Size(752, 17);
         this.toolStripStatusLabel1.Spring = true;
         // 
         // statusStripLength
         // 
         this.statusStripLength.Name = "statusStripLength";
         this.statusStripLength.Size = new System.Drawing.Size(44, 17);
         this.statusStripLength.Text = "Length";
         // 
         // prevFileButton
         // 
         this.prevFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.prevFileButton.Image = global::Texture64.Properties.Resources.arrow_180;
         this.prevFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.prevFileButton.Location = new System.Drawing.Point(668, 514);
         this.prevFileButton.Name = "prevFileButton";
         this.prevFileButton.Size = new System.Drawing.Size(75, 23);
         this.prevFileButton.TabIndex = 21;
         this.prevFileButton.Text = "Prev File";
         this.prevFileButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.prevFileButton.UseVisualStyleBackColor = true;
         this.prevFileButton.Click += new System.EventHandler(this.prevFileButton_Click);
         // 
         // nextFileButton
         // 
         this.nextFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.nextFileButton.Image = global::Texture64.Properties.Resources.arrow;
         this.nextFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.nextFileButton.Location = new System.Drawing.Point(749, 514);
         this.nextFileButton.Name = "nextFileButton";
         this.nextFileButton.Size = new System.Drawing.Size(75, 23);
         this.nextFileButton.TabIndex = 20;
         this.nextFileButton.Text = "Next File";
         this.nextFileButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.nextFileButton.UseVisualStyleBackColor = true;
         this.nextFileButton.Click += new System.EventHandler(this.nextFileButton_Click);
         // 
         // offsetMinusButton
         // 
         this.offsetMinusButton.Image = global::Texture64.Properties.Resources.minus;
         this.offsetMinusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.offsetMinusButton.Location = new System.Drawing.Point(338, 129);
         this.offsetMinusButton.Name = "offsetMinusButton";
         this.offsetMinusButton.Size = new System.Drawing.Size(72, 23);
         this.offsetMinusButton.TabIndex = 18;
         this.offsetMinusButton.Text = "Offset";
         this.offsetMinusButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.offsetMinusButton.UseVisualStyleBackColor = true;
         this.offsetMinusButton.Click += new System.EventHandler(this.offsetMinusButton_Click);
         // 
         // loadPaletteButton
         // 
         this.loadPaletteButton.Image = global::Texture64.Properties.Resources.folder_open_image;
         this.loadPaletteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.loadPaletteButton.Location = new System.Drawing.Point(6, 304);
         this.loadPaletteButton.Name = "loadPaletteButton";
         this.loadPaletteButton.Size = new System.Drawing.Size(75, 23);
         this.loadPaletteButton.TabIndex = 12;
         this.loadPaletteButton.Text = "Load...";
         this.loadPaletteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.loadPaletteButton.UseVisualStyleBackColor = true;
         this.loadPaletteButton.Click += new System.EventHandler(this.loadPaletteButton_Click);
         // 
         // splitMinusButton
         // 
         this.splitMinusButton.Enabled = false;
         this.splitMinusButton.Image = global::Texture64.Properties.Resources.minus;
         this.splitMinusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.splitMinusButton.Location = new System.Drawing.Point(6, 275);
         this.splitMinusButton.Name = "splitMinusButton";
         this.splitMinusButton.Size = new System.Drawing.Size(61, 23);
         this.splitMinusButton.TabIndex = 11;
         this.splitMinusButton.Text = "Length";
         this.splitMinusButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.splitMinusButton.UseVisualStyleBackColor = true;
         this.splitMinusButton.Click += new System.EventHandler(this.splitMinusButton_Click);
         // 
         // palMinusButton
         // 
         this.palMinusButton.Image = global::Texture64.Properties.Resources.minus;
         this.palMinusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.palMinusButton.Location = new System.Drawing.Point(6, 44);
         this.palMinusButton.Name = "palMinusButton";
         this.palMinusButton.Size = new System.Drawing.Size(61, 23);
         this.palMinusButton.TabIndex = 10;
         this.palMinusButton.Text = "0x200";
         this.palMinusButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.palMinusButton.UseVisualStyleBackColor = true;
         this.palMinusButton.Click += new System.EventHandler(this.palMinusButton_Click);
         // 
         // splitPlusButton
         // 
         this.splitPlusButton.Enabled = false;
         this.splitPlusButton.Image = global::Texture64.Properties.Resources.plus;
         this.splitPlusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.splitPlusButton.Location = new System.Drawing.Point(73, 274);
         this.splitPlusButton.Name = "splitPlusButton";
         this.splitPlusButton.Size = new System.Drawing.Size(60, 23);
         this.splitPlusButton.TabIndex = 9;
         this.splitPlusButton.Text = "Length";
         this.splitPlusButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.splitPlusButton.UseVisualStyleBackColor = true;
         this.splitPlusButton.Click += new System.EventHandler(this.splitPlusButton_Click);
         // 
         // palPlusButton
         // 
         this.palPlusButton.Image = global::Texture64.Properties.Resources.plus;
         this.palPlusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.palPlusButton.Location = new System.Drawing.Point(73, 44);
         this.palPlusButton.Name = "palPlusButton";
         this.palPlusButton.Size = new System.Drawing.Size(61, 23);
         this.palPlusButton.TabIndex = 3;
         this.palPlusButton.Text = "0x200";
         this.palPlusButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.palPlusButton.UseVisualStyleBackColor = true;
         this.palPlusButton.Click += new System.EventHandler(this.palPlusButton_Click);
         // 
         // offsetPlusButton
         // 
         this.offsetPlusButton.Image = global::Texture64.Properties.Resources.plus;
         this.offsetPlusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.offsetPlusButton.Location = new System.Drawing.Point(338, 158);
         this.offsetPlusButton.Name = "offsetPlusButton";
         this.offsetPlusButton.Size = new System.Drawing.Size(72, 23);
         this.offsetPlusButton.TabIndex = 14;
         this.offsetPlusButton.Text = "Offset";
         this.offsetPlusButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.offsetPlusButton.UseVisualStyleBackColor = true;
         this.offsetPlusButton.Click += new System.EventHandler(this.offsetPlusButton_Click);
         // 
         // toolStripOpen
         // 
         this.toolStripOpen.Image = global::Texture64.Properties.Resources.folder_open_image;
         this.toolStripOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripOpen.Name = "toolStripOpen";
         this.toolStripOpen.Size = new System.Drawing.Size(56, 22);
         this.toolStripOpen.Text = "Open";
         this.toolStripOpen.Click += new System.EventHandler(this.toolStripOpen_Click);
         // 
         // bgColorButton
         // 
         this.bgColorButton.Image = global::Texture64.Properties.Resources.color;
         this.bgColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.bgColorButton.Name = "bgColorButton";
         this.bgColorButton.Size = new System.Drawing.Size(74, 22);
         this.bgColorButton.Text = "BG Color";
         this.bgColorButton.Click += new System.EventHandler(this.bgColorButton_Click);
         // 
         // gviewPalette
         // 
         this.gviewPalette.BaseFileName = null;
         this.gviewPalette.Location = new System.Drawing.Point(6, 73);
         this.gviewPalette.Name = "gviewPalette";
         this.gviewPalette.PixHeight = 16;
         this.gviewPalette.PixScale = 8;
         this.gviewPalette.PixWidth = 16;
         this.gviewPalette.Size = new System.Drawing.Size(128, 128);
         this.gviewPalette.TabIndex = 0;
         // 
         // graphicsViewerCustom
         // 
         this.graphicsViewerCustom.BaseFileName = null;
         this.graphicsViewerCustom.Location = new System.Drawing.Point(154, 184);
         this.graphicsViewerCustom.Name = "graphicsViewerCustom";
         this.graphicsViewerCustom.PixHeight = 128;
         this.graphicsViewerCustom.PixScale = 2;
         this.graphicsViewerCustom.PixWidth = 128;
         this.graphicsViewerCustom.Size = new System.Drawing.Size(256, 256);
         this.graphicsViewerCustom.TabIndex = 9;
         // 
         // graphicsViewerMap
         // 
         this.graphicsViewerMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
         this.graphicsViewerMap.BaseFileName = null;
         this.graphicsViewerMap.Location = new System.Drawing.Point(20, 55);
         this.graphicsViewerMap.Name = "graphicsViewerMap";
         this.graphicsViewerMap.PixHeight = 200;
         this.graphicsViewerMap.PixScale = 2;
         this.graphicsViewerMap.PixWidth = 64;
         this.graphicsViewerMap.Size = new System.Drawing.Size(128, 482);
         this.graphicsViewerMap.TabIndex = 7;
         // 
         // graphicsViewer8
         // 
         this.graphicsViewer8.BaseFileName = null;
         this.graphicsViewer8.Location = new System.Drawing.Point(288, 137);
         this.graphicsViewer8.Name = "graphicsViewer8";
         this.graphicsViewer8.PixHeight = 8;
         this.graphicsViewer8.PixScale = 2;
         this.graphicsViewer8.PixWidth = 8;
         this.graphicsViewer8.Size = new System.Drawing.Size(16, 16);
         this.graphicsViewer8.TabIndex = 6;
         // 
         // graphicsViewer16
         // 
         this.graphicsViewer16.BaseFileName = null;
         this.graphicsViewer16.Location = new System.Drawing.Point(288, 95);
         this.graphicsViewer16.Name = "graphicsViewer16";
         this.graphicsViewer16.PixHeight = 16;
         this.graphicsViewer16.PixScale = 2;
         this.graphicsViewer16.PixWidth = 16;
         this.graphicsViewer16.Size = new System.Drawing.Size(32, 32);
         this.graphicsViewer16.TabIndex = 5;
         // 
         // graphicsViewer32
         // 
         this.graphicsViewer32.BaseFileName = null;
         this.graphicsViewer32.Location = new System.Drawing.Point(288, 25);
         this.graphicsViewer32.Name = "graphicsViewer32";
         this.graphicsViewer32.PixHeight = 32;
         this.graphicsViewer32.PixScale = 2;
         this.graphicsViewer32.PixWidth = 32;
         this.graphicsViewer32.Size = new System.Drawing.Size(64, 64);
         this.graphicsViewer32.TabIndex = 4;
         // 
         // graphicsViewer64
         // 
         this.graphicsViewer64.BaseFileName = null;
         this.graphicsViewer64.Location = new System.Drawing.Point(154, 25);
         this.graphicsViewer64.Name = "graphicsViewer64";
         this.graphicsViewer64.PixHeight = 64;
         this.graphicsViewer64.PixScale = 2;
         this.graphicsViewer64.PixWidth = 64;
         this.graphicsViewer64.Size = new System.Drawing.Size(128, 128);
         this.graphicsViewer64.TabIndex = 3;
         // 
         // ImageForm
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(836, 562);
         this.Controls.Add(this.prevFileButton);
         this.Controls.Add(this.nextFileButton);
         this.Controls.Add(this.fileListView);
         this.Controls.Add(this.offsetMinusButton);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.offsetPlusButton);
         this.Controls.Add(this.numericUpDownHeight);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.numericUpDownWidth);
         this.Controls.Add(this.graphicsViewerCustom);
         this.Controls.Add(this.textHex);
         this.Controls.Add(this.graphicsViewerMap);
         this.Controls.Add(this.graphicsViewer8);
         this.Controls.Add(this.graphicsViewer16);
         this.Controls.Add(this.graphicsViewer32);
         this.Controls.Add(this.graphicsViewer64);
         this.Controls.Add(this.vScrollBar1);
         this.Controls.Add(this.toolStrip1);
         this.Controls.Add(this.statusStrip1);
         this.Name = "ImageForm";
         this.Text = "Texture64";
         this.ResizeEnd += new System.EventHandler(this.ImageForm_ResizeEnd);
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageForm_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImageForm_DragEnter);
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripFile;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.ToolStripButton toolStripOpen;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private GraphicsViewer graphicsViewer64;
        private GraphicsViewer graphicsViewer32;
        private GraphicsViewer graphicsViewer16;
        private GraphicsViewer graphicsViewer8;
        private GraphicsViewer graphicsViewerMap;
        private System.Windows.Forms.TextBox textHex;
        private GraphicsViewer graphicsViewerCustom;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.ToolStripComboBox toolStripCodec;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Button offsetPlusButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private GraphicsViewer gviewPalette;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textPalette;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button palPlusButton;
        private System.Windows.Forms.CheckBox splitPaletteCheck;
        private System.Windows.Forms.Button splitPlusButton;
        private System.Windows.Forms.TextBox splitOffsetText;
        private System.Windows.Forms.TextBox splitLengthText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton bgColorButton;
        private System.Windows.Forms.Button palMinusButton;
        private System.Windows.Forms.Button offsetMinusButton;
        private System.Windows.Forms.Button splitMinusButton;
        private System.Windows.Forms.Button loadPaletteButton;
        private System.Windows.Forms.Label paletteFileLabel;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.Button nextFileButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLength;
        private System.Windows.Forms.Button prevFileButton;
    }
}

