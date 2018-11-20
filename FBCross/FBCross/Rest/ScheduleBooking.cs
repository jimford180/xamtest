using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class ScheduleBooking : RestBase, IScheduleBooking
    {
        public Task<IRestResponse<ScheduleBookingResponse>> Post(ScheduleBookingRequest bookingRequest, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("scheduleBooking");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddJsonBody(bookingRequest);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.POST;
            return Client.ExecuteTaskAsync<ScheduleBookingResponse>(request);
        }
        public Task<IRestResponse<BookingResponse>> Put(ScheduleBookingRequest update, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("scheduleBooking");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddBody(update);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.PUT;
            return Client.ExecuteTaskAsync<BookingResponse>(request);
        }

        public Task<IRestResponse<BookingCancellationResponse>> Delete(Guid id, Guid merchantGuid, string sessionToken, bool refundPayment, string reason)
        {
            var request = new RestRequest("scheduleBooking");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id.ToString());
            request.AddQueryParameter("reason", reason);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.DELETE;
            return Client.ExecuteTaskAsync<BookingCancellationResponse>(request);
        }
    }
}
