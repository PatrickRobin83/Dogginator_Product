using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
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
        private string _city = "";
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
        #endregion

        #region Properties
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                NotifyOfPropertyChange(() => Birthday);
            }
        }
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
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyOfPropertyChange(() => City);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
        public string ZipCode
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;
                NotifyOfPropertyChange(() => ZipCode);
                NotifyOfPropertyChange(() => CanCreateCustomer);
            }
        }
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
        public string NoteToSave
        {
            get { return _noteToSave; }
            set
            {
                _noteToSave = value;
                NotifyOfPropertyChange(() => NoteToSave);
            }
        }
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
        public BindableCollection<DogModel> OwnedDogs

        {
            get { return _ownedDogs; }
            set
            {
                _ownedDogs = value;
                NotifyOfPropertyChange(() => OwnedDogs);
            }
        }
        public List<NoteModel> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                NotifyOfPropertyChange(() => Notes);
            }

        }
        public NoteModel SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                NotifyOfPropertyChange(() => SelectedNote);
            }
        }
        public BindableCollection<DogModel> AvailableDogs
        {
            get { return _availableDogs; }
            set
            {
                _availableDogs = value;
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }
        public string CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                NotifyOfPropertyChange(() => CreateDate);
            }
        }
        public string EditDate
        {
            get { return _editDate; }
            set
            {
                _editDate = value;
                NotifyOfPropertyChange(() => EditDate);
            }
        }
        public Screen ActiveAddCreateNewDogView
        {
            get { return _activeAddCreateNewDogView; }
            set
            {
                _activeAddCreateNewDogView = value;
                NotifyOfPropertyChange(() => ActiveAddCreateNewDogView);
            }
        }
        public bool DogListsIsVisible
        {
            get { return _dogListsIsVisible; }
            set
            {
                _dogListsIsVisible = value;
                NotifyOfPropertyChange(() => DogListsIsVisible);
            }
        }
        public bool CreateNewDogIsVisible
        {
            get { return _createNewDogIsVisible; }
            set
            {
                _createNewDogIsVisible = value;
                NotifyOfPropertyChange(() => CreateNewDogIsVisible);
            }
        }
        public bool RDBFemaleSalutionIsChecked
        {
            get { return _rDBFemaleSalutionIsChecked; }
            set
            {
                _rDBFemaleSalutionIsChecked = value;
                NotifyOfPropertyChange(() => RDBFemaleSalutionIsChecked);
            }
        }
        public bool RDBMaleSalutionIsChecked
        {
            get { return _rDBMaleSalutionIsChecked; }
            set
            {
                _rDBMaleSalutionIsChecked = value;
                NotifyOfPropertyChange(() => RDBMaleSalutionIsChecked);
            }
        }
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


        #endregion

        #region Contstructor
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
                    System.Windows.MessageBox.Show($"Der Hund {SelectedDogToRemove.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                                   $"{SelectedDogToRemove.CustomerList.First().FirstName} {SelectedDogToRemove.CustomerList.First().LastName} der einzige Besitzer ist");
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
                    !string.IsNullOrWhiteSpace(Housenumber) && !string.IsNullOrWhiteSpace(ZipCode) && !string.IsNullOrWhiteSpace(City) && 
                    (!string.IsNullOrWhiteSpace(PhoneNumber) || !string.IsNullOrWhiteSpace(MobileNumber) || !string.IsNullOrWhiteSpace(Email)))
                {
                    output = true;
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
            cm.City = City;
            cm.PhoneNumber = PhoneNumber;
            cm.MobileNumber = MobileNumber;
            cm.Email = Email;
            cm.Birthday = Birthday.ToString("dd.MM.yyyy");
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
            ActiveAddCreateNewDogView = new CreateNewDogViewModel();
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
