using FBCross.ViewModels.Month;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab, Title ="Calendar", Icon = "imgCalendar.png")]
    public partial class Month : MvxContentPage<MonthViewModel>
    {
		public Month ()
		{
			InitializeComponent ();
            calendar.MonthViewSettings.DateSelectionColor = Color.FromHex("#0D82DB");
            calendar.MonthViewSettings.SelectedDayTextColor = Color.White;
            calendar.MonthViewSettings.TodayTextColor = Color.FromHex("#0D82DB");
		}

        private void CellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            if (e.Date == DateTime.Now.Date)
            {
                e.BackgroundColor = Color.Transparent;
            }
        }
    }
}