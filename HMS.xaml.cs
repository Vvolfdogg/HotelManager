using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManager
{
    /// <summary>
    /// Interaction logic for HMS.xaml
    /// </summary>
    public partial class HMS : Window
    {
        public HMS()
        {
            InitializeComponent();

            HotelManagementDBEntities db = new HotelManagementDBEntities();
            
            var visits = from v in db.Visits
                         select v;

            var clients = from c in db.Clients
                          select c;

            /*foreach (var visit in visits)
            {
                Console.WriteLine(visit.Client);
            }*/

            this.gridVisits.ItemsSource = visits.ToList();
            this.gridClients.ItemsSource = clients.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            HotelManagementDBEntities db = new HotelManagementDBEntities();

            Client clientObject = new Client()
            {
                Name = txtName.Text,
                Surname = txtSur.Text,
                Phone_number = txtPhone.Text
            };

            db.Clients.Add(clientObject);
            db.SaveChanges();

            this.gridClients.ItemsSource = db.Clients.ToList();
        }


        private void btnAddVisit_Click(object sender, RoutedEventArgs e)
        {
            HotelManagementDBEntities db = new HotelManagementDBEntities();

            Visit visitObject = new Visit()
            {
                Id_client = Convert.ToInt32(txtClient.Text),
                Id_room = Convert.ToInt32(txtRoom.Text),
                //Check_in = txtCheckIn.Text,
            };

            db.Visits.Add(visitObject);
            db.SaveChanges();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateVisit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
