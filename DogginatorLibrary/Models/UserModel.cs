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
