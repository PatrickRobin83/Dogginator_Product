/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   DateCalculator.cs
 *   Date			:   20.07.2019 00:42:24
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;

namespace de.rietrob.dogginator_product.AppointmentLibrary.Helper
{

    public static class DateCalculator
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor
        public DateCalculator()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// calculates the days between arriving date and leaving date
        /// </summary>
        /// <returns>the calculated days as an int</returns>
        public static int getDays(DateTime dateToSubstractFrom, DateTime dateSubstract)
        {
            return dateToSubstractFrom.Subtract(dateSubstract).Days + 1;
        }
        #endregion

    }
}
