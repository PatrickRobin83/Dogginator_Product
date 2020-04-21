/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   UserModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class UserModel
    {
        #region Fields
        #endregion

        #region Properties

        /// <summary>
        /// Unique ID from Data store
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username as string
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// encrypted password string
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// true if admin status is granted
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// true if user is an active user
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date of creation
        /// </summary>
        public string create_date { get; set; }

        /// <summary>
        /// date of last editing
        /// </summary>
        public string edit_date { get; set; }

        #endregion

        #region Constructor
        #endregion

        #region Methods
        #endregion

    }
}
