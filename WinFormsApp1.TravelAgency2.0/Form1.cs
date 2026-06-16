using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace WinFormsApp1.TravelAgency2._0
{
    public partial class Form1 : Form
    {
        private TravelAgencyDbContext _db;

        public Form1()
        {
            InitializeComponent();
            _db = new TravelAgencyDbContext();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _db = new TravelAgencyDbContext();
        }

        //showClients
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            _db.Clients.Load();
            dataGridView1.DataSource = _db.Clients.Local.ToBindingList();
        }

        //showTrips
        private void btnAddTrip_Click(object sender, EventArgs e)
        {
            _db.Trips.Load();
            dataGridView1.DataSource = _db.Trips.Local.ToBindingList();
        }

        //showBookings
        private void btnAddBooking_Click(object sender, EventArgs e)
        {
            _db.Bookings.Load();
            dataGridView1.DataSource = _db.Bookings.Local.ToBindingList();
        }

       
        //save changes
        private void btnShowTrips_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                _db.SaveChanges();
                MessageBox.Show("✅ Всичко е записано успешно в SQL базата данни!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Грешка при запис: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private void btnShowBookings_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _db?.Dispose();
        }
    }
}
