using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class MerchantState
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool DisplaySmsOption { get; set; }
        public bool DisplayEmailOption { get; set; }
        public bool DisplayNumberOfAttendees { get; set; }
        public bool ShowPendingBookingsButton { get; set; }
        public bool HasBookingForms { get; set; }
        public IEnumerable<MasterClass> Events { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public List<AppointmentType> Services { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
        public MerchantFieldRules FieldRules { get; set; }
        public bool DisplayTimeZoneLabel { get; set; }
        public List<Location> Locations { get; set; }
        public List<ServiceCategory> ServiceCategories { get; set; }
        public List<Language> Languages { get; set; }
        public List<Package> Packages { get; set; }
        public List<CustomStatus> CustomStatuses { get; set; }
    }
}
