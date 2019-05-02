using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class Holiday : ResponseBase
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StringStartDate { get; set; }
        public string StringEndDate { get; set; }
        public string Title { get; set; }
        public bool VisibleToCustomers { get; set; }
        public bool InformationalOnly { get; set; }
        public bool IsSeries { get; set; }
        public int NumberOfOthersInSeries { get; set; }
        public List<OtherHolidaySeriesDates> OtherSeriesDates { get; set; }

        public int PeriodStep { get; set; }
        public string PeriodType { get; set; }
        public int Recurrences { get; set; }
    }

    public class OtherHolidaySeriesDates
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StringStartDate { get; set; }
        public string StringEndDate { get; set; }
    }

    public class Holidays
    {
        public List<Holiday> HolidayList { get; set; }
    }

    public class HolidayDisplay
    {
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
    }
    public class UpdateMerchantHolidayResponse : ResponseBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public List<int> IdsToCancel { get; set; }
    }

    public class CreateMerchantHolidayResponse : ResponseBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public List<int> IdsToCancel { get; set; }

    }

    public class DeleteMerchantHolidayResponse : ResponseBase
    {

    }
}
