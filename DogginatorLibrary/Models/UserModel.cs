using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary.Models
{
    public class UserModel
    {
        #region Fields
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string create_date { get; set; }
        public string edit_date { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Methods
        #endregion

    }
}
