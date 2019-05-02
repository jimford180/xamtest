using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public class Holiday : RestBase, IHoliday
    {
        public Task<IRestResponse<DeleteMerchantHolidayResponse>> Delete(int id, Guid merchantGuid, string sessionToken)
        {
            var request = new RestRequest("holiday");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("id", id.ToString());
            request.AddQueryParameter("deleteEntireSeries", "false");
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.DELETE;
            return Client.ExecuteTaskAsync<DeleteMerchantHolidayResponse>(request);
        }

        public Task<IRestResponse<Dto.Holiday>> Get(int id, Guid merchantGuid, string sessionToken)
        {
            var request = new RestRequest("holiday");
            request.AddQueryParameter("id", id.ToString());
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.GET;
            return Client.ExecuteTaskAsync<Dto.Holiday>(request);
        }

        public Task<IRestResponse<CreateMerchantHolidayResponse>> Post(Dto.Holiday holiday, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("holiday");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddJsonBody(holiday);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.POST;
            return Client.ExecuteTaskAsync<CreateMerchantHolidayResponse>(request);
        }

        public Task<IRestResponse<UpdateMerchantHolidayResponse>> Put(Dto.Holiday holiday, string sessionToken, Guid merchantGuid)
        {
            var request = new RestRequest("holiday");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("updateEntireSeries", "false");
            request.AddJsonBody(holiday);
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.PUT;
            return Client.ExecuteTaskAsync<UpdateMerchantHolidayResponse>(request);
        }
    }
}
