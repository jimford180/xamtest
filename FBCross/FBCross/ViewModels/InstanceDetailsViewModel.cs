using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
namespace FBCross.ViewModels
{
    public class InstanceDetailsViewModel : ViewModelBase
    {
        public string EmployeeName { get; set; }
        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set
            {
                _serviceName = value;
                TriggerChange("ServiceName");
            }
        }
        public string DateTime { get; set; }
        public string SpotsTaken { get; set; }
        public int TotalSpots { get; set; }
        public string SecondEmployeeName { get; set; }
        
        public string InstanceDetails { get; set; }

        public async void Load(string instanceId)
        {
            IsBusy = true;
            var tokens = await App.GetSessionTokenAndMerchantGuid();
            var response = await new Rest.InstanceDetails().Get(tokens.MerchantGuid, tokens.SessionToken, instanceId);
            IsBusy = false;
            if (response.IsSuccessful && response.Data != null)
            {
                EmployeeName = response.Data.EmployeeName;
                ServiceName = response.Data.ServiceId.ToString();
                DateTime = response.Data.Date;
                SpotsTaken = response.Data.Bookings.Sum(b => b.NumberOfSlots).ToString();
                TotalSpots = response.Data.TotalSlots;
                SecondEmployeeName = response.Data.SecondEmployeeId.ToString();
            }
        }
    }

}
