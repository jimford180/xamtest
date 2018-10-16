using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class MerchantState : RestBase, IMerchantState
    {
        public Task<IRestResponse<Dto.MerchantState>> Get(Guid merchantGuid, string sessionToken)
        {
            var request = new RestRequest("merchantState");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<Dto.MerchantState>(request);
        }
    }
}
