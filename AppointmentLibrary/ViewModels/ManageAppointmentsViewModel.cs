using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


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
        BindableCollection<DateTime> _dates = new BindableCollection<DateTime>();
        
        #endregion

        #region Properties
       
        public BindableCollection<DogModel> AvailableDogs
        {
            get { return _availableDogs; }
            set
            {
                _availableDogs = value;
                NotifyOfPropertyChange(() => AvailableDogs);
            }
        }
        public DogModel SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                NotifyOfPropertyChange(() => SelectedDog);
            }
        }
        
        public DateTime ArrivingDay
        {
            get { return _arrivingDay; }
            set
            {
                _arrivingDay = value;
                NotifyOfPropertyChange(() => CanSaveAppointment);
                NotifyOfPropertyChange(() => ArrivingDay);
                
            }
        }
        public DateTime LeavingDay
        {
            get { return _leavingDay; }
            set
            {
                _leavingDay = value;
                NotifyOfPropertyChange(() => CanSaveAppointment);
                NotifyOfPropertyChange(() => LeavingDay);
                
            }
        }
        public bool IsDailyGuest
        {
            get { return _isDailyGuest; }
            set
            {
                _isDailyGuest = value;
                NotifyOfPropertyChange(() => IsDailyGuest);
            }
        }

        public AppointmentModel AppointmentModel
        {
            get { return _appointmentModel; }
            set
            {
                _appointmentModel = value;
                NotifyOfPropertyChange(() => AppointmentModel);
            }
        }

        public int DaysOfVisit
        {
            get { return _daysofVisit; }
            set
            {
                _daysofVisit = value;
                NotifyOfPropertyChange(() => DaysOfVisit);
            }
        }

        public BindableCollection<AppointmentModel> AvailableAppointments
        {
            get { return _availableAppointments; }

            set
            {
                _availableAppointments = value;
                NotifyOfPropertyChange(() => AvailableAppointments);
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
        #endregion

        #region Constructor
        public ManageAppointmentsViewModel()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());

            SelectedDog = AvailableDogs.First();
            ArrivingDay = DateTime.Now;
            LeavingDay = DateTime.Now;
            AvailableAppointments = new BindableCollection<AppointmentModel>(GlobalConfig.Connection.getAppointments());
            AppointmentsInCurrentWeek(AvailableAppointments);
            //foreach (AppointmentModel apm in IsInWeekAppointments)
            //{
            //    if (apm.date_from)
            //    Console.WriteLine(GlobalConfig.Connection.GetDog(apm.DogId).Name + " ist diese Woche gebucht");
            //}

            
        }
        #endregion

        #region Methods
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
        public void SaveAppointment()
        {
            DaysOfVisit = getDays();

            AppointmentModel.dogFromCustomer = SelectedDog;
            AppointmentModel.date_from = ArrivingDay;
            AppointmentModel.date_to = LeavingDay;
            AppointmentModel.IsDailyGuest = IsDailyGuest;
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
        private int getDays()
        {
            return LeavingDay.Subtract(ArrivingDay).Days + 1;
        }

        public BindableCollection<AppointmentModel> AppointmentsInCurrentWeek(BindableCollection<AppointmentModel> appointmentlist)
        {
            foreach (AppointmentModel ap in appointmentlist)
            {
                if(GlobalConfig.getWeekOfYear(DateTime.Today) <= GlobalConfig.getWeekOfYear(ap.date_to) && 
                    GlobalConfig.getWeekOfYear(DateTime.Today) >= GlobalConfig.getWeekOfYear(ap.date_from))
                {
                    IsInWeekAppointments.Add(ap);
                }
            }

            return IsInWeekAppointments;
        }


        #endregion
    }
}
