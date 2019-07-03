using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary.Models
{
    public class ProductModel
    {
        #region Fields

        #endregion

        #region Properties
        public bool isActive;
        public int ItemNumber { get; set; }
        public string Shortdescription { get; set; }
        public string Longdescription { get; set; }
        public float Price { get; set; }
        public string Create_Date { get; set; }
        public string Edit_Date { get; set; }
        #endregion

        #region Contstructor

        #endregion

        #region Methods

        #endregion
    }

}
