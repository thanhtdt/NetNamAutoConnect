﻿using Microsoft.Toolkit.Uwp.Notifications;
using NetNamAutoConnect.ViewModels;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NetNamAutoConnect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainPageViewModel();
        }

        public MainPageViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.Focus(FocusState.Programmatic);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var content = new ToastContent()
            {                       
                Scenario = ToastScenario.Default,
                Visual = new ToastVisual()
                {                                       
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "NetNamAutoConnect"
                            },
                            new AdaptiveText()
                            {
                                Text = "NetNamAutoConnect sent login request."
                            },
                        }
                    }
                }
            };
            var xml = content.GetXml();
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(xml));
        }
    }
}