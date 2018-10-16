using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IInstanceDetails
    {
        Task<IRestResponse<ClassInstanceDetail>> Get(Guid merchantGuid, string sessionToken, string id);
    }
}