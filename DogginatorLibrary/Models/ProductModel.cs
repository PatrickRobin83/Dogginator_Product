/*
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
        /// Unique Id from the data store and also the itemnumber of the product
        /// </summary>
        public int ItemNumber { get; set; }

        /// <summary>
        /// Shortdescription Text for the Product
        /// </summary>
        public string Shortdescription { get; set; }

        /// <summary>
        /// Long Description with more details 
        /// </summary>
        public string Longdescription { get; set; }

        /// <summary>
        /// The Price for the Product
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Date of creation
        /// </summary>
        public string Create_Date { get; set; }

        /// <summary>
        /// Date of last edit
        /// </summary>
        public string Edit_Date { get; set; }

        #endregion

        #region Contstructor

        #endregion

        #region Methods

        #endregion
    }

}
