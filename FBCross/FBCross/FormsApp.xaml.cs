using FBCross.Data;
using FBCross.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FBCross.Pages;
using FBCross.Rest.Dto;
using FBCross.ViewModels.Shared;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace FBCross
{
	public partial class FormsApp : Application
	{
        static FBDatabase database;
        private static Guid _merchantGuid;
        private static string _sessionToken;
        private static string _email;

        public static FBDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FBDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FBDatabase.db3"));
                }
                return database;
            }
        }
        public async static Task Logout()
        {
            await Database.Sessions.RemoveAll();
            _merchantGuid = Guid.Empty;
            _sessionToken = null;
            _email = null;
        }
        public static DateTime SelectedDate { get; set; }
        public static string CurrentInstanceId { get; set; }
        public static string CurrentScheduleBookingId { get; set; }
        public static string CurrentBlockId { get; set; }
        public static BookingDetail CurrentFixedTimeBooking { get; set; }
        public static EmployeeViewModel CurrentAgendaEmployee { get; internal set; }
        public static MerchantFieldRules MerchantFieldRules { get; internal set; }

        public FormsApp ()
		{
			//InitializeComponent();
            //MainPage = new RootPage();
		}

        public async static Task<SessionInformation> GetSessionTokenAndMerchantGuid()
        {
            var allTokens = await Database.Sessions.GetEntitiesAsync();
            if (_merchantGuid == null || _sessionToken == null || _email == null)
            {
                if (allTokens.Any() && (allTokens.Any(t => t.IsCurrent) || allTokens.Count == 1))
                {
                    _merchantGuid = allTokens.OrderByDescending(t => t.IsCurrent).First().MerchantGuid;
                    _sessionToken = allTokens.OrderByDescending(t => t.IsCurrent).First().SessionToken;
                    _email = allTokens.OrderByDescending(t => t.IsCurrent).First().Email;
                }
            }
            var info = new SessionInformation { MerchantGuid = _merchantGuid, SessionToken = _sessionToken, Email = _email };
            return info;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
