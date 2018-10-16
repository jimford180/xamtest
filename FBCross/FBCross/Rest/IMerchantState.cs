using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IMerchantState
    {
        Task<IRestResponse<Dto.MerchantState>> Get(Guid merchantGuid, string sessionToken);
    }
}