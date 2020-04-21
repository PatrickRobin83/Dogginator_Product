/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   OverViewViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.OptionLibrary.ViewModels;
using de.rietrob.dogginator_product.UserLibrary.ViewModels;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System.Windows;

namespace de.rietrob.dogginator_product.OverviewLibrary.ViewModels
{
    public class OverViewViewModel : Conductor<object>.Collection.OneActive, IHandle<string>
    {
        #region Fields
        private int _customerCount;
        private int _dogCount;
        private bool _manageUserIsVisible = false;
        private string _userSearchText = "";
        private bool _showAlsoInactive = false;
        private BindableCollection<UserModel> _availableUserList;
        private UserModel _selectedUser = null; 
        private Screen _activeAddUser;
        private bool _addUserIsVisible;
        private Screen _activeEditUser;
        private bool _editUserIsVisible;
        private bool _optionIsVisible;
        private Screen _activeOption;
        protected Visibility _optionVisibility;

        #endregion

        #region Properties

        /// <summary>
        /// Label customercount show how many customers currently in data store
        /// </summary>
        public int CustomerCount
        {
            get { return _customerCount; }
            set
            {
                _customerCount = value;
                NotifyOfPropertyChange(() => CustomerCount);
            }
        }

        /// <summary>
        /// Label Dog Count show how many customers currently in data store
        /// </summary>
        public int DogCount
        {
            get { return _dogCount; }
            set
            {
                _dogCount = value;
                NotifyOfPropertyChange(() => DogCount);
            }
        }

        /// <summary>
        /// if true UsermanagementView is visible
        /// </summary>
        public bool ManageUserIsVisible
        {
            get { return _manageUserIsVisible; }
            set
            {
                _manageUserIsVisible = value;
                NotifyOfPropertyChange(() => _manageUserIsVisible);
            }
        }

        /// <summary>
        /// Text from TextBox Search Text
        /// </summary>
        public string UserSearchText
        {
            get { return _userSearchText; }
            set
            {
                _userSearchText = value;
                NotifyOfPropertyChange(() => UserSearchText);
                AvailableUserList = getUser();
                NotifyOfPropertyChange(() => AvailableUserList);
            }
        }

        /// <summary>
        /// Value of Checkbox Show also Incative
        /// </summary>
        public bool ShowAlsoInactive
        {
            get { return _showAlsoInactive; }
            set
            {
                _showAlsoInactive = value;
                NotifyOfPropertyChange(() => ShowAlsoInactive);
                AvailableUserList = getUser();
                NotifyOfPropertyChange(() => AvailableUserList);
              
            }
        }

        /// <summary>
        /// If True the OptionsButton is visible
        /// </summary>
        public Visibility OptionVisibility
        {
            get { return _optionVisibility; }
            set
            {
                _optionVisibility = value;
                NotifyOfPropertyChange(() => OptionVisibility);
            }
        }

        /// <summary>
        /// List of all Availlable User shown in DataGrid
        /// </summary>
        public BindableCollection<UserModel> AvailableUserList
        {
            get { return _availableUserList; }
            set
            {
                _availableUserList = value;
                NotifyOfPropertyChange(() => AvailableUserList);
            }
        }

