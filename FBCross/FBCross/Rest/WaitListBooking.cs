using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public class WaitListBooking : RestBase, IWaitListBooking
    {
        public Task<IRestResponse<WaitListDeletionResponse>> Delete(int id, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("waitList");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id.ToString());
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.DELETE;
            return Client.ExecuteTaskAsync<WaitListDeletionResponse>(request);
        }

        public Task<IRestResponse<WaitListCreationResponse>> Post(WaitListRequest bookingRequest, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("waitList");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddJsonBody(bookingRequest);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.POST;
            return Client.ExecuteTaskAsync<WaitListCreationResponse>(request);
        }

        public Task<IRestResponse<WaitListUpdateResponse>> Put(int waitListId, WaitListRequest update, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("waitList");
            request.AddQueryParameter("id", waitListId.ToString());
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddJsonBody(update);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.PUT;
            return Client.ExecuteTaskAsync<WaitListUpdateResponse>(request);
        }
    }
}
