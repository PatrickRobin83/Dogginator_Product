using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ManageDogsViewModel : Conductor<object>.Collection.OneActive, IHandle<DogModel>
    {
        #region Fields
        BindableCollection<DogModel> _availableDogs = new BindableCollection<DogModel>();
        private DogModel _selectedDog = new DogModel();
        private bool _dogOverviewIsVisible = true;
        private bool _dogDetailsIsVisible = false;

        private Screen _activeDogsDetailsView;

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
        #endregion

        #region Contstructor
        public ManageDogsViewModel()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
        }
        #endregion

        #region Methods
        // TODO - Comment Code
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

        public void DeleteDog()
        {
            GlobalConfig.Connection.DeleteDogDiseasesRelation(SelectedDog);
            GlobalConfig.Connection.DeleteDogToCharacteristicsRelation(SelectedDog);
            GlobalConfig.Connection.DeleteDogFromDatabase(SelectedDog);
            foreach (CustomerModel cModel in SelectedDog.CustomerList)
            {
                GlobalConfig.Connection.DeleteDogToCustomerRelation(cModel, SelectedDog);
            }
            
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
        }

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

        public void EditDog()
        {
            ActiveDogsDetailsView = new DogDetailsViewModel(SelectedDog);
            Items.Add(ActiveDogsDetailsView);
            DogOverviewIsVisible = false;
            DogDetailsIsVisible = true;
        }

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
        }


        #endregion
    }
}
