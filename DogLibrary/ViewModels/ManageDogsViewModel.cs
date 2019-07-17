using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;

namespace de.rietrob.dogginator_product.DogLibrary.ViewModels
{
    public class ManageDogsViewModel : Conductor<object>.Collection.OneActive, IHandle<DogModel>
    {
        #region Fields
        BindableCollection<DogModel> _availableDogs = new BindableCollection<DogModel>();
        private DogModel _selectedDog = null;
        private bool _dogOverviewIsVisible = true;
        private bool _dogDetailsIsVisible = false;
        private Screen _activeDogsDetailsView;

        private string _dogSearchText = "";
        private bool _showalsoInactive = false;
        private string _marker = "";

        #endregion

        #region Properties
        public BindableCollection<DogModel> AvailableDogs
        {
            get { return _availableDogs; }
            set
            {
                _availableDogs = value;
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }
        public DogModel SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                NotifyOfPropertyChange(() => SelectedDog);
                NotifyOfPropertyChange(() => CanDeleteDog);
                NotifyOfPropertyChange(() => CanEditDog);
            }
        }
        public bool DogOverviewIsVisible
        {
            get { return _dogOverviewIsVisible; }
            set
            {
                _dogOverviewIsVisible = value;
                NotifyOfPropertyChange(() => DogOverviewIsVisible);
            }
        }
        public bool DogDetailsIsVisible
        {
            get { return _dogDetailsIsVisible; }
            set
            {
                _dogDetailsIsVisible = value;
                NotifyOfPropertyChange(() => DogDetailsIsVisible);
            }
        }
        public Screen ActiveDogsDetailsView
        {
            get { return _activeDogsDetailsView; }
            set
            {
                _activeDogsDetailsView = value;
                NotifyOfPropertyChange(() => ActiveDogsDetailsView);
            }
        }

        public string DogSearchText
        {
            get { return _dogSearchText; }
            set
            {
                _dogSearchText = value;
                NotifyOfPropertyChange(() => DogSearchText);
                AvailableDogs = getDogs();
                ActiveDog(AvailableDogs);
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }
        public bool ShowalsoInactive
        {
            get { return _showalsoInactive; }
            set
            {
                _showalsoInactive = value;
                NotifyOfPropertyChange(() => _showalsoInactive);
                AvailableDogs = getDogs();
                ActiveDog(AvailableDogs);
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }

        public string Marker
        {
            get { return _marker; }
            set
            {
                _marker = value;
                NotifyOfPropertyChange(() => Marker);
            }
        }

        #endregion

        #region Contstructor
        public ManageDogsViewModel()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            ActiveDog(AvailableDogs);
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get all Available Dogs which contain the text from the searchbox
        /// </summary>
        /// <returns></returns>
        private BindableCollection<DogModel> getDogs()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.SearchResultDogs(DogSearchText, ShowalsoInactive));

            return AvailableDogs;
        }

        private void ActiveDog(BindableCollection<DogModel> dogList)
        {
            foreach (DogModel model in dogList)

            {
                if (model.Active)
                {
                    model.DogActive = "Aktiv";
                }
                else
                {
                    model.DogActive = "inaktiv";
                }

            }
        }

        /// <summary>
        /// Activates or deactivates the Delete Dog Button
        /// </summary>
        public bool CanDeleteDog
        {
            get
            {
                bool output = false; ;

                if (SelectedDog != null)
                {
                    output = true;
                }
                return output;
            }
        }

        /// <summary>
        /// Deletes the dog to customer relation and dog to disease relation and the dog to characteristic relation and finally sets the dog active
        /// </summary>
        public void DeleteDog()
        {
            SelectedDog.Active = false;
            GlobalConfig.Connection.DeleteDogDiseasesRelation(SelectedDog);
            GlobalConfig.Connection.DeleteDogToCharacteristicsRelation(SelectedDog);
            GlobalConfig.Connection.DeleteDogFromDatabase(SelectedDog);
            foreach (CustomerModel cModel in SelectedDog.CustomerList)
            {
                GlobalConfig.Connection.DeleteDogToCustomerRelation(cModel, SelectedDog);
            }
            
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            ActiveDog(AvailableDogs);
            NotifyOfPropertyChange(() => AvailableDogs);
            ShowalsoInactive = false;
            NotifyOfPropertyChange(() => ShowalsoInactive);
        }

        /// <summary>
        /// Activates or deactivate the EditDog Button
        /// </summary>
        public bool CanEditDog
        {
            get
            {
                bool output = false; ;

                if (SelectedDog != null)
                {
                    output = true;
                }
                return output;
            }
        }

        /// <summary>
        /// opens the DogsDetailsView
        /// </summary>
        public void EditDog()
        {
            ActiveDogsDetailsView = new DogDetailsViewModel(SelectedDog);
            Items.Add(ActiveDogsDetailsView);
            DogOverviewIsVisible = false;
            DogDetailsIsVisible = true;
        }

        /// <summary>
        /// Is triggered if a Dogmodel is given back to the UI-Thread
        /// </summary>
        /// <param name="message"></param>
        public void Handle(DogModel message)
        {
            if(message != null && message.Id > 0)
            {
                GlobalConfig.Connection.UpdateDog(message);
                AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());  
            }
            DogOverviewIsVisible = true;
            DogDetailsIsVisible = false;
            SelectedDog = null;
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            ActiveDog(AvailableDogs);
            NotifyOfPropertyChange(() => AvailableDogs);
            ShowalsoInactive = false;
            NotifyOfPropertyChange(() => ShowalsoInactive);
        }


        #endregion
    }
}
