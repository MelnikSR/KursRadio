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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KursRadio.DB;

namespace KursRadio
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBase _database;
        public MainWindow()
        {
            InitializeComponent();
            _database = new DataBase();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text; 
            string password = txtPassword.Password; 

            if (_database.ValidateUser(username, password))
            {
                
                ProjectList projectListWindow = new ProjectList(); 
                projectListWindow.Show(); 
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!"); 
            }
        }
    }
}
