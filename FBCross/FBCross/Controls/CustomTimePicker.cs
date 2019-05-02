using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace FBCross.Controls
{
    public class CustomTimePicker : SfPicker

    {

        // Time api is used to modify the Hour collection as per change in Time

        /// <summary>

        /// Time is the actual DataSource for SfPicker control which will holds the collection of Hour ,Minute and Format

        /// </summary>

        public ObservableCollection<object> Time { get; set; }

        //Minute is the collection of minute numbers

        public ObservableCollection<object> Minute;

        //Hour is the collection of hour numbers

        public ObservableCollection<object> Hour;

        //Format is the collection of AM and PM

        public ObservableCollection<object> Format;

        /// <summary>

        /// Header api is holds the column name for every column in time picker

        /// </summary>

        public ObservableCollection<string> Headers { get; set; }

        public CustomTimePicker()

        {

            Time = new ObservableCollection<object>();

            Hour = new ObservableCollection<object>();

            Minute = new ObservableCollection<object>();

            Format = new ObservableCollection<object>();

            PopulateTimeCollection();

            this.ItemsSource = Time;
            Headers = new ObservableCollection<string>();

            if (Device.RuntimePlatform == Device.Android)

            {

                Headers.Add("HOUR");

                Headers.Add("MINUTE");

                Headers.Add("FORMAT");

            }

            else

            {

                Headers.Add("Hour");

                Headers.Add("Minute");

                Headers.Add("Format");

            }

            //SfPicker header text

            HeaderText = "TIME PICKER";



            // Column header text collection

            this.ColumnHeaderText = Headers;

            //Enable Footer of SfPicker

            ShowFooter = false;

            //Enable Header of SfPicker

            ShowHeader = false;

            //Enable Column Header of SfPicker

            ShowColumnHeader = true;
        }

       

        private void PopulateTimeCollection()

        {

            //Populate Hour

            for (int i = 1; i <= 12; i++)

            {

                Hour.Add(i.ToString());

            }

            //Populate Minute

            for (int j = 0; j < 60; j++)

            {

                if (j < 10)

                {

                    Minute.Add("0" + j);

                }

                else

                    Minute.Add(j.ToString());

            }


            //Populate Format

            Format.Add("AM");

            Format.Add("PM");

            Time.Add(Hour);

            Time.Add(Minute);

            Time.Add(Format);

        }

    }
}
