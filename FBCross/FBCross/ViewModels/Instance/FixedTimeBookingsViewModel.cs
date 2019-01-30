using AutoMapper;
using FBCross.Rest.Dto;
using FBCross.ViewModels.Appointment;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Instance
{
    public class FixedTimeBookingsViewModel : ViewModelBase
    {
        private readonly InstanceDetailsViewModel _instanceDetails;
        private List<BookingDetail> _details;
        private readonly IMvxNavigationService _navigationService;
        private readonly Rest.IUnifiedAvailability _unifiedAvailability;
        private List<FixedTimeBookingViewModel> _bookings;
        public List<FixedTimeBookingViewModel> Bookings { get => _bookings; set { _bookings = value; RaisePropertyChanged(() => Bookings); } }
        private Rest.ICustomer _customerService { get; }
        private Rest.IScheduleBooking _scheduleBookingService { get; }
        private Rest.IFixedTimeBooking _fixedTimeBookingService { get; }

        public FixedTimeBookingsViewModel(InstanceDetailsViewModel instanceDetails, List<BookingDetail> bookings, IMvxNavigationService navigationService, Rest.IUnifiedAvailability unifiedAvailability, Rest.ICustomer customerService, Rest.IScheduleBooking scheduleBookingService, Rest.IFixedTimeBooking fixedTimeBookingService)
        {
            _bookings = bookings.Select(b => Mapper.Map<FixedTimeBookingViewModel>(b)).ToList();
            _instanceDetails = instanceDetails;
            _details = bookings;
            _navigationService = navigationService;
            _unifiedAvailability = unifiedAvailability;
            _customerService = customerService;
            _scheduleBookingService = scheduleBookingService;
            _fixedTimeBookingService = fixedTimeBookingService;
        }

        public IMvxAsyncCommand<FixedTimeBookingViewModel> ItemSelectedCommand => new MvxAsyncCommand<FixedTimeBookingViewModel>(ItemSelected);
        public IMvxAsyncCommand AddBookingCommand => new MvxAsyncCommand(AddBooking);

        private async Task AddBooking()
        {
            var appointment = new AppointmentViewModel(_navigationService, _unifiedAvailability, _customerService, _scheduleBookingService, _fixedTimeBookingService);
            appointment.IsFixedTimeAppointment = true;
            appointment.DateTime = Convert.ToDateTime(_instanceDetails.DateTime);
            appointment.ClassInstanceSlug = _instanceDetails.Id;
            var services = await FormsApp.Database.Services.GetEntitiesAsync();
            appointment.Service = Mapper.Map<ServiceViewModel>(services.First(s => _instanceDetails.ServiceName == s.Name));
            var employees = await FormsApp.Database.Employees.GetEntitiesAsync();
            if (_instanceDetails.Employee != null)
            {
                appointment.Employee = Mapper.Map<EmployeeViewModel>(employees.First(s => s.Id == _instanceDetails.Employee.Id));
            }
            appointment.LockedForFixedTime = true;
            await _navigationService.Navigate(appointment);
        }

        private async Task ItemSelected(FixedTimeBookingViewModel item)
        {
            var booking = _details.First(d => d.BookingId == item.BookingId);
            var appointment = new AppointmentViewModel(_navigationService, _unifiedAvailability, _customerService, _scheduleBookingService, _fixedTimeBookingService);
            appointment.Guid = Guid.Parse(booking.BookingId);
            appointment.IsFixedTimeAppointment = true;
            appointment.DateTime = Convert.ToDateTime(_instanceDetails.DateTime);
            appointment.ClassInstanceSlug = _instanceDetails.Id;
            appointment.Customer = Mapper.Map<Customer.Customer>(booking);
            var services = await FormsApp.Database.Services.GetEntitiesAsync();
            appointment.Service = Mapper.Map<ServiceViewModel>(services.First(s => _instanceDetails.ServiceName == s.Name));
            var employees = await FormsApp.Database.Employees.GetEntitiesAsync();
            if (_instanceDetails.Employee != null)
            {
                appointment.Employee = Mapper.Map<EmployeeViewModel>(employees.First(s => s.Id == _instanceDetails.Employee.Id));
            }
            appointment.LockedForFixedTime = true;
            appointment.RemindByEmail = booking.RemindByEmail;
            appointment.RemindBySms = booking.RemindBySms;
            await _navigationService.Navigate(appointment);
        }
    }
}
