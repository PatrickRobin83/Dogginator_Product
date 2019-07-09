using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ManageAppointmentsViewModel : Conductor<object>
    {

        #region Fields
        BindableCollection<DogModel> _availableDogs;
        bool _isDailyGuest;
        DogModel _selectedDog;
        DateTime _arrivingDay;
        DateTime _leavingDay;
        

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
        #endregion

        #region Constructor
        public ManageAppointmentsViewModel()
        {
            AvailableDogs = new BindableCollection<DogModel>(GlobalConfig.Connection.Get_DogsAll());
            SelectedDog = AvailableDogs.First();
            ArrivingDay = DateTime.Now;
            LeavingDay = DateTime.Now;

        }
        #endregion

        #region Methods
        public bool CanSaveAppointment
        {
            get
            {
                bool canSave = false;

                int result = DateTime.Compare(LeavingDay, ArrivingDay);
                if (result >= 0)
                {
                    canSave = true;
                }
                else
                {
                    canSave = false;
                }

                return canSave;
            }

           

        }
        public void SaveAppointment()
        {
            //TODO: Save the Appointment in Database
            //TODO: Calculate the days in total for 1 Month for every Dog in the Month.
            Console.WriteLine($"Hund: {SelectedDog.Name} kommt am: {ArrivingDay.ToString("dddd")} den {ArrivingDay.Date.ToShortDateString()} und geht am: {LeavingDay.ToString("dddd")} den {LeavingDay.ToShortDateString()}");
            Console.WriteLine($"{SelectedDog.Name} ist im Monat {DateTime.Today.ToString("MMMM")} {LeavingDay.Subtract(ArrivingDay).Days} Tage gekommen.");
            if (IsDailyGuest)
            {
                Console.WriteLine($"{SelectedDog.Name} ist ein Tagesgast");
            }
            else
            {
                Console.WriteLine($"{SelectedDog.Name} ist ein Übernachtungsgast");

            }
        }
        public int getDays()
        {
            //TODO: Figure out how to calculate the Days of Visit
            return 0;
        }
        #endregion

    }
}
