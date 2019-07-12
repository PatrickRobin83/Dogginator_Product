using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Models
{
    public class AppointmentModel
    {

        #region Fields
        #endregion

        #region Properties
        public int Id { get; set; }
        public DateTime arrivingDate { get; set; }
        public DateTime leavingDate { get; set; }
        public DogModel dogFromCustomer { get; set; }
        public bool IsDailyGuest { get; set; }
        public int days { get; set; }
        #endregion

        #region Constructor

        #endregion

        #region Methods
        #endregion
    }
}
