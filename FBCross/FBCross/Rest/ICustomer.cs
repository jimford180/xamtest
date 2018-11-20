using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface ICustomer
    {
        Task<IRestResponse<CustomerList>> Get(Guid merchantGuid, string sessionToken, string searchTerm);
    }
}