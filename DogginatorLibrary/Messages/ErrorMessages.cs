using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Messages
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
            MessageBox.Show("Benutzername oder Passwort ist leer oder falsch", "Fehler: Benutzername / Passwort", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowUserNotActiveError()
        {
            MessageBox.Show("Der Benutzer ist nicht mehr aktiv", "Fehler: User ist Inaktiv", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void DogToRemoveError(DogModel dogToRemove)
        {
            MessageBox.Show($"Der Hund {dogToRemove.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                                   $"{dogToRemove.CustomerList.First().FirstName} {dogToRemove.CustomerList.First().LastName} der einzige Besitzer ist"
                                                   , "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void DogAlreadCustomerRelationError()
        {
            MessageBox.Show("Hund ist dem Kunden bereits zugeordnet!", "Hinweis" , MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void DogCanNotRemovedFromCustomerError(DogModel selectedDog)
        {
            MessageBox.Show($"Der Hund {selectedDog.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                               $"{selectedDog.CustomerList.First().FirstName} {selectedDog.CustomerList.First().LastName} der einzige Besitzer ist", "Fehler"
                                               , MessageBoxButton.OK, MessageBoxImage.Error); ;
        }

        public static void AppointmentIsAlreadyInDatabaseError(AppointmentModel appointmentModel)
        {
            MessageBox.Show($"Der Eintrag für {appointmentModel.dogFromCustomer.Name} wurde mit diesen Details schon eingetragen.\r\nBitte verwenden Sie die Funktion: Termin bearbeiten" +
                $"          \r\noder legen Sie einen neuen Termin mit anderen Details an.","Fehler - Termin Duplikat",MessageBoxButton.OK,MessageBoxImage.Error);
        }

        public static void DogIsInThisTimespanAlreadyInDatabaseError(AppointmentModel appointmentModel)
        {
            MessageBox.Show($"Der Eintrag für {appointmentModel.dogFromCustomer.Name} ist in diesem Zeitraum schon gebucht.\r\nBitte verwenden Sie die Funktion: Termin bearbeiten" +
                $"          \r\noder legen Sie einen neuen Termin mit anderen Daten an.", "Fehler - Hund wurde schon gebucht", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}
