/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   NoteModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class NoteModel
    {
       
        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Unique Id from the database
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Note Content like "Customer does not pay on time"
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Contstructor

        #endregion

        #region Methods

        #endregion
    }
}
