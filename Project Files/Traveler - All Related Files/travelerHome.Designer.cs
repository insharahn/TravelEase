namespace dbfinalproject_interfaces
{
    partial class travelerHome
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
            this.flowLayoutPackages = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPackages
            // 
            this.flowLayoutPackages.AutoScroll = true;
            this.flowLayoutPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPackages.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPackages.Name = "flowLayoutPackages";
            this.flowLayoutPackages.Size = new System.Drawing.Size(970, 450);
            this.flowLayoutPackages.TabIndex = 0;
            // 
            // travelerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 450);
            this.Controls.Add(this.flowLayoutPackages);
            this.Name = "travelerHome";
            this.Text = "travelerHome";
            this.Load += new System.EventHandler(this.travelerHome_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPackages;
    }
}