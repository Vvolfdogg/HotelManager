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

            HotelManagerDBEntities db = new HotelManagerDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var visits = from v in db.Visits
                    select v;
                    /*{
                        //VisitId = v.Id_visit,
                        VisitClientID = v.Id_client,
                        VisitRoomID = v.Id_room,
                        VisitCheckIn = v.Check_in,
                        VisitCheckOut = v.Check_out
                    };*/

            var clients = from c in db.Clients
                    select new
                    {
                        ClientID = c.Id,
                        ClientName = c.Name,
                        ClientSurname = c.Surname,
                        ClientPhone = c.Phone_number
                    }; //c;

            /*foreach (var visit in visits)
            {
                Console.WriteLine(visit.Client);
            }*/

            this.gridVisits.ItemsSource = visits.ToList();
            this.gridClients.ItemsSource = clients.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            HotelManagerDBEntities db = new HotelManagerDBEntities();

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
            HotelManagerDBEntities db = new HotelManagerDBEntities();

            Visit visitObject = new Visit()
            {
                Id_client = Convert.ToInt32(txtClient.Text),
                Id_room = Convert.ToInt32(txtRoom.Text),
                Check_in = Convert.ToDateTime(txtCheckIn.Text),
                Check_out = Convert.ToDateTime(txtCheckOut.Text)
            };

            db.Visits.Add(visitObject);
            db.SaveChanges();

            this.gridVisits.ItemsSource = db.Visits.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HotelManagerDBEntities db = new HotelManagerDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            int id = Convert.ToInt32(txtClientId.Text);

            var row = from c in db.Clients
                      where c.Id == id
                      select c;

            Client client = row.SingleOrDefault();

            if (client != null)
            {
                client.Name = txtName.Text;
                client.Surname = txtSur.Text;
                client.Phone_number = txtPhone.Text;
                db.SaveChanges();
            }

            txtName.Text = "";
            txtSur.Text = "";
            txtPhone.Text = "";

            this.gridVisits.ItemsSource = db.Visits.ToList();
        }

        private void btnUpdateVisit_Click(object sender, RoutedEventArgs e)
        {
            HotelManagerDBEntities db = new HotelManagerDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            int id = Convert.ToInt32(txtVisitId.Text);

            var row = from v in db.Visits
                      where v.Id == id
                      select v;

            Visit visit = row.SingleOrDefault();

            if (visit != null)
            {
                visit.Id_client = Convert.ToInt32(txtClient.Text);
                visit.Id_room = Convert.ToInt32(txtRoom.Text);
                visit.Check_in = Convert.ToDateTime(txtCheckIn.Text);
                visit.Check_out = Convert.ToDateTime(txtCheckOut.Text);
                db.SaveChanges();
            }

            txtClient.Text = "";
            txtRoom.Text = "";
            txtCheckIn.Text = "";
            txtCheckOut.Text = "";

            this.gridVisits.ItemsSource = db.Visits.ToList();
        }

        private void gridClients_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridClients.SelectedIndex >= 0)
            {
                if (this.gridClients.SelectedItems.Count >= 0)
                {
                    
                    if (this.gridClients.SelectedItems[0].GetType() == typeof(Client))
                    {
                        Client c = (Client)this.gridClients.SelectedItems[0];
                        this.txtName.Text = c.Name;
                        this.txtSur.Text = c.Surname;
                        this.txtPhone.Text = c.Phone_number;
                    }
                }
            }
        }

        private void gridVisits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridVisits.SelectedIndex >= 0)
            {
                if (this.gridVisits.SelectedItems.Count >= 0)
                {

                    if (this.gridVisits.SelectedItems[0].GetType() == typeof(Visit))
                    {
                        Visit v = (Visit)this.gridVisits.SelectedItems[0];
                        this.txtClient.Text = v.Id_client.ToString();
                        this.txtRoom.Text = v.Id_room.ToString();
                        this.txtCheckIn.Text = v.Check_in.ToString();
                        this.txtCheckOut.Text = v.Check_out.ToString();
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            HotelManagerDBEntities db = new HotelManagerDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            this.gridClients.ItemsSource = db.Clients.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msbResult = MessageBox.Show("Are you sure?",
                "Delete Doctor",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if(msbResult == MessageBoxResult.Yes)
            {
                HotelManagerDBEntities db = new HotelManagerDBEntities();
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                int id = Convert.ToInt32(txtClientId.Text);

                var row = from c in db.Clients
                          where c.Id == id
                          select c;

                Client client = row.SingleOrDefault();

                if (client != null)
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }


                this.gridVisits.ItemsSource = db.Visits.ToList();
            }

            
        }

        private void btnLoadVisit_Click(object sender, RoutedEventArgs e)
        {
            HotelManagerDBEntities db = new HotelManagerDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            this.gridVisits.ItemsSource = db.Visits.ToList();
        }

        private void btnDeleteVisit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
