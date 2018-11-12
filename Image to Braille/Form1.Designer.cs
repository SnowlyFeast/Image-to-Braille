namespace Image_to_Braille
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox_original = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox_masked = new System.Windows.Forms.GroupBox();
            this.pictureBox_masked = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label_sourceResolution = new System.Windows.Forms.Label();
            this.panel_resolution = new System.Windows.Forms.Panel();
            this.checkBox_steamFixed = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numeric_width = new System.Windows.Forms.NumericUpDown();
            this.checkBox_keepAspectRatio = new System.Windows.Forms.CheckBox();
            this.numeric_height = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_useOriginalSize = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox_quantized = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBox_result = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox_setting = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_maskFormat = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label_spacing = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_spacingChar = new System.Windows.Forms.ComboBox();
            this.checkBox_applyLaplacian = new System.Windows.Forms.CheckBox();
            this.button_genBraille = new System.Windows.Forms.Button();
            this.checkBox_inverseBW = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_previewScaling = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar_threshold = new System.Windows.Forms.TrackBar();
            this.numeric_threshold = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_loadImage = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_original)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox_masked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_masked)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.panel_resolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_height)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_quantized)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox_setting.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_threshold)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_original
            // 
            this.pictureBox_original.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_original.Location = new System.Drawing.Point(3, 21);
            this.pictureBox_original.Name = "pictureBox_original";
            this.pictureBox_original.Size = new System.Drawing.Size(450, 292);
            this.pictureBox_original.TabIndex = 1;
            this.pictureBox_original.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.pictureBox_original);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 316);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Original";
            // 
            // groupBox_masked
            // 
            this.groupBox_masked.AutoSize = true;
            this.groupBox_masked.Controls.Add(this.pictureBox_masked);
            this.groupBox_masked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_masked.Location = new System.Drawing.Point(465, 3);
            this.groupBox_masked.Name = "groupBox_masked";
            this.groupBox_masked.Size = new System.Drawing.Size(457, 316);
            this.groupBox_masked.TabIndex = 3;
            this.groupBox_masked.TabStop = false;
            this.groupBox_masked.Text = "Masked Image";
            // 
            // pictureBox_masked
            // 
            this.pictureBox_masked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_masked.Location = new System.Drawing.Point(3, 21);
            this.pictureBox_masked.Name = "pictureBox_masked";
            this.pictureBox_masked.Size = new System.Drawing.Size(451, 292);
            this.pictureBox_masked.TabIndex = 2;
            this.pictureBox_masked.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label_sourceResolution);
            this.groupBox5.Controls.Add(this.panel_resolution);
            this.groupBox5.Controls.Add(this.checkBox_useOriginalSize);
            this.groupBox5.Location = new System.Drawing.Point(6, 94);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(321, 205);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Resolution Settings";
            // 
            // label_sourceResolution
            // 
            this.label_sourceResolution.AutoSize = true;
            this.label_sourceResolution.Location = new System.Drawing.Point(6, 21);
            this.label_sourceResolution.Name = "label_sourceResolution";
            this.label_sourceResolution.Size = new System.Drawing.Size(85, 15);
            this.label_sourceResolution.TabIndex = 8;
            this.label_sourceResolution.Text = "#Resolution";
            // 
            // panel_resolution
            // 
            this.panel_resolution.Controls.Add(this.checkBox_steamFixed);
            this.panel_resolution.Controls.Add(this.label3);
            this.panel_resolution.Controls.Add(this.numeric_width);
            this.panel_resolution.Controls.Add(this.checkBox_keepAspectRatio);
            this.panel_resolution.Controls.Add(this.numeric_height);
            this.panel_resolution.Controls.Add(this.label1);
            this.panel_resolution.Controls.Add(this.label2);
            this.panel_resolution.Location = new System.Drawing.Point(6, 39);
            this.panel_resolution.Name = "panel_resolution";
            this.panel_resolution.Size = new System.Drawing.Size(312, 129);
            this.panel_resolution.TabIndex = 7;
            // 
            // checkBox_steamFixed
            // 
            this.checkBox_steamFixed.AutoSize = true;
            this.checkBox_steamFixed.Checked = true;
            this.checkBox_steamFixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_steamFixed.Location = new System.Drawing.Point(6, 99);
            this.checkBox_steamFixed.Name = "checkBox_steamFixed";
            this.checkBox_steamFixed.Size = new System.Drawing.Size(250, 19);
            this.checkBox_steamFixed.TabIndex = 9;
            this.checkBox_steamFixed.Text = "Fix to steam information box size";
            this.checkBox_steamFixed.UseVisualStyleBackColor = true;
            this.checkBox_steamFixed.CheckedChanged += new System.EventHandler(this.CheckBox_steamFixed_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "The width and height of the output will be a\r\nmultiple of four.";
            // 
            // numeric_width
            // 
            this.numeric_width.Enabled = false;
            this.numeric_width.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeric_width.Location = new System.Drawing.Point(50, 43);
            this.numeric_width.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.numeric_width.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numeric_width.Name = "numeric_width";
            this.numeric_width.Size = new System.Drawing.Size(100, 25);
            this.numeric_width.TabIndex = 0;
            this.numeric_width.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numeric_width.ValueChanged += new System.EventHandler(this.Numeric_width_ValueChanged);
            // 
            // checkBox_keepAspectRatio
            // 
            this.checkBox_keepAspectRatio.AutoSize = true;
            this.checkBox_keepAspectRatio.Checked = true;
            this.checkBox_keepAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_keepAspectRatio.Location = new System.Drawing.Point(6, 74);
            this.checkBox_keepAspectRatio.Name = "checkBox_keepAspectRatio";
            this.checkBox_keepAspectRatio.Size = new System.Drawing.Size(219, 19);
            this.checkBox_keepAspectRatio.TabIndex = 5;
            this.checkBox_keepAspectRatio.Text = "Keep the original aspect ratio";
            this.checkBox_keepAspectRatio.UseVisualStyleBackColor = true;
            this.checkBox_keepAspectRatio.CheckedChanged += new System.EventHandler(this.CheckBox_keepAspectRatio_CheckedChanged);
            // 
            // numeric_height
            // 
            this.numeric_height.Enabled = false;
            this.numeric_height.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeric_height.Location = new System.Drawing.Point(208, 43);
            this.numeric_height.Maximum = new decimal(new int[] {
            2160,
            0,
            0,
            0});
            this.numeric_height.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numeric_height.Name = "numeric_height";
            this.numeric_height.Size = new System.Drawing.Size(100, 25);
            this.numeric_height.TabIndex = 1;
            this.numeric_height.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numeric_height.ValueChanged += new System.EventHandler(this.Numeric_height_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // checkBox_useOriginalSize
            // 
            this.checkBox_useOriginalSize.AutoSize = true;
            this.checkBox_useOriginalSize.Location = new System.Drawing.Point(6, 174);
            this.checkBox_useOriginalSize.Name = "checkBox_useOriginalSize";
            this.checkBox_useOriginalSize.Size = new System.Drawing.Size(163, 19);
            this.checkBox_useOriginalSize.TabIndex = 6;
            this.checkBox_useOriginalSize.Text = "Use the original size";
            this.checkBox_useOriginalSize.UseVisualStyleBackColor = true;
            this.checkBox_useOriginalSize.CheckedChanged += new System.EventHandler(this.CheckBox_useOriginalSize_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.pictureBox_quantized);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 325);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(456, 317);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Quantized Image";
            // 
            // pictureBox_quantized
            // 
            this.pictureBox_quantized.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_quantized.Location = new System.Drawing.Point(3, 21);
            this.pictureBox_quantized.Name = "pictureBox_quantized";
            this.pictureBox_quantized.Size = new System.Drawing.Size(450, 293);
            this.pictureBox_quantized.TabIndex = 0;
            this.pictureBox_quantized.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.richTextBox_result);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(465, 325);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(457, 317);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output (Ctrl + C)";
            // 
            // richTextBox_result
            // 
            this.richTextBox_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_result.Location = new System.Drawing.Point(3, 21);
            this.richTextBox_result.Name = "richTextBox_result";
            this.richTextBox_result.Size = new System.Drawing.Size(451, 293);
            this.richTextBox_result.TabIndex = 0;
            this.richTextBox_result.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox_masked, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(925, 645);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox_setting);
            this.splitContainer1.Size = new System.Drawing.Size(1262, 645);
            this.splitContainer1.SplitterDistance = 925;
            this.splitContainer1.TabIndex = 7;
            // 
            // groupBox_setting
            // 
            this.groupBox_setting.Controls.Add(this.groupBox2);
            this.groupBox_setting.Controls.Add(this.groupBox8);
            this.groupBox_setting.Controls.Add(this.groupBox7);
            this.groupBox_setting.Controls.Add(this.groupBox6);
            this.groupBox_setting.Controls.Add(this.groupBox5);
            this.groupBox_setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_setting.Location = new System.Drawing.Point(0, 0);
            this.groupBox_setting.Name = "groupBox_setting";
            this.groupBox_setting.Size = new System.Drawing.Size(333, 645);
            this.groupBox_setting.TabIndex = 3;
            this.groupBox_setting.TabStop = false;
            this.groupBox_setting.Text = "Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.comboBox_maskFormat);
            this.groupBox2.Location = new System.Drawing.Point(6, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 63);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operation Format";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Masking format";
            // 
            // comboBox_maskFormat
            // 
            this.comboBox_maskFormat.FormattingEnabled = true;
            this.comboBox_maskFormat.Location = new System.Drawing.Point(121, 28);
            this.comboBox_maskFormat.Name = "comboBox_maskFormat";
            this.comboBox_maskFormat.Size = new System.Drawing.Size(183, 23);
            this.comboBox_maskFormat.TabIndex = 0;
            this.comboBox_maskFormat.SelectedIndexChanged += new System.EventHandler(this.ComboBox_maskFormat_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label_spacing);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.comboBox_spacingChar);
            this.groupBox8.Controls.Add(this.checkBox_applyLaplacian);
            this.groupBox8.Controls.Add(this.button_genBraille);
            this.groupBox8.Controls.Add(this.checkBox_inverseBW);
            this.groupBox8.Location = new System.Drawing.Point(6, 399);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(321, 169);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Generate options";
            // 
            // label_spacing
            // 
            this.label_spacing.AutoSize = true;
            this.label_spacing.Location = new System.Drawing.Point(142, 99);
            this.label_spacing.Name = "label_spacing";
            this.label_spacing.Size = new System.Drawing.Size(74, 15);
            this.label_spacing.TabIndex = 5;
            this.label_spacing.Text = "]Spacing[";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Spacing Character";
            // 
            // comboBox_spacingChar
            // 
            this.comboBox_spacingChar.FormattingEnabled = true;
            this.comboBox_spacingChar.Location = new System.Drawing.Point(6, 96);
            this.comboBox_spacingChar.Name = "comboBox_spacingChar";
            this.comboBox_spacingChar.Size = new System.Drawing.Size(130, 23);
            this.comboBox_spacingChar.TabIndex = 3;
            this.comboBox_spacingChar.SelectedIndexChanged += new System.EventHandler(this.ComboBox_spacingChar_SelectedIndexChanged);
            // 
            // checkBox_applyLaplacian
            // 
            this.checkBox_applyLaplacian.AutoSize = true;
            this.checkBox_applyLaplacian.Location = new System.Drawing.Point(7, 50);
            this.checkBox_applyLaplacian.Name = "checkBox_applyLaplacian";
            this.checkBox_applyLaplacian.Size = new System.Drawing.Size(183, 19);
            this.checkBox_applyLaplacian.TabIndex = 2;
            this.checkBox_applyLaplacian.Text = "Apply Sharpening Mask";
            this.checkBox_applyLaplacian.UseVisualStyleBackColor = true;
            this.checkBox_applyLaplacian.CheckedChanged += new System.EventHandler(this.CheckBox_applyLaplacian_CheckedChanged);
            // 
            // button_genBraille
            // 
            this.button_genBraille.Location = new System.Drawing.Point(191, 125);
            this.button_genBraille.Name = "button_genBraille";
            this.button_genBraille.Size = new System.Drawing.Size(124, 38);
            this.button_genBraille.TabIndex = 1;
            this.button_genBraille.Text = "Generate Braille";
            this.button_genBraille.UseVisualStyleBackColor = true;
            this.button_genBraille.Click += new System.EventHandler(this.Button_genBraille_Click);
            // 
            // checkBox_inverseBW
            // 
            this.checkBox_inverseBW.AutoSize = true;
            this.checkBox_inverseBW.Location = new System.Drawing.Point(7, 25);
            this.checkBox_inverseBW.Name = "checkBox_inverseBW";
            this.checkBox_inverseBW.Size = new System.Drawing.Size(182, 19);
            this.checkBox_inverseBW.TabIndex = 0;
            this.checkBox_inverseBW.Text = "Inverse black and white";
            this.checkBox_inverseBW.UseVisualStyleBackColor = true;
            this.checkBox_inverseBW.CheckedChanged += new System.EventHandler(this.CheckBox_inverseBW_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.comboBox_previewScaling);
            this.groupBox7.Location = new System.Drawing.Point(6, 574);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(321, 55);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Preview Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Scaling";
            // 
            // comboBox_previewScaling
            // 
            this.comboBox_previewScaling.FormattingEnabled = true;
            this.comboBox_previewScaling.Items.AddRange(new object[] {
            "Keep aspect ratio",
            "Fill",
            "Original Size"});
            this.comboBox_previewScaling.Location = new System.Drawing.Point(67, 18);
            this.comboBox_previewScaling.Name = "comboBox_previewScaling";
            this.comboBox_previewScaling.Size = new System.Drawing.Size(184, 23);
            this.comboBox_previewScaling.TabIndex = 0;
            this.comboBox_previewScaling.SelectedIndexChanged += new System.EventHandler(this.ComboBox_previewScaling_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.trackBar_threshold);
            this.groupBox6.Controls.Add(this.numeric_threshold);
            this.groupBox6.Location = new System.Drawing.Point(6, 305);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(321, 88);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Threshold";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "(0~255)";
            // 
            // trackBar_threshold
            // 
            this.trackBar_threshold.Location = new System.Drawing.Point(81, 24);
            this.trackBar_threshold.Maximum = 255;
            this.trackBar_threshold.Name = "trackBar_threshold";
            this.trackBar_threshold.Size = new System.Drawing.Size(234, 56);
            this.trackBar_threshold.TabIndex = 1;
            this.trackBar_threshold.Value = 128;
            this.trackBar_threshold.Scroll += new System.EventHandler(this.TrackBar_threshold_Scroll);
            // 
            // numeric_threshold
            // 
            this.numeric_threshold.Location = new System.Drawing.Point(9, 24);
            this.numeric_threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numeric_threshold.Name = "numeric_threshold";
            this.numeric_threshold.Size = new System.Drawing.Size(68, 25);
            this.numeric_threshold.TabIndex = 0;
            this.numeric_threshold.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numeric_threshold.ValueChanged += new System.EventHandler(this.Numeric_threshold_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1262, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_loadImage});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MenuItem_loadImage
            // 
            this.MenuItem_loadImage.Name = "MenuItem_loadImage";
            this.MenuItem_loadImage.ShortcutKeyDisplayString = "D";
            this.MenuItem_loadImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.MenuItem_loadImage.Size = new System.Drawing.Size(184, 26);
            this.MenuItem_loadImage.Text = "Load image";
            this.MenuItem_loadImage.ToolTipText = "Loads the selected image in Explorer.";
            this.MenuItem_loadImage.Click += new System.EventHandler(this.File_Load_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Image to Braille";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_original)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox_masked.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_masked)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel_resolution.ResumeLayout(false);
            this.panel_resolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_height)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_quantized)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox_setting.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_threshold)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox_original;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox_masked;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox_masked;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numeric_height;
        private System.Windows.Forms.NumericUpDown numeric_width;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBox_result;
        private System.Windows.Forms.PictureBox pictureBox_quantized;
        private System.Windows.Forms.Panel panel_resolution;
        private System.Windows.Forms.CheckBox checkBox_keepAspectRatio;
        private System.Windows.Forms.CheckBox checkBox_useOriginalSize;
        private System.Windows.Forms.GroupBox groupBox_setting;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox checkBox_inverseBW;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_previewScaling;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar_threshold;
        private System.Windows.Forms.NumericUpDown numeric_threshold;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_loadImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_maskFormat;
        private System.Windows.Forms.Label label_sourceResolution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_genBraille;
        private System.Windows.Forms.CheckBox checkBox_applyLaplacian;
        private System.Windows.Forms.CheckBox checkBox_steamFixed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_spacingChar;
        private System.Windows.Forms.Label label_spacing;
    }
}

