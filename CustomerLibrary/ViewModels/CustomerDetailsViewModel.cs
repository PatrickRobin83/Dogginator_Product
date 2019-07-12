using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Messages;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary.ViewModels
{
    public class CustomerDetailsViewModel : Conductor<object>.Collection.OneActive , IHandle<DogModel>
    {
        #region Fields
        private CustomerModel _cModel;
        private int _id;
        private string _salution;
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
        private string _create_Date = "";
        private string _edit_Date = "";
        private string _noteToAdd = "";
        private DogModel _selectedDog = new DogModel();
        private DogModel _availableSelectedDog = new DogModel();
        private BindableCollection<DogModel> _ownedDogs = new BindableCollection<DogModel>();
        private BindableCollection<NoteModel> _notes = new BindableCollection<NoteModel>();
        private BindableCollection<DogModel> _availableDogs = new BindableCollection<DogModel>();
        private NoteModel _selectedNote = new NoteModel();
        private bool _customerDetailsIsVisible;
        private bool _addDogIsVisible;
        private bool _editDogIsVisible;
        private Screen _activeAddDogView;
        private Screen _activeEditDogView;
        private bool _rDBFemaleSalutionIsChecked;
        private bool _rDBMaleSalutionIsChecked;
        private bool _isDogToSave = false;
        private bool _isDogRemoveToSave = false;
        private DogModel _dogToEdit = new DogModel();
        private bool _notActive;
        private List<string> _citys = new List<string>();
        #endregion

        #region Properties
        public List<string> Citys
        {
            get { return _citys; }
            set
            {
                _citys = value;
                NotifyOfPropertyChange(() => Citys);
            }
        }
        public CustomerModel CModel
        {
            get { return _cModel; }
            set
            {
                _cModel = value;
                NotifyOfPropertyChange(() => CModel);
            }
        }

        public Screen ActiveEditDogView
        {
            get { return _activeEditDogView ; }
            set
            {
                _activeEditDogView = value;
                NotifyOfPropertyChange(() => ActiveEditDogView);
            }
        }

        public bool EditDogIsVisible
        {
            get { return _editDogIsVisible; }
            set
            {
                _editDogIsVisible = value;
                NotifyOfPropertyChange(() => EditDogIsVisible);
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }
        public string Salution
        {
            get { return _salution; }
            set
            {
                _salution = value;
                NotifyOfPropertyChange(() => Salution);
            }
        }
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                NotifyOfPropertyChange(() => Birthday);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                _mobileNumber = value;
                NotifyOfPropertyChange(() => MobileNumber);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string SelectedCity
        {
            get { return _selectedcity; }
            set
            {
                _selectedcity = value;
                NotifyOfPropertyChange(() => SelectedCity);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
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
                    NotifyOfPropertyChange(() => CanSaveCustomer);
                }
            }
        }
        public string Housenumber
        {
            get { return _housenumber; }
            set
            {
                _housenumber = value;
                NotifyOfPropertyChange(() => Housenumber);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                NotifyOfPropertyChange(() => Street);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public string NoteToAdd
        {
            get { return _noteToAdd; }
            set
            {
                _noteToAdd = value;
                NotifyOfPropertyChange(() => NoteToAdd);
                NotifyOfPropertyChange(() => CanSaveNoteInList);
            }
        }
        public DogModel SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                NotifyOfPropertyChange(() => SelectedDog);
                NotifyOfPropertyChange(() => CanRemoveDogFromList);
                NotifyOfPropertyChange(() => CanEditDog);
                if (SelectedDog != null)
                {
                    _dogToEdit = SelectedDog;
                }
                else
                {
                    _dogToEdit = AvailableSelectedDog;
                }
                
            }
        }
        public DogModel AvailableSelectedDog
        {
            get { return _availableSelectedDog; }
            set
            {
                _availableSelectedDog = value;
                NotifyOfPropertyChange(() => AvailableSelectedDog);
                NotifyOfPropertyChange(() => CanAddDogToList);
                NotifyOfPropertyChange(() => CanEditDog);
                if(AvailableSelectedDog != null)
                {
                    _dogToEdit = AvailableSelectedDog;

                }
                else
                {
                    _dogToEdit = SelectedDog;
                }
            }
        }
        public BindableCollection<DogModel> OwnedDogs

        {
            get { return _ownedDogs; }
            set
            {
                _ownedDogs = value;
                NotifyOfPropertyChange(() => OwnedDogs);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public BindableCollection<NoteModel> Notes
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
                NotifyOfPropertyChange(() => CanDeleteNoteInList);
            }
        }
        public string Create_Date
        {
            get { return _create_Date; }
            set
            {
                _create_Date = value;
                NotifyOfPropertyChange(() => Create_Date);
            }
        }
        public string Edit_Date
        {
            get { return _edit_Date; }
            set
            {
                _edit_Date = value;
                NotifyOfPropertyChange(() => Edit_Date);
            }
        }
        public bool CustomerDetailsIsVisible
        {
            get { return _customerDetailsIsVisible; }
            set
            {
                _customerDetailsIsVisible = value;
                NotifyOfPropertyChange(() => CustomerDetailsIsVisible);
            }
        }
        public bool AddDogIsVisible
        {
            get { return _addDogIsVisible; }
            set
            {
                _addDogIsVisible = value;
                NotifyOfPropertyChange(() => AddDogIsVisible);
            }
        }
        public Screen ActiveAddDogView
        {
            get { return _activeAddDogView; }
            set
            {
                _activeAddDogView = value;
                NotifyOfPropertyChange(() => ActiveAddDogView);
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
            get  { return _rDBMaleSalutionIsChecked; }
            set
            {
                _rDBMaleSalutionIsChecked = value;
                NotifyOfPropertyChange(() => RDBMaleSalutionIsChecked);
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
        public bool IsDogToSave
        {
            get { return _isDogToSave; }
            set
            {
                _isDogToSave = value;
                NotifyOfPropertyChange(() => IsDogToSave);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public bool IsDogRemoveToSave
        {
            get { return _isDogRemoveToSave; }
            set
            {
                _isDogRemoveToSave = value;
                NotifyOfPropertyChange(() => IsDogRemoveToSave);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        public bool NotActive
        {
            get { return _notActive; }
            set
            {
                _notActive = value;
                NotifyOfPropertyChange(() => NotActive);
                NotifyOfPropertyChange(() => CanSaveCustomer);
            }
        }
        #endregion

        #region Contstructor
        public CustomerDetailsViewModel(CustomerModel customerModel)
        {
            CModel = customerModel;
            DataToFormularFields();
            CustomerDetailsIsVisible = true;
            AddDogIsVisible = false;
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Puts all filled in Data in the Customermodel Properties
        /// </summary>
        private void DataToFormularFields()
        {
            Salution = CModel.Salution;
            FirstName = CModel.FirstName;
            LastName = CModel.LastName;
            Street = CModel.Street;
            Housenumber = CModel.HouseNumber;
            ZipCode = CModel.ZipCode;
            SelectedCity = CModel.City;
            PhoneNumber = CModel.PhoneNumber;
            MobileNumber = CModel.MobileNumber;
            Email = CModel.Email;
            Birthday = CModel.Birthday;
            OwnedDogs = new BindableCollection<DogModel>(CModel.OwnedDogs);
            if(OwnedDogs != null && OwnedDogs.Count > 0)
            {
                foreach (DogModel dog in OwnedDogs)
                {
                    GlobalConfig.Connection.GetAllCustomerForDog(dog);
                }
            }
            Notes = new BindableCollection<NoteModel>(CModel.Notes);
            if (CModel.Active)
            {
                NotActive = false;

            }
            else
            {
                NotActive = true;                
            }
        }

        /// <summary>
        /// Checks if a note is Selected and activates the remove button
        /// </summary>
        public bool CanDeleteNoteInList
        {
            get
            {
                if(SelectedNote != null)
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
        /// Checks if a string is in the Notes Textbox and activates the AddNode Button
        /// </summary>
        public bool CanSaveNoteInList
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NoteToAdd))
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
        /// Saves the note in the List and in the Database to the corrosponding customer.
        /// </summary>
        public void SaveNoteInList()
        {
            Notes.Add(GlobalConfig.Connection.AddNoteToCustomer(CModel, NoteToAdd));
            CModel = GlobalConfig.Connection.Get_Customer(CModel);
            NotifyOfPropertyChange(() => CModel);
            NotifyOfPropertyChange(() => Notes);
            Edit_Date = CModel.Edit_Date;
            NotifyOfPropertyChange(() => Edit_Date);
        }

        /// <summary>
        /// Delete the note from the list and from the Database and the corrosponding customer
        /// </summary>
        public void DeleteNoteInList()
        {
            GlobalConfig.Connection.DeleteNoteFromList(SelectedNote, CModel);
            Notes.Remove(SelectedNote);
        }

        /// <summary>
        /// Delete the Dog from the Customer and sets the dog inactive
        /// </summary>
        public void RemoveDogFromList()
        {
            if (SelectedDog.CustomerList.Count > 1)
            {
                GlobalConfig.Connection.DeleteDogToCustomerRelation(CModel, SelectedDog);
                GlobalConfig.Connection.GetAllCustomerForDog(SelectedDog);
                SelectedDog.CustomerList.Remove(CModel);
                OwnedDogs.Remove(SelectedDog);
                NotifyOfPropertyChange(() => OwnedDogs);
                NotifyOfPropertyChange(() => AvailableDogs);
                IsDogRemoveToSave = true;
            }
            else
            {
                ErrorMessages.DogCanNotRemovedFromCustomerError(SelectedDog);
            }
            AvailableSelectedDog = null;
            SelectedDog = null;
        }

        /// <summary>
        /// Checks is a dog selected in the owned dogs list and activates the remove button
        /// </summary>
        public bool CanRemoveDogFromList
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

        public bool CanAddDogToList
        {
            get
            {
                bool output = false;
               
                if (OwnedDogs != null && OwnedDogs.Count > 0)
                {
                    foreach (DogModel dogModel in OwnedDogs)
                    {
                        if (AvailableSelectedDog != null && OwnedDogs.Any(d => AvailableSelectedDog.Id == d.Id))
                        {
                            output = false;
                            break;
                        }
                        else
                        {
                            output = true;
                        }
                        
                    }
                }
                else
                {
                    output = true;
                }
                if (AvailableSelectedDog == null)
                {
                    output = false;
                }
                return output;
            }
        }
        
        public void AddDogToList()
        {
            if(OwnedDogs.Count > 0)
            {
                    if (AvailableDogs.Any(ad => AvailableSelectedDog.Id != ad.Id))
                    {
                        OwnedDogs.Add(AvailableSelectedDog);
                        NotifyOfPropertyChange(() => OwnedDogs);
                    }
                    else
                    {
                    ErrorMessages.DogAlreadCustomerRelationError();
                        return;
                    }
                
            }
            else
            {
                OwnedDogs.Add(AvailableSelectedDog);
                NotifyOfPropertyChange(() => OwnedDogs);
            }
            if(AvailableSelectedDog.CustomerList == null)
            {
                AvailableSelectedDog.CustomerList = new List<CustomerModel>();
            }
            AvailableSelectedDog.CustomerList.Add(CModel);
            GlobalConfig.Connection.AddDogToCustomer(AvailableSelectedDog, CModel);
            GlobalConfig.Connection.GetAllCustomerForDog(AvailableSelectedDog);
            AvailableSelectedDog = null;
            SelectedDog = null;
            IsDogToSave = true;
        }

        public bool CanSaveCustomer
        {
            get
            {
                bool output = false;
                if (IsDogToSave || IsDogRemoveToSave)
                {
                    output = true;
                    
                }

                if (NotActive)
                {
                    if (CModel.Active == true)
                    {
                        output = true;
                    }
                }
                else
                {
                    if(CModel.Active == false)
                    {
                        output = true;
                    }
                }

                if (!CModel.FirstName.Equals(FirstName) || !CModel.LastName.Equals(LastName) || !CModel.Street.Equals(Street)
                    || !CModel.HouseNumber.Equals(Housenumber) || !CModel.City.Equals(SelectedCity) || !CModel.ZipCode.Equals(ZipCode) || !CModel.Birthday.Equals(Birthday)) 
                {
                    output = true;

                }
                if (string.IsNullOrWhiteSpace(CModel.Email) && !string.IsNullOrWhiteSpace(Email))
                {
                    if (Email.Contains("@") && Email.Length >= 6)
                    {
                        output = true;
                    }
                }
                if (Email.Contains("@") && Email.Length >= 6)
                {
                    if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(CModel.Email) && !Email.Equals(CModel.Email))
                    {
                        output = true;
                    }
                }
               
                if (string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(CModel.Email))
                {
                    output = true;
                }

                if (string.IsNullOrWhiteSpace(CModel.PhoneNumber) && !string.IsNullOrWhiteSpace(PhoneNumber))
                {
                    output = true;
                }
                if (!string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrWhiteSpace(CModel.PhoneNumber) && !PhoneNumber.Equals(CModel.PhoneNumber))
                {
                    output = true;
                }
                if (string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(CModel.PhoneNumber))
                {
                    output = true;
                }

                if (string.IsNullOrWhiteSpace(CModel.MobileNumber) && !string.IsNullOrWhiteSpace(MobileNumber))
                {
                    output = true;
                }

                if (!string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(CModel.MobileNumber) && !MobileNumber.Equals(CModel.MobileNumber))
                {
                    output = true;
                }
                if (string.IsNullOrEmpty(MobileNumber) && !string.IsNullOrEmpty(CModel.MobileNumber))
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanEditDog
        {
            get
            {
                bool output = true;
                if (SelectedDog == null && AvailableSelectedDog == null)
                {
                    output = false;
                }
                return output;
            }
        }
        
        public void EditDog(DogModel dog)
        {
            ActiveEditDogView = new DogLibrary.ViewModels.DogDetailsViewModel(_dogToEdit);
            Items.Add(ActiveEditDogView);
            EditDogIsVisible = true;
            AddDogIsVisible = false;
        }

        public void SaveCustomer()
        {
            IsDogToSave = false;
            IsDogRemoveToSave = false;
            CModel.FirstName = FirstName;
            CModel.LastName = LastName;
            CModel.Street = Street;
            CModel.HouseNumber = Housenumber;
            CModel.ZipCode = ZipCode;
            CModel.City = SelectedCity;
            CModel.PhoneNumber = PhoneNumber;
            CModel.MobileNumber = MobileNumber;
            CModel.Email = Email;
            CModel.Birthday = Birthday;
            if (NotActive)
            {
                CModel.Active = false;
            }
            else
            {
                CModel.Active = true;
            }

            GlobalConfig.Connection.UpdateCustomer(CModel);
            GlobalConfig.Connection.Get_Customer(CModel);
            SuccessMessages.ChangesSavedSuccess();
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(CModel);
            
        }

        /// <summary>
        /// Changes the bottom part of the view and loads the CreatenNwDogView 
        /// </summary>
        public void AddDog()
        {
            ActiveAddDogView = new DogLibrary.ViewModels.CreateNewDogViewModel();
            Items.Add(ActiveAddDogView);
            CustomerDetailsIsVisible = false;
            AddDogIsVisible = true;
        }

        public void Handle(DogModel dogModel)
        {
            if (dogModel.Id > 0)
            {
                GlobalConfig.Connection.UpdateDog(dogModel);
                _isDogToSave = true;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(dogModel.Name))
                {
                    if (dogModel.CustomerList == null)
                    {
                        dogModel.CustomerList = new List<CustomerModel>();
                    }
                    dogModel.CustomerList.Add(CModel);
                    GlobalConfig.Connection.AddDog(dogModel, CModel);
                    GlobalConfig.Connection.GetAllCustomerForDog(dogModel);
                    AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
                    OwnedDogs.Add(dogModel);
                    AvailableSelectedDog = null;
                    SelectedDog = null;
                    _isDogToSave = true;
                    NotifyOfPropertyChange(() => CanSaveCustomer);
                    NotifyOfPropertyChange(() => OwnedDogs);
                    NotifyOfPropertyChange(() => AvailableDogs);
                }
            }
            CustomerDetailsIsVisible = true;
            AddDogIsVisible = false;
            EditDogIsVisible = false;

        }

        /// <summary>
        /// Cancel the customer edit and returns to the ManageCustomerView
        /// </summary>
        public void Back()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new CustomerModel());
            this.TryClose();
        }

        #endregion
    }
}
