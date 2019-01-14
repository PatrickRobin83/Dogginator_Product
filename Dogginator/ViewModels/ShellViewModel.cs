using Caliburn.Micro;
using DogginatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Contstructor
        public ShellViewModel()
        {
            GlobalConfig.InitalizeConnections(DataBaseType.SQLite);
            ActivateItem(new OverViewViewModel());
        }
        #endregion

        #region Methods
        public void LoadOverview()
        {
           ActivateItem(new OverViewViewModel());
        }

        public void LoadCustomer()
        {
            ActivateItem(new ManageCustomerViewModel());
        }
        public void LoadDog()
        {
            ActivateItem(new ManageDogsViewModel());
        }
        public void LoadAppointment()
        {
            ActivateItem(new ManageAppointmentsViewModel());
        }
        public void LoadConsistedBook()
        {
            ActivateItem(new ConsistedBookViewModel());
        }
        public void Exit()
        {
            TryClose();
        }

        #endregion
    }
}
