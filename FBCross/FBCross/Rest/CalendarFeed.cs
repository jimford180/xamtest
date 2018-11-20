using FBCross.Data;
using FBCross.Rest.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.Rest
{

    public class CalendarFeed : RestBase, ICalendarFeed
    {
        public Task<IRestResponse<IEnumerable<CalendarEvent>>> Get(Guid merchantGuid, string sessionToken, DateTime start, DateTime end)
        {
            var request = new RestRequest("calendarFeed");
            request.AddQueryParameter("merchantGuid", merchantGuid.ToString());
            request.AddQueryParameter("sessionToken", sessionToken);
            request.AddQueryParameter("start", start.ToShortDateString());
            request.AddQueryParameter("end", end.ToShortDateString());
            request.AddQueryParameter("showUnavailableTimes", "true");
            request.AddQueryParameter("showBlocks", "true");
            request.RequestFormat = DataFormat.Json;
            return Client.ExecuteTaskAsync<IEnumerable<CalendarEvent>>(request);
        }
    }
}
