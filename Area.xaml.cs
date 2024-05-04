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
    public partial class Area : Window
    {
        private int _selectedAreaId;
        DataBase _database = new DataBase();
        public Area(int selectedAreaId)
        {
            InitializeComponent();
            _selectedAreaId = selectedAreaId;
            Loaded += Area_Loaded;
            LoadProfiles(_selectedAreaId);
            
        }
        private void Area_Loaded(object sender, RoutedEventArgs e)
        {
           
            
            int selectedAreaId = _selectedAreaId; 

            
            List<Picket> pickets = _database.GetPickets(selectedAreaId);

            
            var plotModel = new PlotModel();
            var scatterSeries = new ScatterSeries();

            foreach (var picket in pickets)
            {
                scatterSeries.Points.Add(new ScatterPoint(picket.X, picket.Y));
            }

            plotModel.Series.Add(scatterSeries);
            Plot.Model = plotModel;
            
        }

        private void LoadProfiles(int areaId)
        {
            
            DataBase _database = new DataBase();

            
            List<Profile> profiles = _database.GetProfiles(areaId);

            
            lstProfiles.Items.Clear();

            
            foreach (var profile in profiles)
            {
                lstProfiles.Items.Add(profile);
            }
        }

        

       
        private void lstProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstProfiles.SelectedItem != null)
            {
                
                Profile selectedProfile = (Profile)lstProfiles.SelectedItem;

                
                ProfileWindow profileWindow = new ProfileWindow(selectedProfile.Id);
                profileWindow.Show();
            }
        }

        private void OpenProfile_Click(object sender, RoutedEventArgs e)
        {
            if (lstProfiles.SelectedItem != null)
            {
                
                Profile selectedProfile = (Profile)lstProfiles.SelectedItem;

                
                ProfileWindow profileWindow = new ProfileWindow(selectedProfile.Id);
                profileWindow.Show();
                
            }
            else
            {
                MessageBox.Show("Выберите профиль!");
            }
        }
    }
}
