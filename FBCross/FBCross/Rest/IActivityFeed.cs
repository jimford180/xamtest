using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IActivityFeed
    {
        Task<IRestResponse<IEnumerable<Audit>>> Get(Guid merchantGuid, string sessionToken);
    }
}