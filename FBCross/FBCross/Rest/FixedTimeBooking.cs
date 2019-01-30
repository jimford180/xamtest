using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class FixedTimeBooking : RestBase, IFixedTimeBooking
    {
        public Task<IRestResponse<BookingResponse>> Post(BookingRequest bookingRequest, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("booking");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddJsonBody(bookingRequest);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.POST;
            return Client.ExecuteTaskAsync<BookingResponse>(request);
        }
        public Task<IRestResponse<BookingResponse>> Put(BookingDetail update, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("booking");
            request.AddQueryParameter("id", update.BookingId);
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddJsonBody(update);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.PUT;
            return Client.ExecuteTaskAsync<BookingResponse>(request);
        }

        public Task<IRestResponse<BookingCancellationResponse>> Delete(Guid id, Guid merchantGuid, string sessionToken, bool refundPayment)
        {
            var request = new RestRequest("booking");
            request.AddQueryParameter("gymGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id.ToString());
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.DELETE;
            return Client.ExecuteTaskAsync<BookingCancellationResponse>(request);
        }
    }
}
