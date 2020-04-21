/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   DogDetailsViewModel.cs
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
using System;
using System.Collections.Generic;

namespace de.rietrob.dogginator_product.DogLibrary.ViewModels
{
    public class DogDetailsViewModel : Screen
    {
        #region Fields
        private DogModel _dogToEdit;
        private bool _whenIsVisible = false;
        private bool _effectiveUntilIsVisible;
        private BindableCollection<string> _gender = new BindableCollection<string>();
        private string _dogName = "";
        private string _breed = "";
        private string _color = "";
        private string _selectedGender = "";
        private DateTime _birthday;
        private bool _castrationIsDurable = false;
        private bool _castrationIsNotDurable = false;
        private DateTime _castratedSince;
        private DateTime _effectiveUntil;
        private bool _permanentCastrated;
        private string _addDiseaseText = "";
        private BindableCollection<DiseasesModel> _diseasesList = new BindableCollection<DiseasesModel>();
        private DiseasesModel _selectedDisease = new DiseasesModel();
        private string _addCharacteristicsText = "";
        private BindableCollection<CharacteristicsModel> _characteristicsList = new BindableCollection<CharacteristicsModel>();
        private CharacteristicsModel _selectedCharacteristics = new CharacteristicsModel();
        private bool _notActive;
        private BindableCollection<CustomerModel> _owner = new BindableCollection<CustomerModel>();
        private bool _isDiseaseToSave = false;
        private bool _isCharacteristicToSave = false;
        #endregion

        #region Properties

        /// <summary>
        /// The given Object<DogModel> 
        /// </summary>
        public DogModel DogToEdit
        {
            get { return _dogToEdit; }
            set
            {
                _dogToEdit = value;
                NotifyOfPropertyChange(() => DogToEdit);
            }
        }

