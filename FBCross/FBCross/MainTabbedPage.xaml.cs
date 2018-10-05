using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FBCross
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage
    {
        public MainTabbedPage ()
        {
            InitializeComponent();
            this.Children.Add(new MonthView() { Title = "Month", Icon = Device.RuntimePlatform == Device.iOS ? "imgCalendar.png" : null });
            this.Children.Add(new AgendaListView() { Title = "Agenda", Icon = Device.RuntimePlatform == Device.iOS ? "imgToday.png" : null });
            this.Children.Add(new ActivityListView() { Title = "Activity", Icon = Device.RuntimePlatform == Device.iOS ? "imgActivity.png" : null });
            this.Title = "Calendar";
            this.CurrentPage = this.Children[1];
        }

        internal void GoToDayView()
        {
            CurrentPage = Children[1];
        }
    }
}