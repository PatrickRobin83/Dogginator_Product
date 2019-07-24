/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ManageAppointmentViewModel.cs
 *   Date			:   2019-07-17
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.AppointmentLibrary.Helper;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Globalization;
using System.Linq;


namespace de.rietrob.dogginator_product.AppointmentLibrary.ViewModels
{
    public class ManageAppointmentsViewModel : Conductor<object>
    {

        #region Fields
        BindableCollection<DogModel> _availableDogs;
        
        BindableCollection<AppointmentModel> _availableAppointments = new BindableCollection<AppointmentModel>();
       
        BindableCollection<AppointmentModel> _isinWeekAppointments = new BindableCollection<AppointmentModel>();

        bool _isDailyGuest;
       
        DogModel _selectedDog;

        DateTime _arrivingDay;

        DateTime _leavingDay;

        AppointmentModel _appointmentModel = new AppointmentModel();

        int _daysofVisit = 0;

        DateTime _firstDayOfWeek = GlobalConfig.GetFirstDayOfWeek(DateTime.Today);

        DateTime _lastDayOfWeek = GlobalConfig.GetFirstDayOfWeek(GlobalConfig.GetFirstDayOfWeek(DateTime.Today)).AddDays(6);

        AppointmentModel _selectedAppointment;

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
                    IsDailyGuest = false;
                }
                NotifyOfPropertyChange(() => CanSaveAppointment);
                NotifyOfPropertyChange(() => ArrivingDay);
                
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
                    IsDailyGuest = false;
                }
                NotifyOfPropertyChange(() => CanSaveAppointment);
                NotifyOfPropertyChange(() => LeavingDay);
                
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
            }
        }
        /// <summary>
        /// how many days is the appointment
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
        /// list of all available appointments in the Database
        /// </summary>
        public BindableCollection<AppointmentModel> AvailableAppointments
        {
            get { return _availableAppointments; }

            set
            {
                _availableAppointments = value;
                NotifyOfPropertyChange(() => AvailableAppointments);
            }
        }
        /// <summary>
        /// List of all appointments in the current week
        /// </summary>
        public BindableCollection<AppointmentModel> IsInWeekAppointments
        {
            get { return _isinWeekAppointments; }

            set
            {
                _isinWeekAppointments = value;
                NotifyOfPropertyChange(() => IsInWeekAppointments);
            }
        }

        public AppointmentModel SelectedAppointment
        {
            get { return _selectedAppointment; }
            set
            {
                _selectedAppointment = value;
                NotifyOfPropertyChange(() => SelectedAppointment);
            }
        }

        public string FirstDayOfWeek
        {
            get { return _firstDayOfWeek.ToShortDateString(); }
            set
            {
                value = _firstDayOfWeek.ToShortDateString();
            }
        }
        public string LastDayOfWeek
        {
            get { return _lastDayOfWeek.ToShortDateString(); }
            set
            {
                value = _lastDayOfWeek.ToShortDateString();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Get everything initilized
        /// </summary>
        public ManageAppointmentsViewModel()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            SelectedDog = AvailableDogs.First();
            ArrivingDay = DateTime.Now;
            LeavingDay = DateTime.Now;
            AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.getAppointments());
            //_isinWeekAppointments = AppointmentsInCurrentWeek(AvailableAppointments);
            AppointmentsInCurrentWeek(AvailableAppointments); 
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Enables or disables the "Save Appointment Button"
        /// </summary>
        /// <returns>the bool value if the Appointment Save Button is enabeld or disabled</returns>
        public bool CanSaveAppointment
        {
            get
            {
                bool canSave = false;

                if(ArrivingDay >= DateTime.Today && LeavingDay >= DateTime.Today && ArrivingDay <= LeavingDay)
                {
                    canSave = true;
                }                
                return canSave;
            }
        }
        /// <summary>
        /// Saves the Appointment with the data into the database
        /// </summary>
        public void SaveAppointment()
        {
            DaysOfVisit = DateCalculator.getDays(LeavingDay,ArrivingDay);

            AppointmentModel.dogFromCustomer = SelectedDog;
            AppointmentModel.date_from = ArrivingDay;
            AppointmentModel.date_to = LeavingDay;
            AppointmentModel.isdailyguest = IsDailyGuest;
            AppointmentModel.days = DaysOfVisit;

            if(GlobalConfig.Connection.isAppointmentInDatabase(AppointmentModel))
            {
                ErrorMessages.AppointmentIsAlreadyInDatabaseError(AppointmentModel);
            }
            else if (GlobalConfig.Connection.isDogInTimeSpanAlreadyInDatabase(AppointmentModel))
            {
                ErrorMessages.DogIsInThisTimespanAlreadyInDatabaseError(AppointmentModel);
            }
            else
            {
                AppointmentModel = GlobalConfig.Connection.AddAppointmentToDatabase(AppointmentModel);
                SuccessMessages.AppointmentCreatedSuccess();
                ArrivingDay = DateTime.Today;
                LeavingDay = DateTime.Today;
                IsDailyGuest = false;
            }
            
        }
        
        /// <summary>
        /// Gets all appoitnments in the current Week of the Year 
        /// </summary>
        /// <param name="appointmentlist">The List of all available Appointment</param>
        /// <returns>A list of Appointments in the current week</returns>
        public BindableCollection<AppointmentModel> AppointmentsInCurrentWeek(BindableCollection<AppointmentModel> appointmentlist)
        {
            foreach (AppointmentModel ap in appointmentlist)
            {
                DisplayAppointmentAsString(ap);
            }
            return IsInWeekAppointments;
        }
        // TODO: Get the Buttons work Create an DetailsView For Appointments
        private string DisplayAppointmentAsString(AppointmentModel appointmentModel)
        {
            string resultstring = "";

            if (appointmentModel.date_from >= _firstDayOfWeek && appointmentModel.date_from <= _lastDayOfWeek)
            {
                if (appointmentModel.isdailyguest)
                {
                    Console.WriteLine($"{appointmentModel.dogFromCustomer.Name} ist gebucht vom {appointmentModel.date_from} bis {appointmentModel.date_to} und wird morgens abgegeben und abends abgeholt.");
                }
                else
                {
                    Console.WriteLine($"{appointmentModel.dogFromCustomer.Name} ist gebucht vom {appointmentModel.date_from} bis zum {appointmentModel.date_to}");
                }
                IsInWeekAppointments.Add(appointmentModel);
            }
            return resultstring;
        }
        #endregion
    }
}
