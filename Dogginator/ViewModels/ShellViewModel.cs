using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Models;
using DogginatorLibrary.Enums;
using DogginatorLibrary.Helper;
using LoginLibrary.ViewModels;
using AppointmentLibrary.ViewModels;
using ConsistedBookLibrary.ViewModels;
using CustomerLibrary.ViewModels;
using DogLibrary.ViewModels;
using InvoiceLibrary.ViewModels;
using OverviewLibrary.ViewModels;
using ProductLibrary.ViewModels;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<UserModel>
    {
        #region Fields
        private bool _isLoggedIn = false;
        private bool _isAdmin = false;
        #endregion

        #region Properties
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                NotifyOfPropertyChange(() => IsLoggedIn);
            }
        }
        public bool IsAdmin {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                NotifyOfPropertyChange(() => IsAdmin);
            }
        }
    

        #endregion

        #region Contstructor
        public ShellViewModel()
        {
            GlobalConfig.InitalizeConnections(DataBaseType.SQLite);
            BackupDatabaseHelper.BackupDatabase();
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
            //TODO: Activate the LoginView after Debugging
            ActivateItem(new LoginViewModel());
            //ActivateItem(new ManageAppointmentsViewModel());
            //ActivateItem(new ManageProductsViewModel());
            //ActivateItem(new OverViewViewModel(true,true));
        }
        #endregion

        #region Methods
       
        public bool CanLoadOverview
        {
            get { return IsLoggedIn; }
        }
        
        public void LoadOverview()
        {
           ActivateItem(new OverViewViewModel(IsLoggedIn, IsAdmin));
        }

        public bool CanLoadCustomer
        {
            get { return IsLoggedIn; }
        }

        public void LoadCustomer()
        {
            ActivateItem(new ManageCustomerViewModel());
        }

        public bool CanLoadDog
        {
            get { return IsLoggedIn; }
        }

        public void LoadDog()
        {
            ActivateItem(new ManageDogsViewModel());
        }

        public bool CanLoadAppointment
        {
            get { return IsLoggedIn; }
        }

        public void LoadAppointment()
        {
            ActivateItem(new ManageAppointmentsViewModel());
        }

        public bool CanLoadConsistedBook
        {
            get { return IsLoggedIn; }
        }

        public void LoadConsistedBook()
        {
            ActivateItem(new ConsistedBookViewModel());
        }

        public bool CanLoadBilling
        {
            get { return IsLoggedIn; }
        }

        public void LoadBilling()
        {
            ActivateItem(new ManageInvoicesViewModel());
        }

        public bool CanLoadProducts
        {
            get { return IsLoggedIn; }
        }

        public void LoadProducts()
        {
            ActivateItem(new ManageProductsViewModel());
        }

        public bool CanLogout
        {
            get
            {
                return IsLoggedIn;
            }
        }

        public void Logout()
        {
            IsLoggedIn = false;
            IsAdmin = false;
            ActivateItem(new LoginViewModel());
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => CanLoadAppointment);
            NotifyOfPropertyChange(() => CanLoadConsistedBook);
            NotifyOfPropertyChange(() => CanLoadCustomer);
            NotifyOfPropertyChange(() => CanLoadDog);
            NotifyOfPropertyChange(() => CanLoadOverview);
            NotifyOfPropertyChange(() => CanLogout);
            NotifyOfPropertyChange(() => CanLoadBilling);
            NotifyOfPropertyChange(() => CanLoadProducts);
        }

        public void Exit()
        {
            TryClose();
        }

        public void Handle(UserModel message)
        {

            if (!string.IsNullOrWhiteSpace(message.Password))
            {
                IsLoggedIn = true;
                if (message.IsAdmin)
                {
                    IsAdmin = true;
                }
                LoadOverview();
            }
            else
            {
                IsLoggedIn = false;
            }
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => CanLoadAppointment);
            NotifyOfPropertyChange(() => CanLoadConsistedBook);
            NotifyOfPropertyChange(() => CanLoadCustomer);
            NotifyOfPropertyChange(() => CanLoadDog);
            NotifyOfPropertyChange(() => CanLoadOverview);
            NotifyOfPropertyChange(() => CanLogout);
            NotifyOfPropertyChange(() => CanLoadBilling);
            NotifyOfPropertyChange(() => CanLoadProducts);
        }

        #endregion
    }
}
