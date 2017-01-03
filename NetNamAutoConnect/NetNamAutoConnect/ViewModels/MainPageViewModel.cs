﻿using NetNamAutoConnect.Services;
using NetNamAutoConnect.ViewModels.Base;
using NetNamServices;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace NetNamAutoConnect.ViewModels
{
    // TODO: Pop toast
    // TODO: show remind use time
    // TODO: Show password
    public class MainPageViewModel : BindableBase
    {
        private bool _isLogingin;
        private bool _isLogingOut;
        private bool _isStarted;
        private bool _isStarting;
        private string _password;
        private string _userName;


        public MainPageViewModel()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            OnInitializing();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public bool IsLogingIn
        {
            get { return _isLogingin; }
            set { Set(ref _isLogingin, value); }
        }

        public bool IsLogingOut
        {
            get { return _isLogingOut; }
            set { Set(ref _isLogingOut, value); }
        }

        public bool IsStarted
        {
            get { return _isStarted; }
            set { Set(ref _isStarted, value); }
        }

        public bool IsStarting
        {
            get { return _isStarting; }
            set { Set(ref _isStarting, value); }
        }

        public DelegateCommand LoginCommand => new DelegateCommand(async () =>
        {
            await Task.Yield();
            await InnerLogin();
        }, () => !IsLogingIn);

        public DelegateCommand LogoutCommand => new DelegateCommand(async () =>
        {
            await Task.Yield();
            await InnerLogout();
        }, () => !IsLogingOut);

        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        public DelegateCommand TurnOnCommand => new DelegateCommand(async () =>
        {
            await Task.Yield();
            try
            {
                IsStarting = true;
                IsStarted = true;
                var oldState = false; /// TODO: Get state 
                await Task.Delay(2000);/// TODO: Remove this line
                if (oldState)
                {
                    //unregister
                }
                else
                {
                    //register
                }
                //isStarted = getstate
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                InnerLogin();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            finally
            {
                IsStarting = false;
            }
        }, () => !IsStarting);

        ///TODO: Using BitFlag to detect busy

        public string UserName
        {
            get { return _userName; }
            set { Set(ref _userName, value); }
        }

        private async Task InnerLoadCredential()
        {
            await Task.Yield();
            PasswordCredential credential = CredentialLocker.Retrieving();
            if (credential != null)
            {
                UserName = credential.UserName;
                Password = credential.Password;
            }
        }

        private async Task InnerLoadState()
        {
            await Task.Yield();
            // TODO: load state of background task
            //IsStarted = true;
        }

        private async Task InnerLogin()
        {
            try
            {
                await Task.Yield();
                IsLogingIn = true;
                await InnerSaveCredential();
                await WifiServices.Login(UserName, Password);
            }
            finally
            {
                IsLogingIn = false;
            }
        }

        private async Task InnerLogout()
        {
            try
            {
                await Task.Yield();
                IsLogingOut = true;
                await WifiServices.Logout();
            }
            finally
            {
                IsLogingOut = false;
            }
        }

        private async Task InnerSaveCredential()
        {
            await Task.Yield();
            CredentialLocker.Save(UserName, Password);
        }

        private async Task OnInitializing()
        {
            await Task.Yield();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            InnerLoadState();
            InnerLoadCredential();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}