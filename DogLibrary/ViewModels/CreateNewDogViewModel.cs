/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CreateNewDogViewModel.cs
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
    public class CreateNewDogViewModel : Screen
    {
        #region Fields
        private bool _whenIsVisible = false;
        private bool _effectiveUntilIsVisible = false;
        private string  _name = "";
        private string _breed = "";
        private string _color = "";
        private BindableCollection<string> _gender = new BindableCollection<string>();
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
        #endregion

        #region Properties
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
                NotifyOfPropertyChange(() => CanSaveDog);
            }
        }
        public string Breed
        {
            get { return _breed; }
            set
            {
                _breed = value;
                NotifyOfPropertyChange(() => Breed);
                NotifyOfPropertyChange(() => CanSaveDog);
            }
        }
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyOfPropertyChange(() => Color);
                NotifyOfPropertyChange(() => CanSaveDog);
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
        public string SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                NotifyOfPropertyChange(() => Gender);
                NotifyOfPropertyChange(() => CanSaveDog);
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
        public bool CastrationIsDurable
        {
            get { return _castrationIsDurable; }
            set
            {
                _castrationIsDurable = value;
                NotifyOfPropertyChange(() => CastrationIsDurable);
                NotifyOfPropertyChange(() => CanSaveDog);
                if (CastrationIsDurable)
                {
                    WhenIsVisible = true;
                    EffectiveUntilIsVisible = false;
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
                NotifyOfPropertyChange(() => CanSaveDog);

            }
        }
        public DateTime CastratedSince
        {
            get { return _castratedSince; }
            set
            {
                _castratedSince = value;
                NotifyOfPropertyChange(() => CastratedSince);
            }
        }
        public DateTime EffectiveUntil
        {
            get { return _effectiveUntil; }
            set
            {
                _effectiveUntil = value;
                NotifyOfPropertyChange(() => EffectiveUntil);
                NotifyOfPropertyChange(() => CanSaveDog);
            }
        }
        public bool PermanentCastrated
        {
            get { return _permanentCastrated; }
            set
            {
                _permanentCastrated = value;
                NotifyOfPropertyChange(() => PermanentCastrated);
                NotifyOfPropertyChange(() => CanSaveDog);
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
            }
        }
        #endregion

        #region Contstructor
        public CreateNewDogViewModel()
        {
            Gender.Add("Weibchen");
            Gender.Add("Rüde");
            Birthday = DateTime.Now;
            CastratedSince = DateTime.Now;
            EffectiveUntil = DateTime.Now;
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
        }
        /// <summary>
        /// This checks if it is possible to remove a disease from the List
        /// </summary>
        public bool CanRemoveDisease
        {
            get
            {
                if(SelectedDisease != null)
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
        }
        /// <summary>
        /// This checks if it is possible to remove a characteristic from the List
        /// </summary>
        public bool CanRemoveCharacteristics
        {
            get
            {
                if(SelectedCharacteristics != null)
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
        /// This removes the selected entry from the list
        /// </summary>
        public void RemoveCharacteristics()
        {
            CharacteristicsList.Remove(SelectedCharacteristics);
        }
        /// <summary>
        /// this checks if all required fields are set
        /// </summary>
        public bool CanSaveDog
        {
            get
            {
                if (Name.Length > 0 && Breed.Length > 0 && Color.Length > 0 && SelectedGender.Length > 0)
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
        /// this saves the dog to the list
        /// </summary>
        public void SaveDog()
        {
            DogModel dogModel = new DogModel();

            dogModel.Name = Name;
            dogModel.Breed = Breed;
            dogModel.Color = Color;
            dogModel.Gender = SelectedGender;
            if (Birthday != null)
            {
                dogModel.Birthday = Birthday.ToString("dd.MM.yyyy");
            }
           
            if (PermanentCastrated)
            {
                dogModel.CastratedSince = CastratedSince.ToString("dd.MM.yyyy");
                dogModel.PermanentCastrated = true;
            }
            else
            {
                dogModel.EffectiveUntil = EffectiveUntil.ToString("dd.MM.yyyy");
                dogModel.PermanentCastrated = false;
            }


            if(DiseasesList.Count > 0)
            {
                dogModel.Diseases = new List<DiseasesModel>(DiseasesList);
            }
            if(CharacteristicsList.Count > 0)
            {
                dogModel.Characteristics = new List<CharacteristicsModel>(CharacteristicsList);
            }
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(dogModel);
        }
        /// <summary>
        /// This abort the Dog creation and return to the CreateNewCustomer View
        /// </summary>
        public void CancelCreation()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new DogModel());
            this.TryClose();
        }
        #endregion
    }
}
