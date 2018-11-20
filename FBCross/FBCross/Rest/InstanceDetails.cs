using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class InstanceDetails : RestBase, IInstanceDetails
    {
        public Task<IRestResponse<ClassInstanceDetail>> Get(Guid merchantGuid, string sessionToken, string id)
        {
            var request = new RestRequest("classInstance");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id);
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<ClassInstanceDetail>(request);
        }

        public Task<IRestResponse<ClassInstanceUpdateResponse>> Put(ClassInstanceUpdate update, string id, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("classInstance");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id);
            request.AddBody(update);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.PUT;
            return Client.ExecuteTaskAsync<ClassInstanceUpdateResponse>(request);
        }

        public Task<IRestResponse<ClassInstanceCancellationResponse>> Delete(string id, bool confirmCancel, bool refundCharges, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("classInstance");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id);
            request.AddQueryParameter("confirmCancel", confirmCancel.ToString());
            request.AddQueryParameter("refundCharges", refundCharges.ToString());
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.DELETE;
            return Client.ExecuteTaskAsync<ClassInstanceCancellationResponse>(request);
        }
    }
}
