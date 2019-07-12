using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.UserLibrary.ViewModels
{
    public class CreateUserViewModel : Screen
    {
        #region Fields

        private UserModel _user = new UserModel();
        private string _username = "";
        private string _userPassword = "";
        private string _userPasswordRepeat = "";
        private bool _isAdmin;

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
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                NotifyOfPropertyChange(() => UserPassword);
                NotifyOfPropertyChange(() => CanCreateUser);
            }
        }
        public string UserPasswordRepeat
        {
            get { return _userPasswordRepeat; }
            set
            {
                _userPasswordRepeat = value;
                NotifyOfPropertyChange(() => UserPasswordRepeat);
                NotifyOfPropertyChange(() => CanCreateUser);
            }
        }
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                NotifyOfPropertyChange(() => IsAdmin);
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
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(GlobalConfig.CANCEL);
            TryClose();
        }

        public bool CanCreateUser
        {
            get
            {
                bool output = false;

                if(UserName.Length > 3 &&  UserPassword.Length > 3 && UserPasswordRepeat.Length > 3 && UserPassword.Equals(UserPasswordRepeat))
                {
                    output = true;
                }

                return output;
            }

            
        }

        public void CreateUser()
        {
            User.Username = UserName.ToLower();

            User.Password = GlobalConfig.HashThePassword(UserPassword);

            if (IsAdmin)
            {
                User.IsAdmin = true;
            }
            else
            {
                User.IsAdmin = false;
            }

            GlobalConfig.Connection.InsertUserToDatabase(User);
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(GlobalConfig.USERCREATED);
        }
        


        #endregion
    }
}
