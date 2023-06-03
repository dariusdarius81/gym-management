using gym_management.ViewModels;
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

namespace gym_management
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            this.DataContext = new AdminVM();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MemberWindow.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Visible;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Buttons.Visibility = Visibility.Visible;
            MemberWindow.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Hidden;
            AbWindow.Visibility = Visibility.Hidden;
            FurnizorWindow.Visibility = Visibility.Hidden;
            EquipmentWindow.Visibility = Visibility.Hidden;
            UtilitatiWindow.Visibility = Visibility.Hidden;
        }

        private void AbBtn_Click(object sender, RoutedEventArgs e)
        {
            AbWindow.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Visible;
        }

        private void FurnizorBtn_Click(object sender, RoutedEventArgs e)
        {
            FurnizorWindow.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Visible;
        }

        private void EchipamentBtn_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Visible;
        }

        private void UtilsBtn_Click(object sender, RoutedEventArgs e)
        {
            UtilitatiWindow.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Visible;
        }
    }
}
