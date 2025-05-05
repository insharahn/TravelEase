
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TourOperator
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();

            // Set form properties
            this.BackColor = Color.FromArgb(180, 192, 185); // background color
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(300, 200); // square-ish size
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Message";

            // Create and set a TableLayoutPanel to organize layout
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 2;
            layout.ColumnCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // 70% for label
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F)); // 30% for button

            // Create label for message
            Label messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.ForeColor = Color.FromArgb(34, 53, 53); // text color
            messageLabel.Font = new Font("Microsoft Uighur", 20.25F, FontStyle.Bold);
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Padding = new Padding(0, 35, 0, 0); // move it slightly down


            // Create OK button
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.BackColor = Color.FromArgb(34, 53, 53); // button background
            okButton.ForeColor = Color.FromArgb(180, 192, 185); // button text
            okButton.Font = new Font("Microsoft Uighur", 14F, FontStyle.Bold);
            okButton.Anchor = AnchorStyles.None;
            okButton.Size = new Size(100, 40);
            okButton.Click += (s, e) => this.Close();

            // Add controls to layout
            layout.Controls.Add(messageLabel, 0, 0);
            layout.Controls.Add(okButton, 0, 1);

            // Add layout to form
            this.Controls.Add(layout);
            this.AcceptButton = okButton;
        }
        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            // Not used, can leave empty
        }

        // Static method to use like MessageBox.Show
        public static void Show(string message)
        {
            using (CustomMessageBox box = new CustomMessageBox(message))
            {
                box.ShowDialog();
            }
        }
    }
}

