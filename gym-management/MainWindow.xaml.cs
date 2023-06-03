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
using Npgsql;
using System.Data;
using gym_management.ViewModels;

namespace gym_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowVM(this);
            Hidden.Visibility = Visibility.Hidden;
        }
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Hidden.Text = Password.Password.ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            //ToHide.Visibility = Visibility.Hidden;
            //Title.Content = "SIGN IN";
            //ContinueBtn.Visibility = Visibility.Hidden;
            //LoginBtn.Visibility = Visibility.Visible;
        }

        private void ToHide_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ToHide.Visibility = Visibility.Hidden;
            Title.Content = "SIGN IN";
            ContinueBtn.Visibility = Visibility.Hidden;
            LoginBtn.Visibility = Visibility.Visible;
        }

    }
}
