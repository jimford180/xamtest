using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest
{
    public class RestBase
    {
        protected RestClient Client;
        public RestBase()
        {
            Client = new RestClient(RestConstants.ApiRoot);
        }
    }
}
