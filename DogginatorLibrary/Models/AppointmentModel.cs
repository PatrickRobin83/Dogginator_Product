/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   AppointmentModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class AppointmentModel
    {

        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// int ID from Database
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Arriving Date for the Appointment
        /// </summary>
        public DateTime date_from { get; set; }
        /// <summary>
        /// Leaving Date from the Appointment
        /// </summary>
        public DateTime date_to { get; set; }
        /// <summary>
        /// DogModel for the Details in the appointment
        /// </summary>
        public DogModel dogFromCustomer { get; set; }
        /// <summary>
        /// True if the Dog is a daily guest False if the dog is an overnighter
        /// </summary>
        public int isdailyguest { get; set; }
        /// <summary>
        /// Days the dog stays for this appointment
        /// </summary>
        public int days { get; set; }
        /// <summary>
        /// Id from the Dog what the appointment is for
        /// </summary>
        public int dogID { get; set; }

        public string ArrivingDateForTable
        {
            get
            {
                return date_from.ToShortDateString();
            }
        }
        public string LeavingDateForTable
        {
            get { return date_to.ToShortDateString(); }
        }
        #endregion

        #region Constructor

        #endregion

        #region Methods
        #endregion
    }
}
