using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class ActivityFeed : RestBase, IActivityFeed
    {
        public Task<IRestResponse<IEnumerable<Audit>>> Get(Guid merchantGuid, string sessionToken)
        {
            var request = new RestRequest("recentActivity");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<IEnumerable<Audit>>(request);
        }
    }
}
