using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class Customer : RestBase, ICustomer
    {
        public Task<IRestResponse<CustomerList>> Get(Guid merchantGuid, string sessionToken, string searchTerm)
        {
            var request = new RestRequest("customer");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("searchTerm", searchTerm);
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<Dto.CustomerList>(request);
        }
    }
}
