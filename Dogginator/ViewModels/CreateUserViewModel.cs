using Caliburn.Micro;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class CreateUserViewModel : Screen
    {
        #region Fields

        private UserModel _user = new UserModel();

        #endregion

        #region Properties

        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        #endregion

        #region Constructor
        public CreateUserViewModel()
        {
            
        }
        #endregion
        #region Methods

        public void CancelCreation()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(User);
            TryClose();
        }

        #endregion
    }
}
