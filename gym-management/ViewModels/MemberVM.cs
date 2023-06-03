using gym_management.Helper;
using gym_management.Models;
using gym_management.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gym_management.ViewModels
{
    public class MemberVM : BasePropertyChanged
    {
        MemberRepo mr = new MemberRepo();
        UserRepository ur = new UserRepository();
        MembershipRepo memr = new MembershipRepo();
        public RelayCommand EditC { get; set; }
        public Member CurrentMember { get; set; }
        public User CurrentUser { get; set; }
        public Abonament Membership { get; set; }
        public MemberWindow CurrentWindow { get; set; }
        public MemberVM() { }
        public MemberVM(User user, MemberWindow window)
        {
            ObservableCollection<Member> all = mr.GetAllMembers();
            ObservableCollection<Abonament> allMemberships = memr.GetAllMemberships();
            CurrentUser = user;
            CurrentWindow = window;

            bool exists = false;
            foreach (var m in all)
            {
                if (m.UserId == CurrentUser.Id)
                {
                    CurrentMember = m;
                    Membership = allMemberships.First(x => x.Id == CurrentMember.MemebershipId);
                    exists = true;
                    break;
                }
            }
            if (exists == false)
                CurrentWindow.NoAb.Visibility = System.Windows.Visibility.Visible;
            else CurrentWindow.HasAb.Visibility = System.Windows.Visibility.Visible;
            EditC = new RelayCommand(o => EditContact());
        }
        public string Update { get; set; }
        private void EditContact()
        {
            ur.ModifyUserContact(CurrentUser, Update);
            CurrentUser.Contact = Update;
            Update = String.Empty;
        }
    }
}
