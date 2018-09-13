using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FBCross.Rest.Dto;

namespace FBCross.ViewModels
{
    public class AgendaItemGroup : ObservableCollection<AgendaItem>
    {
        public string Date { get; set; }
        public AgendaItemGroup(string date): base()
        {
            Date = date;
        }
        public AgendaItemGroup(string date, IEnumerable<AgendaItem> source) : base(source)
        {
            Date = date;
        }

        internal static ObservableCollection<AgendaItemGroup> FromCalendarFeedResponse(IEnumerable<CalendarEvent> data)
        {
            return new ObservableCollection<AgendaItemGroup>(data.GroupBy(g => g.start.Date).Select(g => new AgendaItemGroup(g.Key.ToString("D"), g.Select(e => new AgendaItem {
                Employee = e.title,
                StartTime = e.start.ToString("h:mm tt"),
                EndTime = e.end.ToString("h:mm tt"),
                Title = e.title,
                Url = e.url
            }))));
        }
    }
}
