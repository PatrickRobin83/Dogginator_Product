/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CustomerModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Collections.Generic;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class CustomerModel
    { 

        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Unique ID from the database to identify the Customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Salution Mr. or Mrs. in German
        /// </summary>
        public string Salution { get; set; }
        /// <summary>
        /// Lastname of the Customer
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Firstname of the Customer
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Street where the customer lives
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Housenumber from the house or flat of the customer
        /// </summary>
        public string HouseNumber { get; set; }
        /// <summary>
        /// German Zipcode from the city where the customer lives
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// City where the customer lives
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Landline phone number where the customer is available
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Mobile Number where the customer is also available
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// Email adress from the Customer to send information about the summer vacation or to send the invoice or whatever
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Birthday Date of the Customer
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// When was the Customer created automatically filled in from the database
        /// </summary>
        public string Create_Date { get; set; }
        /// <summary>
        /// When was the Customer the last time updated this is autmatically filled from the database
        /// </summary>
        public string Edit_Date { get; set; }
        /// <summary>
        /// List of Notes or remarks about the Customer
        /// </summary>
        public List<NoteModel> Notes { get; set; }
        /// <summary>
        /// List of Owned dogs
        /// </summary>
        public List<DogModel> OwnedDogs { get; set; }
        /// <summary>
        /// Customer active = true | Customer inactive = false;
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gives back a string if the customer is active or not
        /// </summary>
        public string CustomerActive { get; set; }
        /// <summary>
        /// Gives back the Street and the housenumber in one string like "Musterstrasse 25"
        /// </summary>
        public string Address
        {
            get
            {
                return $"{Street} {HouseNumber}";
            }
        }
        /// <summary>
        /// Gives a string with Salution Firstname and Lastname back like --> "Herr Markus Mustermann"
        /// </summary>
        public string FullCustomer
        { 
            get
            {
                return $" { Salution }  { FirstName }  { LastName }";
            }
        }
        public string Name
        {
            get { return $"{LastName}, {FirstName}"; }
        }
#endregion

        #region Contstructor

        #endregion

        #region Methods

        #endregion
    }
}
