using System.Collections.Generic;
using System.Threading.Tasks;
using FBCross.Data;
using RestSharp;

namespace FBCross.Rest
{
    public interface ISessionAuth
    {
        Task<IRestResponse<IEnumerable<SessionMerchant>>> Get(string email, string password);
        Task<IRestResponse<IEnumerable<SessionMerchant>>> Get(string sessionToken);
    }
}