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

namespace Departments
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientList clients = new ClientList
        {
            new Client("dep1", "sur1", "name1", "sec1", "111", "111"),
            new Client("dep2", "sur21", "name21", "sec21", "2221", "2221"),
            new Client("dep3", "sur3", "name3", "sec3", "333", "333"),
            new Client("dep2", "sur22", "name22", "sec22", "2222", "2222"),
            new Client("dep2", "sur2", "name2", "sec2", "222", "222"),
            new Client("dep1", "sur12", "name12", "sec12", "1112", "1112")
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            cb_dep_client_refresh();
            cb_dep_refresh();

            chb_create_dep.IsChecked = false;
            text_dep.Visibility = Visibility.Collapsed;

        }

        private void dg_refresh()
        {

            if ((cb_dep.SelectedItem as string) == "All")
            {
                dg_clients.ItemsSource = clients;
            }
            else
            {
                dg_clients.ItemsSource = clients.GetDepartment(cb_dep.SelectedItem as string);
            }

            dg_clients.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void dg_clients_Loaded(object sender, RoutedEventArgs e)
        {
            dg_refresh();
        }

        private void dg_clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = (Client)dg_clients.SelectedItem;

            text_dep.Text = client.Department;
            cb_client_dep.SelectedItem = client.Department;
            text_surname.Text = client.Surname;
            text_name.Text = client.Name;
            text_secondname.Text = client.Secondname;
            text_phone.Text = client.Phone;
            text_passport.Text = client.Passport;
        }

        private void cb_dep_client_refresh()
        {
            List<string> deps = new List<string>();

            foreach(Client client in clients)
            {
                if (!deps.Contains(client.Department))
                {
                    deps.Add(client.Department);
                }
            }

            cb_client_dep.ItemsSource = deps;
        }

        private void cb_dep_refresh()
        {
            List<string> deps = new List<string>
            {
                "All"
            };

            foreach (Client client in clients)
            {
                if (!deps.Contains(client.Department))
                {
                    deps.Add(client.Department);
                }
            }

            cb_dep.ItemsSource = deps;
        }

        private void chb_create_dep_Click(object sender, RoutedEventArgs e)
        {
            if (chb_create_dep.IsChecked == true)
            {
                text_dep.Visibility = Visibility.Visible;
                cb_client_dep.Visibility = Visibility.Collapsed;
            }
            else
            {
                text_dep.Visibility = Visibility.Collapsed;
                cb_client_dep.Visibility = Visibility.Visible;
            }
        }

        private void cb_dep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg_refresh();
        }
    }
}
