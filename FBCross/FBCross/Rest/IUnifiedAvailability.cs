using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IUnifiedAvailability
    {
        Task<IRestResponse<UnifiedAvailabilityResponse>> Get(Guid merchantGuid, string sessionToken, List<int> serviceIds, int? employeeId, DateTime startDate, DateTime endDate);
    }
}