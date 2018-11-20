using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IScheduleBooking
    {
        Task<IRestResponse<BookingCancellationResponse>> Delete(Guid id, Guid merchantGuid, string sessionToken, bool refundPayment, string reason);
        Task<IRestResponse<ScheduleBookingResponse>> Post(ScheduleBookingRequest bookingRequest, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<BookingResponse>> Put(ScheduleBookingRequest update, string sessionToken, Guid merchantGuid);
    }
}