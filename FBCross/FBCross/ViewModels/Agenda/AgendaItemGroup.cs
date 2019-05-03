using FBCross.Rest.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FBCross.ViewModels.Agenda
{
    public class AgendaItemGroup : ObservableCollection<AgendaItem>
    {
        public string Date { get; set; }
        public AgendaItemGroup(string date) : base()
        {
            Date = date;
        }
        public AgendaItemGroup(string date, IEnumerable<AgendaItem> source) : base(source)
        {
            Date = date;
        }

        internal static ObservableCollection<AgendaItemGroup> FromCalendarFeedResponse(IEnumerable<CalendarEvent> data, DateTime startDate, DateTime endDate)
        {
            var set = new ObservableCollection<AgendaItemGroup>();
            while (startDate <= endDate)
            {
                var group = new AgendaItemGroup(startDate.ToString("D"), data.Where(d => d.start.Date == startDate).Select(e => new AgendaItem
                {
                    Employee = e.title,
                    StartTime = e.start.ToString("h:mm tt"),
                    EndTime = e.end.ToString("h:mm tt"),
                    Title = e.title,
                    Url = e.url
                }));
                if (!group.Items.Any())
                {
                    group.Items.Add(new AgendaItem
                    {
                        Title = "No events on this day"
                    });
                }
                set.Add(group);
                startDate = startDate.AddDays(1);
            }
            return set;
        }
    }
}
