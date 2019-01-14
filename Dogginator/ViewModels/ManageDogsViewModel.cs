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
    public class ManageDogsViewModel : Conductor<object>
    {
        #region Fields
        BindableCollection<DogModel> _availableDogs = new BindableCollection<DogModel>();
        private DogModel _selectedDog = new DogModel();

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
            }
        }
        #endregion

        #region Contstructor
        public ManageDogsViewModel()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
        }
        #endregion

        #region Methods

        #endregion
    }
}
