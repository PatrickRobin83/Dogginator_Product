using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary.Models
{
    public class CustomerModel : IEquatable<CustomerModel>
    { 

        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Salution { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string Birthday { get; set; }

        public string Create_Date { get; set; }

        public string Edit_Date { get; set; }

        public List<NoteModel> Notes { get; set; }
        
        public List<DogModel> OwnedDogs { get; set; }

        public string FullCustomer
        { 
            get
            {
                return $" { Salution }  { FirstName }  { LastName }  { Street } { HouseNumber }  { ZipCode }  { City } ";
            }
        }

        public bool Equals(CustomerModel other)
        {
            return this.Id == other.Id;
        }

        #endregion

        #region Contstructor

        #endregion

        #region Methods

        #endregion
    }
}
