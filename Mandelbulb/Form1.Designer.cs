namespace Mandelbulb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.glControl1 = new OpenTK.GLControl();
            this.btnDraw = new System.Windows.Forms.Button();
            this.udPower = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udIterations = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnZoomOriginal = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomCube = new System.Windows.Forms.Button();
            this.tbCubeSize = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCubeZ = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCubeY = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCubeX = new System.Windows.Forms.TrackBar();
            this.chkShowSelectionCube = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.udPointsPerAxis = new System.Windows.Forms.NumericUpDown();
            this.btnColorize = new System.Windows.Forms.Button();
            this.chkAntialias = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.udPointWidth = new System.Windows.Forms.NumericUpDown();
            this.chkShowAxes = new System.Windows.Forms.CheckBox();
            this.chkShowFog = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnTransformQuads = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udIterations)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPointsPerAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPointWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 12);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(528, 524);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            // 
            // btnDraw
            // 
            this.btnDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDraw.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDraw.Location = new System.Drawing.Point(709, 12);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(94, 35);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "&Redraw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // udPower
            // 
            this.udPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udPower.Location = new System.Drawing.Point(633, 12);
            this.udPower.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.udPower.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udPower.Name = "udPower";
            this.udPower.Size = new System.Drawing.Size(66, 21);
            this.udPower.TabIndex = 2;
            this.udPower.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(586, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Power:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(569, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Iterations:";
            // 
            // udIterations
            // 
            this.udIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udIterations.Location = new System.Drawing.Point(633, 38);
            this.udIterations.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.udIterations.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udIterations.Name = "udIterations";
            this.udIterations.Size = new System.Drawing.Size(66, 21);
            this.udIterations.TabIndex = 4;
            this.udIterations.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnZoomOriginal);
            this.groupBox1.Controls.Add(this.btnZoomOut);
            this.groupBox1.Controls.Add(this.btnZoomCube);
            this.groupBox1.Controls.Add(this.tbCubeSize);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbCubeZ);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbCubeY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbCubeX);
            this.groupBox1.Controls.Add(this.chkShowSelectionCube);
            this.groupBox1.Location = new System.Drawing.Point(549, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 297);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection cube";
            // 
            // btnZoomOriginal
            // 
            this.btnZoomOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOriginal.Location = new System.Drawing.Point(84, 258);
            this.btnZoomOriginal.Name = "btnZoomOriginal";
            this.btnZoomOriginal.Size = new System.Drawing.Size(106, 27);
            this.btnZoomOriginal.TabIndex = 12;
            this.btnZoomOriginal.Text = "Original zoom";
            this.btnZoomOriginal.UseVisualStyleBackColor = true;
            this.btnZoomOriginal.Click += new System.EventHandler(this.btnZoomOriginal_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOut.Location = new System.Drawing.Point(84, 225);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(106, 27);
            this.btnZoomOut.TabIndex = 11;
            this.btnZoomOut.Text = "Zoom out";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomCube
            // 
            this.btnZoomCube.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomCube.Location = new System.Drawing.Point(84, 192);
            this.btnZoomCube.Name = "btnZoomCube";
            this.btnZoomCube.Size = new System.Drawing.Size(106, 27);
            this.btnZoomCube.TabIndex = 10;
            this.btnZoomCube.Text = "Zoom into cube";
            this.btnZoomCube.UseVisualStyleBackColor = true;
            this.btnZoomCube.Click += new System.EventHandler(this.btnZoomCube_Click);
            // 
            // tbCubeSize
            // 
            this.tbCubeSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCubeSize.LargeChange = 100;
            this.tbCubeSize.Location = new System.Drawing.Point(29, 153);
            this.tbCubeSize.Maximum = 1000;
            this.tbCubeSize.Minimum = 1;
            this.tbCubeSize.Name = "tbCubeSize";
            this.tbCubeSize.Size = new System.Drawing.Size(219, 45);
            this.tbCubeSize.TabIndex = 9;
            this.tbCubeSize.TickFrequency = 100;
            this.tbCubeSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbCubeSize.Value = 500;
            this.tbCubeSize.Scroll += new System.EventHandler(this.tbCubeSize_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Cube size:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(14, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Z:";
            // 
            // tbCubeZ
            // 
            this.tbCubeZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCubeZ.LargeChange = 100;
            this.tbCubeZ.Location = new System.Drawing.Point(29, 105);
            this.tbCubeZ.Maximum = 1000;
            this.tbCubeZ.Name = "tbCubeZ";
            this.tbCubeZ.Size = new System.Drawing.Size(219, 45);
            this.tbCubeZ.TabIndex = 6;
            this.tbCubeZ.TickFrequency = 100;
            this.tbCubeZ.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbCubeZ.Value = 500;
            this.tbCubeZ.Scroll += new System.EventHandler(this.tbCubeSize_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(14, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Y:";
            // 
            // tbCubeY
            // 
            this.tbCubeY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCubeY.LargeChange = 100;
            this.tbCubeY.Location = new System.Drawing.Point(29, 80);
            this.tbCubeY.Maximum = 1000;
            this.tbCubeY.Name = "tbCubeY";
            this.tbCubeY.Size = new System.Drawing.Size(219, 45);
            this.tbCubeY.TabIndex = 4;
            this.tbCubeY.TickFrequency = 100;
            this.tbCubeY.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbCubeY.Value = 500;
            this.tbCubeY.Scroll += new System.EventHandler(this.tbCubeSize_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(14, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cube position:";
            // 
            // tbCubeX
            // 
            this.tbCubeX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCubeX.LargeChange = 100;
            this.tbCubeX.Location = new System.Drawing.Point(29, 55);
            this.tbCubeX.Maximum = 1000;
            this.tbCubeX.Name = "tbCubeX";
            this.tbCubeX.Size = new System.Drawing.Size(219, 45);
            this.tbCubeX.TabIndex = 1;
            this.tbCubeX.TickFrequency = 100;
            this.tbCubeX.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbCubeX.Value = 500;
            this.tbCubeX.Scroll += new System.EventHandler(this.tbCubeSize_Scroll);
            // 
            // chkShowSelectionCube
            // 
            this.chkShowSelectionCube.AutoSize = true;
            this.chkShowSelectionCube.Location = new System.Drawing.Point(16, 19);
            this.chkShowSelectionCube.Name = "chkShowSelectionCube";
            this.chkShowSelectionCube.Size = new System.Drawing.Size(123, 17);
            this.chkShowSelectionCube.TabIndex = 0;
            this.chkShowSelectionCube.Text = "Show selection cube";
            this.chkShowSelectionCube.UseVisualStyleBackColor = true;
            this.chkShowSelectionCube.CheckedChanged += new System.EventHandler(this.chkShowSelectionCube_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(546, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Points per axis:";
            // 
            // udPointsPerAxis
            // 
            this.udPointsPerAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udPointsPerAxis.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udPointsPerAxis.Location = new System.Drawing.Point(633, 64);
            this.udPointsPerAxis.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udPointsPerAxis.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udPointsPerAxis.Name = "udPointsPerAxis";
            this.udPointsPerAxis.Size = new System.Drawing.Size(66, 21);
            this.udPointsPerAxis.TabIndex = 7;
            this.udPointsPerAxis.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // btnColorize
            // 
            this.btnColorize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorize.Location = new System.Drawing.Point(709, 120);
            this.btnColorize.Name = "btnColorize";
            this.btnColorize.Size = new System.Drawing.Size(94, 27);
            this.btnColorize.TabIndex = 11;
            this.btnColorize.Text = "Colorize points";
            this.btnColorize.UseVisualStyleBackColor = true;
            this.btnColorize.Click += new System.EventHandler(this.btnColorize_Click);
            // 
            // chkAntialias
            // 
            this.chkAntialias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAntialias.AutoSize = true;
            this.chkAntialias.Location = new System.Drawing.Point(633, 186);
            this.chkAntialias.Name = "chkAntialias";
            this.chkAntialias.Size = new System.Drawing.Size(66, 17);
            this.chkAntialias.TabIndex = 12;
            this.chkAntialias.Text = "Antialias";
            this.chkAntialias.UseVisualStyleBackColor = true;
            this.chkAntialias.CheckedChanged += new System.EventHandler(this.chkShowSelectionCube_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(563, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Point width:";
            // 
            // udPointWidth
            // 
            this.udPointWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udPointWidth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.udPointWidth.Location = new System.Drawing.Point(633, 120);
            this.udPointWidth.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.udPointWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPointWidth.Name = "udPointWidth";
            this.udPointWidth.Size = new System.Drawing.Size(66, 21);
            this.udPointWidth.TabIndex = 13;
            this.udPointWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udPointWidth.ValueChanged += new System.EventHandler(this.chkShowSelectionCube_CheckedChanged);
            // 
            // chkShowAxes
            // 
            this.chkShowAxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowAxes.AutoSize = true;
            this.chkShowAxes.Checked = true;
            this.chkShowAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowAxes.Location = new System.Drawing.Point(633, 166);
            this.chkShowAxes.Name = "chkShowAxes";
            this.chkShowAxes.Size = new System.Drawing.Size(78, 17);
            this.chkShowAxes.TabIndex = 15;
            this.chkShowAxes.Text = "Show axes";
            this.chkShowAxes.UseVisualStyleBackColor = true;
            this.chkShowAxes.CheckedChanged += new System.EventHandler(this.chkShowSelectionCube_CheckedChanged);
            // 
            // chkShowFog
            // 
            this.chkShowFog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowFog.AutoSize = true;
            this.chkShowFog.Checked = true;
            this.chkShowFog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowFog.Location = new System.Drawing.Point(633, 147);
            this.chkShowFog.Name = "chkShowFog";
            this.chkShowFog.Size = new System.Drawing.Size(71, 17);
            this.chkShowFog.TabIndex = 16;
            this.chkShowFog.Text = "Show fog";
            this.chkShowFog.UseVisualStyleBackColor = true;
            this.chkShowFog.CheckedChanged += new System.EventHandler(this.chkShowSelectionCube_CheckedChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(549, 513);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(61, 23);
            this.btnAbout.TabIndex = 17;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnTransformQuads
            // 
            this.btnTransformQuads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransformQuads.Location = new System.Drawing.Point(736, 176);
            this.btnTransformQuads.Name = "btnTransformQuads";
            this.btnTransformQuads.Size = new System.Drawing.Size(67, 27);
            this.btnTransformQuads.TabIndex = 18;
            this.btnTransformQuads.Text = "Yes";
            this.btnTransformQuads.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 548);
            this.Controls.Add(this.btnTransformQuads);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.chkShowFog);
            this.Controls.Add(this.chkShowAxes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.udPointWidth);
            this.Controls.Add(this.chkAntialias);
            this.Controls.Add(this.btnColorize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.udPointsPerAxis);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.udIterations);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.udPower);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.glControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.udPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udIterations)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCubeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPointsPerAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPointWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.NumericUpDown udPower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udIterations;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tbCubeZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tbCubeY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbCubeX;
        private System.Windows.Forms.CheckBox chkShowSelectionCube;
        private System.Windows.Forms.TrackBar tbCubeSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnZoomCube;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown udPointsPerAxis;
        private System.Windows.Forms.Button btnColorize;
        private System.Windows.Forms.CheckBox chkAntialias;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown udPointWidth;
        private System.Windows.Forms.Button btnZoomOriginal;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.CheckBox chkShowAxes;
        private System.Windows.Forms.CheckBox chkShowFog;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnTransformQuads;
    }
}