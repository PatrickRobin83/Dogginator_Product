/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CreateCustomerViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;

namespace de.rietrob.dogginator_product.CustomerLibrary.ViewModels
{
    public class CreateCustomerViewModel : Conductor<object>.Collection.OneActive, IHandle<DogModel>
    {
        #region Fields
        private int _id;
        private string _firstName = "";
        private string _lastName = "";
        private string _street = "";
        private string _housenumber = "";
        private string _zipcode = "";
        private string _selectedcity = "";
        private string _phoneNumber = "";
        private string _mobileNumber = "";
        private string _email = "";
        private DateTime _birthday;
        private string _noteToSave = "";
        private string _createDate = "";
        private string _editDate = "";
        private DogModel _selectedDog = new DogModel();
        private DogModel _selectedDogToRemove = new DogModel();
        private BindableCollection<DogModel> _ownedDogs = new BindableCollection<DogModel>();
        private List<NoteModel> _notes = new List<NoteModel>();
        private NoteModel _selectedNote = new NoteModel();
        private BindableCollection<DogModel> _availableDogs = new BindableCollection<DogModel>();
        private Screen _activeAddCreateNewDogView;
        private bool _dogListsIsVisible = true;
        private bool _createNewDogIsVisible = false;
        private bool _rDBFemaleSalutionIsChecked;
        private bool _rDBMaleSalutionIsChecked;
        private bool _dogCreated = false;
        private List<string> _citys = new List<string>();
        #endregion

        #region Properties

