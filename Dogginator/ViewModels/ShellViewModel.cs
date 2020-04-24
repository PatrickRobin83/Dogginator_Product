/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ShellViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System.Data.Entity.Core.Metadata.Edm;
using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using de.rietrob.dogginator_product.DogginatorLibrary.Enums;
using de.rietrob.dogginator_product.DogginatorLibrary.Helper;
using de.rietrob.dogginator_product.LoginLibrary.ViewModels;
using de.rietrob.dogginator_product.CustomerLibrary.ViewModels;
using de.rietrob.dogginator_product.DogLibrary.ViewModels;
using de.rietrob.dogginator_product.InvoiceLibrary.ViewModels;
using de.rietrob.dogginator_product.ProductLibrary.ViewModels;
using de.rietrob.dogginator_product.AppointmentLibrary.ViewModels;
using de.rietrob.dogginator_product.ConsistedBookLibrary.ViewModels;
using de.rietrob.dogginator_product.OverviewLibrary.ViewModels;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<UserModel>
    {
        #region Fields
        private bool _isLoggedIn = false;
        private bool _isAdmin = false;
        #endregion

        #region Properties

        /// <summary>
        /// Tells the Programlogic that the User is logged in.
        /// </summary>
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                NotifyOfPropertyChange(() => IsLoggedIn);
            }
        }

        /// <summary>
        /// Tells the Logic, that the logged in User is an admin user an can open the Options and change everything.
        /// </summary>
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
            //ActivateItem(new ManageInvoicesViewModel());
            //ActivateItem(new ManageAppointmentsViewModel());
            //ActivateItem(new ManageProductsViewModel());
            //ActivateItem(new OverViewViewModel(true,true));
            //ActivateItem(new ManageCustomerViewModel());
            //ActivateItem(new ManageDogsViewModel());
        }
        #endregion

        #region Methods

        /// <summary>
        /// If true the Load OverView Button is activated
        /// </summary>
        public bool CanLoadOverview
        {
            get { return IsLoggedIn; }
        }
        
        /// <summary>
        /// Loads the Overview View
        /// </summary>
        public void LoadOverview()
        {
           ActivateItem(new OverViewViewModel(IsLoggedIn, IsAdmin));
        }

        /// <summary>
        /// if true the Load Customer Button is activated
        /// </summary>
        public bool CanLoadCustomer
        {
            get { return IsLoggedIn; }
        }

        /// <summary>
        /// Loads the Customer View
        /// </summary>
        public void LoadCustomer()
        {
            ActivateItem(new ManageCustomerViewModel());
        }

        /// <summary>
        /// If true the Load Dog View Button is activated
        /// </summary>
        public bool CanLoadDog
        {
            get { return IsLoggedIn; }
        }

        /// <summary>
        /// Loads the Dog OverView View
        /// </summary>
        public void LoadDog()
        {
            ActivateItem(new ManageDogsViewModel());
        }

        /// <summary>
        /// If true the Load Appointment View Button is activated
        /// </summary>
        public bool CanLoadAppointment
        {
            get { return IsLoggedIn; }
        }

        /// <summary>
        /// Loads the Appointment Overview View
        /// </summary>
        public void LoadAppointment()
        {
            ActivateItem(new ManageAppointmentsViewModel());
        }

        /// <summary>
        /// If true the Load Consisted Book Button is activated
        /// </summary>
        public bool CanLoadConsistedBook
        {
            get { return IsLoggedIn; }
        }

        /// <summary>
        /// Loads the Consisted Book Overview View
        /// </summary>
        public void LoadConsistedBook()
        {
            ActivateItem(new ConsistedBookViewModel());
        }

        /// <summary>
        /// if true the Load Biling button is activated
        /// </summary>
        public bool CanLoadBilling
        {
            get { return IsLoggedIn; }
        }

        /// <summary>
        /// Loads the Billing Overview View
        /// </summary>
        public void LoadBilling()
        {
            ActivateItem(new ManageInvoicesViewModel());
        }

        /// <summary>
        /// If true the Load Product Button is activated
        /// </summary>
        public bool CanLoadProducts
        {
            get { return IsLoggedIn; }
        }

        /// <summary>
        /// Loads the Products Overview View
        /// </summary>
        public void LoadProducts()
        {
            ActivateItem(new ManageProductsViewModel());
        }

        /// <summary>
        /// If true the Logout Button is activated
        /// </summary>
        public bool CanLogout
        {
            get
            {
                return IsLoggedIn;
            }
        }

        /// <summary>
        /// Logging out the user and return to login window
        /// </summary>
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

        /// <summary>
        /// Closes the complete program
        /// </summary>
        public void Exit()
        {
            TryClose();
        }

        /// <summary>
        /// Indicates is the given Usermodel from the login form an Admin Account or not
        /// </summary>
        /// <param name="message"></param>
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
