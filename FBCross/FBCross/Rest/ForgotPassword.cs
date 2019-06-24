using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class ForgotPassword : RestBase, IForgotPassword
    {
        public Task<IRestResponse<ForgottenPasswordResponse>> Post(ForgotPasswordRequest forgotPasswordRequest)
        {
            var request = new RestRequest("forgotPassword");
            request.AddJsonBody(forgotPasswordRequest);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<ForgottenPasswordResponse>(request);
        }
    }
}
