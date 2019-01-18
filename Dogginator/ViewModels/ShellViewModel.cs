using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.DataAccess;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
            ActivateItem(new LoginViewModel());
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
                // TODO What happens if a Customer Creation is cancled?
                IsLoggedIn = false;
            }
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => CanLoadAppointment);
            NotifyOfPropertyChange(() => CanLoadConsistedBook);
            NotifyOfPropertyChange(() => CanLoadCustomer);
            NotifyOfPropertyChange(() => CanLoadDog);
            NotifyOfPropertyChange(() => CanLoadOverview);
            NotifyOfPropertyChange(() => CanLogout);
        }


        #endregion
    }
}
