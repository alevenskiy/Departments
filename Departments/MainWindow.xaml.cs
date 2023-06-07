using System;
using System.Collections.Generic;
using System.IO;
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
        ClientList clients = new ClientList();

        IEmployee employee = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login();
            Refresh();
        }

        public void Refresh()
        {
            chb_create_dep.IsChecked = false; // checkbox create department in client field
            text_dep.Visibility = Visibility.Collapsed;
            dp_client_dep.Visibility = Visibility.Visible;

            if (employee is Manager)
            {
                ManagerModSet(true); // make all features turn on
            }
            else
            {
                ManagerModSet(false); // turn off features for manager
            }

            butt_save.Content = "Save"; // turn off creating new client
            butt_del_client.IsEnabled = false;

            cb_client_dep.SelectedIndex = -1; // clear client field
            text_dep.Clear();
            text_surname.Clear();
            text_name.Clear();
            text_secondname.Clear();
            text_phone.Clear();
            text_passport.Clear();

            dg_clients.SelectedItem = null;

            clients = employee.DownloadClients();


            if (clients.Count == 0)
            {
                MessageBox.Show("Client base is empty.\nPlease Create new Client as a Manager");
            }
            else
            {
                dg_refresh(); // datagrid refresh
                cb_dep_client_refresh(); // combobox departments in client field
                cb_dep_refresh(); // combobox departments 
            }
        }

        private void ManagerModSet(bool enable)
        {
            cb_client_dep.IsEnabled = enable;
            chb_create_dep.IsEnabled = enable;

            text_dep.IsEnabled = enable;
            text_surname.IsEnabled = enable;
            text_name.IsEnabled = enable;
            text_secondname.IsEnabled = enable;
            text_passport.IsEnabled = enable;

            butt_del_client.IsEnabled = enable;
            butt_del_dep.IsEnabled = enable;
            butt_create_client.IsEnabled = enable;
        }

        private void dg_refresh() // here we need to check the file
        {
            if ((cb_dep.SelectedItem as string) == "All")
            {
                dg_clients.ItemsSource = clients;
            }
            else
            {
                dg_clients.ItemsSource = clients.GetDepartment(cb_dep.SelectedItem as string);
            }
            if(dg_clients.Columns.Count > 0)
                dg_clients.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void dg_clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = (Client)dg_clients.SelectedItem;

            if (client != null)
            {
                cb_client_dep.SelectedItem = client.Department;
                text_dep.Text = client.Department;
                text_surname.Text = client.Surname;
                text_name.Text = client.Name;
                text_secondname.Text = client.Secondname;
                text_phone.Text = client.Phone;
                text_passport.Text = client.Passport;

                if(employee is Manager)
                    butt_del_client.IsEnabled = true;
            }
            else
            {
                cb_client_dep.SelectedIndex = -1;
                text_dep.Clear();
                text_surname.Clear();
                text_name.Clear();
                text_secondname.Clear();
                text_phone.Clear();
                text_passport.Clear();
            }
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
                dp_client_dep.Visibility = Visibility.Collapsed;
            }
            else
            {
                text_dep.Visibility = Visibility.Collapsed;
                dp_client_dep.Visibility = Visibility.Visible;
            }
        }

        private void cb_dep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg_refresh();
        }

        private void butt_logout_Click(object sender, RoutedEventArgs e)
        {
            Login();
            Refresh();
        }

        private void Login()
        {
            WindowLogin windowLogin = new WindowLogin();
            windowLogin.Owner = this;

            if (windowLogin.ShowDialog() == true)
            {
                if (windowLogin.isManager)
                    employee = new Manager();
                else
                    employee = new Consultant();
            }
            else
                MessageBox.Show("Log In failed");
        }

        private void butt_save_Click(object sender, RoutedEventArgs e)
        {
            if(employee is Manager)
            {
                if (butt_save.Content.ToString() == "Save") // edit client
                {
                    if (!string.IsNullOrWhiteSpace(text_dep.Text) &&
                        !string.IsNullOrWhiteSpace(text_surname.Text) &&
                        !string.IsNullOrWhiteSpace(text_name.Text) &&
                        !string.IsNullOrWhiteSpace(text_secondname.Text) &&
                        !string.IsNullOrWhiteSpace(text_phone.Text) &&
                        !string.IsNullOrWhiteSpace(text_passport.Text))
                    {
                        if(chb_create_dep.IsChecked == true)
                            (employee as Manager).DepartmentChange((Client)dg_clients.SelectedItem, text_dep.Text);
                        else
                            (employee as Manager).DepartmentChange((Client)dg_clients.SelectedItem, cb_client_dep.SelectedItem.ToString());

                        (employee as Manager).SurnameChange((Client)dg_clients.SelectedItem, text_surname.Text);
                        (employee as Manager).NameChange((Client)dg_clients.SelectedItem, text_name.Text);
                        (employee as Manager).SecondnameChange((Client)dg_clients.SelectedItem, text_secondname.Text);
                        (employee as Manager).PhoneChange((Client)dg_clients.SelectedItem, text_phone.Text);
                        (employee as Manager).PassportChange((Client)dg_clients.SelectedItem, text_passport.Text);

                        employee.SaveClients();

                        MessageBox.Show("Changes saved");

                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Data fields can not be empty");
                    }
                }
                else  // create client
                {
                    if (!string.IsNullOrWhiteSpace(text_dep.Text) &&
                        !string.IsNullOrWhiteSpace(text_surname.Text) &&
                        !string.IsNullOrWhiteSpace(text_name.Text) &&
                        !string.IsNullOrWhiteSpace(text_secondname.Text) &&
                        !string.IsNullOrWhiteSpace(text_phone.Text) &&
                        !string.IsNullOrWhiteSpace(text_passport.Text))
                    {
                        Client client = new Client(
                            text_dep.Text,
                            text_surname.Text,
                            text_name.Text,
                            text_secondname.Text,
                            text_phone.Text,
                            text_passport.Text);

                        (employee as Manager).AddClient(client);

                        employee.SaveClients();

                        MessageBox.Show($"{text_surname.Text} added");

                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Data fields can not be empty");
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(text_phone.Text))
                {
                    (employee as Consultant).PhoneChange((Client)dg_clients.SelectedItem, text_phone.Text);

                    employee.SaveClients();

                    MessageBox.Show("Change saved");

                    Refresh();
                }
                else
                {
                    MessageBox.Show("Data fields can not be empty");
                }
            }
        }

        private void butt_cancel_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void butt_del_client_Click(object sender, RoutedEventArgs e)
        {
            string surname = (dg_clients.SelectedItem as Client).Surname;

            if (MessageBox.Show(
                $"Are you sure want to remove client {surname}?", 
                $"Removal {surname}", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
            {
                (employee as Manager).RemoveClient((Client)dg_clients.SelectedItem);

                employee.SaveClients();

                MessageBox.Show($"{surname} removed");
            }

            Refresh();
        }

        private void butt_del_dep_Click(object sender, RoutedEventArgs e)
        {
            if (cb_dep.Text == "All")
            {
                if (MessageBox.Show(
                $"Are you sure want to remove all database?",
                $"Removal all",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //-----------------------------------------------------------------------------------
                }
            }
            else
            {
                string dep = cb_dep.Text;

                if (MessageBox.Show(
                $"Are you sure want to remove department {dep}?",
                $"Removal {dep}",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    (employee as Manager).RemoveDepartment(cb_dep.Text);

                    employee.SaveClients();

                    MessageBox.Show($"{dep} removed");
                }
            }

            Refresh() ;
        }

        private void butt_create_client_Click(object sender, RoutedEventArgs e)
        {
            butt_save.Content = "Add";
        }
    }
}
