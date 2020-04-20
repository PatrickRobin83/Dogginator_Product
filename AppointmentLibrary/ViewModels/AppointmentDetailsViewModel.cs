/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   AppointmentDetailsViewModel.cs
 *   Date			:   30.07.2019 13:13:19
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.AppointmentLibrary.Helper;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;

namespace de.rietrob.dogginator_product.AppointmentLibrary.ViewModels
{

    public class AppointmentDetailsViewModel : Conductor<object>
    {

        #region Fields

        BindableCollection<DogModel> _availableDogs;
        bool _isDailyGuest;
        DogModel _selectedDog;
        DateTime _arrivingDay;
        DateTime _leavingDay;
        AppointmentModel _appointmentModel = new AppointmentModel();
        int _daysofVisit = 0;
        string _dogAndCustomer;
        bool _isAppointmentActive;

        #endregion

        #region Properties

        /// <summary>
        /// list of all available Dogs in the Database
        /// </summary>
        public BindableCollection<DogModel> AvailableDogs
        {
            get { return _availableDogs; }
            set
            {
                _availableDogs = value;
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }

        /// <summary>
        /// represents the selected Dog from the Combobox
        /// </summary>
        public DogModel SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                NotifyOfPropertyChange(() => SelectedDog);
                NotifyOfPropertyChange(() => CanEditAppointment);
              
            }
        }
        /// <summary>
        /// represents the arriving date for the apponintment
        /// </summary>
        public DateTime ArrivingDay
        {
            get { return _arrivingDay; }
            set
            {
                _arrivingDay = value;
                if (LeavingDay.Equals(ArrivingDay))
                {
                    IsDailyGuest = true;
                }
                else
                {
                    IsDailyGuest = Convert.ToBoolean(AppointmentModel.isdailyguest);
                    //IsDailyGuest = false;
                }
                NotifyOfPropertyChange(() => ArrivingDay);
                NotifyOfPropertyChange(() => CanEditAppointment);
                NotifyOfPropertyChange(() => IsDailyGuest);
            }
        }
        /// <summary>
        /// represents the leaving day for the appointment
        /// </summary>
        public DateTime LeavingDay
        {
            get { return _leavingDay; }
            set
            {
                _leavingDay = value.Date;
                if (ArrivingDay.Equals(LeavingDay))
                {
                    IsDailyGuest = true;
                }
                else 
                { 
                    IsDailyGuest = Convert.ToBoolean(AppointmentModel.isdailyguest);
                }
                NotifyOfPropertyChange(() => CanEditAppointment);
                NotifyOfPropertyChange(() => LeavingDay);
                NotifyOfPropertyChange(() => IsDailyGuest);
            }
        }
        /// <summary>
        /// indicates is it a daily guest appointment or a overnight appointment
        /// </summary>
        public bool IsDailyGuest
        {
            get { return _isDailyGuest; }
            set
            {
                _isDailyGuest = value;
                NotifyOfPropertyChange(() => IsDailyGuest);
                NotifyOfPropertyChange(() => CanEditAppointment);
            }
        }
        /// <summary>
        /// represents the AppointmentModel for holding all the data
        /// </summary>
        public AppointmentModel AppointmentModel
        {
            get { return _appointmentModel; }
            set
            {
                _appointmentModel = value;
                NotifyOfPropertyChange(() => AppointmentModel);
                NotifyOfPropertyChange(() => CanEditAppointment);
            }
        }
        /// <summary>
        /// represents the Number of days for this appointment
        /// </summary>
        public int DaysOfVisit 
        {
            get { return _daysofVisit; }
            set
            {
                _daysofVisit = value;
                NotifyOfPropertyChange(() => DaysOfVisit);
            }
        }
        /// <summary>
        /// represents the Name from the dog and customer in a special format
        /// </summary>
        public string DogAndCustomer
        {
            get {return _dogAndCustomer; }
            set
            {
                _dogAndCustomer = value;
                NotifyOfPropertyChange(() => DogAndCustomer);
            }
        }
        /// <summary>
        /// indicates if the appointment is active or not
        /// </summary>
        public bool IsAppointmentActive
        {
            get { return _isAppointmentActive; }
            set
            {
                _isAppointmentActive = value;
                NotifyOfPropertyChange(() => IsActive);
            }

        }

