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
using OxyPlot;
using OxyPlot.Series;
using KursRadio.DB;


namespace KursRadio
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private int _profileId;
        private DataBase _database = new DataBase();

        public ProfileWindow(int profileId)
        {
            InitializeComponent();
            _profileId = profileId;
            LoadProfileCoordinates();
        }

        private void LoadProfileCoordinates()
        {
            
            List<ProfileCoordinate> coordinates = _database.GetProfileCoordinates(_profileId);

            
            var model = new PlotModel { Title = "Profile Coordinates" };

            
            var series = new LineSeries { MarkerType = MarkerType.Circle };

            
            foreach (var coordinate in coordinates)
            {
                series.Points.Add(new DataPoint(coordinate.X, coordinate.Y));
            }

            
            model.Series.Add(series);

            
            plotView.Model = model;
        }
    }
}