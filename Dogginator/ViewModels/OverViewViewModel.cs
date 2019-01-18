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
    public class OverViewViewModel : Conductor<object>.Collection.OneActive, IHandle<UserModel>
    {
        #region Fields
        private int _customerCount;
        private int _dogCount;
        private bool _manageUserIsVisible = false;
        private string _userSearchText = "";
        private bool _showAlsoInactive = false;
        private BindableCollection<UserModel> _availableUserList;
        private UserModel _selectedUser = new UserModel(); 
        private Screen _activeAddUser;
        private bool _addUserIsVisible;
        private Screen _activeEditUser;
        private bool _editUserIsVisible;
        

        #endregion

        #region Properties

        public int CustomerCount
        {
            get { return _customerCount; }
            set
            {
                _customerCount = value;
                NotifyOfPropertyChange(() => CustomerCount);
            }
        }
        public int DogCount
        {
            get { return _dogCount; }
            set
            {
                _dogCount = value;
                NotifyOfPropertyChange(() => DogCount);
            }
        }
        public bool ManageUserIsVisible
        {
            get { return _manageUserIsVisible; }
            set
            {
                _manageUserIsVisible = value;
                NotifyOfPropertyChange(() => _manageUserIsVisible);
            }
        }

        public string UserSearchText
        {
            get { return _userSearchText; }
            set
            {
                _userSearchText = value;
                NotifyOfPropertyChange(() => UserSearchText);
            }
        }
        public bool ShowAlsoInactive
        {
            get { return _showAlsoInactive; }
            set
            {
                _showAlsoInactive = value;
                NotifyOfPropertyChange(() => ShowAlsoInactive);
            }
        }

        public BindableCollection<UserModel> AvailableUserList
        {
            get { return _availableUserList; }
            set
            {
                _availableUserList = value;
                NotifyOfPropertyChange(() => ShowAlsoInactive);
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public Screen ActiveAddUser
        {
            get { return _activeAddUser; }
            set
            {
                _activeAddUser = value;
                NotifyOfPropertyChange(() => ActiveAddUser);
            }
        }
        public bool AddUserIsVisible
        {
            get { return _addUserIsVisible; }
            set
            {
                _addUserIsVisible = value;
                NotifyOfPropertyChange(() => AddUserIsVisible);
            }
        }

        public Screen ActiveEditUser
        {
            get { return _activeEditUser; }
            set
            {
                _activeEditUser = value;
                NotifyOfPropertyChange(() => ActiveEditUser);
            }
        }
        public bool EditUserIsVisible
        {
            get { return _editUserIsVisible;  }
            set
            {
                _editUserIsVisible = value;
                NotifyOfPropertyChange(() => EditUserIsVisible);
            }
        }
        #endregion

        #region Constructor
        public OverViewViewModel(bool isLoggedIn, bool isAdmin)
        {
            ManageUserIsVisible = isAdmin;
            CustomerCount = GlobalConfig.Connection.Get_CustomerInactiveAndActive().Count;
            DogCount = GlobalConfig.Connection.Get_DogsAll().Count;
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
        }

        #endregion

        #region Methods
        public void LoadCreateUser()
        {
            ActiveAddUser = new CreateUserViewModel();
            Items.Add(ActiveAddUser);
            EditUserIsVisible = false;
            AddUserIsVisible = true;
            ManageUserIsVisible = false;
            NotifyOfPropertyChange(() => ManageUserIsVisible);
        }
        public void LoadUserDetails()
        {

        }

        public void DeletUser()
        {

        }

        public void Handle(UserModel message)
        {
            if(message != null && message.Username != null && message.Username.Length > 0)
            {
                // TODO Do Something
            }
            else
            {
                EditUserIsVisible = false;
                AddUserIsVisible = false; ;
                ManageUserIsVisible = true;
                NotifyOfPropertyChange(() => ManageUserIsVisible);
                NotifyOfPropertyChange(() => AddUserIsVisible);
                NotifyOfPropertyChange(() => EditUserIsVisible);
            }
            
        }


        #endregion
    }
}
