/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   LoginViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;


namespace de.rietrob.dogginator_product.LoginLibrary.ViewModels
{
    public class LoginViewModel : Screen
    {
        #region Fields
        private string _username;
        private string _password;
        private bool _isUserValid;
        private UserModel _user = new UserModel();
        


        #endregion

        #region Properties

        /// <summary>
        /// Bool to determine is User valid
        /// </summary>
        public bool IsUserValid
        {
            get { return _isUserValid; }
            set
            {
                _isUserValid = value;
                NotifyOfPropertyChange(() => IsUserValid);
            }
        }
       
        /// <summary>
        /// string from passwordbox
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        /// <summary>
        /// string from Username TextBox
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
        /// UserModel
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

        #endregion

        #region Contstructor

        public LoginViewModel()
        {
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Method to validate a user password combination
        /// </summary>
        public void Login()
        {
                if (!string.IsNullOrWhiteSpace(UserName))
            {
                User.Username = UserName.ToLower();
                User = GlobalConfig.Connection.IsUserAndPasswordRight(User);

                 if (!User.IsActive)
                {
                    ErrorMessages.ShowUserNotActiveError();
                    return;

                }

                else if (User != null && !string.IsNullOrWhiteSpace(User.Password) && !string.IsNullOrWhiteSpace(Password) && User.Password.Equals(GlobalConfig.HashThePassword(Password)))
                {
                    EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(User);
                    TryClose();
                }
                
                else
                {
                    ErrorMessages.ShowUserPasswordError();
                    EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new UserModel());
                }
            }
            else
            {
                ErrorMessages.ShowUserPasswordError();
            }
        }
        
        #endregion
    }
}