        /// <summary>
        /// The selected User from the DataGrid
        /// </summary>
        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanLoadUserDetails);
                NotifyOfPropertyChange(() => CanDeleteUser);
            }
        }

        /// <summary>
        /// View of the Add User mask 
        /// </summary>
        public Screen ActiveAddUser
        {
            get { return _activeAddUser; }
            set
            {
                _activeAddUser = value;
                NotifyOfPropertyChange(() => ActiveAddUser);
            }
        }

        /// <summary>
        /// True if the Add User View is visible
        /// </summary>
        public bool AddUserIsVisible
        {
            get { return _addUserIsVisible; }
            set
            {
                _addUserIsVisible = value;
                NotifyOfPropertyChange(() => AddUserIsVisible);
            }
        }

        /// <summary>
        /// View if the Edit User mask
        /// </summary>
        public Screen ActiveEditUser
        {
            get { return _activeEditUser; }
            set
            {
                _activeEditUser = value;
                NotifyOfPropertyChange(() => ActiveEditUser);
            }
        }

        /// <summary>
        /// True if the Edit User View is Visible
        /// </summary>
        public bool EditUserIsVisible
        {
            get { return _editUserIsVisible;  }
            set
            {
                _editUserIsVisible = value;
                NotifyOfPropertyChange(() => EditUserIsVisible);
            }
        }

        /// <summary>
        /// True if the Options View is Visible
        /// </summary>
        public bool OptionIsVisible
        {
            get { return _optionIsVisible; }
            set
            {
                _optionIsVisible = value;
                NotifyOfPropertyChange(() => OptionIsVisible);
            }
        }

        /// <summary>
        /// View of the Options mask
        /// </summary>
        public Screen ActiveOption
        {
            get { return _activeOption; }
            set
            {
                _activeOption = value;
                NotifyOfPropertyChange(() => ActiveOption);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor intializes everything
        /// </summary>
        /// <param name="isLoggedIn"></param>
        /// <param name="isAdmin"></param>
        public OverViewViewModel(bool isLoggedIn, bool isAdmin)
        {
            OptionVisibility = Visibility.Hidden;
            ManageUserIsVisible = isAdmin;
            if (isAdmin)
            {
                OptionVisibility = Visibility.Visible;
            }
            CustomerCount = GlobalConfig.Connection.Get_CustomerInactiveAndActive().Count;
            DogCount = GlobalConfig.Connection.Get_DogsInactiveAndActive().Count;
            AvailableUserList = new BindableCollection<UserModel>(GlobalConfig.Connection.GetAllActiveUser());
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
        }

        #endregion

        #region Methods
        public void LoadCreateUser()
        {
            ActiveAddUser = new CreateUserViewModel();
            Items.Add(ActiveAddUser);
            EditUserIsVisible = false;
            OptionIsVisible = false;
            AddUserIsVisible = true;
            ManageUserIsVisible = false;
            NotifyOfPropertyChange(() => ManageUserIsVisible);
        }

        public bool CanLoadUserDetails
        {
            get
            {
                bool output = false; 

                if(SelectedUser != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void LoadUserDetails()
        {
            ActiveEditUser = new UserDetailsViewModel(SelectedUser);
            Items.Add(ActiveEditUser);
            EditUserIsVisible = true;
            AddUserIsVisible = false;
            ManageUserIsVisible = false;
            OptionIsVisible = false;
            NotifyOfPropertyChange(() => ManageUserIsVisible);
        }

        public bool CanDeleteUser
        {
            get
            {
                bool output = false;

                if (SelectedUser != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void DeleteUser()
        {
            GlobalConfig.Connection.DeleteUserFromDataBase(SelectedUser);
            AvailableUserList = new BindableCollection<UserModel>(GlobalConfig.Connection.GetAllActiveUser());
        }

        public void LoadOption()
        {
            ActiveOption = new OptionViewModel();
            Items.Add(ActiveOption);
            OptionIsVisible = true;
            EditUserIsVisible = false;
            AddUserIsVisible = false;
            ManageUserIsVisible = false;
            NotifyOfPropertyChange(() => ManageUserIsVisible);
        }


        public void Handle(string message)
        {
            if (message.Equals(GlobalConfig.CANCEL))
            {
                EditUserIsVisible = false;
                AddUserIsVisible = false; ;
                ManageUserIsVisible = true;
                NotifyOfPropertyChange(() => ManageUserIsVisible);
                NotifyOfPropertyChange(() => AddUserIsVisible);
                NotifyOfPropertyChange(() => EditUserIsVisible);
            }
            if (message.Equals(GlobalConfig.USERCREATED))
            {
                EditUserIsVisible = false;
                AddUserIsVisible = false;
                ManageUserIsVisible = true;
                AvailableUserList = new BindableCollection<UserModel>(GlobalConfig.Connection.GetAllActiveUser());
                NotifyOfPropertyChange(() => AvailableUserList);
                NotifyOfPropertyChange(() => ManageUserIsVisible);
                NotifyOfPropertyChange(() => AddUserIsVisible);
                NotifyOfPropertyChange(() => EditUserIsVisible);
            }

            if (message.Equals(GlobalConfig.USEREDIT))
            {
                EditUserIsVisible = false;
                AddUserIsVisible = false;
                ManageUserIsVisible = true;
                AvailableUserList = new BindableCollection<UserModel>(GlobalConfig.Connection.GetAllActiveUser());
                NotifyOfPropertyChange(() => AvailableUserList);
                NotifyOfPropertyChange(() => ManageUserIsVisible);
                NotifyOfPropertyChange(() => AddUserIsVisible);
                NotifyOfPropertyChange(() => EditUserIsVisible);
            }

            if (message.Equals(GlobalConfig.USERDELETED))
            {
                AvailableUserList = new BindableCollection<UserModel>(GlobalConfig.Connection.GetAllActiveUser());
            }
            SelectedUser = null;
            ShowAlsoInactive = false;
        }

        private BindableCollection<UserModel> getUser()
        {
            AvailableUserList = new BindableCollection<UserModel>(GlobalConfig.Connection.SearchResultUser(UserSearchText, ShowAlsoInactive));

            return AvailableUserList;
        }


        #endregion
    }
}
