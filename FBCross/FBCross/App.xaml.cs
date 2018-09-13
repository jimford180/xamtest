using FBCross.Data;
using FBCross.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace FBCross
{
	public partial class App : Application
	{
        static FBDatabase database;
        private static Guid _merchantGuid;
        private static string _sessionToken;

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

        public static DateTime SelectedDate { get; internal set; }

        public App ()
		{
			InitializeComponent();
            MainPage = new MainMasterDetail();
		}

        public async static Task<SessionInformation> GetSessionTokenAndMerchantGuid()
        {
            var allTokens = await Database.Sessions.GetEntitiesAsync();
            if (_merchantGuid == null || _sessionToken == null)
            {
                if (allTokens.Any())
                {
                    _merchantGuid = allTokens.First().MerchantGuid;
                    _sessionToken = allTokens.First().SessionToken;
                }
            }
            var info = new SessionInformation { MerchantGuid = _merchantGuid, SessionToken = _sessionToken };
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
