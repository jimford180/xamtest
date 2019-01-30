using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class UnifiedAvailability : RestBase, IUnifiedAvailability
    {
        public Task<IRestResponse<UnifiedAvailabilityResponse>> Get(Guid merchantGuid, string sessionToken, List<int> serviceIds,
            int? employeeId, DateTime startDate, DateTime endDate)
        {
            var request = new RestRequest("unifiedAvailability");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("csvServiceIds", string.Join(",", serviceIds));
            request.AddQueryParameter("employeeId", employeeId.GetValueOrDefault(0).ToString());
            request.AddQueryParameter("startDate", startDate.ToString());
            request.AddQueryParameter("endDate", endDate.ToString());
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<UnifiedAvailabilityResponse>(request);
        }

        
    }
}
