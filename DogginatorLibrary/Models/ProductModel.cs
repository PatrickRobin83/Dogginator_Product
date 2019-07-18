/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ProductModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class ProductModel
    {
        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Is Item Active
        /// </summary>
        public bool Active;
        /// <summary>
        /// Unique Id from the database and also the itemnumber of the product
        /// </summary>
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
