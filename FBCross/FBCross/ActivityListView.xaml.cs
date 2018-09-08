using FBCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FBCross
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityListView : ContentPage
    {
        public ObservableCollection<ActivityItemViewModel> Items { get; set; }

        public ActivityListView()
        {
            InitializeComponent();

            Items = new ObservableCollection<ActivityItemViewModel>
            {
                new ActivityItemViewModel{ ActionText = "Created Appointment", Description = "Booking created for James Ford for 9/1/2018 at 9:00am for Tax Prep."},
                new ActivityItemViewModel{ ActionText = "Cancelled Appointment", Description = "Booking cancelled for James Ford for 9/1/2018 at 9:00am for Tax Prep."},
                new ActivityItemViewModel{ ActionText = "Updated Appointment", Description = "Booking updated for James Ford for 9/1/2018 at 9:00am for Tax Prep."},
                new ActivityItemViewModel{ ActionText = "Created Appointment", Description = "Booking created for James Ford for 9/1/2018 at 9:00am for Tax Prep."}
            };
			
			MyListView.ItemsSource = Items;
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
