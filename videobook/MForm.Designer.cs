namespace videobook
{
    partial class MForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MForm));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lbstatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.probar = new System.Windows.Forms.ProgressBar();
            this.kryptonLinkLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.btn_exit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_convert = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_customheight = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_customwidth = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txt_column = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_row = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_interval = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_bookauthor = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_booktitle = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txt_coverpath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txt_videopath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.top_menu_ctrl = new System.Windows.Forms.MenuStrip();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.top_menu_ctrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.lbstatus);
            this.kryptonPanel1.Controls.Add(this.probar);
            this.kryptonPanel1.Controls.Add(this.kryptonLinkLabel1);
            this.kryptonPanel1.Controls.Add(this.btn_exit);
            this.kryptonPanel1.Controls.Add(this.btn_convert);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 24);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(528, 454);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // lbstatus
            // 
            this.lbstatus.Location = new System.Drawing.Point(12, 426);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(50, 20);
            this.lbstatus.TabIndex = 16;
            this.lbstatus.Values.Text = "Status :";
            // 
            // probar
            // 
            this.probar.Location = new System.Drawing.Point(104, 395);
            this.probar.Name = "probar";
            this.probar.Size = new System.Drawing.Size(317, 23);
            this.probar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.probar.TabIndex = 6;
            this.probar.Visible = false;
            // 
            // kryptonLinkLabel1
            // 
            this.kryptonLinkLabel1.Location = new System.Drawing.Point(469, 426);
            this.kryptonLinkLabel1.Name = "kryptonLinkLabel1";
            this.kryptonLinkLabel1.Size = new System.Drawing.Size(44, 20);
            this.kryptonLinkLabel1.TabIndex = 5;
            this.kryptonLinkLabel1.Values.Text = "About";
            this.kryptonLinkLabel1.LinkClicked += new System.EventHandler(this.kryptonLinkLabel1_LinkClicked);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(12, 393);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(86, 25);
            this.btn_exit.TabIndex = 4;
            this.btn_exit.Values.Text = "E&xit";
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_convert
            // 
            this.btn_convert.Location = new System.Drawing.Point(427, 393);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(86, 25);
            this.btn_convert.TabIndex = 3;
            this.btn_convert.Values.Text = "&Convert";
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel10);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_customheight);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel9);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_customwidth);
            this.kryptonGroupBox1.Panel.Controls.Add(this.checkBox1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_column);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_row);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_interval);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_bookauthor);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_booktitle);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_coverpath);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt_videopath);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(503, 360);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Text = "Video to ePub Conversion";
            this.kryptonGroupBox1.Values.Heading = "Video to ePub Conversion";
            this.kryptonGroupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonGroupBox1_Paint);
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(8, 299);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(53, 20);
            this.kryptonLabel10.TabIndex = 22;
            this.kryptonLabel10.Values.Text = "Height :";
            // 
            // txt_customheight
            // 
            this.txt_customheight.Enabled = false;
            this.txt_customheight.Location = new System.Drawing.Point(99, 301);
            this.txt_customheight.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.txt_customheight.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txt_customheight.Name = "txt_customheight";
            this.txt_customheight.Size = new System.Drawing.Size(81, 22);
            this.txt_customheight.TabIndex = 21;
            this.txt_customheight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(8, 273);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel9.TabIndex = 20;
            this.kryptonLabel9.Values.Text = "Width :";
            // 
            // txt_customwidth
            // 
            this.txt_customwidth.Enabled = false;
            this.txt_customwidth.Location = new System.Drawing.Point(99, 273);
            this.txt_customwidth.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.txt_customwidth.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txt_customwidth.Name = "txt_customwidth";
            this.txt_customwidth.Size = new System.Drawing.Size(81, 22);
            this.txt_customwidth.TabIndex = 19;
            this.txt_customwidth.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(99, 247);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(116, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Custom Frame Size";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txt_column
            // 
            this.txt_column.Location = new System.Drawing.Point(304, 211);
            this.txt_column.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_column.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_column.Name = "txt_column";
            this.txt_column.Size = new System.Drawing.Size(81, 22);
            this.txt_column.TabIndex = 16;
            this.txt_column.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(197, 211);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(99, 20);
            this.kryptonLabel8.TabIndex = 15;
            this.kryptonLabel8.Values.Text = "Layout Column :";
            // 
            // txt_row
            // 
            this.txt_row.Location = new System.Drawing.Point(99, 211);
            this.txt_row.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_row.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_row.Name = "txt_row";
            this.txt_row.Size = new System.Drawing.Size(81, 22);
            this.txt_row.TabIndex = 14;
            this.txt_row.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(8, 211);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel7.TabIndex = 13;
            this.kryptonLabel7.Values.Text = "Layout Row :";
            this.kryptonLabel7.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel7_Paint);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(186, 178);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(78, 20);
            this.kryptonLabel6.TabIndex = 12;
            this.kryptonLabel6.Values.Text = "Milliseconds";
            // 
            // txt_interval
            // 
            this.txt_interval.Location = new System.Drawing.Point(99, 176);
            this.txt_interval.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.txt_interval.Minimum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.txt_interval.Name = "txt_interval";
            this.txt_interval.Size = new System.Drawing.Size(81, 22);
            this.txt_interval.TabIndex = 11;
            this.txt_interval.Value = new decimal(new int[] {
            4200,
            0,
            0,
            0});
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(8, 176);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(87, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "Time Interval :";
            // 
            // txt_bookauthor
            // 
            this.txt_bookauthor.Location = new System.Drawing.Point(99, 138);
            this.txt_bookauthor.Name = "txt_bookauthor";
            this.txt_bookauthor.Size = new System.Drawing.Size(315, 20);
            this.txt_bookauthor.TabIndex = 9;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(8, 139);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(89, 20);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Book Author  :";
            // 
            // txt_booktitle
            // 
            this.txt_booktitle.Location = new System.Drawing.Point(99, 101);
            this.txt_booktitle.Name = "txt_booktitle";
            this.txt_booktitle.Size = new System.Drawing.Size(315, 20);
            this.txt_booktitle.TabIndex = 7;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(8, 102);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Book Title :";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(420, 62);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(68, 25);
            this.kryptonButton2.TabIndex = 5;
            this.kryptonButton2.Values.Text = "B&rowse";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // txt_coverpath
            // 
            this.txt_coverpath.Location = new System.Drawing.Point(99, 63);
            this.txt_coverpath.Name = "txt_coverpath";
            this.txt_coverpath.Size = new System.Drawing.Size(315, 20);
            this.txt_coverpath.TabIndex = 4;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(8, 64);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Cover Image :";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(420, 24);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(68, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Values.Text = "&Browse";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // txt_videopath
            // 
            this.txt_videopath.Location = new System.Drawing.Point(99, 26);
            this.txt_videopath.Name = "txt_videopath";
            this.txt_videopath.Size = new System.Drawing.Size(315, 20);
            this.txt_videopath.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(8, 26);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Video File :";
            // 
            // top_menu_ctrl
            // 
            this.top_menu_ctrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.top_menu_ctrl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseToolStripMenuItem});
            this.top_menu_ctrl.Location = new System.Drawing.Point(0, 0);
            this.top_menu_ctrl.Name = "top_menu_ctrl";
            this.top_menu_ctrl.Size = new System.Drawing.Size(528, 24);
            this.top_menu_ctrl.TabIndex = 1;
            this.top_menu_ctrl.Text = "menuStrip1";
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.releaseLicenseToolStripMenuItem});
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.licenseToolStripMenuItem.Text = "&License";
            // 
            // releaseLicenseToolStripMenuItem
            // 
            this.releaseLicenseToolStripMenuItem.Name = "releaseLicenseToolStripMenuItem";
            this.releaseLicenseToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.releaseLicenseToolStripMenuItem.Text = "Release";
            this.releaseLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseLicenseToolStripMenuItem_Click);
            // 
            // MForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 478);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.top_menu_ctrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.top_menu_ctrl;
            this.MaximizeBox = false;
            this.Name = "MForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VideoBook";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.top_menu_ctrl.ResumeLayout(false);
            this.top_menu_ctrl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_coverpath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_videopath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txt_interval;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_bookauthor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_booktitle;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_convert;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txt_column;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txt_row;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_exit;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel kryptonLinkLabel1;
        private System.Windows.Forms.ProgressBar probar;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbstatus;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txt_customheight;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txt_customwidth;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.MenuStrip top_menu_ctrl;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseLicenseToolStripMenuItem;

    }
}

