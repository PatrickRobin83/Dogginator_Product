/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ErrorMessages.cs
 *   Date			:   2019-07-18 08:18
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System.Linq;
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
        /// <summary>
        /// Shows a Messagebox if the given user or password is wrong
        /// </summary>
        public static void ShowUserPasswordError()
        {
            MessageBox.Show("Benutzername oder Passwort ist leer oder falsch", "Fehler: Benutzername / Passwort", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// Shows a messagebox if the user cant login because it is set inactive
        /// </summary>
        public static void ShowUserNotActiveError()
        {
            MessageBox.Show("Der Benutzer ist nicht mehr aktiv", "Fehler: User ist Inaktiv", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// Shows a messagebox if the dog has only one owner
        /// </summary>
        /// <param name="dogToRemove">Dog where the customer to remove from</param>
        public static void DogToRemoveError(DogModel dogToRemove)
        {
            MessageBox.Show($"Der Hund {dogToRemove.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                                   $"{dogToRemove.CustomerList.First().FirstName} {dogToRemove.CustomerList.First().LastName} der einzige Besitzer ist"
                                                   , "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// Shows a messagebox if the Customer has already a relation to the dog
        /// </summary>
        public static void DogAlreadCustomerRelationError()
        {
            MessageBox.Show("Hund ist dem Kunden bereits zugeordnet!", "Hinweis" , MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// Shows a messagebox if the dog has only one owner
        /// </summary>
        /// <param name="selectedDog">Dog where the customer to remove from</param>
        public static void DogCanNotRemovedFromCustomerError(DogModel selectedDog)
        {
            MessageBox.Show($"Der Hund {selectedDog.Name} kann nicht von der Liste entfernt werden, \r\nweil der Besitzer " +
                                               $"{selectedDog.CustomerList.First().FirstName} {selectedDog.CustomerList.First().LastName} der einzige Besitzer ist", "Fehler"
                                               , MessageBoxButton.OK, MessageBoxImage.Error); ;
        }
        /// <summary>
        /// Shows a messagebox if the Appoitnment is already in the Database
        /// </summary>
        /// <param name="appointmentModel">AppointmentModel to check</param>
        public static void AppointmentIsAlreadyInDatabaseError(AppointmentModel appointmentModel)
        {
            MessageBox.Show($"Der Eintrag für {appointmentModel.dogFromCustomer.Name} wurde mit diesen Details schon eingetragen.\r\nBitte verwenden Sie die Funktion: Termin bearbeiten" +
                $"          \r\noder legen Sie einen neuen Termin mit anderen Details an.","Fehler - Termin Duplikat",MessageBoxButton.OK,MessageBoxImage.Error);
        }
        /// <summary>
        /// Shows a messagebox if the dog is already booked in the given AppointmentModel Timespan 
        /// </summary>
        /// <param name="appointmentModel">AppointmentModel to get the Timespan from</param>
        public static void DogIsInThisTimespanAlreadyInDatabaseError(AppointmentModel appointmentModel)
        {
            MessageBox.Show($"Der Eintrag für {appointmentModel.dogFromCustomer.Name} ist in dem Zeitraum {appointmentModel.date_from.ToShortDateString()} - {appointmentModel.date_to.ToShortDateString()} schon gebucht.\r\nBitte verwenden Sie die Funktion: Termin bearbeiten" +
                $"          \r\noder legen Sie einen neuen Termin mit anderen Daten an.", "Fehler - Hund wurde schon gebucht", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
