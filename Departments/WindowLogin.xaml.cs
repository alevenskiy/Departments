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

namespace Departments
{
    /// <summary>
    /// Логика взаимодействия для WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public bool isManager = false;

        public WindowLogin()
        {
            InitializeComponent();
        }

        private void butt_manager_Click(object sender, RoutedEventArgs e)
        {
            isManager = true;
            this.DialogResult = true;
        }

        private void butt_consultant_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
