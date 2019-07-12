﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Messages
{
    public static class SuccessMessages
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Methods
        public static void ChangesSavedSuccess()
        {
            MessageBox.Show("Änderungen wurden gespeichert", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void UserCreatedSuccess()
        {
            MessageBox.Show("User wurde angelegt", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void AppointmentCreatedSuccess()
        {
            MessageBox.Show("Termin wurde angelegt", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
