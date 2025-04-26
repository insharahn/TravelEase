namespace Hotel_and_Transport
{
    partial class TransportLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransportLogin));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usrPassword = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.usrEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.goBackButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(266, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(534, 31);
            this.label4.TabIndex = 14;
            this.label4.Text = "Please enter your user credentials to log in!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(280, 331);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 29);
            this.label3.TabIndex = 13;
            this.label3.Text = "PASSWORD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(281, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "EMAIL:";
            // 
            // usrPassword
            // 
            this.usrPassword.BackColor = System.Drawing.SystemColors.ControlLight;
            this.usrPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.usrPassword.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.usrPassword.Location = new System.Drawing.Point(504, 326);
            this.usrPassword.Name = "usrPassword";
            this.usrPassword.Size = new System.Drawing.Size(255, 34);
            this.usrPassword.TabIndex = 11;
            this.usrPassword.Text = "Password";
            this.usrPassword.TextChanged += new System.EventHandler(this.usrPassword_TextChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel1.Location = new System.Drawing.Point(342, 553);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(345, 29);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Don\'t have an account? Sign up";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // usrEmail
            // 
            this.usrEmail.BackColor = System.Drawing.SystemColors.ControlLight;
            this.usrEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.usrEmail.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.usrEmail.Location = new System.Drawing.Point(504, 254);
            this.usrEmail.Name = "usrEmail";
            this.usrEmail.Size = new System.Drawing.Size(255, 34);
            this.usrEmail.TabIndex = 9;
            this.usrEmail.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(409, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 69);
            this.label1.TabIndex = 8;
            this.label1.Text = "LOGIN";
            // 
            // goBackButton
            // 
            this.goBackButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.goBackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.goBackButton.Location = new System.Drawing.Point(534, 445);
            this.goBackButton.Name = "goBackButton";
            this.goBackButton.Size = new System.Drawing.Size(169, 51);
            this.goBackButton.TabIndex = 16;
            this.goBackButton.Text = "GO BACK";
            this.goBackButton.UseVisualStyleBackColor = false;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.loginButton.Location = new System.Drawing.Point(334, 445);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(169, 51);
            this.loginButton.TabIndex = 15;
            this.loginButton.Text = "LOGIN";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // TransportLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.goBackButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usrPassword);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.usrEmail);
            this.Controls.Add(this.label1);
            this.Name = "TransportLogin";
            this.Text = "TransportLogin";
            this.Load += new System.EventHandler(this.TransportLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usrPassword;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox usrEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button goBackButton;
        private System.Windows.Forms.Button loginButton;
    }
}