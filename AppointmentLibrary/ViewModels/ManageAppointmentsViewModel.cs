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

        public BindableCollection<AppointmentModel> IsInWeekAppointments
        {
            get { return _isinWeekAppointments; }
            set
            {
                _isinWeekAppointments = value;
                NotifyOfPropertyChange(() => IsInWeekAppointments);
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
        public AppointmentModel SelectedAppointment
        {
            get { return _selectedAppointment; }
            set
            {
                _selectedAppointment = value;
                NotifyOfPropertyChange(() => SelectedAppointment);
                NotifyOfPropertyChange(() => CanLoadAppointment);
                NotifyOfPropertyChange(() => CanDeleteAppointment);
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
            foreach (AppointmentModel model in AvailableAppointments)
            {
                AppointmentsInCurrentWeek(model);
            }
            IsInWeekAppointments.OrderBy(x => x.date_from);
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

                if (ArrivingDay >= DateTime.Today && LeavingDay >= DateTime.Today && ArrivingDay <= LeavingDay)
                {
                    canSave = true;
                }
                return canSave;
            }
        }

        public bool CanLoadAppointment
        {
            get
            {
                bool canEdit = false;
                if (SelectedAppointment != null)
                {
                    canEdit = true;
                }
                return canEdit;
            }
        }

        public void LoadAppointment(AppointmentModel model)
        {
            model = SelectedAppointment;
            Console.WriteLine(model.dogFromCustomer.Name);
        }

        public bool CanDeleteAppointment
        {
            get
            {
                bool canDelete = false;
                if (SelectedAppointment != null)
                {
                    canDelete = true;
                }
                return canDelete;
            }
        }

        public void DeleteAppointment(AppointmentModel model)
        {
            model = SelectedAppointment;
            Console.WriteLine(model.dogFromCustomer.Name);
        }

        /// <summary>
        /// Saves the Appointment with the data into the database
        /// </summary>
        public void SaveAppointment()
        {
            IsInWeekAppointments.Clear();
            DaysOfVisit = DateCalculator.getDays(LeavingDay, ArrivingDay);

            AppointmentModel.dogFromCustomer = SelectedDog;
            AppointmentModel.date_from = ArrivingDay;
            AppointmentModel.date_to = LeavingDay;
            AppointmentModel.isdailyguest = IsDailyGuest;
            AppointmentModel.days = DaysOfVisit;
            AppointmentModel.dogID = SelectedDog.Id;

            if (GlobalConfig.Connection.isAppointmentInDatabase(AppointmentModel))
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
                AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.getAppointments());
            }

            foreach (AppointmentModel model in AvailableAppointments)
            {
                AppointmentsInCurrentWeek(model);
            }
            IsInWeekAppointments.OrderBy(x => x.date_from);
            ArrivingDay = DateTime.Today;
            LeavingDay = DateTime.Today;
            IsDailyGuest = false;

        }

        /// <summary>
        /// Gets all appoitnments in the current Week of the Year 
        /// </summary>
        /// <param name="appointmentlist">The List of all available Appointment</param>
        /// <returns>A list of Appointments in the current week</returns>
        /// 

        public void PreviousWeek()
        {
            FirstDayOfWeek = GlobalConfig.GetFirstDayOfWeek(DateTime.Today.AddDays(7)).ToShortDateString();
            Console.WriteLine(FirstDayOfWeek);
        }
        public void AppointmentsInCurrentWeek(AppointmentModel appointment)
        {
            if (appointment.date_from >= _firstDayOfWeek && appointment.date_from <= _lastDayOfWeek)
            {
                if (!IsInWeekAppointments.Contains(appointment))
                {
                    IsInWeekAppointments.Add(appointment);
                }

                //foreach (AppointmentModel ap in IsInWeekAppointments)
                //{
                //    if (ap.Id != appointment.Id)
                //    {
                //        IsInWeekAppointments.Add(appointment);
                //    }
            }
           
        }
    }
    // TODO: Create an EditView For Appointments
    #endregion
}
