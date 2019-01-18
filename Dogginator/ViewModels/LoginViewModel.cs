﻿using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Helper;
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
        private string _passfromDatabase;


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
       
        #endregion

        #region Contstructor
        public LoginViewModel()
        {
            
        }
        #endregion

        #region Methods


        public void Login()
        {
            _passfromDatabase = GlobalConfig.Connection.IsUserAndPasswordRight(UserName);

            if (!string.IsNullOrEmpty(_passfromDatabase) && _passfromDatabase.Equals(HashThePassword(Password)))
            {
                EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(true);
                TryClose();
            }
            else
            {
                EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(false);
            }
        }

        private string HashThePassword(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string passHash = GetMd5Hash(md5Hash, password);
                

                return passHash;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        #endregion
    }
}