        #endregion

        #region Constructor
        public AppointmentDetailsViewModel(AppointmentModel appointment, BindableCollection<DogModel> dogs)
        {
            AppointmentModel = appointment;
            AvailableDogs = dogs;
            foreach (DogModel dog in AvailableDogs)
            {
                if (dog.DogAndCustomer.Equals(appointment.dogFromCustomer.DogAndCustomer))
                {
                    SelectedDog = dog;
                }
            }
            ArrivingDay = AppointmentModel.date_from;
            LeavingDay = AppointmentModel.date_to;
            IsDailyGuest = Convert.ToBoolean(AppointmentModel.isdailyguest);
            DogAndCustomer = AppointmentModel.dogFromCustomer.DogAndCustomer;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A Method to cancel editing the appoointment
        /// </summary>
        public void CancelEdit()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new AppointmentModel());
            this.TryClose();
        }
        /// <summary>
        /// Deletes the Appoitnment from database and refreshes the UI
        /// </summary>
        public void DeleteAppointment()
        {
            GlobalConfig.Connection.deleteAppointmentModel(AppointmentModel);
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new AppointmentModel());
            this.TryClose();
        }
        /// <summary>
        /// Activates or deactivates the Edit Button.
        /// </summary>
        public bool CanEditAppointment
        {
            get
            {
                bool canEdit = false;
                if (ArrivingDay != AppointmentModel.date_from)
                {
                    canEdit = true;
                }
                else if (LeavingDay != AppointmentModel.date_to)
                {
                    canEdit = true;
                }

                else if (IsDailyGuest != Convert.ToBoolean(AppointmentModel.isdailyguest))
                {
                    canEdit = true;
                }

                else if (AppointmentModel.dogID != SelectedDog.Id)
                {
                    canEdit = true;
                }
                return canEdit;
            }
            
            
        }
        /// <summary>
        /// 
        /// </summary>
        public void EditAppointment()
        {
           // // TODO: implement a check if the choosen dog is already booked in the edited timespan 
           //AppointmentModel tempAppontmentModel = new AppointmentModel();
           //tempAppontmentModel.date_from = ArrivingDay;
           //tempAppontmentModel.date_to = LeavingDay;
           //tempAppontmentModel.isdailyguest = Convert.ToInt32(IsDailyGuest);
           //tempAppontmentModel.dogFromCustomer = SelectedDog;
           //tempAppontmentModel.isActive = IsAppointmentActive;
           //tempAppontmentModel.days = DateCalculator.getDays(LeavingDay, ArrivingDay);

           // if (GlobalConfig.Connection.isDogInTimeSpanAlreadyInDatabase(tempAppontmentModel))
           // {
           //     ErrorMessages.DogIsInThisTimespanAlreadyInDatabaseError(tempAppontmentModel);
           //     EventAggregationProvider.DogginatorAggregator.PublishOnUIThread( new AppointmentModel());
           // }
           // else
           // {

                AppointmentModel.date_from = ArrivingDay;
                AppointmentModel.date_to = LeavingDay;
                AppointmentModel.isdailyguest = Convert.ToInt32(IsDailyGuest);
                AppointmentModel.dogFromCustomer = SelectedDog;
                AppointmentModel.isActive = IsAppointmentActive;
                AppointmentModel.days = DateCalculator.getDays(LeavingDay, ArrivingDay);
                EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(AppointmentModel);
           // }
            
            this.TryClose();
        }
        #endregion
    }
}
