﻿using FBCross.ViewModels;
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
	public partial class MonthView : ContentPage
	{
        CalendarMonth _month;
		public MonthView ()
        {
            InitializeComponent();
            _month = new CalendarMonth();
            SeedCalendarView();
        }

        private void SeedCalendarView()
        {
            InsertHeaderChildren();
            PopulateWithDays();
        }

        private void PopulateWithDays()
        {
            foreach (var day in _month.Days)
            {
                var btn = new Button { Text = day.Day.ToString(), FontSize = 11, CornerRadius = 25 };
                btn.Clicked += SelectDate;
                if (day.IsSelected)
                {
                    btn.BackgroundColor = Color.Green;
                    btn.TextColor = Color.White;
                }
                else
                {
                    btn.BackgroundColor = Color.Transparent;
                    btn.TextColor = Color.Black;
                }
                calendarGrid.Children.Add(btn, day.ColumnNumber, day.RowNumber + 2);
            }
        }

        private void InsertHeaderChildren()
        {
            calendarGrid.Children.Clear();
            var previousMonthButton = new Image
            {
                Source = "imgPrev.png",
                WidthRequest = 10,
                HeightRequest = 10,
                VerticalOptions = LayoutOptions.Center
            };
            var nextMonthButton = new Image { Source = "imgNext.png", WidthRequest = 10, HeightRequest = 10, VerticalOptions = LayoutOptions.Center };

            var previousTapEvent = new TapGestureRecognizer();
            previousTapEvent.Tapped += PreviousMonth;
            
            previousMonthButton.GestureRecognizers.Add(previousTapEvent);
            var nextTapEvent = new TapGestureRecognizer();
            nextTapEvent.Tapped += NextMonth;
            nextMonthButton.GestureRecognizers.Add(nextTapEvent);

            
            
            calendarGrid.Children.Add(previousMonthButton, 0, 0);
            var monthLabel = new Label { Text = _month.MonthName, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            calendarGrid.Children.Add(monthLabel, 1, 0);
            Grid.SetColumnSpan(monthLabel, 5);
            calendarGrid.Children.Add(nextMonthButton, 6, 0);
            calendarGrid.Children.Add(new Label { Text = "S", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 0, 1);
            calendarGrid.Children.Add(new Label { Text = "M", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 1, 1);
            calendarGrid.Children.Add(new Label { Text = "T", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 2, 1);
            calendarGrid.Children.Add(new Label { Text = "W", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 3, 1);
            calendarGrid.Children.Add(new Label { Text = "T", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 4, 1);
            calendarGrid.Children.Add(new Label { Text = "F", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 5, 1);
            calendarGrid.Children.Add(new Label { Text = "S", FontSize = 9, HorizontalOptions = LayoutOptions.Center }, 6, 1);
        }

        private void NextMonth(object sender, EventArgs e)
        {
            _month.Next();
            SeedCalendarView();
        }

        private void PreviousMonth(object sender, EventArgs e)
        {
            _month.Previous();
            SeedCalendarView();
        }

        private void SelectDate(object sender, EventArgs e)
        {
            var button = (sender as Button);
            _month.SelectedDay = Convert.ToInt32(button.Text);
            ReselectDay();
        }

        private void ReselectDay()
        {
            foreach (var child in calendarGrid.Children)
            {
                int day;
                if (child is Button && Int32.TryParse(((Button)child).Text, out day))
                {
                    if (day == _month.SelectedDay)
                    {
                        ((Button)child).BackgroundColor = Color.Green;
                        ((Button)child).TextColor = Color.White;
                    }
                    else
                    {
                        ((Button)child).BackgroundColor = Color.Transparent;
                        ((Button)child).TextColor = Color.Black;
                    }
                }
            }
        }
    }
}