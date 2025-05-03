namespace Hotel_and_Transport
{
    partial class GuideSignup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuideSignup));
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbActivityType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudCapacity = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.goBack = new System.Windows.Forms.Button();
            this.signup_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radSignLanguageYes = new System.Windows.Forms.RadioButton();
            this.radSignLanguageNo = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbLocation
            // 
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(749, 354);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(255, 24);
            this.cmbLocation.TabIndex = 69;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(574, 354);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 25);
            this.label9.TabIndex = 68;
            this.label9.Text = "LOCATION:";
            // 
            // cmbActivityType
            // 
            this.cmbActivityType.FormattingEnabled = true;
            this.cmbActivityType.Items.AddRange(new object[] {
            "Hiking",
            "Luxury Tour",
            "Cultural Tours",
            "Cruise",
            "Shopping",
            "Museum Visit",
            "Historical Site Tour",
            "Food Street",
            "Local Market Visit"});
            this.cmbActivityType.Location = new System.Drawing.Point(265, 295);
            this.cmbActivityType.Name = "cmbActivityType";
            this.cmbActivityType.Size = new System.Drawing.Size(255, 24);
            this.cmbActivityType.TabIndex = 67;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(38, 296);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(167, 25);
            this.label7.TabIndex = 66;
            this.label7.Text = "ACTIVITY TYPE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(38, 351);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 25);
            this.label6.TabIndex = 65;
            this.label6.Text = "CAPACITY:";
            // 
            // nudCapacity
            // 
            this.nudCapacity.Location = new System.Drawing.Point(265, 354);
            this.nudCapacity.Name = "nudCapacity";
            this.nudCapacity.Size = new System.Drawing.Size(255, 22);
            this.nudCapacity.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(574, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 25);
            this.label5.TabIndex = 63;
            this.label5.Text = "EMAIL:";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEmail.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtEmail.Location = new System.Drawing.Point(749, 295);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(255, 26);
            this.txtEmail.TabIndex = 62;
            this.txtEmail.Text = "example: johndoe@abc.com";
            // 
            // goBack
            // 
            this.goBack.BackColor = System.Drawing.SystemColors.ControlLight;
            this.goBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.goBack.Location = new System.Drawing.Point(541, 517);
            this.goBack.Name = "goBack";
            this.goBack.Size = new System.Drawing.Size(169, 51);
            this.goBack.TabIndex = 61;
            this.goBack.Text = "GO BACK";
            this.goBack.UseVisualStyleBackColor = false;
            this.goBack.Click += new System.EventHandler(this.goBack_Click);
            // 
            // signup_button
            // 
            this.signup_button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.signup_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.signup_button.Location = new System.Drawing.Point(341, 517);
            this.signup_button.Name = "signup_button";
            this.signup_button.Size = new System.Drawing.Size(169, 51);
            this.signup_button.TabIndex = 60;
            this.signup_button.Text = "SIGNUP";
            this.signup_button.UseVisualStyleBackColor = false;
            this.signup_button.Click += new System.EventHandler(this.signup_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(317, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(418, 31);
            this.label4.TabIndex = 59;
            this.label4.Text = "Please enter the following details:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(574, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 25);
            this.label3.TabIndex = 58;
            this.label3.Text = "PASSWORD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(38, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 25);
            this.label2.TabIndex = 57;
            this.label2.Text = "NAME:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPassword.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtPassword.Location = new System.Drawing.Point(749, 233);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(255, 26);
            this.txtPassword.TabIndex = 56;
            this.txtPassword.Text = "Password";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtName.Location = new System.Drawing.Point(265, 231);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 26);
            this.txtName.TabIndex = 55;
            this.txtName.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(392, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 69);
            this.label1.TabIndex = 54;
            this.label1.Text = "SIGNUP";
            // 
            // radSignLanguageYes
            // 
            this.radSignLanguageYes.AutoSize = true;
            this.radSignLanguageYes.BackColor = System.Drawing.Color.Transparent;
            this.radSignLanguageYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radSignLanguageYes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radSignLanguageYes.Location = new System.Drawing.Point(879, 406);
            this.radSignLanguageYes.Name = "radSignLanguageYes";
            this.radSignLanguageYes.Size = new System.Drawing.Size(62, 24);
            this.radSignLanguageYes.TabIndex = 70;
            this.radSignLanguageYes.TabStop = true;
            this.radSignLanguageYes.Text = "YES";
            this.radSignLanguageYes.UseVisualStyleBackColor = false;
            // 
            // radSignLanguageNo
            // 
            this.radSignLanguageNo.AutoSize = true;
            this.radSignLanguageNo.BackColor = System.Drawing.Color.Transparent;
            this.radSignLanguageNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radSignLanguageNo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radSignLanguageNo.Location = new System.Drawing.Point(949, 407);
            this.radSignLanguageNo.Name = "radSignLanguageNo";
            this.radSignLanguageNo.Size = new System.Drawing.Size(55, 24);
            this.radSignLanguageNo.TabIndex = 71;
            this.radSignLanguageNo.TabStop = true;
            this.radSignLanguageNo.Text = "NO";
            this.radSignLanguageNo.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Location = new System.Drawing.Point(574, 406);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(278, 25);
            this.label8.TabIndex = 72;
            this.label8.Text = "SIGN LANGUAGE FLUENCY:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(38, 401);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 25);
            this.label10.TabIndex = 73;
            this.label10.Text = "PRICE:";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPrice.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtPrice.Location = new System.Drawing.Point(265, 403);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(255, 26);
            this.txtPrice.TabIndex = 74;
            this.txtPrice.Text = "e.g 32.50";
            // 
            // GuideSignup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.radSignLanguageNo);
            this.Controls.Add(this.radSignLanguageYes);
            this.Controls.Add(this.cmbLocation);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbActivityType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudCapacity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.goBack);
            this.Controls.Add(this.signup_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "GuideSignup";
            this.Text = "GuideSignup";
            this.Load += new System.EventHandler(this.GuideSignup_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbActivityType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudCapacity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button goBack;
        private System.Windows.Forms.Button signup_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radSignLanguageYes;
        private System.Windows.Forms.RadioButton radSignLanguageNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrice;
    }
}