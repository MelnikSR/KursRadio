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
    /// Логика взаимодействия для ProjectList.xaml
    /// </summary>
    public partial class ProjectList : Window
    {
        private DataBase _dataBase;
        private List<Project> _projects;
        private ListView _lstProjects;
        DataBase database = new DataBase();
        
        public ProjectList()
        {
            InitializeComponent();
            _dataBase = database;
            _lstProjects = lstProjects;
            LoadProjects();
        }

        private void LoadProjects()
        {
            _projects = _dataBase.GetProjects(); // Получаем список проектов из БД
            _lstProjects.ItemsSource = _projects; // Отображаем список проектов в ListView
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            if (_lstProjects.SelectedItem != null)
            {
                Project selectedProject = (Project)_lstProjects.SelectedItem;
                int selectedProjectId = selectedProject.Id;

                AreaList areaListWindow = new AreaList(selectedProjectId);
                areaListWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите проект!");
            }
        }
    }
}
