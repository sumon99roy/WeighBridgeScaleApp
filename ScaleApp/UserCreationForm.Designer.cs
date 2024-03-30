namespace ScaleApp
{
    partial class userCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userCreationForm));
            this.exitBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.employeeIDTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.passWordTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.usrNameText = new System.Windows.Forms.TextBox();
            this.ipAddressText=new System.Windows.Forms.TextBox();
            this.macAddressText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboScale = new System.Windows.Forms.ComboBox();
            this.comboPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.macAddressLabel = new System.Windows.Forms.Label();
            this.empNameLabel = new System.Windows.Forms.Label();
            this.fatherNameLabel = new System.Windows.Forms.Label();
            this.motherNameLabel = new System.Windows.Forms.Label();
            this.mobileLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.designationLabel = new System.Windows.Forms.Label();
            this.nidLabel = new System.Windows.Forms.Label();
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dobLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.empIdLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.updateBtn = new System.Windows.Forms.Button();
            this.usrid = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.infoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.Location = new System.Drawing.Point(386, 198);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(67, 32);
            this.exitBtn.TabIndex = 15;
            this.exitBtn.Text = "Close";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.Location = new System.Drawing.Point(197, 198);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(61, 32);
            this.saveBtn.TabIndex = 14;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // employeeIDTextBox
            // 
            this.employeeIDTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.employeeIDTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employeeIDTextBox.Location = new System.Drawing.Point(158, 58);
            this.employeeIDTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.employeeIDTextBox.Name = "employeeIDTextBox";
            this.employeeIDTextBox.Size = new System.Drawing.Size(209, 26);
            this.employeeIDTextBox.TabIndex = 19;
            this.employeeIDTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.employeeIDTextBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "Employee ID/Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(313, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 145);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "Weighbridge Scale:";
            // 
            // passWordTextBox
            // 
            this.passWordTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passWordTextBox.Location = new System.Drawing.Point(451, 58);
            this.passWordTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.passWordTextBox.Name = "passWordTextBox";
            this.passWordTextBox.Size = new System.Drawing.Size(159, 26);
            this.passWordTextBox.TabIndex = 31;
            this.passWordTextBox.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(378, 61);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 18);
            this.label8.TabIndex = 30;
            this.label8.Text = "Password:";
            // 
            // usrNameText
            // 
            this.usrNameText.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrNameText.Location = new System.Drawing.Point(158, 102);
            this.usrNameText.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.usrNameText.Name = "usrNameText";
            this.usrNameText.Size = new System.Drawing.Size(145, 26);
            this.usrNameText.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 105);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 18);
            this.label9.TabIndex = 28;
            this.label9.Text = "User Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(328, 1);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(217, 25);
            this.label10.TabIndex = 34;
            this.label10.Text = "User Creation Form";
            // 
            // comboScale
            // 
            this.comboScale.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboScale.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboScale.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboScale.FormattingEnabled = true;
            this.comboScale.Location = new System.Drawing.Point(158, 145);
            this.comboScale.Name = "comboScale";
            this.comboScale.Size = new System.Drawing.Size(145, 27);
            this.comboScale.TabIndex = 36;
            // 
            // comboPort
            // 
            this.comboPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboPort.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPort.FormattingEnabled = true;
            this.comboPort.Location = new System.Drawing.Point(386, 102);
            this.comboPort.Name = "comboPort";
            this.comboPort.Size = new System.Drawing.Size(224, 27);
            this.comboPort.TabIndex = 39;
            this.comboPort.SelectedIndexChanged += new System.EventHandler(this.comboPort_SelectedIndexChanged);



            // 
            // ipAdressText
            // 
            this.ipAddressText.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressText.Location = new System.Drawing.Point(400, 145);
            this.ipAddressText.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ipAddressText.Name = "ipAddressText";
            this.ipAddressText.Size = new System.Drawing.Size(175, 26);
            this.ipAddressText.TabIndex = 29;


            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressLabel.Location = new System.Drawing.Point(313, 148);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(70, 16);
            this.ipAddressLabel.TabIndex = 30;
            this.ipAddressLabel.Text = "IP Address :";

             
            this.macAddressLabel.AutoSize = true;
            this.macAddressLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macAddressLabel.Location = new System.Drawing.Point(583, 148);
            this.macAddressLabel.Name = "macAddressLabel";
            this.macAddressLabel.Size = new System.Drawing.Size(70, 16);
            this.macAddressLabel.TabIndex = 30;
            this.macAddressLabel.Text = "Mac Address :";

             this.macAddressText.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macAddressText.Location = new System.Drawing.Point(690, 145);
            this.macAddressText.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.macAddressText.Name = "macAddressText";
            this.macAddressText.Size = new System.Drawing.Size(175, 26);
            this.macAddressText.TabIndex = 29;

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 40;
            this.label1.Text = "Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Mother Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 42;
            this.label5.Text = "Father Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 16);
            this.label7.TabIndex = 43;
            this.label7.Text = "Mobile: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(9, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 16);
            this.label11.TabIndex = 44;
            this.label11.Text = "Email: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 16);
            this.label12.TabIndex = 45;
            this.label12.Text = "Designation: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 217);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 16);
            this.label13.TabIndex = 46;
            this.label13.Text = "National ID: ";
            // 
            // empNameLabel
            // 
            this.empNameLabel.AutoSize = true;
            this.empNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empNameLabel.Location = new System.Drawing.Point(134, 54);
            this.empNameLabel.Name = "empNameLabel";
            this.empNameLabel.Size = new System.Drawing.Size(13, 16);
            this.empNameLabel.TabIndex = 48;
            this.empNameLabel.Text = "-";
            // 
            // fatherNameLabel
            // 
            this.fatherNameLabel.AutoSize = true;
            this.fatherNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fatherNameLabel.Location = new System.Drawing.Point(134, 77);
            this.fatherNameLabel.Name = "fatherNameLabel";
            this.fatherNameLabel.Size = new System.Drawing.Size(13, 16);
            this.fatherNameLabel.TabIndex = 49;
            this.fatherNameLabel.Text = "-";
            // 
            // motherNameLabel
            // 
            this.motherNameLabel.AutoSize = true;
            this.motherNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motherNameLabel.Location = new System.Drawing.Point(134, 126);
            this.motherNameLabel.Name = "motherNameLabel";
            this.motherNameLabel.Size = new System.Drawing.Size(13, 16);
            this.motherNameLabel.TabIndex = 50;
            this.motherNameLabel.Text = "-";
            // 
            // mobileLabel
            // 
            this.mobileLabel.AutoSize = true;
            this.mobileLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mobileLabel.Location = new System.Drawing.Point(134, 152);
            this.mobileLabel.Name = "mobileLabel";
            this.mobileLabel.Size = new System.Drawing.Size(13, 16);
            this.mobileLabel.TabIndex = 51;
            this.mobileLabel.Text = "-";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLabel.Location = new System.Drawing.Point(134, 171);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(13, 16);
            this.emailLabel.TabIndex = 52;
            this.emailLabel.Text = "-";
            // 
            // designationLabel
            // 
            this.designationLabel.AutoSize = true;
            this.designationLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.designationLabel.Location = new System.Drawing.Point(134, 195);
            this.designationLabel.Name = "designationLabel";
            this.designationLabel.Size = new System.Drawing.Size(13, 16);
            this.designationLabel.TabIndex = 53;
            this.designationLabel.Text = "-";
            // 
            // nidLabel
            // 
            this.nidLabel.AutoSize = true;
            this.nidLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nidLabel.Location = new System.Drawing.Point(134, 217);
            this.nidLabel.Name = "nidLabel";
            this.nidLabel.Size = new System.Drawing.Size(13, 16);
            this.nidLabel.TabIndex = 54;
            this.nidLabel.Text = "-";
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.label15);
            this.infoGroupBox.Controls.Add(this.dobLabel);
            this.infoGroupBox.Controls.Add(this.label14);
            this.infoGroupBox.Controls.Add(this.empIdLabel);
            this.infoGroupBox.Controls.Add(this.label1);
            this.infoGroupBox.Controls.Add(this.nidLabel);
            this.infoGroupBox.Controls.Add(this.label3);
            this.infoGroupBox.Controls.Add(this.designationLabel);
            this.infoGroupBox.Controls.Add(this.label5);
            this.infoGroupBox.Controls.Add(this.emailLabel);
            this.infoGroupBox.Controls.Add(this.label7);
            this.infoGroupBox.Controls.Add(this.mobileLabel);
            this.infoGroupBox.Controls.Add(this.label11);
            this.infoGroupBox.Controls.Add(this.motherNameLabel);
            this.infoGroupBox.Controls.Add(this.label12);
            this.infoGroupBox.Controls.Add(this.fatherNameLabel);
            this.infoGroupBox.Controls.Add(this.label13);
            this.infoGroupBox.Controls.Add(this.empNameLabel);
            this.infoGroupBox.Location = new System.Drawing.Point(611, 12);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new System.Drawing.Size(346, 239);
            this.infoGroupBox.TabIndex = 55;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Employee Info";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(10, 102);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 16);
            this.label15.TabIndex = 57;
            this.label15.Text = "Date of Birth:";
            // 
            // dobLabel
            // 
            this.dobLabel.AutoSize = true;
            this.dobLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dobLabel.Location = new System.Drawing.Point(134, 100);
            this.dobLabel.Name = "dobLabel";
            this.dobLabel.Size = new System.Drawing.Size(13, 16);
            this.dobLabel.TabIndex = 58;
            this.dobLabel.Text = "-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 33);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 16);
            this.label14.TabIndex = 55;
            this.label14.Text = "ID :";
            // 
            // empIdLabel
            // 
            this.empIdLabel.AutoSize = true;
            this.empIdLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empIdLabel.Location = new System.Drawing.Point(135, 33);
            this.empIdLabel.Name = "empIdLabel";
            this.empIdLabel.Size = new System.Drawing.Size(13, 16);
            this.empIdLabel.TabIndex = 56;
            this.empIdLabel.Text = "-";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(25, 275);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(678, 233);
            this.dataGridView1.TabIndex = 56;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // updateBtn
            // 
            this.updateBtn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBtn.Location = new System.Drawing.Point(256, 198);
            this.updateBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(69, 32);
            this.updateBtn.TabIndex = 57;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // usrid
            // 
            this.usrid.AutoSize = true;
            this.usrid.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrid.Location = new System.Drawing.Point(313, 145);
            this.usrid.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.usrid.Name = "usrid";
            this.usrid.Size = new System.Drawing.Size(13, 18);
            this.usrid.TabIndex = 58;
            this.usrid.Text = "-";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(833, 465);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(124, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 59;
            this.pictureBox2.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 250;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "User Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Scale Name";
            this.Column3.Name = "Column3";
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Column4.HeaderText = "Details";
            this.Column4.Name = "Column4";
            this.Column4.Text = "";
            this.Column4.Visible = false;
            // 
            // Column5
            // 
            // 
            this.Column5.HeaderText = "IP Address";
            this.Column5.Name = "Column5";
            this.Column5.Width = 120;
            // 

             // 
            this.Column6.HeaderText = "Mac Address";
            this.Column6.Name = "Column6";
            this.Column6.Width = 120;
            // 


            this.Column7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Column7.HeaderText = "Action";
            this.Column7.Name = "Column7";
            this.Column7.Text = "Edit";
            this.Column7.UseColumnTextForButtonValue = true;
            // 
            // userCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(984, 520);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.usrid);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.comboPort);
            this.Controls.Add(this.comboScale);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.passWordTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.usrNameText);
            this.Controls.Add(this.ipAddressText);
            this.Controls.Add(this.macAddressText);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ipAddressLabel);
            this.Controls.Add(this.macAddressLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.employeeIDTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.saveBtn);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "userCreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserCreationForm";
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox employeeIDTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox passWordTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox usrNameText;
        private System.Windows.Forms.TextBox ipAddressText;
        private System.Windows.Forms.TextBox macAddressText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboScale;
        private System.Windows.Forms.ComboBox comboPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.Label macAddressLabel;
        private System.Windows.Forms.Label empNameLabel;
        private System.Windows.Forms.Label fatherNameLabel;
        private System.Windows.Forms.Label motherNameLabel;
        private System.Windows.Forms.Label mobileLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label designationLabel;
        private System.Windows.Forms.Label nidLabel;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label empIdLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label dobLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.Label usrid;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
         private System.Windows.Forms.DataGridViewButtonColumn Column7;
    }
}