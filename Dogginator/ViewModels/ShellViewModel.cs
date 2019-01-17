using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ShellViewModel : Conductor<object>, IPassword
    {
        #region Fields
        private string _username;
        private SecureString _password;
        #endregion

        #region Properties
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public System.Security.SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        #endregion

        #region Contstructor
        public ShellViewModel()
        {
            GlobalConfig.InitalizeConnections(DataBaseType.SQLite);
            ActivateItem(new OverViewViewModel());
        }
        #endregion

        #region Methods

        public bool CanLoadOverview()
        {
            return true;
        }
        
        public void LoadOverview()
        {
           ActivateItem(new OverViewViewModel());
        }

        public bool CanLoadCustomer()
        {
            return true;
        }

        public void LoadCustomer()
        {
            ActivateItem(new ManageCustomerViewModel());
        }

        public bool CanLoadDog()
        {
            return true;
        }

        public void LoadDog()
        {
            ActivateItem(new ManageDogsViewModel());
        }

        public bool CanLoadAppointment()
        {
            return true;
        }

        public void LoadAppointment()
        {
            ActivateItem(new ManageAppointmentsViewModel());
        }

        public bool CanLoadConsistedBook()
        {
            return true;
        }

        public void LoadConsistedBook()
        {
            ActivateItem(new ConsistedBookViewModel());
        }
        public void Exit()
        {
            TryClose();
        }
        public bool CanLogin()
        {
            return true;
        }

        

        public void Login()
        {
            
        }

        #endregion
    }
}
