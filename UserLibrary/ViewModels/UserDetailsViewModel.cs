using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;

namespace de.rietrob.dogginator_product.UserLibrary.ViewModels
{
    public class UserDetailsViewModel : Screen
    {
        #region Fields
        private UserModel _user = new UserModel();
        private string _username = "";
        private string _userPassword = "";
        private string _userPasswordRepeat = "";
        private bool _isAdmin;
        private bool _isUserActive;

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
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                NotifyOfPropertyChange(() => UserPassword);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }
        public string UserPasswordRepeat
        {
            get { return _userPasswordRepeat; }
            set
            {
                _userPasswordRepeat = value;
                NotifyOfPropertyChange(() => UserPasswordRepeat);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                NotifyOfPropertyChange(() => IsAdmin);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }
        public bool IsUserActive
        {
            get { return _isUserActive; }
            set
            {
                _isUserActive = value;
                NotifyOfPropertyChange(() => IsUserActive);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }
        #endregion

        #region Constructor
        public UserDetailsViewModel(UserModel userToEdit)
        {
            User = userToEdit;
            UserName = User.Username;
            IsAdmin = User.IsAdmin;
            IsUserActive = User.IsActive;
        }
        #endregion

        #region Method

        public bool CanEditUser
        {
            get
            {
                bool output = false;

                if (!UserName.Equals(User.Username) && UserName.Length > 3)
                {
                    output = true;
                }

                if(!string.IsNullOrWhiteSpace(UserPassword) && !string.IsNullOrWhiteSpace(UserPasswordRepeat) && UserPassword.Equals(UserPasswordRepeat))
                {
                    output = true;
                }
                if(IsAdmin == false && User.IsAdmin == true)
                {
                    output = true;
                }
                if (IsAdmin == true && User.IsAdmin == false)
                {
                    output = true;
                }
                if (IsUserActive == true && User.IsActive == false)
                {
                    output = true;
                }
                if(IsUserActive == false && User.IsActive == true)
                {
                    output = true;
                }

                return output;
            }
        }

        public void EditUser()
        {
            User.Username = UserName;
            if (!string.IsNullOrWhiteSpace(UserPassword))
            {
                User.Password = GlobalConfig.HashThePassword(UserPassword);
            }
            User.IsActive = IsUserActive;
            User.IsAdmin = IsAdmin;
            GlobalConfig.Connection.UpdateUser(User);
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(GlobalConfig.USEREDIT);

        }

        public void CancelEdit()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(GlobalConfig.CANCEL);
            TryClose();
        }

        #endregion
    }
}
