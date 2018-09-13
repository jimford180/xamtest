using FBCross.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class SessionAuth : RestBase
    {
        public Task<IRestResponse<IEnumerable<SessionMerchant>>> Get(string email, string password)
        {
            var request = new RestRequest("sessionAuth");
            request.AddQueryParameter("email", email);
            request.AddQueryParameter("password", password);
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<IEnumerable<SessionMerchant>>(request);
        }
    }
}
