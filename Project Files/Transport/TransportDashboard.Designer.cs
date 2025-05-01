namespace Hotel_and_Transport
{
    partial class TransportDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransportDashboard));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sqlDataAdapter1 = new Microsoft.Data.SqlClient.SqlDataAdapter();
            this.label9 = new System.Windows.Forms.Label();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbGraphType = new System.Windows.Forms.ComboBox();
            this.Reports = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Bookings = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelAllocation = new System.Windows.Forms.Button();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.Listing = new System.Windows.Forms.TabControl();
            this.Integration = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.dgvTrips = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Button();
            this.EditArrival = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvServiceListing = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.Reports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.Bookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.Listing.SuspendLayout();
            this.Integration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceListing)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(895, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "MENU";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 26F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(350, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(389, 55);
            this.label4.TabIndex = 14;
            this.label4.Text = "MY WORKSPACE";
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.exitButton.Location = new System.Drawing.Point(856, 401);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(143, 67);
            this.exitButton.TabIndex = 13;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.logoutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logoutButton.Location = new System.Drawing.Point(856, 307);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(143, 67);
            this.logoutButton.TabIndex = 12;
            this.logoutButton.Text = "LOG OUT";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.pictureBox1.Location = new System.Drawing.Point(846, 206);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 319);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Palatino Linotype", 14F);
            this.label9.Location = new System.Drawing.Point(34, 26);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(461, 38);
            this.label9.TabIndex = 10;
            this.label9.Text = "Please select the report you want to view : ";
            this.label9.UseCompatibleTextRendering = true;
            // 
            // chartPerformance
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPerformance.Legends.Add(legend1);
            this.chartPerformance.Location = new System.Drawing.Point(129, 93);
            this.chartPerformance.Name = "chartPerformance";
            this.chartPerformance.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPerformance.Series.Add(series1);
            this.chartPerformance.Size = new System.Drawing.Size(523, 279);
            this.chartPerformance.TabIndex = 9;
            this.chartPerformance.Text = "chart1";
            // 
            // cmbGraphType
            // 
            this.cmbGraphType.Font = new System.Drawing.Font("Palatino Linotype", 12F);
            this.cmbGraphType.FormattingEnabled = true;
            this.cmbGraphType.Items.AddRange(new object[] {
            "Occupancy",
            "Ratings",
            "Revenue"});
            this.cmbGraphType.Location = new System.Drawing.Point(530, 26);
            this.cmbGraphType.Name = "cmbGraphType";
            this.cmbGraphType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbGraphType.Size = new System.Drawing.Size(196, 35);
            this.cmbGraphType.TabIndex = 8;
            this.cmbGraphType.SelectedIndexChanged += new System.EventHandler(this.cmbGraphType_SelectedIndexChanged);
            // 
            // Reports
            // 
            this.Reports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.Reports.Controls.Add(this.label9);
            this.Reports.Controls.Add(this.chartPerformance);
            this.Reports.Controls.Add(this.cmbGraphType);
            this.Reports.Location = new System.Drawing.Point(4, 32);
            this.Reports.Name = "Reports";
            this.Reports.Padding = new System.Windows.Forms.Padding(3);
            this.Reports.Size = new System.Drawing.Size(780, 404);
            this.Reports.TabIndex = 3;
            this.Reports.Text = "Reports";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(880, 226);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // Bookings
            // 
            this.Bookings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.Bookings.Controls.Add(this.label6);
            this.Bookings.Controls.Add(this.label3);
            this.Bookings.Controls.Add(this.btnCancelAllocation);
            this.Bookings.Controls.Add(this.btnConfirmPayment);
            this.Bookings.Controls.Add(this.dgvBookings);
            this.Bookings.Location = new System.Drawing.Point(4, 32);
            this.Bookings.Name = "Bookings";
            this.Bookings.Padding = new System.Windows.Forms.Padding(3);
            this.Bookings.Size = new System.Drawing.Size(780, 404);
            this.Bookings.TabIndex = 2;
            this.Bookings.Text = "Booking Confirmations";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(630, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 35);
            this.label6.TabIndex = 5;
            this.label6.Text = "Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(71, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(472, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Pending Payment Confirmations";
            // 
            // btnCancelAllocation
            // 
            this.btnCancelAllocation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelAllocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelAllocation.Location = new System.Drawing.Point(621, 248);
            this.btnCancelAllocation.Name = "btnCancelAllocation";
            this.btnCancelAllocation.Size = new System.Drawing.Size(143, 65);
            this.btnCancelAllocation.TabIndex = 2;
            this.btnCancelAllocation.Text = "CANCEL";
            this.btnCancelAllocation.UseVisualStyleBackColor = false;
            this.btnCancelAllocation.Click += new System.EventHandler(this.btnCancelAllocation_Click);
            // 
            // btnConfirmPayment
            // 
            this.btnConfirmPayment.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConfirmPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnConfirmPayment.Location = new System.Drawing.Point(621, 152);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Size = new System.Drawing.Size(143, 67);
            this.btnConfirmPayment.TabIndex = 1;
            this.btnConfirmPayment.Text = "CONFIRM";
            this.btnConfirmPayment.UseVisualStyleBackColor = false;
            this.btnConfirmPayment.Click += new System.EventHandler(this.btnConfirmPayment_Click);
            // 
            // dgvBookings
            // 
            this.dgvBookings.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookings.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvBookings.Location = new System.Drawing.Point(30, 67);
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.RowHeadersWidth = 51;
            this.dgvBookings.RowTemplate.Height = 24;
            this.dgvBookings.Size = new System.Drawing.Size(567, 325);
            this.dgvBookings.TabIndex = 0;
            // 
            // Listing
            // 
            this.Listing.Controls.Add(this.Integration);
            this.Listing.Controls.Add(this.tabPage2);
            this.Listing.Controls.Add(this.Bookings);
            this.Listing.Controls.Add(this.Reports);
            this.Listing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Listing.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Listing.Location = new System.Drawing.Point(33, 135);
            this.Listing.Name = "Listing";
            this.Listing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Listing.SelectedIndex = 0;
            this.Listing.Size = new System.Drawing.Size(788, 440);
            this.Listing.TabIndex = 10;
            // 
            // Integration
            // 
            this.Integration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.Integration.Controls.Add(this.label8);
            this.Integration.Controls.Add(this.label2);
            this.Integration.Controls.Add(this.btnReject);
            this.Integration.Controls.Add(this.btnAccept);
            this.Integration.Controls.Add(this.dgvTrips);
            this.Integration.Location = new System.Drawing.Point(4, 32);
            this.Integration.Name = "Integration";
            this.Integration.Padding = new System.Windows.Forms.Padding(3);
            this.Integration.Size = new System.Drawing.Size(780, 404);
            this.Integration.TabIndex = 0;
            this.Integration.Text = "Ride Approvals";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(633, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 35);
            this.label8.TabIndex = 6;
            this.label8.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(161, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pending Approvals";
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnReject.Location = new System.Drawing.Point(624, 256);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(142, 64);
            this.btnReject.TabIndex = 4;
            this.btnReject.Text = "REJECT";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAccept.Location = new System.Drawing.Point(624, 148);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(142, 64);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "ACCEPT";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // dgvTrips
            // 
            this.dgvTrips.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrips.Location = new System.Drawing.Point(31, 74);
            this.dgvTrips.Name = "dgvTrips";
            this.dgvTrips.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvTrips.RowHeadersWidth = 51;
            this.dgvTrips.RowTemplate.Height = 24;
            this.dgvTrips.Size = new System.Drawing.Size(561, 322);
            this.dgvTrips.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.price);
            this.tabPage2.Controls.Add(this.EditArrival);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgvServiceListing);
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Service Listing";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(622, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 35);
            this.label7.TabIndex = 6;
            this.label7.Text = "Options";
            // 
            // price
            // 
            this.price.BackColor = System.Drawing.SystemColors.ControlLight;
            this.price.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.price.Location = new System.Drawing.Point(612, 243);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(143, 65);
            this.price.TabIndex = 5;
            this.price.Text = "Edit Price";
            this.price.UseVisualStyleBackColor = false;
            this.price.Click += new System.EventHandler(this.price_Click);
            // 
            // EditArrival
            // 
            this.EditArrival.BackColor = System.Drawing.SystemColors.ControlLight;
            this.EditArrival.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.EditArrival.Location = new System.Drawing.Point(612, 147);
            this.EditArrival.Name = "EditArrival";
            this.EditArrival.Size = new System.Drawing.Size(143, 67);
            this.EditArrival.TabIndex = 4;
            this.EditArrival.Text = "Edit Arrival Time";
            this.EditArrival.UseVisualStyleBackColor = false;
            this.EditArrival.Click += new System.EventHandler(this.EditArrival_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(182, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "Listed Rides";
            // 
            // dgvServiceListing
            // 
            this.dgvServiceListing.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvServiceListing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceListing.Location = new System.Drawing.Point(32, 66);
            this.dgvServiceListing.Name = "dgvServiceListing";
            this.dgvServiceListing.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvServiceListing.RowHeadersWidth = 51;
            this.dgvServiceListing.RowTemplate.Height = 24;
            this.dgvServiceListing.Size = new System.Drawing.Size(558, 326);
            this.dgvServiceListing.TabIndex = 2;
            // 
            // TransportDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Listing);
            this.Name = "TransportDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transport Service Dashboard";
            this.Load += new System.EventHandler(this.TransportDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.Reports.ResumeLayout(false);
            this.Reports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.Bookings.ResumeLayout(false);
            this.Bookings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.Listing.ResumeLayout(false);
            this.Integration.ResumeLayout(false);
            this.Integration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceListing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Microsoft.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.ComboBox cmbGraphType;
        private System.Windows.Forms.TabPage Reports;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TabPage Bookings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelAllocation;
        private System.Windows.Forms.Button btnConfirmPayment;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.TabControl Listing;
        private System.Windows.Forms.TabPage Integration;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridView dgvTrips;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button price;
        private System.Windows.Forms.Button EditArrival;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvServiceListing;
    }
}