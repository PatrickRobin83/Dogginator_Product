﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class ProductModel
    {
        #region Fields

        #endregion

        #region Properties
        public bool Active;
        public int ItemNumber { get; set; }
        public string Shortdescription { get; set; }
        public string Longdescription { get; set; }
        public string Price { get; set; }
        public string Create_Date { get; set; }
        public string Edit_Date { get; set; }
        #endregion

        #region Contstructor

        #endregion

        #region Methods

        #endregion
    }

}
