namespace ScaleApp
{
    partial class WeighbridgeEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeighbridgeEntry));
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtgross = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txttare = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SLno = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ManifestNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Vtruck_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VGrossWt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VGross_DT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Net_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tareWeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VTare_DT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Commodity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TruckCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.reportButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBDTruckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dBUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bDTruckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBdTruckToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datewiseWeighbridgeExitReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.entryReportButton = new System.Windows.Forms.Button();
            this.exitReportButton = new System.Windows.Forms.Button();
            this.reportDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.cargo_entry_rpt_btn = new System.Windows.Forms.Button();
            this.yearly_entry_exit_rpt_btn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_month_report = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.yearPicker = new System.Windows.Forms.DateTimePicker();
            this.monthdtpicker = new System.Windows.Forms.DateTimePicker();
            this.goods_report_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SeaGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 20;
            this.button1.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(138, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.comboBox1.Location = new System.Drawing.Point(877, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(65, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "COM2";
            this.comboBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Navy;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(71, -3);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(99, 108);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(878, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "9600";
            this.textBox1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(439, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 105);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(239, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Scale Weight :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Manifest :";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Calisto MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(111, 150);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(147, 26);
            this.textBox2.TabIndex = 8;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyUp);
            // 
            // txtgross
            // 
            this.txtgross.BackColor = System.Drawing.Color.SlateGray;
            this.txtgross.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgross.ForeColor = System.Drawing.Color.Red;
            this.txtgross.Location = new System.Drawing.Point(126, 424);
            this.txtgross.Name = "txtgross";
            this.txtgross.ReadOnly = true;
            this.txtgross.Size = new System.Drawing.Size(132, 29);
            this.txtgross.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 424);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gross Weight :";
            // 
            // txttare
            // 
            this.txttare.BackColor = System.Drawing.Color.SlateGray;
            this.txttare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttare.ForeColor = System.Drawing.Color.Red;
            this.txttare.Location = new System.Drawing.Point(126, 473);
            this.txttare.Name = "txttare";
            this.txttare.ReadOnly = true;
            this.txttare.Size = new System.Drawing.Size(132, 29);
            this.txttare.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 473);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Tare Weight :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "....";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.PowderBlue;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SLno,
            this.ManifestNo,
            this.Vtruck_No,
            this.VGrossWt,
            this.VGross_DT,
            this.Net_Weight,
            this.tareWeight,
            this.VTare_DT,
            this.Commodity,
            this.TruckCategory});
            this.listView1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 183);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1171, 229);
            this.listView1.TabIndex = 61;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView1_DrawItem);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // SLno
            // 
            this.SLno.Text = "SL No";
            this.SLno.Width = 50;
            // 
            // ManifestNo
            // 
            this.ManifestNo.Text = "Manifest No";
            this.ManifestNo.Width = 140;
            // 
            // Vtruck_No
            // 
            this.Vtruck_No.Text = "Truck No";
            this.Vtruck_No.Width = 113;
            // 
            // VGrossWt
            // 
            this.VGrossWt.Text = "Loaded Truck";
            this.VGrossWt.Width = 113;
            // 
            // VGross_DT
            // 
            this.VGross_DT.Text = "Loaded Truck DT";
            this.VGross_DT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.VGross_DT.Width = 138;
            // 
            // Net_Weight
            // 
            this.Net_Weight.Text = "Goods Weight";
            this.Net_Weight.Width = 115;
            // 
            // tareWeight
            // 
            this.tareWeight.Text = "Empty Truck";
            this.tareWeight.Width = 119;
            // 
            // VTare_DT
            // 
            this.VTare_DT.Text = "Empty Truck DT";
            this.VTare_DT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.VTare_DT.Width = 138;
            // 
            // Commodity
            // 
            this.Commodity.Text = "Commodity";
            this.Commodity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Commodity.Width = 92;
            // 
            // TruckCategory
            // 
            this.TruckCategory.Text = "Truck Category";
            this.TruckCategory.Width = 147;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(290, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 19);
            this.label8.TabIndex = 66;
            this.label8.Text = "Truck No :";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Calisto MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(380, 151);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(125, 26);
            this.textBox3.TabIndex = 67;
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "In",
            "Out"});
            this.comboBox2.Location = new System.Drawing.Point(844, 147);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(80, 30);
            this.comboBox2.TabIndex = 68;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(694, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 19);
            this.label9.TabIndex = 69;
            this.label9.Text = "Movement Type :";
            // 
            // reportButton
            // 
            this.reportButton.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportButton.Location = new System.Drawing.Point(254, 523);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(80, 37);
            this.reportButton.TabIndex = 70;
            this.reportButton.Text = "Report";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(36, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 29);
            this.button3.TabIndex = 71;
            this.button3.Text = "Log out";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.bDTruckToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1273, 24);
            this.menuStrip1.TabIndex = 72;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBDTruckToolStripMenuItem,
            this.usToolStripMenuItem,
            this.dBUserToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.fileToolStripMenuItem.Text = "Form";
            // 
            // addBDTruckToolStripMenuItem
            // 
            this.addBDTruckToolStripMenuItem.Name = "addBDTruckToolStripMenuItem";
            this.addBDTruckToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addBDTruckToolStripMenuItem.Text = "Add BD Truck";
            this.addBDTruckToolStripMenuItem.Visible = false;
            this.addBDTruckToolStripMenuItem.Click += new System.EventHandler(this.addBDTruckToolStripMenuItem_Click);
            // 
            // usToolStripMenuItem
            // 
            this.usToolStripMenuItem.Name = "usToolStripMenuItem";
            this.usToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.usToolStripMenuItem.Text = "USER Creation";
            this.usToolStripMenuItem.Visible = false;
            this.usToolStripMenuItem.Click += new System.EventHandler(this.usrCreationToolStripMenuItem_Click);
            // 
            // dBUserToolStripMenuItem
            // 
            this.dBUserToolStripMenuItem.Name = "dBUserToolStripMenuItem";
            this.dBUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dBUserToolStripMenuItem.Text = "Encryption";
            this.dBUserToolStripMenuItem.Visible = false;
            this.dBUserToolStripMenuItem.Click += new System.EventHandler(this.dBUserToolStripMenuItem_Click);
            // 
            // bDTruckToolStripMenuItem
            // 
            this.bDTruckToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBdTruckToolStripMenuItem1});
            this.bDTruckToolStripMenuItem.Name = "bDTruckToolStripMenuItem";
            this.bDTruckToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.bDTruckToolStripMenuItem.Text = "BD Truck";
            // 
            // addBdTruckToolStripMenuItem1
            // 
            this.addBdTruckToolStripMenuItem1.Name = "addBdTruckToolStripMenuItem1";
            this.addBdTruckToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.addBdTruckToolStripMenuItem1.Text = "Add Bd Truck";
            this.addBdTruckToolStripMenuItem1.Click += new System.EventHandler(this.addBdTruckToolStripMenuItem1_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewReportToolStripMenuItem,
            this.datewiseWeighbridgeExitReportToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.reportToolStripMenuItem.Text = "Report";
            // 
            // viewReportToolStripMenuItem
            // 
            this.viewReportToolStripMenuItem.Name = "viewReportToolStripMenuItem";
            this.viewReportToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.viewReportToolStripMenuItem.Text = "Datewise Weighbridge Entry Report";
            this.viewReportToolStripMenuItem.Click += new System.EventHandler(this.viewReportToolStripMenuItem_Click);
            // 
            // datewiseWeighbridgeExitReportToolStripMenuItem
            // 
            this.datewiseWeighbridgeExitReportToolStripMenuItem.Name = "datewiseWeighbridgeExitReportToolStripMenuItem";
            this.datewiseWeighbridgeExitReportToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.datewiseWeighbridgeExitReportToolStripMenuItem.Text = "Datewise Weighbridge Exit Report";
            this.datewiseWeighbridgeExitReportToolStripMenuItem.Click += new System.EventHandler(this.datewiseWeighbridgeExitReportToolStripMenuItem_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(533, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 19);
            this.label10.TabIndex = 74;
            this.label10.Text = "Scale:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(836, 523);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 18);
            this.label12.TabIndex = 76;
            this.label12.Text = "Developed By:";
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.BackColor = System.Drawing.SystemColors.Control;
            this.scaleLabel.Font = new System.Drawing.Font("Lucida Bright", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.scaleLabel.Location = new System.Drawing.Point(589, 155);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(20, 22);
            this.scaleLabel.TabIndex = 78;
            this.scaleLabel.Text = "--";
            // 
            // entryReportButton
            // 
            this.entryReportButton.BackColor = System.Drawing.Color.Lime;
            this.entryReportButton.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryReportButton.Location = new System.Drawing.Point(945, 416);
            this.entryReportButton.Name = "entryReportButton";
            this.entryReportButton.Size = new System.Drawing.Size(99, 29);
            this.entryReportButton.TabIndex = 81;
            this.entryReportButton.Text = "Entry Report";
            this.entryReportButton.UseVisualStyleBackColor = false;
            this.entryReportButton.Click += new System.EventHandler(this.entryReportButton_Click);
            // 
            // exitReportButton
            // 
            this.exitReportButton.BackColor = System.Drawing.Color.Salmon;
            this.exitReportButton.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitReportButton.Location = new System.Drawing.Point(1047, 416);
            this.exitReportButton.Name = "exitReportButton";
            this.exitReportButton.Size = new System.Drawing.Size(91, 29);
            this.exitReportButton.TabIndex = 82;
            this.exitReportButton.Text = "Exit Report";
            this.exitReportButton.UseVisualStyleBackColor = false;
            this.exitReportButton.Click += new System.EventHandler(this.exitReportButton_Click);
            // 
            // reportDateTimePicker
            // 
            this.reportDateTimePicker.CustomFormat = "yyyy-MM-dd ";
            this.reportDateTimePicker.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.reportDateTimePicker.Location = new System.Drawing.Point(819, 416);
            this.reportDateTimePicker.Name = "reportDateTimePicker";
            this.reportDateTimePicker.Size = new System.Drawing.Size(118, 27);
            this.reportDateTimePicker.TabIndex = 83;
            // 
            // cargo_entry_rpt_btn
            // 
            this.cargo_entry_rpt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cargo_entry_rpt_btn.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cargo_entry_rpt_btn.ForeColor = System.Drawing.Color.White;
            this.cargo_entry_rpt_btn.Location = new System.Drawing.Point(934, 448);
            this.cargo_entry_rpt_btn.Name = "cargo_entry_rpt_btn";
            this.cargo_entry_rpt_btn.Size = new System.Drawing.Size(153, 37);
            this.cargo_entry_rpt_btn.TabIndex = 86;
            this.cargo_entry_rpt_btn.Text = "Cargo Entry Report";
            this.cargo_entry_rpt_btn.UseVisualStyleBackColor = false;
            this.cargo_entry_rpt_btn.Click += new System.EventHandler(this.cargo_entry_rpt_btn_Click);
            // 
            // yearly_entry_exit_rpt_btn
            // 
            this.yearly_entry_exit_rpt_btn.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearly_entry_exit_rpt_btn.Location = new System.Drawing.Point(680, 465);
            this.yearly_entry_exit_rpt_btn.Name = "yearly_entry_exit_rpt_btn";
            this.yearly_entry_exit_rpt_btn.Size = new System.Drawing.Size(54, 27);
            this.yearly_entry_exit_rpt_btn.TabIndex = 94;
            this.yearly_entry_exit_rpt_btn.Text = "View";
            this.yearly_entry_exit_rpt_btn.UseVisualStyleBackColor = true;
            this.yearly_entry_exit_rpt_btn.Click += new System.EventHandler(this.yearly_entry_exit_rpt_btn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(269, 470);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(198, 19);
            this.label13.TabIndex = 93;
            this.label13.Text = "Yearly Entry/Exit Report: ";
            // 
            // btn_month_report
            // 
            this.btn_month_report.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_month_report.Location = new System.Drawing.Point(680, 421);
            this.btn_month_report.Name = "btn_month_report";
            this.btn_month_report.Size = new System.Drawing.Size(54, 27);
            this.btn_month_report.TabIndex = 92;
            this.btn_month_report.Text = "View";
            this.btn_month_report.UseVisualStyleBackColor = true;
            this.btn_month_report.Click += new System.EventHandler(this.btn_month_report_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(269, 424);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(214, 19);
            this.label11.TabIndex = 91;
            this.label11.Text = "Monthly Entry/Exit Report: ";
            // 
            // yearPicker
            // 
            this.yearPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.yearPicker.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.yearPicker.Location = new System.Drawing.Point(483, 467);
            this.yearPicker.Name = "yearPicker";
            this.yearPicker.Size = new System.Drawing.Size(191, 26);
            this.yearPicker.TabIndex = 90;
            // 
            // monthdtpicker
            // 
            this.monthdtpicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.monthdtpicker.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthdtpicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.monthdtpicker.Location = new System.Drawing.Point(483, 422);
            this.monthdtpicker.Name = "monthdtpicker";
            this.monthdtpicker.Size = new System.Drawing.Size(191, 26);
            this.monthdtpicker.TabIndex = 89;
            // 
            // goods_report_btn
            // 
            this.goods_report_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.goods_report_btn.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goods_report_btn.ForeColor = System.Drawing.Color.White;
            this.goods_report_btn.Location = new System.Drawing.Point(781, 448);
            this.goods_report_btn.Name = "goods_report_btn";
            this.goods_report_btn.Size = new System.Drawing.Size(147, 37);
            this.goods_report_btn.TabIndex = 95;
            this.goods_report_btn.Text = "Goods Wise Report";
            this.goods_report_btn.UseVisualStyleBackColor = false;
            this.goods_report_btn.Click += new System.EventHandler(this.goods_report_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(942, 491);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 81);
            this.pictureBox1.TabIndex = 77;
            this.pictureBox1.TabStop = false;
            // 
            // WeighbridgeEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1273, 581);
            this.Controls.Add(this.goods_report_btn);
            this.Controls.Add(this.yearly_entry_exit_rpt_btn);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_month_report);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.yearPicker);
            this.Controls.Add(this.monthdtpicker);
            this.Controls.Add(this.cargo_entry_rpt_btn);
            this.Controls.Add(this.reportDateTimePicker);
            this.Controls.Add(this.exitReportButton);
            this.Controls.Add(this.entryReportButton);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txttare);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtgross);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WeighbridgeEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weigh Scale";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtgross;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txttare;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader SLno;
        private System.Windows.Forms.ColumnHeader ManifestNo;
        private System.Windows.Forms.ColumnHeader Vtruck_No;
        private System.Windows.Forms.ColumnHeader VGrossWt;
        private System.Windows.Forms.ColumnHeader VGross_DT;
        private System.Windows.Forms.ColumnHeader Net_Weight;
        private System.Windows.Forms.ColumnHeader VTare_DT;
        private System.Windows.Forms.ColumnHeader Commodity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBDTruckToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader TruckCategory;
        private System.Windows.Forms.ColumnHeader tareWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.Button entryReportButton;
        private System.Windows.Forms.Button exitReportButton;
        private System.Windows.Forms.DateTimePicker reportDateTimePicker;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datewiseWeighbridgeExitReportToolStripMenuItem;
        private System.Windows.Forms.Button cargo_entry_rpt_btn;
        private System.Windows.Forms.Button yearly_entry_exit_rpt_btn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_month_report;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker yearPicker;
        private System.Windows.Forms.DateTimePicker monthdtpicker;
        private System.Windows.Forms.Button goods_report_btn;
        private System.Windows.Forms.ToolStripMenuItem usToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dBUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bDTruckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBdTruckToolStripMenuItem1;
    }
}

