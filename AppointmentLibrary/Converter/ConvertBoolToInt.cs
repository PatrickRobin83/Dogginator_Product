/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ConvertBoolToInt.cs
 *   Date			:   31.07.2019 11:48:50
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;

namespace de.rietrob.dogginator_product.AppointmentLibrary.Converter
{

    public static class ConvertBoolToInt
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor
        
        #endregion

        #region Methods
        public static int GetBoolToInt(bool isDailyGuest)
        {
            int result = 0;

            if (isDailyGuest)
            {
                result = 1;
            }
            
            return result;

        }

        public static bool GetIntToBool(int isDailyGuest)
        {
            bool result = false;

            if (isDailyGuest == 1)
            {
                result = true;
            }
            
            return result;

        }
        #endregion

    }
}
