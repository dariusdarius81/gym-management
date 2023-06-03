using gym_management.Helper;
using gym_management.Models;
using gym_management.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gym_management.ViewModels
{
    public class MainWindowVM
    {
        UserRepository ur = new UserRepository();
        public MainWindowVM(MainWindow window)
        {

        }
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<User>(ur.AddUser);
                }
                return addCommand;
            }
        }
        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand<User>(ur.GetType);
                }
                return loginCommand;
            }
        }
    }
}
