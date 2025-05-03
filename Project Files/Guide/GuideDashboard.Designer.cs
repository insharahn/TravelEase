namespace Hotel_and_Transport
{
    partial class GuideDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuideDashboard));
            this.label7 = new System.Windows.Forms.Label();
            this.btnEditPrice = new System.Windows.Forms.Button();
            this.btnEditCapacity = new System.Windows.Forms.Button();
            this.dgvActivityListing = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.dgvActivityRequests = new System.Windows.Forms.DataGridView();
            this.Integration = new System.Windows.Forms.TabPage();
            this.Listing = new System.Windows.Forms.TabControl();
            this.Bookings = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelAllocation = new System.Windows.Forms.Button();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.dgvBookingManagement = new System.Windows.Forms.DataGridView();
            this.Reports = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.chartReports = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbGraphType = new System.Windows.Forms.ComboBox();
            this.sqlDataAdapter1 = new Microsoft.Data.SqlClient.SqlDataAdapter();
            this.label4 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnEditTimes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityListing)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityRequests)).BeginInit();
            this.Integration.SuspendLayout();
            this.Listing.SuspendLayout();
            this.Bookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookingManagement)).BeginInit();
            this.Reports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(622, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 35);
            this.label7.TabIndex = 6;
            this.label7.Text = "Options";
            // 
            // btnEditPrice
            // 
            this.btnEditPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEditPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEditPrice.Location = new System.Drawing.Point(608, 309);
            this.btnEditPrice.Name = "btnEditPrice";
            this.btnEditPrice.Size = new System.Drawing.Size(143, 65);
            this.btnEditPrice.TabIndex = 5;
            this.btnEditPrice.Text = "Edit Price";
            this.btnEditPrice.UseVisualStyleBackColor = false;
            this.btnEditPrice.Click += new System.EventHandler(this.btnEditPrice_Click);
            // 
            // btnEditCapacity
            // 
            this.btnEditCapacity.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEditCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEditCapacity.Location = new System.Drawing.Point(608, 215);
            this.btnEditCapacity.Name = "btnEditCapacity";
            this.btnEditCapacity.Size = new System.Drawing.Size(143, 67);
            this.btnEditCapacity.TabIndex = 4;
            this.btnEditCapacity.Text = "Edit Capacity";
            this.btnEditCapacity.UseVisualStyleBackColor = false;
            this.btnEditCapacity.Click += new System.EventHandler(this.btnEditCapacity_Click);
            // 
            // dgvActivityListing
            // 
            this.dgvActivityListing.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvActivityListing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivityListing.Location = new System.Drawing.Point(25, 64);
            this.dgvActivityListing.Name = "dgvActivityListing";
            this.dgvActivityListing.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvActivityListing.RowHeadersWidth = 51;
            this.dgvActivityListing.RowTemplate.Height = 24;
            this.dgvActivityListing.Size = new System.Drawing.Size(558, 326);
            this.dgvActivityListing.TabIndex = 2;
            this.dgvActivityListing.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServiceListing_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.tabPage2.Controls.Add(this.btnEditTimes);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.btnEditPrice);
            this.tabPage2.Controls.Add(this.btnEditCapacity);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgvActivityListing);
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Service Listing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(161, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "Listed Activities";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(624, 81);
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
            this.label2.Location = new System.Drawing.Point(152, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pending Approvals";
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnReject.Location = new System.Drawing.Point(615, 256);
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
            this.btnAccept.Location = new System.Drawing.Point(615, 148);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(142, 64);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "ACCEPT";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // dgvActivityRequests
            // 
            this.dgvActivityRequests.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvActivityRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivityRequests.Location = new System.Drawing.Point(22, 74);
            this.dgvActivityRequests.Name = "dgvActivityRequests";
            this.dgvActivityRequests.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvActivityRequests.RowHeadersWidth = 51;
            this.dgvActivityRequests.RowTemplate.Height = 24;
            this.dgvActivityRequests.Size = new System.Drawing.Size(561, 322);
            this.dgvActivityRequests.TabIndex = 1;
            this.dgvActivityRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrips_CellContentClick);
            // 
            // Integration
            // 
            this.Integration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.Integration.Controls.Add(this.label8);
            this.Integration.Controls.Add(this.label2);
            this.Integration.Controls.Add(this.btnReject);
            this.Integration.Controls.Add(this.btnAccept);
            this.Integration.Controls.Add(this.dgvActivityRequests);
            this.Integration.Location = new System.Drawing.Point(4, 32);
            this.Integration.Name = "Integration";
            this.Integration.Padding = new System.Windows.Forms.Padding(3);
            this.Integration.Size = new System.Drawing.Size(780, 404);
            this.Integration.TabIndex = 0;
            this.Integration.Text = "Activity Approvals";
            // 
            // Listing
            // 
            this.Listing.Controls.Add(this.Integration);
            this.Listing.Controls.Add(this.tabPage2);
            this.Listing.Controls.Add(this.Bookings);
            this.Listing.Controls.Add(this.Reports);
            this.Listing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Listing.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Listing.Location = new System.Drawing.Point(27, 141);
            this.Listing.Name = "Listing";
            this.Listing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Listing.SelectedIndex = 0;
            this.Listing.Size = new System.Drawing.Size(788, 440);
            this.Listing.TabIndex = 17;
            // 
            // Bookings
            // 
            this.Bookings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.Bookings.Controls.Add(this.label6);
            this.Bookings.Controls.Add(this.label3);
            this.Bookings.Controls.Add(this.btnCancelAllocation);
            this.Bookings.Controls.Add(this.btnConfirmPayment);
            this.Bookings.Controls.Add(this.dgvBookingManagement);
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
            this.label6.Location = new System.Drawing.Point(634, 83);
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
            this.label3.Location = new System.Drawing.Point(75, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(472, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Pending Payment Confirmations";
            // 
            // btnCancelAllocation
            // 
            this.btnCancelAllocation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelAllocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelAllocation.Location = new System.Drawing.Point(625, 241);
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
            this.btnConfirmPayment.Location = new System.Drawing.Point(625, 145);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Size = new System.Drawing.Size(143, 67);
            this.btnConfirmPayment.TabIndex = 1;
            this.btnConfirmPayment.Text = "CONFIRM";
            this.btnConfirmPayment.UseVisualStyleBackColor = false;
            this.btnConfirmPayment.Click += new System.EventHandler(this.btnConfirmPayment_Click);
            // 
            // dgvBookingManagement
            // 
            this.dgvBookingManagement.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvBookingManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookingManagement.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvBookingManagement.Location = new System.Drawing.Point(34, 60);
            this.dgvBookingManagement.Name = "dgvBookingManagement";
            this.dgvBookingManagement.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvBookingManagement.RowHeadersWidth = 51;
            this.dgvBookingManagement.RowTemplate.Height = 24;
            this.dgvBookingManagement.Size = new System.Drawing.Size(567, 325);
            this.dgvBookingManagement.TabIndex = 0;
            // 
            // Reports
            // 
            this.Reports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.Reports.Controls.Add(this.label9);
            this.Reports.Controls.Add(this.chartReports);
            this.Reports.Controls.Add(this.cmbGraphType);
            this.Reports.Location = new System.Drawing.Point(4, 32);
            this.Reports.Name = "Reports";
            this.Reports.Padding = new System.Windows.Forms.Padding(3);
            this.Reports.Size = new System.Drawing.Size(780, 404);
            this.Reports.TabIndex = 3;
            this.Reports.Text = "Reports";
            this.Reports.Click += new System.EventHandler(this.Reports_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Palatino Linotype", 14F);
            this.label9.Location = new System.Drawing.Point(29, 41);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(461, 38);
            this.label9.TabIndex = 10;
            this.label9.Text = "Please select the report you want to view : ";
            this.label9.UseCompatibleTextRendering = true;
            // 
            // chartReports
            // 
            chartArea1.Name = "ChartArea1";
            this.chartReports.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartReports.Legends.Add(legend1);
            this.chartReports.Location = new System.Drawing.Point(116, 82);
            this.chartReports.Name = "chartReports";
            this.chartReports.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartReports.Series.Add(series1);
            this.chartReports.Size = new System.Drawing.Size(523, 279);
            this.chartReports.TabIndex = 9;
            this.chartReports.Text = "chart1";
            this.chartReports.Click += new System.EventHandler(this.chartPerformance_Click);
            // 
            // cmbGraphType
            // 
            this.cmbGraphType.Font = new System.Drawing.Font("Palatino Linotype", 12F);
            this.cmbGraphType.FormattingEnabled = true;
            this.cmbGraphType.Location = new System.Drawing.Point(525, 41);
            this.cmbGraphType.Name = "cmbGraphType";
            this.cmbGraphType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbGraphType.Size = new System.Drawing.Size(196, 35);
            this.cmbGraphType.TabIndex = 8;
            this.cmbGraphType.SelectedIndexChanged += new System.EventHandler(this.cmbGraphType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 26F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(344, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(389, 55);
            this.label4.TabIndex = 21;
            this.label4.Text = "MY WORKSPACE";
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.exitButton.Location = new System.Drawing.Point(850, 407);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(143, 67);
            this.exitButton.TabIndex = 20;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.logoutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logoutButton.Location = new System.Drawing.Point(850, 313);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(143, 67);
            this.logoutButton.TabIndex = 19;
            this.logoutButton.Text = "LOG OUT";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(198)))), ((int)(((byte)(192)))));
            this.pictureBox1.Location = new System.Drawing.Point(840, 212);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 319);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(889, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 25);
            this.label5.TabIndex = 23;
            this.label5.Text = "MENU";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(889, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 25);
            this.label10.TabIndex = 24;
            this.label10.Text = "MENU";
            // 
            // btnEditTimes
            // 
            this.btnEditTimes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEditTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEditTimes.Location = new System.Drawing.Point(608, 118);
            this.btnEditTimes.Name = "btnEditTimes";
            this.btnEditTimes.Size = new System.Drawing.Size(143, 67);
            this.btnEditTimes.TabIndex = 7;
            this.btnEditTimes.Text = "Edit Timings";
            this.btnEditTimes.UseVisualStyleBackColor = false;
            this.btnEditTimes.Click += new System.EventHandler(this.btnEditTimes_Click);
            // 
            // GuideDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Listing);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Name = "GuideDashboard";
            this.Text = "GuideDashboard";
            this.Load += new System.EventHandler(this.GuideDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityListing)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityRequests)).EndInit();
            this.Integration.ResumeLayout(false);
            this.Integration.PerformLayout();
            this.Listing.ResumeLayout(false);
            this.Bookings.ResumeLayout(false);
            this.Bookings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookingManagement)).EndInit();
            this.Reports.ResumeLayout(false);
            this.Reports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnEditPrice;
        private System.Windows.Forms.Button btnEditCapacity;
        private System.Windows.Forms.DataGridView dgvActivityListing;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridView dgvActivityRequests;
        private System.Windows.Forms.TabPage Integration;
        private System.Windows.Forms.TabControl Listing;
        private System.Windows.Forms.TabPage Bookings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelAllocation;
        private System.Windows.Forms.Button btnConfirmPayment;
        private System.Windows.Forms.DataGridView dgvBookingManagement;
        private System.Windows.Forms.TabPage Reports;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReports;
        private System.Windows.Forms.ComboBox cmbGraphType;
        private Microsoft.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnEditTimes;
    }
}