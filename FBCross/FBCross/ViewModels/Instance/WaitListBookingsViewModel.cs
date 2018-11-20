using AutoMapper;
using FBCross.Rest.Dto;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Instance
{
    public class WaitListBookingsViewModel : ViewModelBase
    {
        private List<FixedTimeBookingViewModel> _bookings;
        public List<FixedTimeBookingViewModel> Bookings { get => _bookings; set { _bookings = value; RaisePropertyChanged(() => Bookings); } }

        public WaitListBookingsViewModel(List<WaitListDetail> bookings)
        {
            _bookings = bookings.Select(b => Mapper.Map<FixedTimeBookingViewModel>(b)).ToList();
        }

        public IMvxAsyncCommand<FixedTimeBookingViewModel> ItemSelectedCommand => new MvxAsyncCommand<FixedTimeBookingViewModel>(ItemSelected);

        private async Task ItemSelected(FixedTimeBookingViewModel item)
        {
            await FormsApp.Database.Services.GetEntitiesAsync();
        }
    }
}
