using System.Collections.Generic;
using System.Threading.Tasks;
using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IForgotPassword
    {
        Task<IRestResponse<ForgottenPasswordResponse>> Post(ForgotPasswordRequest request);
    }
}