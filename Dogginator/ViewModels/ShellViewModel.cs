using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.DataAccess;
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
    public class ShellViewModel : Conductor<object>, IHandle<bool>
    {
        #region Fields
        private bool _isLoggedIn = false;
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
        #endregion

        #region Contstructor
        public ShellViewModel()
        {
            GlobalConfig.InitalizeConnections(DataBaseType.SQLite);
            ActivateItem(new LoginViewModel());
        }
        #endregion

        #region Methods
       
        public bool CanLoadOverview()
        {
            return IsLoggedIn;
        }
        
        public void LoadOverview()
        {
           ActivateItem(new OverViewViewModel());
        }

        public bool CanLoadCustomer()
        {
            return IsLoggedIn;
        }

        public void LoadCustomer()
        {
            ActivateItem(new ManageCustomerViewModel());
        }

        public bool CanLoadDog()
        {
            return IsLoggedIn;
        }

        public void LoadDog()
        {
            ActivateItem(new ManageDogsViewModel());
        }

        public bool CanLoadAppointment()
        {
            return IsLoggedIn;
        }

        public void LoadAppointment()
        {
            ActivateItem(new ManageAppointmentsViewModel());
        }

        public bool CanLoadConsistedBook()
        {
            return IsLoggedIn;
        }

        public void LoadConsistedBook()
        {
            ActivateItem(new ConsistedBookViewModel());
        }
        public void Exit()
        {
            TryClose();
        }

        public void Handle(bool message)
        {
            if (message)
            {
                IsLoggedIn = true;
            }
            else
            {
                IsLoggedIn = false;
            }

        }


        #endregion
    }
}
