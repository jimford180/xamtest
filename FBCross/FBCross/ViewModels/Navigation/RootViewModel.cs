using FBCross.Rest;
using FBCross.ViewModels.Agenda;
using FBCross.ViewModels.Authentication;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Xamarin.Forms;
using FBCross.Dependency;

namespace FBCross.ViewModels.Navigation
{
    public class RootViewModel : ViewModelBase
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMerchantState _merchantStateService;
        private readonly ISessionAuth _sessionAuthService;
        public RootViewModel(IMvxNavigationService navigationService, IMerchantState merchantStateService, ISessionAuth sessionAuthService)
        {
            _navigationService = navigationService;
            _merchantStateService = merchantStateService;
            _sessionAuthService = sessionAuthService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            RedirectIfNecessary();
            MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async void RedirectIfNecessary()
        {
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            if (sessionInfo == null || string.IsNullOrEmpty(sessionInfo.SessionToken) || sessionInfo.MerchantGuid == null || sessionInfo.MerchantGuid == Guid.Empty)
            {
                await _navigationService.Navigate<LoginViewModel>();
            }
            else
            {
                var verifyResponse = await _sessionAuthService.Get(sessionInfo.SessionToken);
                if (verifyResponse.Data == null || !verifyResponse.Data.Any(m => m.MerchantGuid == sessionInfo.MerchantGuid))
                {
                    await _navigationService.Navigate<LoginViewModel>();
                }
            }
        }

        private async Task InitializeViewModels()
        {
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            if (!string.IsNullOrWhiteSpace(sessionInfo.Email))
            {
                DependencyService.Get<IIntercom>().RegisterLoggedInUser(sessionInfo.Email);
                await _navigationService.Navigate<MenuViewModel>();
                await _navigationService.Navigate<TabbedHomeViewModel>();
            }
        }

        public override async void Start()
        {
            base.Start();
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            var response = await _merchantStateService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken);
            if (response.IsSuccessful)
            {
                var employees = response.Data.Employees.Select(e => Mapper.Map<Data.Employee>(e));
                var services = response.Data.Services.Select(s => Mapper.Map<Data.Service>(s));
                var masterClasses = response.Data.Events.Select(e => Mapper.Map<Data.MasterClass>(e));
                var masterSchedules = response.Data.Schedules.Select(s => Mapper.Map<Data.MasterSchedule>(s));
                FormsApp.MerchantFieldRules = response.Data.FieldRules;
                await FormsApp.Database.Employees.RemoveAll();
                await FormsApp.Database.Employees.CreateManyAsync(employees);
                await FormsApp.Database.Services.RemoveAll();
                await FormsApp.Database.Services.CreateManyAsync(services);
                await FormsApp.Database.MasterClasses.RemoveAll();
                await FormsApp.Database.MasterClasses.CreateManyAsync(masterClasses);
                await FormsApp.Database.MasterSchedules.RemoveAll();
                await FormsApp.Database.MasterSchedules.CreateManyAsync(masterSchedules);
            }

            Loading = false;
        }
    }
}
