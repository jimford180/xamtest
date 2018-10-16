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
    }
}
