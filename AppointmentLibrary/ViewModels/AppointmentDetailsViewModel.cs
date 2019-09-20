/**
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
using de.rietrob.dogginator_product.AppointmentLibrary.Converter;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Messages;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Globalization;
using System.Linq;

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
                //NotifyOfPropertyChange(() => CanSaveAppointment);
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
                //NotifyOfPropertyChange(() => CanSaveAppointment);
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

        }
        #endregion

        #region Methods
        public void CancelEdit()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new AppointmentModel());
            this.TryClose();
        }
        //public bool CanEditAppointment()
        //{
        //    bool canEdit = false;
        //    if(ArrivingDay != AppointmentModel.date_from || LeavingDay != AppointmentModel.date_to || IsDailyGuest != AppointmentModel.isdailyguest || AppointmentModel.dogID != SelectedDog.Id)
        //    {
        //        canEdit = true;
        //    }
        //    return canEdit;
        //}
        public void EditAppointment()
        {
            AppointmentModel.date_from = ArrivingDay;
            AppointmentModel.date_to = LeavingDay;
            AppointmentModel.isdailyguest =ConvertBoolToInt.GetBoolToInt(IsDailyGuest);
            AppointmentModel.dogFromCustomer = SelectedDog;
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(AppointmentModel);
            this.TryClose();
        }
        #endregion
    }
}
