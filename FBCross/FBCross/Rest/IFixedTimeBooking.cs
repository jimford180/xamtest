using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IFixedTimeBooking
    {
        Task<IRestResponse<BookingCancellationResponse>> Delete(Guid id, Guid merchantGuid, string sessionToken, bool refundPayment);
        Task<IRestResponse<BookingResponse>> Post(BookingRequest bookingRequest, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<BookingResponse>> Put(BookingDetail update, string sessionToken, Guid merchantGuid);
    }
}