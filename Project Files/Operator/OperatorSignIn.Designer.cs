namespace TourOperator
{
    partial class OperatorSignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorSignIn));
            this.Pass = new System.Windows.Forms.TextBox();
            this.PASSWORD = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Pass
            // 
            this.Pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.Pass.Location = new System.Drawing.Point(395, 283);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(145, 20);
            this.Pass.TabIndex = 55;
            this.Pass.TextChanged += new System.EventHandler(this.Pass_TextChanged);
            this.Pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pass_KeyDown_1);
            // 
            // PASSWORD
            // 
            this.PASSWORD.AutoSize = true;
            this.PASSWORD.BackColor = System.Drawing.Color.Transparent;
            this.PASSWORD.Font = new System.Drawing.Font("Microsoft Uighur", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PASSWORD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.PASSWORD.Location = new System.Drawing.Point(265, 283);
            this.PASSWORD.Name = "PASSWORD";
            this.PASSWORD.Size = new System.Drawing.Size(93, 34);
            this.PASSWORD.TabIndex = 54;
            this.PASSWORD.Text = "Password";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Lucida Handwriting", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.label10.Location = new System.Drawing.Point(268, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(236, 63);
            this.label10.TabIndex = 51;
            this.label10.Text = "Sign In";
            // 
            // Email
            // 
            this.Email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.Email.Location = new System.Drawing.Point(393, 234);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(147, 20);
            this.Email.TabIndex = 57;
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Uighur", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.label2.Location = new System.Drawing.Point(265, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 34);
            this.label2.TabIndex = 56;
            this.label2.Text = "Email";
            // 
            // OperatorSignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.PASSWORD);
            this.Controls.Add(this.label10);
            this.Name = "OperatorSignIn";
            this.Text = "OperatorSignIn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.Label PASSWORD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.Label label2;
    }
}