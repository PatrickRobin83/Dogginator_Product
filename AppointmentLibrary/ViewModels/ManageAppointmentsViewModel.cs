/*
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
using System.Linq;
using System.Windows.Navigation;


namespace de.rietrob.dogginator_product.AppointmentLibrary.ViewModels
{
    public class ManageAppointmentsViewModel : Conductor<object>.Collection.OneActive, IHandle<AppointmentModel>
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
        Screen _appointmentsDetailsView;
        bool _appointmentsDetailsViewIsVisible;
        bool _manageAppointmentsIsVisible;
        DateTime _datePickerForWeek;


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

        public DateTime DatePickerForWeek
        {
            get { return _datePickerForWeek; }

            set
            {
                _datePickerForWeek = value.Date;
                WeekSelectedShow();
                NotifyOfPropertyChange(() => IsInWeekAppointments);
            }
        }
        
        /// <summary>
        /// All Appointments in the shown Week
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
        /// The selected Appointment in the DataGrid
        /// </summary>
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

        /// <summary>
        /// The Date of the First Day of the Week
        /// </summary>
        public string FirstDayOfWeek
        {
            get { return _firstDayOfWeek.ToShortDateString(); }
            set
            {
                _firstDayOfWeek = Convert.ToDateTime(value);
                NotifyOfPropertyChange(() => FirstDayOfWeek);
                CheckIsInWeek(AvailableAppointments);
               
            }
        }

        /// <summary>
        /// The Date of the last day of the week
        /// </summary>
        public string LastDayOfWeek
        {
            get { return _lastDayOfWeek.ToShortDateString(); }
            set
            {
                _lastDayOfWeek = Convert.ToDateTime(value);
                NotifyOfPropertyChange(() => LastDayOfWeek);
                CheckIsInWeek(AvailableAppointments);
            }
        }

        /// <summary>
        /// The View for the Appointmentdetails
        /// </summary>
        public Screen AppointmentsDetailsView
        {
            get { return _appointmentsDetailsView; }
            set
            {
                _appointmentsDetailsView = value;
                NotifyOfPropertyChange(() => AppointmentsDetailsView);
            }
        }

        /// <summary>
        /// Returns true if the DetailsView is visible
        /// </summary>
        public bool AppointmentsDetailsViewIsVisible
        {
            get { return _appointmentsDetailsViewIsVisible; }
            set
            {
                _appointmentsDetailsViewIsVisible = value;
                NotifyOfPropertyChange(() => AppointmentsDetailsViewIsVisible);
            }
        }

        /// <summary>
        /// Returns true if the ManageAppointmentsView is visible
        /// </summary>
        public bool ManageAppointmentsIsVisible
        {
            get { return _manageAppointmentsIsVisible; }
            set
            {
                _manageAppointmentsIsVisible = value;
                NotifyOfPropertyChange(() => ManageAppointmentsIsVisible);
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
            //DatePickerForWeek = DateTime.Now;
            IsInWeekAppointments.OrderBy(x => x.date_from);
            ManageAppointmentsIsVisible = true;
            AppointmentsDetailsViewIsVisible = false;
            AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.GetAppointments());
            foreach (AppointmentModel model in AvailableAppointments)
            {
                AppointmentsInCurrentWeek(model);
            }
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
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

        /// <summary>
        /// Enables the Load Appointment Button
        /// </summary>
        /// <returns> the bool value if the Load Appointment Button is enabled or disabled</returns>
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

        /// <summary>
        /// Loads the Appointment Details View and fill the fields with the values from the given AppoinmentModel
        /// </summary>
        /// <param name="model">Appointment to edit</param>
        public void LoadAppointment(AppointmentModel model)
        {
            model = SelectedAppointment;
            AppointmentsDetailsView = new AppointmentDetailsViewModel(model, AvailableDogs);
            Items.Add(AppointmentsDetailsView);
            ManageAppointmentsIsVisible = false;
            AppointmentsDetailsViewIsVisible = true;
        }

        /// <summary>
        /// Enables or disables the Delete Appointment Button 
        /// </summary>
        /// <returns>the bool value Button enabled = true disabled = false</returns>
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

        /// <summary>
        /// Deletes the given appointment from the list and the database
        /// </summary>
        /// <param name="model">Apointment to delete</param>
        public void DeleteAppointment()
        {
            GlobalConfig.Connection.DeleteAppointmentModel(SelectedAppointment);
            AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.GetAppointments());
            CheckIsInWeek(AvailableAppointments);
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
            AppointmentModel.isdailyguest = Convert.ToInt32(IsDailyGuest);
            AppointmentModel.days = DaysOfVisit;
            AppointmentModel.dogID = SelectedDog.Id;
            AppointmentModel.isActive = true;
            

            if (GlobalConfig.Connection.IsAppointmentInDataStore(AppointmentModel))
            {
                ErrorMessages.AppointmentIsAlreadyInDatabaseError(AppointmentModel);
            }
            else if (GlobalConfig.Connection.IsDogInTimeSpanAlreadyInDataStore(AppointmentModel))
            {
                ErrorMessages.DogIsInThisTimespanAlreadyInDatabaseError(AppointmentModel);
            }
            else
            {
                AppointmentModel = GlobalConfig.Connection.AddAppointmentToDataStore(AppointmentModel);
                SuccessMessages.AppointmentCreatedSuccess();
                AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.GetAppointments());
            }

            foreach (AppointmentModel model in AvailableAppointments)
            {
                AppointmentsInCurrentWeek(model);
            }
            IsInWeekAppointments.OrderBy(x => x.date_from);
            ArrivingDay = DateTime.Today;
            LeavingDay = DateTime.Today;
            IsDailyGuest = false;
            DatePickerForWeek = DateTime.Parse(FirstDayOfWeek);

        }

        /// <summary>
        /// Gets all appoitnments in the previous Week of the Year 
        /// </summary>
        /// <param name="appointmentlist">The List of all available Appointment</param>
        /// <returns>A list of Appointments in the previous week</returns>
        /// 
        public void PreviousWeek()
        {
            FirstDayOfWeek = GlobalConfig.GetFirstDayOfWeek(Convert.ToDateTime(FirstDayOfWeek).AddDays(-7)).ToShortDateString();
            LastDayOfWeek = Convert.ToDateTime(FirstDayOfWeek).AddDays(6).ToShortDateString();
            DatePickerForWeek = DateTime.Parse(FirstDayOfWeek);
            NotifyOfPropertyChange(() => DatePickerForWeek);
        }

        /// <summary>
        /// Gets all appoitnments in the next Week of the Year 
        /// </summary>
        /// <param name="appointmentlist">The List of all available Appointment</param>
        /// <returns>A list of Appointments in the next week</returns>
        /// 
        public void NextWeek()
        {
            FirstDayOfWeek = GlobalConfig.GetFirstDayOfWeek(Convert.ToDateTime(FirstDayOfWeek).AddDays(7)).ToShortDateString();
            LastDayOfWeek = Convert.ToDateTime(FirstDayOfWeek).AddDays(6).ToShortDateString();
            DatePickerForWeek = DateTime.Parse(FirstDayOfWeek);
            NotifyOfPropertyChange(() => DatePickerForWeek);
        }

        /// <summary>
        /// Gets all appoitnments in the current Week of the Year 
        /// </summary>
        /// <param name="appointmentlist">The List of all available Appointment</param>
        /// <returns>A list of Appointments in the current week</returns>
        /// 
        public void CurrentWeek()
        {
            FirstDayOfWeek = GlobalConfig.GetFirstDayOfWeek(DateTime.Today).ToShortDateString();
            LastDayOfWeek = Convert.ToDateTime(FirstDayOfWeek).AddDays(6).ToShortDateString();
            DatePickerForWeek = DateTime.Parse(FirstDayOfWeek);
            NotifyOfPropertyChange(() => DatePickerForWeek);
        }

        public void WeekSelectedShow()
        {
            FirstDayOfWeek = GlobalConfig.GetFirstDayOfWeek(DatePickerForWeek).ToShortDateString();
            LastDayOfWeek = Convert.ToDateTime(FirstDayOfWeek).AddDays(6).ToShortDateString();
            //DatePickerForWeek = DateTime.Parse(FirstDayOfWeek);
            //NotifyOfPropertyChange(() => DatePickerForWeek);
        }

        /// <summary>
        /// Compares the given appointment with the date values of the current week. If the appointment is in the current week it will saved in a List
        /// </summary>
        /// <param name="appointment">Appointment to check if it is in the current week</param>
        private void AppointmentsInCurrentWeek(AppointmentModel appointment)
        {
            if (appointment.date_from >= Convert.ToDateTime(FirstDayOfWeek) && appointment.date_from <= Convert.ToDateTime(LastDayOfWeek))
            {
                if (!IsInWeekAppointments.Contains(appointment))
                {
                    IsInWeekAppointments.Add(appointment);
                }
            }
        }

        /// <summary>
        /// After checking is the appointment in the current displayed week adds it to the list, if it is not already in
        /// </summary>
        /// <param name="appointments">List of AppointmentModels</param>
        public void CheckIsInWeek(BindableCollection<AppointmentModel> appointments)
        {
            IsInWeekAppointments.Clear();
            foreach (AppointmentModel ap in appointments)
            {
                AppointmentsInCurrentWeek(ap);
            }
        }

        /// <summary>
        /// Handles the given AppointmentModel and refreshes the UI
        /// </summary>
        /// <param name="ap"></param>
        public void Handle(AppointmentModel ap)
        {
            if (ap != null && ap.Id > 0)
            {
                GlobalConfig.Connection.EditAppointmentModel(ap);
            }
            AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.GetAppointments());
            CheckIsInWeek(AvailableAppointments);
            ManageAppointmentsIsVisible = true;
            AppointmentsDetailsViewIsVisible = false;
        }
    }
    #endregion
}
