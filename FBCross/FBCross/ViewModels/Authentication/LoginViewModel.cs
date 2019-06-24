﻿using FBCross.Rest;
using FBCross.ViewModels.Navigation;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FBCross.ViewModels.Authentication
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel(IMvxNavigationService navigationService, ISessionAuth sessionAuth)
        {
            _navigationService = navigationService;
            _sessionAuth = sessionAuth;
        }
        private string _email;
        private string _password;
        private readonly IMvxNavigationService _navigationService;
        private readonly ISessionAuth _sessionAuth;

        public string Email { get => _email; set { _email = value; RaisePropertyChanged(() => Email); } }
        public string Password { get => _password; set { _password = value; RaisePropertyChanged(() => Password); } }

        public IMvxCommand PerformLoginCommand => new MvxCommand(Login);
        public IMvxCommand ForgotPasswordCommand => new MvxCommand(ForgotPassword);
        public IMvxCommand SignupCommand => new MvxCommand(Signup);

        private void Signup()
        {
            Device.OpenUri(new Uri("https://www.flexbooker.com/"));
        }

        private void ForgotPassword()
        {
            _navigationService.Navigate<ForgotPasswordViewModel>();
        }

        private async void Login()
        {
            Loading = true;
            var sessionAuthClient = new SessionAuth();
            var loginResult = await sessionAuthClient.Get(Email, Password);
            Loading = false;
            if (loginResult.IsSuccessful && loginResult.Data.Any())
            {
                await FormsApp.Logout();
                foreach (var session in loginResult.Data)
                {
                    session.Email = Email;
                    await FormsApp.Database.Sessions.CreateEntityAsync(session);
                }
                if (loginResult.Data.Count() > 1)
                {
                    await _navigationService.Navigate<MerchantChoiceViewModel>();
                }
                else
                {
                    await _navigationService.Navigate<RootViewModel>();
                }
            }
            else
            {
                MessagingCenter.Send<LoginViewModel>(this, "LoginFailed");
            }
        }
    }

   
}
