using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Helper;
using DogginatorLibrary.Messages;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
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

        public bool IsUserValid
        {
            get { return _isUserValid; }
            set
            {
                _isUserValid = value;
                NotifyOfPropertyChange(() => IsUserValid);
            }
        }
       
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
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
