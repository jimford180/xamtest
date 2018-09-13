using FBCross.Rest;
using FBCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaListView : ContentPage
    {
        public ObservableCollection<AgendaItemGroup> Items { get; set; }

        public AgendaListView()
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(AgendaListView)).Assembly,
            "FBCross.Styles.global.css"));
            IsBusy = true;
            Items = new ObservableCollection<AgendaItemGroup>();
            //var dates = new List<string>();
            //for (int i = 0; i < 5; i++)
            //{
            //    dates.Add(DateTime.Now.Date.AddDays(i).ToString("D"));
            //}
            //Items = new ObservableCollection<AgendaItemGroup>(dates.Select(d => new AgendaItemGroup(d, new List<AgendaItem>
            //{
            //    new AgendaItem { Employee = "James Ford", StartTime="6:00 am", EndTime="6:30 am", Title="Expert Tax Prep", Url="taxprep"},
            //     new AgendaItem { Employee = "Andrew Ford", StartTime="7:00 am", EndTime="7:30 am", Title="Tax Prep", Url="taxprep2"},
            //})));
			
			GroupedAgendaView.ItemsSource = Items;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            var sessionInfo = await App.GetSessionTokenAndMerchantGuid();
            var start = DateTime.Now.Date;
            var end = DateTime.Now.Date.AddDays(8);
            if (App.SelectedDate != null && App.SelectedDate != DateTime.MinValue)
            {
                start = App.SelectedDate;
                end = start.AddDays(1);
            }
            var calendarFeedRequest = new CalendarFeed().Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken, start, end);
            var response = await calendarFeedRequest;
            if (response.IsSuccessful && response.Data != null)
            {
                Items.Clear();
                foreach (var group in AgendaItemGroup.FromCalendarFeedResponse(response.Data))
                {
                    Items.Add(group);
                }
            }
            IsBusy = false;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null || !(e.Item is AgendaItem))
                return;

            var agendaItem = (AgendaItem)e.Item;
            if (agendaItem.Url.StartsWith("#instance/"))
            {
                App.SelectedInstance = agendaItem.Url.Replace("#instance/", string.Empty);
                
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
