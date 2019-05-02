using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface ICalendarFeed
    {
        Task<IRestResponse<IEnumerable<CalendarEvent>>> Get(Guid merchantGuid, string sessionToken, DateTime start, DateTime end, int? employeeId);
    }
}