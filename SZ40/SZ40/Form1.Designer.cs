namespace SZ40
{
    partial class SZ40
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.gToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown10 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.save1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button3 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.textBox1.Location = new System.Drawing.Point(3, 3);
			this.textBox1.MaximumSize = new System.Drawing.Size(751, 813);
			this.textBox1.MinimumSize = new System.Drawing.Size(76, 163);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(344, 265);
			this.textBox1.TabIndex = 14;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.Location = new System.Drawing.Point(27, 25);
			this.maskedTextBox1.Mask = "00000";
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.Size = new System.Drawing.Size(48, 20);
			this.maskedTextBox1.TabIndex = 1;
			this.maskedTextBox1.ValidatingType = typeof(int);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(87, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "долговременный ключ";
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(738, 24);
			this.menuStrip1.TabIndex = 11;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// gToolStripMenuItem
			// 
			this.gToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.gToolStripMenuItem.Name = "gToolStripMenuItem";
			this.gToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
			this.gToolStripMenuItem.Text = "Действие";
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.resetToolStripMenuItem.Text = "Сбросить";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.clearToolStripMenuItem.Text = "Очистить";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.exitToolStripMenuItem.Text = "Выйти";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(489, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(166, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "начальные угловые положения";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(27, 54);
			this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            41,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown1.TabIndex = 2;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(64, 54);
			this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown2.TabIndex = 3;
			this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Location = new System.Drawing.Point(102, 54);
			this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown3.Maximum = new decimal(new int[] {
            29,
            0,
            0,
            0});
			this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown3.TabIndex = 4;
			this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Location = new System.Drawing.Point(140, 54);
			this.numericUpDown4.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown4.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
			this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown4.TabIndex = 5;
			this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Location = new System.Drawing.Point(178, 54);
			this.numericUpDown5.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown5.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this.numericUpDown5.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown5.TabIndex = 6;
			this.numericUpDown5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Location = new System.Drawing.Point(215, 54);
			this.numericUpDown6.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown6.Maximum = new decimal(new int[] {
            61,
            0,
            0,
            0});
			this.numericUpDown6.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown6.TabIndex = 7;
			this.numericUpDown6.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown7
			// 
			this.numericUpDown7.Location = new System.Drawing.Point(253, 54);
			this.numericUpDown7.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown7.Maximum = new decimal(new int[] {
            37,
            0,
            0,
            0});
			this.numericUpDown7.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown7.TabIndex = 8;
			this.numericUpDown7.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown8
			// 
			this.numericUpDown8.Location = new System.Drawing.Point(290, 54);
			this.numericUpDown8.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown8.Maximum = new decimal(new int[] {
            43,
            0,
            0,
            0});
			this.numericUpDown8.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown8.TabIndex = 9;
			this.numericUpDown8.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown9
			// 
			this.numericUpDown9.Location = new System.Drawing.Point(328, 54);
			this.numericUpDown9.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown9.Maximum = new decimal(new int[] {
            47,
            0,
            0,
            0});
			this.numericUpDown9.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown9.Name = "numericUpDown9";
			this.numericUpDown9.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown9.TabIndex = 10;
			this.numericUpDown9.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown10
			// 
			this.numericUpDown10.Location = new System.Drawing.Point(365, 54);
			this.numericUpDown10.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown10.Maximum = new decimal(new int[] {
            51,
            0,
            0,
            0});
			this.numericUpDown10.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown10.Name = "numericUpDown10";
			this.numericUpDown10.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown10.TabIndex = 11;
			this.numericUpDown10.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown11
			// 
			this.numericUpDown11.Location = new System.Drawing.Point(403, 54);
			this.numericUpDown11.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown11.Maximum = new decimal(new int[] {
            53,
            0,
            0,
            0});
			this.numericUpDown11.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown11.Name = "numericUpDown11";
			this.numericUpDown11.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown11.TabIndex = 12;
			this.numericUpDown11.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown12
			// 
			this.numericUpDown12.Location = new System.Drawing.Point(440, 54);
			this.numericUpDown12.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown12.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numericUpDown12.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown12.Name = "numericUpDown12";
			this.numericUpDown12.Size = new System.Drawing.Size(33, 20);
			this.numericUpDown12.TabIndex = 13;
			this.numericUpDown12.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.AutoEllipsis = true;
			this.button1.Location = new System.Drawing.Point(584, 274);
			this.button1.Margin = new System.Windows.Forms.Padding(236, 3, 3, 3);
			this.button1.MaximumSize = new System.Drawing.Size(110, 35);
			this.button1.Name = "button1";
			this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.button1.Size = new System.Drawing.Size(110, 35);
			this.button1.TabIndex = 18;
			this.button1.Text = "Сохранить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.saveButton2_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.textBox1);
			this.flowLayoutPanel1.Controls.Add(this.textBox2);
			this.flowLayoutPanel1.Controls.Add(this.save1);
			this.flowLayoutPanel1.Controls.Add(this.button2);
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(27, 76);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(702, 314);
			this.flowLayoutPanel1.TabIndex = 19;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.textBox2.Location = new System.Drawing.Point(353, 3);
			this.textBox2.MinimumSize = new System.Drawing.Size(76, 82);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(344, 265);
			this.textBox2.TabIndex = 19;
			// 
			// save1
			// 
			this.save1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.save1.Location = new System.Drawing.Point(112, 274);
			this.save1.Margin = new System.Windows.Forms.Padding(112, 3, 3, 3);
			this.save1.Name = "save1";
			this.save1.Size = new System.Drawing.Size(110, 35);
			this.save1.TabIndex = 16;
			this.save1.Text = "Сохранить";
			this.save1.UseVisualStyleBackColor = true;
			this.save1.Click += new System.EventHandler(this.saveButton1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(235, 274);
			this.button2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(110, 35);
			this.button2.TabIndex = 15;
			this.button2.Text = "Загрузить";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(275, 27);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(97, 17);
			this.checkBox1.TabIndex = 20;
			this.checkBox1.Text = "Расшифровка";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(542, 23);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 21;
			this.button3.Text = "Перевести";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// SZ40
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 424);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.numericUpDown12);
			this.Controls.Add(this.numericUpDown11);
			this.Controls.Add(this.numericUpDown10);
			this.Controls.Add(this.numericUpDown9);
			this.Controls.Add(this.numericUpDown8);
			this.Controls.Add(this.numericUpDown7);
			this.Controls.Add(this.numericUpDown6);
			this.Controls.Add(this.numericUpDown5);
			this.Controls.Add(this.numericUpDown4);
			this.Controls.Add(this.numericUpDown3);
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.maskedTextBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MaximumSize = new System.Drawing.Size(754, 463);
			this.MinimumSize = new System.Drawing.Size(754, 463);
			this.Name = "SZ40";
			this.Text = "SZ-40";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.NumericUpDown numericUpDown9;
        private System.Windows.Forms.NumericUpDown numericUpDown10;
        private System.Windows.Forms.NumericUpDown numericUpDown11;
        private System.Windows.Forms.NumericUpDown numericUpDown12;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button save1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button3;

    }
}

