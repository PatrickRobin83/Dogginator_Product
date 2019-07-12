﻿using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogLibrary.ViewModels
{
    public class DogDetailsViewModel : Screen
    {
        #region Fields
        private DogModel _dogToEdit;
        private bool _whenIsVisible = false;
        private bool _effectiveUntilIsVisible;
        private BindableCollection<string> _gender = new BindableCollection<string>();
        private string _name = "";
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

        public DogModel DogToEdit
        {
            get { return _dogToEdit; }
            set
            {
                _dogToEdit = value;
                NotifyOfPropertyChange(() => DogToEdit);
            }
        }
        public BindableCollection<string> Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                NotifyOfPropertyChange(() => Gender);
            }

        }

        public bool WhenIsVisible
        {
            get { return _whenIsVisible; }
            set
            {
                _whenIsVisible = value;
                NotifyOfPropertyChange(() => WhenIsVisible);
            }
        }

        public bool EffectiveUntilIsVisible
        {
            get { return _effectiveUntilIsVisible; }
            set
            {
                _effectiveUntilIsVisible = value;
                NotifyOfPropertyChange(() => EffectiveUntilIsVisible);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }
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
        public BindableCollection<CustomerModel> Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                NotifyOfPropertyChange(() => Owner);
            }
        }
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
        public DogDetailsViewModel(DogModel dog)
        {
            DogToEdit = dog;
            Name = DogToEdit.Name;
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
            if (DogToEdit.Active)
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
                    if (DogToEdit.Active == true)
                    {
                        return true;
                    }
                }
                else
                {
                    if (DogToEdit.Active == false)
                    {
                        return true;
                    }
                }
                if (!DogToEdit.Name.Equals(Name) && Name.Length > 3)
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
            DogToEdit.Name = Name;
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
                DogToEdit.Active = false;
            }
            else
            {
                DogToEdit.Active = true;
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
