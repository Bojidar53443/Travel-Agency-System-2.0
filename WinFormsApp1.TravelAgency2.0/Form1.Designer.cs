namespace WinFormsApp1.TravelAgency2._0
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAddClient = new Button();
            btnAddTrip = new Button();
            btnAddBooking = new Button();
            btnShowTrips = new Button();
            btnShowBookings = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnAddClient
            // 
            btnAddClient.Location = new Point(24, 42);
            btnAddClient.Name = "btnAddClient";
            btnAddClient.Size = new Size(129, 29);
            btnAddClient.TabIndex = 0;
            btnAddClient.Text = "showClients";
            btnAddClient.UseVisualStyleBackColor = true;
            btnAddClient.Click += btnAddClient_Click;
            // 
            // btnAddTrip
            // 
            btnAddTrip.Location = new Point(24, 97);
            btnAddTrip.Name = "btnAddTrip";
            btnAddTrip.Size = new Size(129, 29);
            btnAddTrip.TabIndex = 1;
            btnAddTrip.Text = "showTrips";
            btnAddTrip.UseVisualStyleBackColor = true;
            btnAddTrip.Click += btnAddTrip_Click;
            // 
            // btnAddBooking
            // 
            btnAddBooking.Location = new Point(24, 151);
            btnAddBooking.Name = "btnAddBooking";
            btnAddBooking.Size = new Size(129, 29);
            btnAddBooking.TabIndex = 2;
            btnAddBooking.Text = "showBookings";
            btnAddBooking.UseVisualStyleBackColor = true;
            btnAddBooking.Click += btnAddBooking_Click;
            // 
            // btnShowTrips
            // 
            btnShowTrips.Location = new Point(24, 202);
            btnShowTrips.Name = "btnShowTrips";
            btnShowTrips.Size = new Size(129, 29);
            btnShowTrips.TabIndex = 3;
            btnShowTrips.Text = "save changes";
            btnShowTrips.UseVisualStyleBackColor = true;
            btnShowTrips.Click += btnShowTrips_Click;
            // 
            // btnShowBookings
            // 
            btnShowBookings.Location = new Point(24, 261);
            btnShowBookings.Name = "btnShowBookings";
            btnShowBookings.Size = new Size(129, 29);
            btnShowBookings.TabIndex = 4;
            btnShowBookings.Text = "button5";
            btnShowBookings.UseVisualStyleBackColor = true;
            btnShowBookings.Click += btnShowBookings_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(256, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(494, 344);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(btnShowBookings);
            Controls.Add(btnShowTrips);
            Controls.Add(btnAddBooking);
            Controls.Add(btnAddTrip);
            Controls.Add(btnAddClient);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnAddClient;
        private Button btnAddTrip;
        private Button btnAddBooking;
        private Button btnShowTrips;
        private Button btnShowBookings;
        private DataGridView dataGridView1;
    }
}
