using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.rietrob.dogginator_product.AppointmentLibrary.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppointmentLibraryTests.HelperTests
{
    [TestClass]
    public class DateCalculatorTests
    {
        [TestMethod]
        public void ReturnsGetDaysRightCalculatedDays()
        {
            int calculatedDays = 0;
            DateTime _arrivingDate = new DateTime(2019,08,10);
            DateTime _leavinDate = new DateTime(2019,08,02);
            calculatedDays = DateCalculator.getDays(_arrivingDate,_leavinDate);
            Assert.AreEqual(9,calculatedDays);
        }
    }
}
