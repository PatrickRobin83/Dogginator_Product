/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CreateUserViewModel.cs
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

        /// <summary>
        /// The complete UserModel with all Properties filled
        /// </summary>
        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        /// <summary>
        /// The value from the UserName TextBox
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        /// <summary>
        /// The value from the UserPassword PasswordBox
        /// </summary>
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

        /// <summary>
        /// The Value from the UserPasswordRepeat PasswordBox
        /// </summary>
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

        /// <summary>
        /// The Value of the Is Admin CheckBox
        /// </summary>
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

        /// <summary>
        /// Cancels the User creation and sends a Message to the OverViewViewModel and closes the CreateUserView
        /// </summary>
        public void CancelCreation()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(GlobalConfig.CANCEL);
            TryClose();
        }

        /// <summary>
        /// If true the Create User Button is activated
        /// </summary>
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

        /// <summary>
        /// Creates a UserModel and send it to the data store and to the OverviewViewModel and closes the CreateUserView
        /// </summary>
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
