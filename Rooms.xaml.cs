using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class Rooms : Window
    {
        public Rooms()
        {
            InitializeComponent();

            HotelManagerDBEntities db = new HotelManagerDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var rooms = from r in db.Rooms
                        select r;

            var roomPrices = from rp in db.Room_prices
                             select rp;

            this.gridRooms.ItemsSource = rooms.ToList();
            this.gridRoomPrices.ItemsSource = roomPrices.ToList();
        }

        private void OccupiedRooms_Click(object sender, RoutedEventArgs e)
        {
            HotelManagerDBEntities db = new HotelManagerDBEntities();
            var occupied = Convert.ToDateTime(txtOccupied.Text);
            string str = "";

            var occupiedRooms = from or in db.Rooms
                            join v in db.Visits on or.Id equals v.Id_room
                            where v.Check_out > occupied
                            select or.Number;

            foreach (var o in occupiedRooms)
            {
                str += " " + o.ToString();
            }

            MessageBox.Show(str);
        }
    }
}