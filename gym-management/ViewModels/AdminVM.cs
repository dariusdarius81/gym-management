using gym_management.Helper;
using gym_management.Models;
using gym_management.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gym_management.ViewModels
{
    public class UserView
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Abonament { get; set; }
    }
    public class AdminVM : BasePropertyChanged
    {
        SupplierRepo sr = new SupplierRepo();
        UserRepository ur = new UserRepository();
        MembershipRepo mr = new MembershipRepo();
        MemberRepo membr = new MemberRepo();
        EquipmentRepo er = new EquipmentRepo();
        UtilityRepo utilr = new UtilityRepo();

        public RelayCommand AddMember { get; set; }
        public RelayCommand DeleteMember { get; set; }
        public RelayCommand EditMembership { get; set; }
        public RelayCommand AddAb { get; set; }
        public RelayCommand DeleteAb { get; set; }
        public RelayCommand DeleteSup { get; set; }
        public RelayCommand EditSup { get; set; }
        public RelayCommand DeleteEq { get; set; }
        public RelayCommand EditEqu { get; set; }
        public RelayCommand DeleteUtil { get; set; }
        public ObservableCollection<User> AllUsers { get; set; }
        public ObservableCollection<Member> AllMembers { get; set; }
        public ObservableCollection<Abonament> AllMemberships { get; set; }
        public ObservableCollection<Supplier> AllSuppliers { get; set; }
        public ObservableCollection<Equipment> AllEquipments { get; set; }
        public ObservableCollection<Utility> AllUtils { get; set; }
        public ObservableCollection<UserView> UserViews { get; set; }
        private ObservableCollection<string> availableMemberships;
        public ObservableCollection<string> AvailableMemberships
        {
            get => availableMemberships;
            set
            {
                availableMemberships = value;

                NotifyPropertyChanged("AvailableMemberships");
            }
        }
        public ObservableCollection<string> AvailableSuppliers { get; set; }
        public AdminVM()
        {
            AddMember = new RelayCommand(o => AddMemb());
            DeleteMember = new RelayCommand(o => DeleteMemb());
            EditMembership = new RelayCommand(o => EditMemb());
            AddAb = new RelayCommand(o => AddAbon());
            DeleteAb = new RelayCommand(o => DeleteAbon());
            DeleteSup = new RelayCommand(o => Deletesupp());
            EditSup = new RelayCommand(o => EditSupp());
            DeleteEq = new RelayCommand(o => DelEq());
            EditEqu = new RelayCommand(o => EditEq());
            DeleteUtil = new RelayCommand(o => DeleteUt());

            AllUsers = new ObservableCollection<User>();
            AllUsers = ur.GetAllUsers();

            AllMembers = new ObservableCollection<Member>();
            AllMembers = membr.GetAllMembers();

            AllSuppliers = new ObservableCollection<Supplier>();
            AllSuppliers = sr.GetAllSuppliers();

            AllMemberships = new ObservableCollection<Abonament>();
            AllMemberships = mr.GetAllMemberships();
            AvailableMemberships = new ObservableCollection<string>();

            AllEquipments = new ObservableCollection<Equipment>();
            AllEquipments = er.GetAllEquipments();

            AllUtils = new ObservableCollection<Utility>();
            AllUtils = utilr.GetAllUtil();

            AvailableSuppliers = new ObservableCollection<string>();
            foreach (Supplier sup in AllSuppliers)
                AvailableSuppliers.Add(sup.Name);

            foreach (Abonament ab in AllMemberships)
                AvailableMemberships.Add(ab.Name);

            UserViews = new ObservableCollection<UserView>();
            foreach (User stud in AllUsers)
            {
                int ok = 0;
                if (stud.Password != "admin123")
                    if (AllMembers.Count == 0)
                        UserViews.Add(new UserView() { Id = stud.Id, Nume = stud.Nume, Prenume = stud.Prenume, Abonament = "" });
                    else
                    {
                        foreach (Member mb in AllMembers)
                        {
                            if (mb.UserId == stud.Id)
                            {
                                UserViews.Add(new UserView() { Id = stud.Id, Nume = stud.Nume, Prenume = stud.Prenume, Abonament = AllMemberships.First(x => x.Id == mb.MemebershipId).Name });
                                ok = 1;
                            }
                        }
                        if (ok == 0)
                        {
                            UserViews.Add(new UserView() { Id = stud.Id, Nume = stud.Nume, Prenume = stud.Prenume, Abonament = "" });
                        }
                    }
            }
        }

        private void DeleteUt()
        {
            utilr.DeleteUtil(SelectedUtil);
            AllUtils.Remove(SelectedUtil);
        }

        private void EditEq()
        {
            er.ModifyEq(SelectedEquipment, EditedQuantity);
            AllEquipments.Add(new Equipment() { Id = SelectedEquipment.Id, Name = SelectedEquipment.Name, Muscle = SelectedEquipment.Muscle, Price = SelectedEquipment.Price, Quantity = int.Parse(EditedQuantity), IdFurnizor = SelectedEquipment.IdFurnizor });
            AllEquipments.Remove(SelectedEquipment);
        }

        private void DelEq()
        {
            er.DeleteEq(SelectedEquipment);
            AllEquipments.Remove(SelectedEquipment);
        }

        private void EditSupp()
        {
            sr.ModifySupplier(SelectedSupplier, UpdatedLocation);
            AllSuppliers.Add(new Supplier() { Id = SelectedSupplier.Id, Name = SelectedSupplier.Name, Location = UpdatedLocation });
            AllSuppliers.Remove(SelectedSupplier);
            UpdatedLocation = String.Empty;
        }

        private void Deletesupp()
        {
            sr.DeleteSupplier(SelectedSupplier);
            AllSuppliers.Remove(SelectedSupplier);
        }

        private void DeleteAbon()
        {
            int ok = mr.DeleteMember(SelectedAbonamentObj);
            if (ok == 1)
                AllMemberships.Remove(SelectedAbonamentObj);
        }

        private void AddAbon()
        {
            mr.AddAb(new Abonament() { Name = SelectedAb });
        }

        private void EditMemb()
        {
            foreach (var ab in AllMemberships)
            {
                if (ab.Name == SelectedAb)
                {
                    membr.ModifyMember(SelectedUser, ab);
                    break;
                }
            }
        }
        private void DeleteMemb()
        {
            membr.DeleteMember(SelectedUser);
            UserViews.Remove(SelectedUserView);
        }
        private void AddMemb()
        {
            foreach (var ab in AllMemberships)
            {
                if (ab.Name == SelectedAb)
                {
                    membr.AddMember(SelectedUser, ab);
                    UserViews.Remove(SelectedUserView);
                    UserViews.Add(new UserView() { Id = SelectedUser.Id, Nume = SelectedUser.Nume, Prenume = SelectedUser.Prenume, Abonament = selectedAb });
                    break;
                }
            }
        }
        private string selectedAb;
        public string SelectedAb
        {
            get { return selectedAb; }
            set
            {
                selectedAb = value;
                NotifyPropertyChanged("SelectedClass");
            }
        }
        private Abonament selectedAbonamentObj;
        public Abonament SelectedAbonamentObj
        {
            get { return selectedAbonamentObj; }
            set
            {
                selectedAbonamentObj = value;
                NotifyPropertyChanged("SelectedAbonamentObj");
            }
        }
        public string EditedQuantity { get; set; }
        public string MusclesType { get; set; }
        public string UpdatedLocation { get; set; }

        private UserView selectedUserView;
        public UserView SelectedUserView
        {
            get { return selectedUserView; }
            set
            {
                selectedUserView = value;
                IsSelected = selectedUserView != null;
                if (selectedUserView != null)
                    SelectedUser = UserConverter(selectedUserView);
                NotifyPropertyChanged("SelectedStudentView");
            }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
        public User SelectedUser { get; set; }
        public Supplier SelectedSupplier { get; set; }
        public Equipment SelectedEquipment { get; set; }
        public Utility SelectedUtil { get; set; }
        public User UserConverter(UserView view)
        {
            foreach (User stud in AllUsers)
            {
                if (stud.Nume == view.Nume && stud.Prenume == view.Prenume)
                    return stud;
            }
            return null;
        }
        private ICommand addSupplier;
        public ICommand AddSupplier
        {
            get
            {
                if (addSupplier == null)
                {
                    addSupplier = new RelayCommand<Supplier>(sr.AddSupplier);
                }
                return addSupplier;
            }
        }
        private ICommand addUtility;
        public ICommand AddUtility
        {
            get
            {
                if (addUtility == null)
                {
                    addUtility = new RelayCommand<Utility>(utilr.AddEq);
                }
                return addUtility;
            }
        }
        private ICommand addEquipment;
        public ICommand AddEquipment
        {
            get
            {
                if (addEquipment == null)
                {
                    addEquipment = new RelayCommand<Equipment>(er.AddEq);
                }
                return addEquipment;
            }
        }
    }
}
