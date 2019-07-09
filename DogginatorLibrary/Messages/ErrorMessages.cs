﻿using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DogginatorLibrary.Messages
{
    public static class ErrorMessages
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Methods
        public static void ShowUserPasswordError()
        {
            MessageBox.Show("Benutzername oder Passwort ist leer oder falsch", "Fehler: Benutzername / Passwort", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static void ShowUserNotActiveError()
        {
            MessageBox.Show("Der Benutzer ist nicht mehr aktiv", "Fehler: User ist Inaktiv", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static void DogToRemoveError(DogModel dogToRemove)
        {
            MessageBox.Show($"Der Hund {dogToRemove.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                                   $"{dogToRemove.CustomerList.First().FirstName} {dogToRemove.CustomerList.First().LastName} der einzige Besitzer ist");
        }

        public static void DogAlreadCustomerRelationError()
        {
            MessageBox.Show("Hund ist dem Kunden bereits zugeordnet!", "Hinweis", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
        }

        public static void DogCanNotRemovedFromCustomerError(DogModel selectedDog)
        {
            MessageBox.Show($"Der Hund {selectedDog.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                               $"{selectedDog.CustomerList.First().FirstName} {selectedDog.CustomerList.First().LastName} der einzige Besitzer ist");
        }

        #endregion
    }
}
