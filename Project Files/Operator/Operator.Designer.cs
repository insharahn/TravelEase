namespace TourOperator
{
    partial class Operator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operator));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabContrf = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.updateTrip = new System.Windows.Forms.Button();
            this.deleteTrip = new System.Windows.Forms.Button();
            this.Mytrips = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ADDTRIP = new System.Windows.Forms.Button();
            this.capacitytype = new System.Windows.Forms.ComboBox();
            this.triptype = new System.Windows.Forms.ComboBox();
            this.destination = new System.Windows.Forms.ComboBox();
            this.duration = new System.Windows.Forms.TextBox();
            this.description = new System.Windows.Forms.TextBox();
            this.groupsize = new System.Windows.Forms.TextBox();
            this.baseprice = new System.Windows.Forms.TextBox();
            this.title = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.addRide = new System.Windows.Forms.Button();
            this.addAccomodation = new System.Windows.Forms.Button();
            this.ridesgridview = new System.Windows.Forms.DataGridView();
            this.accomodationgridview = new System.Windows.Forms.DataGridView();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.mybookings = new System.Windows.Forms.DataGridView();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.reportChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.reportComboBox = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            this.label10 = new System.Windows.Forms.Label();
            this.logout = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.sqlDataAdapter1 = new Microsoft.Data.SqlClient.SqlDataAdapter();
            this.tabContrf.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mytrips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ridesgridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accomodationgridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mybookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabContrf
            // 
            this.tabContrf.Controls.Add(this.tabPage1);
            this.tabContrf.Controls.Add(this.tabPage2);
            this.tabContrf.Controls.Add(this.tabPage3);
            this.tabContrf.Controls.Add(this.tabPage4);
            this.tabContrf.Controls.Add(this.tabPage5);
            this.tabContrf.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabContrf.Location = new System.Drawing.Point(24, 91);
            this.tabContrf.Name = "tabContrf";
            this.tabContrf.SelectedIndex = 0;
            this.tabContrf.Size = new System.Drawing.Size(644, 335);
            this.tabContrf.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.tabPage1.Controls.Add(this.updateTrip);
            this.tabPage1.Controls.Add(this.deleteTrip);
            this.tabPage1.Controls.Add(this.Mytrips);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.pictureBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Trips Listing";
            // 
            // updateTrip
            // 
            this.updateTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.updateTrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.updateTrip.Font = new System.Drawing.Font("Microsoft YaHei", 10.25F, System.Drawing.FontStyle.Bold);
            this.updateTrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.updateTrip.Location = new System.Drawing.Point(511, 180);
            this.updateTrip.Name = "updateTrip";
            this.updateTrip.Size = new System.Drawing.Size(103, 31);
            this.updateTrip.TabIndex = 19;
            this.updateTrip.Text = "Update Trip";
            this.updateTrip.UseVisualStyleBackColor = false;
            this.updateTrip.Click += new System.EventHandler(this.updateTrip_Click);
            // 
            // deleteTrip
            // 
            this.deleteTrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.deleteTrip.Font = new System.Drawing.Font("Microsoft YaHei", 10.25F, System.Drawing.FontStyle.Bold);
            this.deleteTrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.deleteTrip.Location = new System.Drawing.Point(511, 121);
            this.deleteTrip.Name = "deleteTrip";
            this.deleteTrip.Size = new System.Drawing.Size(103, 31);
            this.deleteTrip.TabIndex = 18;
            this.deleteTrip.Text = "Delete Trip";
            this.deleteTrip.UseVisualStyleBackColor = false;
            this.deleteTrip.Click += new System.EventHandler(this.deleteTrip_Click);
            // 
            // Mytrips
            // 
            this.Mytrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Mytrips.Location = new System.Drawing.Point(17, 75);
            this.Mytrips.Name = "Mytrips";
            this.Mytrips.Size = new System.Drawing.Size(379, 212);
            this.Mytrips.TabIndex = 1;
            this.Mytrips.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Mytrips_CellContentClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.label11.Location = new System.Drawing.Point(200, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 28);
            this.label11.TabIndex = 0;
            this.label11.Text = "My Trips";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-202, -25);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1072, 376);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.ADDTRIP);
            this.tabPage2.Controls.Add(this.capacitytype);
            this.tabPage2.Controls.Add(this.triptype);
            this.tabPage2.Controls.Add(this.destination);
            this.tabPage2.Controls.Add(this.duration);
            this.tabPage2.Controls.Add(this.description);
            this.tabPage2.Controls.Add(this.groupsize);
            this.tabPage2.Controls.Add(this.baseprice);
            this.tabPage2.Controls.Add(this.title);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Trip Creation";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(301, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 209);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ADDTRIP
            // 
            this.ADDTRIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.ADDTRIP.Font = new System.Drawing.Font("Microsoft YaHei", 10.25F, System.Drawing.FontStyle.Bold);
            this.ADDTRIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.ADDTRIP.Location = new System.Drawing.Point(397, 245);
            this.ADDTRIP.Name = "ADDTRIP";
            this.ADDTRIP.Size = new System.Drawing.Size(103, 31);
            this.ADDTRIP.TabIndex = 17;
            this.ADDTRIP.Text = "Add Trip";
            this.ADDTRIP.UseVisualStyleBackColor = false;
            this.ADDTRIP.Click += new System.EventHandler(this.ADDTRIP_Click);
            // 
            // capacitytype
            // 
            this.capacitytype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.capacitytype.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.capacitytype.FormattingEnabled = true;
            this.capacitytype.Location = new System.Drawing.Point(146, 168);
            this.capacitytype.Name = "capacitytype";
            this.capacitytype.Size = new System.Drawing.Size(121, 24);
            this.capacitytype.TabIndex = 16;
            this.capacitytype.SelectedIndexChanged += new System.EventHandler(this.capacitytype_SelectedIndexChanged);
            // 
            // triptype
            // 
            this.triptype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.triptype.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.triptype.FormattingEnabled = true;
            this.triptype.Location = new System.Drawing.Point(146, 138);
            this.triptype.Name = "triptype";
            this.triptype.Size = new System.Drawing.Size(121, 24);
            this.triptype.TabIndex = 15;
            this.triptype.SelectedIndexChanged += new System.EventHandler(this.triptype_SelectedIndexChanged);
            // 
            // destination
            // 
            this.destination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.destination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.destination.FormattingEnabled = true;
            this.destination.Location = new System.Drawing.Point(146, 107);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(121, 24);
            this.destination.TabIndex = 14;
            this.destination.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // duration
            // 
            this.duration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.duration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.duration.Location = new System.Drawing.Point(146, 245);
            this.duration.Name = "duration";
            this.duration.Size = new System.Drawing.Size(109, 15);
            this.duration.TabIndex = 13;
            this.duration.TextChanged += new System.EventHandler(this.duration_TextChanged);
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.description.Location = new System.Drawing.Point(146, 77);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(109, 15);
            this.description.TabIndex = 12;
            this.description.TextChanged += new System.EventHandler(this.description_TextChanged);
            // 
            // groupsize
            // 
            this.groupsize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.groupsize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.groupsize.Location = new System.Drawing.Point(146, 212);
            this.groupsize.Name = "groupsize";
            this.groupsize.Size = new System.Drawing.Size(109, 15);
            this.groupsize.TabIndex = 11;
            this.groupsize.TextChanged += new System.EventHandler(this.groupsize_TextChanged);
            // 
            // baseprice
            // 
            this.baseprice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.baseprice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.baseprice.Location = new System.Drawing.Point(146, 277);
            this.baseprice.Name = "baseprice";
            this.baseprice.Size = new System.Drawing.Size(109, 15);
            this.baseprice.TabIndex = 10;
            this.baseprice.TextChanged += new System.EventHandler(this.baseprice_TextChanged);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.title.Location = new System.Drawing.Point(146, 51);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(109, 15);
            this.title.TabIndex = 9;
            this.title.TextChanged += new System.EventHandler(this.title_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Duration";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 276);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Base Price";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Title";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Destination";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Trip Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Group Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Capacity Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create Trips";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.addRide);
            this.tabPage3.Controls.Add(this.addAccomodation);
            this.tabPage3.Controls.Add(this.ridesgridview);
            this.tabPage3.Controls.Add(this.accomodationgridview);
            this.tabPage3.Controls.Add(this.pictureBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(636, 306);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Trip Resources";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.label13.Location = new System.Drawing.Point(381, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 19);
            this.label13.TabIndex = 21;
            this.label13.Text = "Transportation";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.label12.Location = new System.Drawing.Point(97, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 19);
            this.label12.TabIndex = 20;
            this.label12.Text = "Accomodations";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // addRide
            // 
            this.addRide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.addRide.Font = new System.Drawing.Font("Microsoft YaHei", 10.25F, System.Drawing.FontStyle.Bold);
            this.addRide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.addRide.Location = new System.Drawing.Point(365, 238);
            this.addRide.Name = "addRide";
            this.addRide.Size = new System.Drawing.Size(176, 31);
            this.addRide.TabIndex = 19;
            this.addRide.Text = "Add Transportation";
            this.addRide.UseVisualStyleBackColor = false;
            this.addRide.Click += new System.EventHandler(this.addRide_Click);
            // 
            // addAccomodation
            // 
            this.addAccomodation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.addAccomodation.Font = new System.Drawing.Font("Microsoft YaHei", 10.25F, System.Drawing.FontStyle.Bold);
            this.addAccomodation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.addAccomodation.Location = new System.Drawing.Point(83, 238);
            this.addAccomodation.Name = "addAccomodation";
            this.addAccomodation.Size = new System.Drawing.Size(178, 31);
            this.addAccomodation.TabIndex = 18;
            this.addAccomodation.Text = "Add Accomodation";
            this.addAccomodation.UseVisualStyleBackColor = false;
            this.addAccomodation.Click += new System.EventHandler(this.addAccomodation_Click);
            // 
            // ridesgridview
            // 
            this.ridesgridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ridesgridview.Location = new System.Drawing.Point(325, 63);
            this.ridesgridview.Name = "ridesgridview";
            this.ridesgridview.Size = new System.Drawing.Size(240, 150);
            this.ridesgridview.TabIndex = 2;
            this.ridesgridview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ridesgridview_CellContentClick);
            // 
            // accomodationgridview
            // 
            this.accomodationgridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accomodationgridview.Location = new System.Drawing.Point(46, 63);
            this.accomodationgridview.Name = "accomodationgridview";
            this.accomodationgridview.Size = new System.Drawing.Size(240, 150);
            this.accomodationgridview.TabIndex = 1;
            this.accomodationgridview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.accomodationgridview_CellContentClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(-362, -59);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1392, 465);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.mybookings);
            this.tabPage4.Controls.Add(this.pictureBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(636, 306);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Trip Bookings";
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei", 13.75F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.label14.Location = new System.Drawing.Point(257, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 26);
            this.label14.TabIndex = 20;
            this.label14.Text = "My Bookings";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mybookings
            // 
            this.mybookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mybookings.Location = new System.Drawing.Point(125, 57);
            this.mybookings.Name = "mybookings";
            this.mybookings.Size = new System.Drawing.Size(406, 232);
            this.mybookings.TabIndex = 22;
            this.mybookings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mybookings_CellContentClick);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(-4, -34);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(688, 444);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 23;
            this.pictureBox4.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.tabPage5.Controls.Add(this.reportChart);
            this.tabPage5.Controls.Add(this.reportComboBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(636, 306);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Reports";
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // reportChart
            // 
            chartArea1.Name = "ChartArea1";
            this.reportChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.reportChart.Legends.Add(legend1);
            this.reportChart.Location = new System.Drawing.Point(71, 39);
            this.reportChart.Name = "reportChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.reportChart.Series.Add(series1);
            this.reportChart.Size = new System.Drawing.Size(385, 232);
            this.reportChart.TabIndex = 1;
            this.reportChart.Text = "chart1";
            this.reportChart.Click += new System.EventHandler(this.reportChart_Click);
            // 
            // reportComboBox
            // 
            this.reportComboBox.FormattingEnabled = true;
            this.reportComboBox.Location = new System.Drawing.Point(492, 18);
            this.reportComboBox.Name = "reportComboBox";
            this.reportComboBox.Size = new System.Drawing.Size(121, 24);
            this.reportComboBox.TabIndex = 0;
            this.reportComboBox.SelectedIndexChanged += new System.EventHandler(this.reportComboBox_SelectedIndexChanged);
            // 
            // sqlCommand1
            // 
            this.sqlCommand1.CommandTimeout = 30;
            this.sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lucida Fax", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(206)))));
            this.label10.Location = new System.Drawing.Point(242, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(262, 33);
            this.label10.TabIndex = 1;
            this.label10.Text = "MY WORKSPACE";
            // 
            // logout
            // 
            this.logout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.logout.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.logout.Location = new System.Drawing.Point(691, 184);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(136, 55);
            this.logout.TabIndex = 18;
            this.logout.Text = "LOG OUT";
            this.logout.UseVisualStyleBackColor = false;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(192)))), ((int)(((byte)(185)))));
            this.exit.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.exit.Location = new System.Drawing.Point(691, 274);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(136, 55);
            this.exit.TabIndex = 19;
            this.exit.Text = "EXIT";
            this.exit.UseVisualStyleBackColor = false;
            // 
            // Operator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(857, 494);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.logout);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabContrf);
            this.Name = "Operator";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabContrf.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mytrips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ridesgridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accomodationgridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mybookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reportChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabContrf;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
   
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.ComboBox destination;
        private System.Windows.Forms.TextBox duration;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.TextBox groupsize;
        private System.Windows.Forms.TextBox baseprice;
        private System.Windows.Forms.ComboBox capacitytype;
        private System.Windows.Forms.ComboBox triptype;
        private System.Windows.Forms.Button ADDTRIP;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView Mytrips;
        private System.Windows.Forms.Button updateTrip;
        private System.Windows.Forms.Button deleteTrip;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button exit;
   
        private System.Windows.Forms.PictureBox pictureBox3;
        private Microsoft.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private System.Windows.Forms.DataGridView ridesgridview;
        private System.Windows.Forms.DataGridView accomodationgridview;
        private System.Windows.Forms.Button addRide;
        private System.Windows.Forms.Button addAccomodation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView mybookings;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart reportChart;
        private System.Windows.Forms.ComboBox reportComboBox;
    }
}

