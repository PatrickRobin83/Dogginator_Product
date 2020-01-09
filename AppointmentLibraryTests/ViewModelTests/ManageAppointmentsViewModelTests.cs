using System;
using System.Collections.Generic;
using de.rietrob.dogginator_product.AppointmentLibrary.ViewModels;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppointmentLibraryTests.ViewModelTests
{
   [TestClass]
    public class ManageAppointmentsViewModelTests
    {
        [TestMethod]
        public void DeletesDeleteAppointmentTheGivenElementFromList()
        {
            AppointmentModel selectedAppointment = new AppointmentModel();
            List<AppointmentModel> _availableAppointments = new List<AppointmentModel>();
            selectedAppointment.Id = 1;
            selectedAppointment.date_from = new DateTime(2019,08,02);
            selectedAppointment.date_to = new DateTime(2019,8,10);
            selectedAppointment.Create_Date = "2019.08.01";
            selectedAppointment.Edit_Date = "2019.08.02";
            selectedAppointment.days = 8;
            selectedAppointment.dogID = 1;
            selectedAppointment.isActive = true;
            _availableAppointments.Add(selectedAppointment);
            ManageAppointmentsViewModel _testTarget = new ManageAppointmentsViewModel();
            _testTarget.DeleteAppointment();



        }
    }
}
