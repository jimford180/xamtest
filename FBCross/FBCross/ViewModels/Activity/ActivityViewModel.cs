using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FBCross.ViewModels.Activity
{
    public class ActivityViewModel : ViewModelBase
    {
        public ActivityViewModel()
        {
            Items = new ObservableCollection<ActivityItem>
            {
                new ActivityItem{ ActionText="New", Description="A new thing was booked"},
                new ActivityItem{ ActionText="Deleted", Description="Another new thing was deleted."}
            };
        }
        private ActivityItem _selectedItem;
        public ActivityItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        private ObservableCollection<ActivityItem> _items { get; set; }
        public ObservableCollection<ActivityItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
    }
}