        /// <summary>
        /// ComboBox with a List of strings for the Gender
        /// </summary>
        public BindableCollection<string> Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                NotifyOfPropertyChange(() => Gender);
            }

        }

        /// <summary>
        /// Bool for WhenCastrated Area is visible
        /// </summary>
        public bool WhenIsVisible
        {
            get { return _whenIsVisible; }
            set
            {
                _whenIsVisible = value;
                NotifyOfPropertyChange(() => WhenIsVisible);
            }
        }

        /// <summary>
        /// Bool for EffectiveUntil Area is visible
        /// </summary>
        public bool EffectiveUntilIsVisible
        {
            get { return _effectiveUntilIsVisible; }
            set
            {
                _effectiveUntilIsVisible = value;
                NotifyOfPropertyChange(() => EffectiveUntilIsVisible);
            }
        }

        /// <summary>
        /// Value of the TextBox DogName
        /// </summary>
        public string DogName
        {
            get { return _dogName; }
            set
            {
                _dogName = value;
                NotifyOfPropertyChange(() => DogName);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Text of TextBox Breed
        /// </summary>
        public string Breed
        {
            get { return _breed; }
            set
            {
                _breed = value;
                NotifyOfPropertyChange(() => Breed);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Text of TextBox Color
        /// </summary>
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyOfPropertyChange(() => Color);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// The selected Item from the Gender ComboBox
        /// </summary>
        public string SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                NotifyOfPropertyChange(() => Gender);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Text from the DatePicker Birthday
        /// </summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                NotifyOfPropertyChange(() => Birthday);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Value of RadioButton CastrationIsDurable
        /// </summary>
        public bool CastrationIsDurable
        {
            get { return _castrationIsDurable; }
            set
            {
                _castrationIsDurable = value;
                NotifyOfPropertyChange(() => CastrationIsDurable);
                NotifyOfPropertyChange(() => CanEditDog);
                if (CastrationIsDurable)
                {
                    WhenIsVisible = true;
                    EffectiveUntilIsVisible = false;
                    PermanentCastrated = true;
                }
                else
                {
                    WhenIsVisible = false;
                    EffectiveUntilIsVisible = true;
                }
            }
        }

        /// <summary>
        /// Value of RadioButton CastrationIsNotDurable
        /// </summary>
        public bool CastrationIsNotDurable
        {
            get { return _castrationIsNotDurable; }
            set
            {
                _castrationIsNotDurable = value;
                NotifyOfPropertyChange(() => CastrationIsNotDurable);
                NotifyOfPropertyChange(() => CanEditDog);

                if (CastrationIsNotDurable)
                {
                    PermanentCastrated = false;
                }
                
            }
        }

        /// <summary>
        /// Value of Datepicker CastratedSince
        /// </summary>
        public DateTime CastratedSince
        {
            get { return _castratedSince; }
            set
            {
                _castratedSince = value;
                NotifyOfPropertyChange(() => CastratedSince);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Value of DatePicker EffectiveUntil
        /// </summary>
        public DateTime EffectiveUntil
        {
            get { return _effectiveUntil; }
            set
            {
                _effectiveUntil = value;
                NotifyOfPropertyChange(() => EffectiveUntil);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// If Castration Durable = true --> Permament Castrated also true
        /// </summary>
        public bool PermanentCastrated
        {
            get { return _permanentCastrated; }
            set
            {
                _permanentCastrated = value;
                NotifyOfPropertyChange(() => PermanentCastrated);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Value from TextBox Disease
        /// </summary>
        public string AddDiseaseText
        {
            get { return _addDiseaseText; }
            set
            {
                _addDiseaseText = value;
                NotifyOfPropertyChange(() => AddDiseaseText);
                NotifyOfPropertyChange(() => CanAddDisease);
            }
        }

        /// <summary>
        /// List of values for the DiseasesListView
        /// </summary>
        public BindableCollection<DiseasesModel> DiseasesList
        {
            get { return _diseasesList; }
            set
            {
                _diseasesList = value;
                NotifyOfPropertyChange(() => DiseasesList);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Disease selected on the Diseases ListView
        /// </summary>
        public DiseasesModel SelectedDisease
        {
            get { return _selectedDisease; }
            set
            {
                _selectedDisease = value;
                NotifyOfPropertyChange(() => SelectedDisease);
                NotifyOfPropertyChange(() => CanRemoveDisease);
            }
        }

        /// <summary>
        /// Value from TextBox Characteristics
        /// </summary>
        public string AddCharacteristicsText
        {
            get { return _addCharacteristicsText; }
            set
            {
                _addCharacteristicsText = value;
                NotifyOfPropertyChange(() => AddCharacteristicsText);
                NotifyOfPropertyChange(() => CanAddCharacteristics);
            }
        }

        /// <summary>
        /// List of values for the Characterristics ListView
        /// </summary>
        public BindableCollection<CharacteristicsModel> CharacteristicsList
        {
            get { return _characteristicsList; }
            set
            {
                _characteristicsList = value;
                NotifyOfPropertyChange(() => CharacteristicsList);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Characteristics selected in the characteristics ListView
        /// </summary>
        public CharacteristicsModel SelectedCharacteristics
        {
            get { return _selectedCharacteristics; }
            set
            {
                _selectedCharacteristics = value;
                NotifyOfPropertyChange(() => SelectedCharacteristics);
                NotifyOfPropertyChange(() => CanRemoveCharacteristics);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// List of Owner for ListView Owner
        /// </summary>
        public BindableCollection<CustomerModel> Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                NotifyOfPropertyChange(() => Owner);
            }
        }

        /// <summary>
        /// True if a disease is to Save
        /// </summary>
        public bool IsDiseaseToSave
        {
            get { return _isDiseaseToSave; }
            set
            {
                _isDiseaseToSave = value;
                NotifyOfPropertyChange(() => IsDiseaseToSave);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// True if a Characteristic is to save
        /// </summary>
        public bool IsCharacteristicToSave
        {
            get { return _isCharacteristicToSave; }
            set
            {
                _isCharacteristicToSave = value;
                NotifyOfPropertyChange(() => IsCharacteristicToSave);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        /// <summary>
        /// Indicates whether the Dog is Active or not. True --> is inactive False = Active
        /// </summary>
        public bool NotActive
        {
            get { return _notActive; }
            set
            {
                _notActive = value;
                NotifyOfPropertyChange(() => NotActive);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor initializes everything
        /// </summary>
        /// <param name="dog"></param>
        public DogDetailsViewModel(DogModel dog)
        {
            DogToEdit = dog;
            DogName = DogToEdit.Name;
            Breed = DogToEdit.Breed;
            Color = DogToEdit.Color;
            Gender.Add("Weibchen");
            Gender.Add("Rüde");
            SelectedGender = DogToEdit.Gender;
            Birthday = Convert.ToDateTime(DogToEdit.Birthday);

            if (DogToEdit.PermanentCastrated)
            {
                CastratedSince = Convert.ToDateTime(DogToEdit.CastratedSince);
                EffectiveUntil = DateTime.Now;
                CastrationIsDurable = true;
                PermanentCastrated = true;

            }
            else
            {
                if (DogToEdit.EffectiveUntil != null)
                {
                    EffectiveUntil = Convert.ToDateTime(DogToEdit.EffectiveUntil);
                    CastratedSince = DateTime.Now;
                }
                else
                {
                    EffectiveUntil = DateTime.Now;
                }
                CastrationIsDurable = false;
                CastrationIsNotDurable = true;
                PermanentCastrated = false;
            }

            if (DogToEdit.Diseases != null && DogToEdit.Diseases.Count > 0)
            {
                DiseasesList = new BindableCollection<DiseasesModel>(DogToEdit.Diseases);
            }
            if (DogToEdit.Characteristics != null && DogToEdit.Characteristics.Count > 0)
            {
                CharacteristicsList = new BindableCollection<CharacteristicsModel>(DogToEdit.Characteristics);
            }
            if (DogToEdit.Active == 1)
            {
                NotActive = false;
            }
            else
            {
                NotActive = true;
            }
            DogToEdit.CustomerList = GlobalConfig.Connection.GetAllCustomerForDog(DogToEdit);
            if (DogToEdit.CustomerList != null && DogToEdit.CustomerList.Count > 0)
            {
                Owner = new BindableCollection<CustomerModel>(DogToEdit.CustomerList);

            }
            NotifyOfPropertyChange(() => Owner);
        }
        #endregion

        #region Methods

        /// <summary>
        /// This checks if it is possible to add a disease to the diseases List. 
        /// </summary>
        public bool CanAddDisease
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(AddDiseaseText))
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
        /// Add a disease to the List
        /// </summary>
        public void AddDisease()
        {
            DiseasesModel diseases = new DiseasesModel();
            diseases.Name = AddDiseaseText;
            DiseasesList.Add(diseases);
            AddDiseaseText = "";
            IsDiseaseToSave = true;

        }

        /// <summary>
        /// This checks if it is possible to add a characteristic to the characteristics List
        /// </summary>
        public bool CanAddCharacteristics
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(AddCharacteristicsText))
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
        /// Add a characteristic to the List
        /// </summary>
        public void AddCharacteristics()
        {
            CharacteristicsModel model = new CharacteristicsModel();
            model.Description = AddCharacteristicsText;
            CharacteristicsList.Add(model);
            AddCharacteristicsText = "";
            IsCharacteristicToSave = true;
        }

        /// <summary>
        /// This checks if it is possible to remove a disease from the List
        /// </summary>
        public bool CanRemoveDisease
        {
            get
            {
                if (SelectedDisease != null)
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
        /// This removes the selected entry from the List
        /// </summary>
        public void RemoveDisease()
        {
            DiseasesList.Remove(SelectedDisease);
            IsDiseaseToSave = true;
        }

        /// <summary>
        /// This checks if it is possible to remove a characteristic from the List
        /// </summary>
        public bool CanRemoveCharacteristics
        {
            get
            {
                if (SelectedCharacteristics != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void RemoveCharacteristics()
        {
            CharacteristicsList.Remove(SelectedCharacteristics);
            IsCharacteristicToSave = true;
        }

        /// <summary>
        /// this checks if all required fields are set
        /// </summary>
        public bool CanEditDog
        {
            get
            {
                if (NotActive)
                {
                    if (DogToEdit.Active == 1)
                    {
                        return true;
                    }
                }
                else
                {
                    if (DogToEdit.Active == 0)
                    {
                        return true;
                    }
                }
                if (!DogToEdit.Name.Equals(DogName) && DogName.Length > 3)
                {
                    return true;
                }
                if (!DogToEdit.Breed.Equals(Breed) && Breed.Length > 3)
                {
                    return true;
                }
                if (!DogToEdit.Color.Equals(Color) && Color.Length > 3)
                {
                    return true;
                }

                if (!DogToEdit.Gender.Equals(SelectedGender))
                {
                    return true;
                }

                if (!DogToEdit.Birthday.Equals(Birthday.ToShortDateString()))
                {
                    return true;
                }

                if (DogToEdit.PermanentCastrated == false && CastrationIsDurable == true)
                {
                    return true;
                }
                if (DogToEdit.PermanentCastrated == true && CastrationIsDurable == false)
                {
                    return true;
                }

                if (IsCharacteristicToSave || IsDiseaseToSave)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// this saves the dog to the list
        /// </summary>
        public void EditDog()
        {
            DogToEdit.Name = DogName;
            DogToEdit.Breed = Breed;
            DogToEdit.Color = Color;
            DogToEdit.Gender = SelectedGender;
            if (Birthday != null)
            {
                DogToEdit.Birthday = Birthday.ToString("dd.MM.yyyy");
            }

            if (PermanentCastrated)
            {
                DogToEdit.CastratedSince = CastratedSince.ToString("dd.MM.yyyy");
                DogToEdit.PermanentCastrated = true;
            }
            else
            {
                DogToEdit.EffectiveUntil = EffectiveUntil.ToString("dd.MM.yyyy");
                DogToEdit.PermanentCastrated = false;
            }

            if (DiseasesList.Count > 0)
            {
                DogToEdit.Diseases = new List<DiseasesModel>(DiseasesList);
            }
            if (CharacteristicsList.Count > 0)
            {
                DogToEdit.Characteristics = new List<CharacteristicsModel>(CharacteristicsList);
            }
            if (NotActive)
            {
                DogToEdit.Active = 0;
            }
            else
            {
                DogToEdit.Active = 1;
            }
            IsDiseaseToSave = false;
            IsCharacteristicToSave = false;
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(DogToEdit);
            TryClose();
        }

        /// <summary>
        /// This abort the Dog creation and return to the CreateNewCustomer View
        /// </summary>
        public void CancelCreation()
        {
            IsDiseaseToSave = false;
            IsCharacteristicToSave = false;
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new DogModel());
            this.TryClose();
        }

        #endregion
    }
}
