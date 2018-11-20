using FBCross.ViewModels.Appointment;
using FBCross.ViewModels.Month;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
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
    public partial class SelectAppointmentDateTime : MvxContentPage<ChooseDateTimeViewModel>
    {
		public SelectAppointmentDateTime()
		{
			InitializeComponent ();
		}
	}
}