namespace Hotel_and_Transport
{
    partial class TransportSignup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransportSignup));
            this.usrDestination = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.usrTransportType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.usrTotalSeats = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.usrEmail = new System.Windows.Forms.TextBox();
            this.goBack = new System.Windows.Forms.Button();
            this.signup_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usrPassword = new System.Windows.Forms.TextBox();
            this.usrName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.usrTotalSeats)).BeginInit();
            this.SuspendLayout();
            // 
            // usrDestination
            // 
            this.usrDestination.FormattingEnabled = true;
            this.usrDestination.Location = new System.Drawing.Point(748, 367);
            this.usrDestination.Name = "usrDestination";
            this.usrDestination.Size = new System.Drawing.Size(255, 24);
            this.usrDestination.TabIndex = 53;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(573, 367);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 25);
            this.label9.TabIndex = 52;
            this.label9.Text = "LOCATION:";
            // 
            // usrTransportType
            // 
            this.usrTransportType.FormattingEnabled = true;
            this.usrTransportType.Items.AddRange(new object[] {
            "Bus",
            "Flight",
            "Taxi",
            "Shuttle",
            "Bike",
            "Train"});
            this.usrTransportType.Location = new System.Drawing.Point(264, 292);
            this.usrTransportType.Name = "usrTransportType";
            this.usrTransportType.Size = new System.Drawing.Size(255, 24);
            this.usrTransportType.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(37, 293);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(198, 25);
            this.label7.TabIndex = 48;
            this.label7.Text = "TRANSPORT TYPE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(37, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 25);
            this.label6.TabIndex = 47;
            this.label6.Text = "TOTAL SEATS: ";
            // 
            // usrTotalSeats
            // 
            this.usrTotalSeats.Location = new System.Drawing.Point(264, 370);
            this.usrTotalSeats.Name = "usrTotalSeats";
            this.usrTotalSeats.Size = new System.Drawing.Size(255, 22);
            this.usrTotalSeats.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(573, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 25);
            this.label5.TabIndex = 45;
            this.label5.Text = "EMAIL:";
            // 
            // usrEmail
            // 
            this.usrEmail.BackColor = System.Drawing.SystemColors.ControlLight;
            this.usrEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usrEmail.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.usrEmail.Location = new System.Drawing.Point(748, 292);
            this.usrEmail.Name = "usrEmail";
            this.usrEmail.Size = new System.Drawing.Size(255, 26);
            this.usrEmail.TabIndex = 44;
            this.usrEmail.Text = "example: johndoe@abc.com";
            // 
            // goBack
            // 
            this.goBack.BackColor = System.Drawing.SystemColors.ControlLight;
            this.goBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.goBack.Location = new System.Drawing.Point(522, 485);
            this.goBack.Name = "goBack";
            this.goBack.Size = new System.Drawing.Size(169, 51);
            this.goBack.TabIndex = 43;
            this.goBack.Text = "GO BACK";
            this.goBack.UseVisualStyleBackColor = false;
            this.goBack.Click += new System.EventHandler(this.goBack_Click);
            // 
            // signup_button
            // 
            this.signup_button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.signup_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.signup_button.Location = new System.Drawing.Point(322, 485);
            this.signup_button.Name = "signup_button";
            this.signup_button.Size = new System.Drawing.Size(169, 51);
            this.signup_button.TabIndex = 42;
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
            this.label4.Location = new System.Drawing.Point(316, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(418, 31);
            this.label4.TabIndex = 41;
            this.label4.Text = "Please enter the following details:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(573, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 25);
            this.label3.TabIndex = 40;
            this.label3.Text = "PASSWORD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(37, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 25);
            this.label2.TabIndex = 39;
            this.label2.Text = "USER/HOTEL NAME:";
            // 
            // usrPassword
            // 
            this.usrPassword.BackColor = System.Drawing.SystemColors.ControlLight;
            this.usrPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usrPassword.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.usrPassword.Location = new System.Drawing.Point(748, 213);
            this.usrPassword.Name = "usrPassword";
            this.usrPassword.Size = new System.Drawing.Size(255, 26);
            this.usrPassword.TabIndex = 38;
            this.usrPassword.Text = "Password";
            // 
            // usrName
            // 
            this.usrName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.usrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usrName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.usrName.Location = new System.Drawing.Point(264, 211);
            this.usrName.Name = "usrName";
            this.usrName.Size = new System.Drawing.Size(255, 26);
            this.usrName.TabIndex = 37;
            this.usrName.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(391, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 69);
            this.label1.TabIndex = 36;
            this.label1.Text = "SIGNUP";
            // 
            // TransportSignup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.usrDestination);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.usrTransportType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.usrTotalSeats);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.usrEmail);
            this.Controls.Add(this.goBack);
            this.Controls.Add(this.signup_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usrPassword);
            this.Controls.Add(this.usrName);
            this.Controls.Add(this.label1);
            this.Name = "TransportSignup";
            this.Text = "TransportSignup";
            this.Load += new System.EventHandler(this.TransportSignup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usrTotalSeats)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox usrDestination;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox usrTransportType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown usrTotalSeats;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox usrEmail;
        private System.Windows.Forms.Button goBack;
        private System.Windows.Forms.Button signup_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usrPassword;
        private System.Windows.Forms.TextBox usrName;
        private System.Windows.Forms.Label label1;
    }
}