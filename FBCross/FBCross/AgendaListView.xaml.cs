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
            var dates = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                dates.Add(DateTime.Now.Date.AddDays(i).ToString("D"));
            }
            Items = new ObservableCollection<AgendaItemGroup>(dates.Select(d => new AgendaItemGroup(d, new List<AgendaItem>
            {
                new AgendaItem { Employee = "James Ford", StartTime="6:00 am", EndTime="6:30 am", Title="Expert Tax Prep", Url="taxprep"},
                 new AgendaItem { Employee = "Andrew Ford", StartTime="7:00 am", EndTime="7:30 am", Title="Tax Prep", Url="taxprep2"},
            })));
			
			GroupedAgendaView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