        /// <summary>
        /// Database Table Id from the Customer after creating the customer 
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }
        /// <summary>
        /// Birthday of the Customer
        /// </summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                NotifyOfPropertyChange(() => Birthday);
            }
        }
        /// <summary>
        /// Email of the Customer
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// Mobile Number of the Customer
        /// </summary>
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                _mobileNumber = value;
                NotifyOfPropertyChange(() => MobileNumber);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// PhoneNumber of the Customer
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// the selected city from the combobox
        /// </summary>
        public string SelectedCity
        {
            get { return _selectedcity; }
            set
            {
                _selectedcity = value;
                NotifyOfPropertyChange(() => SelectedCity);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// zipcode of the customer
        /// </summary>
        public string ZipCode
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;

                if (ZipCode.Length > 5)
                {
                    ZipCode = string.Empty;
                }

                if (ZipCode.Length == 5)
                {
                    Int32 inputNumber;

                    if (false == Int32.TryParse(ZipCode, out inputNumber))
                    {
                        ZipCode = string.Empty;
                    }
                    else
                    {
                        
                        Citys = GlobalConfig.Connection.getCityToZipcode(ZipCode);
                        if (Citys.Count > 0)
                        {
                            SelectedCity = Citys[0];
                        }
                        NotifyOfPropertyChange(() => Citys);
                    }
                    if (ZipCode.Length < 5 && ZipCode.Length >= 0)
                    {
                        Citys = new List<string>();
                        NotifyOfPropertyChange(() => Citys);
                    }
                    NotifyOfPropertyChange(() => ZipCode);
                    NotifyOfPropertyChange(() => CanCreateCustomer);
                }

                   
            
            }
        }
        /// <summary>
        /// housenumber of the customer
        /// </summary>
        public string Housenumber
        {
            get { return _housenumber; }
            set
            {
                _housenumber = value;
                NotifyOfPropertyChange(() => Housenumber);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// streetname where the customer lives
        /// </summary>
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                NotifyOfPropertyChange(() => Street);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// Lastname of the customer
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// firstname of the customer
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// special notes for the customer
        /// </summary>
        public string NoteToSave
        {
            get { return _noteToSave; }
            set
            {
                _noteToSave = value;
                NotifyOfPropertyChange(() => NoteToSave);
            }
        }
        /// <summary>
        /// the selected dog to add to the customer
        /// </summary>
        public DogModel SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                NotifyOfPropertyChange(() => SelectedDog);
                NotifyOfPropertyChange(() => CanAddDogToList);
            }
        }
        /// <summary>
        /// Remove a Dog from the Ownershiplist
        /// </summary>
        public DogModel SelectedDogToRemove
        {
            get { return _selectedDogToRemove; }
            set
            {
                _selectedDogToRemove = value;
                NotifyOfPropertyChange(() => SelectedDogToRemove);
                NotifyOfPropertyChange(() => CanRemoveDogFromList);
            }
        }
        /// <summary>
        /// List of dogs from the customer
        /// </summary>
        public BindableCollection<DogModel> OwnedDogs

        {
            get { return _ownedDogs; }
            set
            {
                _ownedDogs = value;
                NotifyOfPropertyChange(() => OwnedDogs);
            }
        }
        /// <summary>
        /// List of the special notes for the customer
        /// </summary>
        public List<NoteModel> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                NotifyOfPropertyChange(() => Notes);
            }

        }
        /// <summary>
        /// Selected note from the Note Listview
        /// </summary>
        public NoteModel SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                NotifyOfPropertyChange(() => SelectedNote);
            }
        }
        /// <summary>
        /// List of all Available Dogs from the Database
        /// </summary>
        public BindableCollection<DogModel> AvailableDogs
        {
            get { return _availableDogs; }
            set
            {
                _availableDogs = value;
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }
        /// <summary>
        /// Date the customer was created.
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                NotifyOfPropertyChange(() => CreateDate);
            }
        }
        /// <summary>
        /// Last date the cutomer was editted
        /// </summary>
        public string EditDate
        {
            get { return _editDate; }
            set
            {
                _editDate = value;
                NotifyOfPropertyChange(() => EditDate);
            }
        }
        /// <summary>
        /// Create New Dog View
        /// </summary>
        public Screen ActiveAddCreateNewDogView
        {
            get { return _activeAddCreateNewDogView; }
            set
            {
                _activeAddCreateNewDogView = value;
                NotifyOfPropertyChange(() => ActiveAddCreateNewDogView);
            }
        }
        /// <summary>
        /// bool that indicates --> is the List with dogs visible
        /// </summary>
        public bool DogListsIsVisible
        {
            get { return _dogListsIsVisible; }
            set
            {
                _dogListsIsVisible = value;
                NotifyOfPropertyChange(() => DogListsIsVisible);
            }
        }
        /// <summary>
        /// bool that indicates --> is the create new dog view is visible
        /// </summary>
        public bool CreateNewDogIsVisible
        {
            get { return _createNewDogIsVisible; }
            set
            {
                _createNewDogIsVisible = value;
                NotifyOfPropertyChange(() => CreateNewDogIsVisible);
            }
        }
        /// <summary>
        /// RadioButton Gender Female
        /// </summary>
        public bool RDBFemaleSalutionIsChecked
        {
            get { return _rDBFemaleSalutionIsChecked; }
            set
            {
                _rDBFemaleSalutionIsChecked = value;
                NotifyOfPropertyChange(() => RDBFemaleSalutionIsChecked);
            }
        }
        /// <summary>
        /// Radiobutton Gender Male
        /// </summary>
        public bool RDBMaleSalutionIsChecked
        {
            get { return _rDBMaleSalutionIsChecked; }
            set
            {
                _rDBMaleSalutionIsChecked = value;
                NotifyOfPropertyChange(() => RDBMaleSalutionIsChecked);
            }
        }
        /// <summary>
        /// gives back true if the creation was successfull or false if the creation fails
        /// </summary>
        public bool DogCreated
        {
            get { return _dogCreated; }
            set
            {
                _dogCreated = value;
                NotifyOfPropertyChange(() => DogCreated);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        /// <summary>
        /// List of citys with the matching zipcode
        /// </summary>
        public List<string> Citys
        {
            get { return _citys; }
            set
            {
                _citys = value;
                NotifyOfPropertyChange(() => Citys);
            }
        }


        #endregion

        #region Contstructor
        /// <summary>
        /// Initilize everything thats important to create an Customer
        /// </summary>
        public CreateCustomerViewModel()
        {
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
            RDBFemaleSalutionIsChecked = true;
            Birthday = DateTime.Now;
            WireUpLists();
        }
        #endregion

        #region Methods

        /// <summary>
        /// This checks if it is possible to put a available dog to the Customer Owned Dog List. 
        /// Therefore a Item has to be selected on the available dogs list.
        /// </summary>
        public bool CanAddDogToList
        {

            get
            {
                if (SelectedDog != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// Adds the selected dog to the owned customer list
        /// </summary>
        public void AddDogToList()
        {
            OwnedDogs.Add(SelectedDog);
            AvailableDogs.Remove(SelectedDog);
        }
        
        /// <summary>
        /// Checks if it is possible to remove a dog from the owned dogs list. 
        /// Therefore an Item has to be selected on the owned dogs list.
        /// </summary>
        public bool CanRemoveDogFromList
        {
            get
            {
                if (SelectedDogToRemove != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Removes the Selected item from the customer owned dogs list. 
        /// </summary>
        public void RemoveDogFromList()
        {
            if(SelectedDogToRemove.Id > 0)
            {
                if(SelectedDogToRemove.CustomerList.Count > 1)
                {
                    AvailableDogs.Add(SelectedDogToRemove);
                    NotifyOfPropertyChange(() => AvailableDogs);
                    OwnedDogs.Remove(SelectedDogToRemove);
                    NotifyOfPropertyChange(() => OwnedDogs);
                }
                else
                {
                    ErrorMessages.DogToRemoveError(SelectedDogToRemove);
                }

            }
            else
            {
               
               OwnedDogs.Remove(SelectedDogToRemove);
               NotifyOfPropertyChange(() => OwnedDogs);
            }

        }

        /// <summary>
        /// Get a List of all Dogs that are stored in the Database so far.
        /// </summary>
        /// <returns>
        /// Returns the List and shows it in the available dogs list
        /// </returns>
        private BindableCollection<DogModel> WireUpLists()
        {
            return AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
        }

        /// <summary>
        /// This checks if all required informations are set to create a new customer
        /// </summary>
        public bool CanCreateCustomer
        {
            get
            {
                bool output = false;
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Street) &&
                    !string.IsNullOrWhiteSpace(Housenumber) && !string.IsNullOrWhiteSpace(ZipCode) && !string.IsNullOrWhiteSpace(SelectedCity) && 
                    (!string.IsNullOrWhiteSpace(PhoneNumber) || !string.IsNullOrWhiteSpace(MobileNumber) || !string.IsNullOrWhiteSpace(Email)))
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                        if (Email.Contains("@") && Email.Length >= 6)
                        {
                            output = true;
                            
                        }
                    }
                    else
                    {
                        output = true;
                    }
                    //output = true;
                }
                if (DogCreated)
                {
                    output = true;
                }
                return output;
            }
        }

        /// <summary>
        /// This creates a new customer put it into the database with all information including the owned dogs
        /// </summary>
        public void CreateCustomer()
        {
            CustomerModel cm = new CustomerModel();
            if (RDBFemaleSalutionIsChecked)
            {
                cm.Salution = "Frau";
            }
            else
            {
                cm.Salution = "Herr";
            }
            cm.FirstName = FirstName;
            cm.LastName = LastName;
            cm.Street = Street;
            cm.HouseNumber = Housenumber;
            cm.ZipCode = ZipCode;
            cm.City = SelectedCity;
            cm.PhoneNumber = PhoneNumber;
            cm.MobileNumber = MobileNumber;
            cm.Email = Email;
            cm.Birthday = Birthday;
            NoteModel noteModel = new NoteModel();
            if (!string.IsNullOrWhiteSpace(NoteToSave))
            {
                noteModel.Description = NoteToSave;
                Notes.Add(noteModel);
            }
            cm.Notes = Notes;
            cm.OwnedDogs = new List<DogModel>(OwnedDogs);
            if (PhoneNumber.Length > 0)
            {
                cm.PhoneNumber = PhoneNumber;
            }
            else
            {
                cm.PhoneNumber = "";
            }
            if (MobileNumber.Length > 0)
            {
                cm.MobileNumber = MobileNumber;
            }
            else
            {
                cm.MobileNumber = "";
            }
            if (Email.Length > 0)
            {
                cm.Email = Email;
            }
            else
            {
                cm.Email = "";
            }
            GlobalConfig.Connection.AddCustomer(cm);
            if(cm.OwnedDogs.Count > 0)
            foreach (DogModel dog in cm.OwnedDogs)
            {
                 GlobalConfig.Connection.GetAllCustomerForDog(dog);
            }
            DogCreated = false;
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(cm);
            this.TryClose();
        }

        /// <summary>
        /// Cancels the customer creation and returns to the ManageCustomerView
        /// </summary>
        public void CancelCreation()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new CustomerModel());
            this.TryClose();
        }

        /// <summary>
        /// Changes the bottom part of the view and loads the CreatenNwDogView 
        /// </summary>
        public void CreateDog()
        {
            ActiveAddCreateNewDogView = new DogLibrary.ViewModels.CreateNewDogViewModel();
            Items.Add(ActiveAddCreateNewDogView);
            DogListsIsVisible = false;
            CreateNewDogIsVisible = true;
        }

        /// <summary>
        /// Handle Message from the eventaggregator.
        /// </summary>
        /// <param name="dogModel">
        /// recieves a complete DogModel from the CreateNewDogViewModel
        /// </param>
        public void Handle(DogModel dogModel)
        {
            if (!string.IsNullOrWhiteSpace(dogModel.Name))
            {
                OwnedDogs.Add(dogModel);
                DogCreated = true;
            }
            DogListsIsVisible = true;
            CreateNewDogIsVisible = false;
            this.TryClose();
        }
        #endregion
    }
}
