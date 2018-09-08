using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }
}
