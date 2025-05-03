namespace Hotel_and_Transport
{
    partial class EditActivityCapacity
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
            this.ActivityCapacity = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRoomDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ActivityCapacity
            // 
            this.ActivityCapacity.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ActivityCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.ActivityCapacity.Location = new System.Drawing.Point(278, 149);
            this.ActivityCapacity.Name = "ActivityCapacity";
            this.ActivityCapacity.Size = new System.Drawing.Size(141, 38);
            this.ActivityCapacity.TabIndex = 31;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCancel.Location = new System.Drawing.Point(259, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 62);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSave.Location = new System.Drawing.Point(47, 247);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(151, 62);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(41, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Add new capacity limit :";
            // 
            // lblRoomDescription
            // 
            this.lblRoomDescription.AutoSize = true;
            this.lblRoomDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblRoomDescription.Location = new System.Drawing.Point(118, 45);
            this.lblRoomDescription.Name = "lblRoomDescription";
            this.lblRoomDescription.Size = new System.Drawing.Size(227, 39);
            this.lblRoomDescription.TabIndex = 27;
            this.lblRoomDescription.Text = "Edit Capacity ";
            // 
            // EditActivityCapacity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(494, 395);
            this.Controls.Add(this.ActivityCapacity);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRoomDescription);
            this.Name = "EditActivityCapacity";
            this.Text = "EditActivityCapacity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ActivityCapacity;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRoomDescription;
    }
}