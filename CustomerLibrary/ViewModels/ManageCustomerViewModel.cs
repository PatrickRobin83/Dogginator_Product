﻿/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ManageCustomerViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System.Linq;

namespace de.rietrob.dogginator_product.CustomerLibrary.ViewModels
{
    public class ManageCustomerViewModel : Conductor<object>.Collection.OneActive, IHandle<CustomerModel>
    {
        #region Fields
        private bool _customerListIsVisible = true;
        private bool _loadCreateCustomerIsVisible = false;
        private bool _loadCustomerDetailsIsVisible = false;
        private Screen _activeAddCreateCustomerView;
        private Screen _activeAddCustomerDetailsView;
        private CustomerModel _selectedCustomer;
        private BindableCollection<CustomerModel> _availableCustomers = new BindableCollection<CustomerModel>();
        private string _customerSearchText = "";
        private bool _showAlsoInactive = false;


        #endregion

        #region Properties
        public bool CustomerListIsVisible
        {
            get { return _customerListIsVisible; }
            set
            {
                _customerListIsVisible = value;
                NotifyOfPropertyChange(() => CustomerListIsVisible);
            }
        }
        public bool LoadCreateCustomerIsVisible
        {
            get { return _loadCreateCustomerIsVisible; }
            set
            {
                _loadCreateCustomerIsVisible = value;
                NotifyOfPropertyChange(() => LoadCreateCustomerIsVisible);
            }
        }
        public bool LoadCustomerDetailsIsVisible
        {
            get { return _loadCustomerDetailsIsVisible; }
            set
            {
                _loadCustomerDetailsIsVisible = value;
                NotifyOfPropertyChange(() => LoadCustomerDetailsIsVisible);
            }
        }
        public Screen ActiveAddCreateCustomerView
        {
            get { return _activeAddCreateCustomerView; }
            set
            {
                _activeAddCreateCustomerView = value;
                NotifyOfPropertyChange(() => ActiveAddCreateCustomerView);
            }
        }
        public Screen ActiveAddCustomerDetailsView
        {
            get { return _activeAddCustomerDetailsView; }
            set
            {
                _activeAddCustomerDetailsView = value;
                NotifyOfPropertyChange(() => ActiveAddCustomerDetailsView);
            }
        }
        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                NotifyOfPropertyChange(() => SelectedCustomer);
                NotifyOfPropertyChange(() => CanDeleteCustomer);
                NotifyOfPropertyChange(() => CanLoadCustomerDetails);
            }
        }
        public BindableCollection<CustomerModel> AvailableCustomers
        {
            get { return _availableCustomers; }
            set
            {
                _availableCustomers = value;
                NotifyOfPropertyChange(() => AvailableCustomers);
            }
        }
        public bool CanLoadCustomerDetails
        {
            get
            {
                bool output = true;

                if (SelectedCustomer == null)
                {
                    output = false;
                }
                return output;
            }
        }
        public bool CanDeleteCustomer
        {
            get
            {
                bool output = true;

                if (SelectedCustomer == null)
                {
                    output = false;
                }
                return output;
            }
        }
        public string CustomerSearchText
        {
            get { return _customerSearchText; }
            set
            {
                _customerSearchText = value;
                NotifyOfPropertyChange(() => CustomerSearchText);
                AvailableCustomers = getCustomers();
                ActiveCustomer(AvailableCustomers);
                NotifyOfPropertyChange(() => AvailableCustomers);

            }
        }
        public bool ShowAlsoInactive
        {
            get { return _showAlsoInactive; }
            set
            {
                _showAlsoInactive = value;
                NotifyOfPropertyChange(() => ShowAlsoInactive);
                AvailableCustomers = getCustomers();
                ActiveCustomer(AvailableCustomers);
                NotifyOfPropertyChange(() => AvailableCustomers);

            }
        }

        #endregion

        #region Contstructor
        public ManageCustomerViewModel()
        { 
            AvailableCustomers = new BindableCollection<CustomerModel>(GlobalConfig.Connection.Get_CustomerAll());
            ActiveCustomer(AvailableCustomers);
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
            
        }
        #endregion

        #region Methods

        private BindableCollection<CustomerModel> getCustomers()
        {
            AvailableCustomers = new BindableCollection<CustomerModel>(GlobalConfig.Connection.SearchResultsCustomer(CustomerSearchText, ShowAlsoInactive));
           
            return AvailableCustomers;
        }

        private void ActiveCustomer(BindableCollection<CustomerModel> customerList)
        {
            foreach (CustomerModel model in customerList)

            {
                if (model.Active)
                {
                    model.CustomerActive = "Aktiv";
                }
                else
                {
                    model.CustomerActive = "inaktiv";
                }
            }
        }

        /// <summary>
        /// Loads the CreateNewCustomer View in the ContentArea
        /// </summary>
        public void LoadCreateNewCustomer()
        {
            ActiveAddCreateCustomerView = new CreateCustomerViewModel();
            Items.Add(ActiveAddCreateCustomerView);
            CustomerListIsVisible = false;
            LoadCreateCustomerIsVisible = true;
            LoadCustomerDetailsIsVisible = false;
        }

        /// <summary>
        /// Opens the CustomerDetailsView in the Contentarea
        /// </summary>
        public void LoadCustomerDetails()
        {
            SelectedCustomer = GlobalConfig.Connection.Get_Customer(SelectedCustomer);
            ActiveAddCustomerDetailsView = new CustomerDetailsViewModel(SelectedCustomer); 
            Items.Add(ActiveAddCustomerDetailsView);
            LoadCustomerDetailsIsVisible = true;
            CustomerListIsVisible = false;
            LoadCreateCustomerIsVisible = false;
        }

        /// <summary>
        /// This Deletes the Customer from the List and deactivates thew Customer, but the Customer is still in the Database.  
        /// </summary>
        public void DeleteCustomer()
        {
            GlobalConfig.Connection.DeleteCustomer(SelectedCustomer);
            AvailableCustomers.Remove(SelectedCustomer);
            
            NotifyOfPropertyChange(() => AvailableCustomers);
        }

        /// <summary>
        /// gets a Model from CreatNewCustomer and Adds this Model to the List in the ManageCustomerView 
        /// and also Add the Customer to the Database
        /// </summary>
        /// <param name="message">
        /// This Param is a CustomerModel with all it needs to be saved in the Database
        /// </param>
        public void Handle(CustomerModel message)
        {
            if(message.FirstName == null)
            {
                LoadCreateCustomerIsVisible = false;
                LoadCustomerDetailsIsVisible = false;
                CustomerListIsVisible = true;
                SelectedCustomer = null;
                return;
            }

            if (AvailableCustomers.Any(c => message.Id == c.Id))
            {
            }
            else
            {
                AvailableCustomers.Add(message);
            }
            LoadCreateCustomerIsVisible = false;
            LoadCustomerDetailsIsVisible = false;
            CustomerListIsVisible = true;
            SelectedCustomer = null;
            AvailableCustomers = new BindableCollection<CustomerModel>(GlobalConfig.Connection.Get_CustomerAll());
            ActiveCustomer(AvailableCustomers);
            NotifyOfPropertyChange(() => AvailableCustomers);


        }
        #endregion
    }
}
