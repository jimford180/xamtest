using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBCross.ViewModels
{
    public class CalendarMonth
    {
        public string MonthName { get; set; }
        public DateTime StartOfMonth { get; set; }
        public List<CalendarDay> Days {get; set; }
        public int SelectedDay {
            get
            {
                if (Days.Any(s => s.IsSelected))
                    return Days.First(s => s.IsSelected).Day;

                return 0;
            }
            set
            {
                if (Days.Any(s => s.Day == value))
                {
                    Days.Where(s => s.Day != value).ToList().ForEach(s => s.IsSelected = false);
                    Days.Single(s => s.Day == value).IsSelected = true;
                }
            }
        }

        public CalendarMonth()
        {
            StartOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            SeedDays();
        }

        private void SeedDays()
        {
            var currentMonthDays = DateTime.DaysInMonth(StartOfMonth.Year, StartOfMonth.Month);
            var c = (int)(StartOfMonth.DayOfWeek);
            MonthName = StartOfMonth.ToString("Y");
            var r = 0;
            Days = new List<CalendarDay>();
            for (int d = 1; d <= currentMonthDays; d++)
            {
                Days.Add(new CalendarDay
                {
                    Day = d,
                    IsSelected = DateTime.Now.Day == d,
                    ColumnNumber = c,
                    RowNumber = r
                });
                c++;
                if (c == 7)
                {
                    r++;
                    c = 0;
                }
            }
        }

        public void Next()
        {
            StartOfMonth = StartOfMonth.AddMonths(1);
            Days.Clear();
            SeedDays();
        }
        public void Previous()
        {
            StartOfMonth = StartOfMonth.AddMonths(-1);
            Days.Clear();
            SeedDays();
        }
    }
    public class CalendarDay
    {
        public int Day { get; set; }
        public bool IsSelected { get; set; }
            public int ColumnNumber { get; set; }
            public int RowNumber { get; set; }

    }
}
