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
using KursRadio.DB;

namespace KursRadio
{
    /// <summary>
    /// Логика взаимодействия для AreaList.xaml
    /// </summary>
    public partial class AreaList : Window
    {
        private DataBase _database;
        private int _projectId;
        private List<KursRadio.DB.Area> _areas;
        private ListView _lstAreas;

        public AreaList(int projectId)
        {
            InitializeComponent();
            _database = new DataBase();
            _projectId = projectId;
            _lstAreas = lstAreas;
            LoadAreas();
        }
        private void LoadAreas()
        {
            List<KursRadio.DB.Area> allAreas = _database.GetAreas(_projectId);
            _areas = allAreas;
            _lstAreas.ItemsSource = _areas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_lstAreas.SelectedItem != null)
            {
                KursRadio.DB.Area selectedDbArea = (KursRadio.DB.Area)_lstAreas.SelectedItem;

                
                int selectedAreaId = selectedDbArea.Id;

                
                Area areaWindow = new Area(selectedAreaId);
                areaWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите площадь!");
            }
        }
    }
}
