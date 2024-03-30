namespace ScaleApp
{
    partial class updateScaleForm
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
            this.updateBtn = new System.Windows.Forms.Button();
            this.comboScale = new System.Windows.Forms.ComboBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usrCombo = new System.Windows.Forms.ComboBox();
            this.curScaleLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.msgLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updateBtn
            // 
            this.updateBtn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBtn.Location = new System.Drawing.Point(112, 240);
            this.updateBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(73, 32);
            this.updateBtn.TabIndex = 15;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // comboScale
            // 
            this.comboScale.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboScale.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboScale.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboScale.FormattingEnabled = true;
            this.comboScale.Location = new System.Drawing.Point(170, 148);
            this.comboScale.Name = "comboScale";
            this.comboScale.Size = new System.Drawing.Size(139, 27);
            this.comboScale.TabIndex = 39;
            // 
            // closeBtn
            // 
            this.closeBtn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(211, 240);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(60, 32);
            this.closeBtn.TabIndex = 40;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 18);
            this.label4.TabIndex = 41;
            this.label4.Text = "Current Scale No:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 42;
            this.label1.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 157);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 18);
            this.label2.TabIndex = 43;
            this.label2.Text = "New Scale No:  ";
            // 
            // usrCombo
            // 
            this.usrCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.usrCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.usrCombo.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrCombo.FormattingEnabled = true;
            this.usrCombo.Location = new System.Drawing.Point(170, 55);
            this.usrCombo.Name = "usrCombo";
            this.usrCombo.Size = new System.Drawing.Size(139, 27);
            this.usrCombo.TabIndex = 44;
            this.usrCombo.SelectedIndexChanged += new System.EventHandler(this.usrCombo_SelectedIndexChanged);
            // 
            // curScaleLabel
            // 
            this.curScaleLabel.AutoSize = true;
            this.curScaleLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curScaleLabel.Location = new System.Drawing.Point(167, 108);
            this.curScaleLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.curScaleLabel.Name = "curScaleLabel";
            this.curScaleLabel.Size = new System.Drawing.Size(18, 18);
            this.curScaleLabel.TabIndex = 45;
            this.curScaleLabel.Text = "--";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(92, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(180, 22);
            this.label10.TabIndex = 46;
            this.label10.Text = "Scale Updation Form";
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.Location = new System.Drawing.Point(146, 196);
            this.msgLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(13, 18);
            this.msgLabel.TabIndex = 47;
            this.msgLabel.Text = "-";
            // 
            // updateScaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 291);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.curScaleLabel);
            this.Controls.Add(this.usrCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.comboScale);
            this.Controls.Add(this.updateBtn);
            this.Name = "updateScaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Scale Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.ComboBox comboScale;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox usrCombo;
        private System.Windows.Forms.Label curScaleLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label msgLabel;
    }
}