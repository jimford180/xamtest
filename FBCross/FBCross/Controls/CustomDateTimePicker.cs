﻿using Syncfusion.SfPicker.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FBCross.Controls
{
    public class CustomDateTimePicker : SfPicker

    {
        #region Public Properties

        // Months API is used to modify the Day collection as per change in Month

        internal Dictionary<string, string> Months { get; set; }

        /// <summary>

        /// Date is the actual DataSource for SfPicker control which will holds the collection of Day ,Month and Year

        /// </summary>

        /// <value>The date.</value>

        public ObservableCollection<object> Date { get; set; }

        //Day is the collection of day numbers

        internal ObservableCollection<object> Day { get; set; }

        //Month is the collection of Month Names

        internal ObservableCollection<object> Month { get; set; }

        //Year is the collection of Years from 1990 to 2042

        internal ObservableCollection<object> Year { get; set; }

        public ObservableCollection<string> Headers { get; set; }

        //Hour is the collection of Hours in Railway time format
        internal ObservableCollection<object> Hour { get; set; }

        //Minute is the collection of Minutes from 00 to 59

        internal ObservableCollection<object> Minute { get; set; }
        #endregion

        public CustomDateTimePicker()

        {

            Months = new Dictionary<string, string>();

            Date = new ObservableCollection<object>();

            Day = new ObservableCollection<object>();

            Month = new ObservableCollection<object>();

            Year = new ObservableCollection<object>();
            Hour = new ObservableCollection<object>();

            Minute = new ObservableCollection<object>();

            PopulateDateCollection();

            this.ItemsSource = Date;
            this.SelectionChanged += CustomDatePicker_SelectionChanged;

            Headers = new ObservableCollection<string>();

            Headers.Add("Month");

            Headers.Add("Day");

            Headers.Add("Year");

            Headers.Add("Hour");

            Headers.Add("Minute");

            //SfPicker header text

            HeaderText = "Date Picker";



            // Column header text collection

            this.ColumnHeaderText = Headers;

            //Enable Footer

            ShowFooter = false;

            //Enable SfPicker Header

            ShowHeader = false;

            //Enable Column Header of SfPicker

            ShowColumnHeader = true;
        }
        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {

            UpdateDays(Date, e);

        }

        //Update days method is used to alter the Date collection as per selection change in Month column(if Feb is Selected day collection has value from 1 to 28)

        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (Date.Count == 3)
                {
                    bool flag = false;
                    if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == (e.NewValue as ObservableCollection<object>).Count)
                    {
                        if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                        {
                            flag = true;
                        }
                        if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {

                        ObservableCollection<object> days = new ObservableCollection<object>();
                        int month = DateTime.ParseExact(Months[(e.NewValue as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                        int year = int.Parse((e.NewValue as IList)[2].ToString());
                        for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                            {
                                days.Add("0" + j);
                            }
                            else
                                days.Add(j.ToString());
                        }
                        ObservableCollection<object> PreviousValue = new ObservableCollection<object>();

                        foreach (var item in e.NewValue as IList)
                        {
                            PreviousValue.Add(item);
                        }
                        if (days.Count > 0)
                        {
                            Date.RemoveAt(1);
                            Date.Insert(1, days);
                        }

                        if ((Date[1] as IList).Contains(PreviousValue[1]))
                        {
                            this.SelectedItem = PreviousValue;
                        }
                        else
                        {
                            PreviousValue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                            this.SelectedItem = PreviousValue;
                        }
                    }
                }
            });
        }
        private void PopulateDateCollection()

        {

            //populate months

            for (int i = 1; i < 13; i++)

            {

                if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3)))

                    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));

                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));

            }

            //populate year

            for (int i = 1990; i < 2050; i++)

            {

                Year.Add(i.ToString());

            }

            //populate Days

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)

            {

                if (i < 10)

                {

                    Day.Add("0" + i);

                }

                else

                    Day.Add(i.ToString());

            }

            for (int i = 0; i <= 23; i++)

            {

                if (i < 10)

                {

                    Hour.Add("0" + i.ToString());

                }

                else

                    Hour.Add(i.ToString());

            }

            //populate Minutes

            for (int j = 0; j < 60; j++)

            {

                if (j < 10)

                {

                    Minute.Add("0" + j);

                }

                else

                    Minute.Add(j.ToString());

            }

            Date.Add(Month);

            Date.Add(Day);

            Date.Add(Year);

            Date.Add(Hour);

            Date.Add(Minute);

        }


    }
}
