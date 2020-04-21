/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ManageDogsViewModel.cs
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
        #endregion

        #region Properties

        /// <summary>
        /// All Available Dogs from the data store
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
        /// Selected Item in the DataGridView
        /// </summary>
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

        /// <summary>
        /// True if the ManageDogsView is visible
        /// </summary>
        public bool DogOverviewIsVisible
        {
            get { return _dogOverviewIsVisible; }
            set
            {
                _dogOverviewIsVisible = value;
                NotifyOfPropertyChange(() => DogOverviewIsVisible);
            }
        }

        /// <summary>
        /// True if the DogDetailsView is visible
        /// </summary>
        public bool DogDetailsIsVisible
        {
            get { return _dogDetailsIsVisible; }
            set
            {
                _dogDetailsIsVisible = value;
                NotifyOfPropertyChange(() => DogDetailsIsVisible);
            }
        }

        /// <summary>
        /// View of the DogDetailsView
        /// </summary>
        public Screen ActiveDogsDetailsView
        {
            get { return _activeDogsDetailsView; }
            set
            {
                _activeDogsDetailsView = value;
                NotifyOfPropertyChange(() => ActiveDogsDetailsView);
            }
        }

        /// <summary>
        /// Value of the SearchText TextBox
        /// </summary>
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

        /// <summary>
        /// Value of CheckBox Show also inactive
        /// </summary>
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

        #endregion

        #region Contstructor

        /// <summary>
        /// Default Construcotr setting up the ManageDogsOverView
        /// </summary>
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

        /// <summary>
        /// Converts the bool isActive into a string True = Aktiv -- False = Inaktiv
        /// </summary>
        /// <param name="dogList"></param>
        private void ActiveDog(BindableCollection<DogModel> dogList)
        {
            foreach (DogModel model in dogList)

            {
                if (model.Active == 1)
                {
                    model.DogActive = "Aktiv";
                }
                else if(model.Active == 0)
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
            SelectedDog.Active = 0;
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
        /// <param name="dogModel"></param>
        public void Handle(DogModel dogModel)
        {
            if(dogModel != null && dogModel.Id > 0)
            {
                GlobalConfig.Connection.UpdateDog(dogModel);  
            }

            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
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
