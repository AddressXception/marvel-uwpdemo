using System;
using System.Collections;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MarvelDemo.Core.Models;
using MarvelDemo.Core.Services;
using MarvelDemo.Core.ViewModels;
using MarvelDemo.Services;

namespace MarvelDemo.Views
{
    public sealed partial class MainView
    {
        MainViewModel Vm => DataContext as MainViewModel;

        public static int MainAppViewId;

        public MainView()
        {
            InitializeComponent();

            // TODO: IoC
            var hashService = new HashService();
            var dataService = new MarvelDataService(hashService);

            DataContext = new MainViewModel(dataService);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var character = e.Parameter as Character;
            await Vm.Init(character);
        }

        private async void ComicsList_OnItemCSelected(object sender, SelectionChangedEventArgs e)
        {
            //Since we're opening a holographic view, make sure we are running the right device family
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Holographic") return;

            var vm = (sender as GridView)?.DataContext as MainViewModel;
            var character = vm?.Character;

#if UNITY_HOLOGRAPHIC

            PickCharacter.CharacterModel model;
            switch (character?.Name)
            {
                case "Iron Man":
                    model = PickCharacter.CharacterModel.IronMan;
                    break;
                case "Captain America":
                    model = PickCharacter.CharacterModel.Captainamerica;
                    break;
                default:
                    model = PickCharacter.CharacterModel.Thor;
                    break;
            }

            var appViewId = MainAppViewId = ApplicationView.GetForCurrentView().Id;

            var newView = CoreApplication.CreateNewView();
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {

                var frame = new Frame();
                Window.Current.Content = frame;
                frame.Navigate(typeof(Marvel_Unity.MainPage), model);

                var res = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                    ApplicationView.GetForCurrentView().Id,
                    ViewSizePreference.Default, appViewId,
                    ViewSizePreference.Default);

                newView.CoreWindow.Activated += WindowActivated;

                newView.CoreWindow.Activate();
            });
#endif
        }

        private void WindowActivated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.CodeActivated
                || e.WindowActivationState == CoreWindowActivationState.PointerActivated)
            {
              //  AppCallbacks.Instance.SetInitialViewActive();
                // Only need to mark initial activation once so unregister ourself
                CoreWindow coreWindowSender = sender as CoreWindow;
                coreWindowSender.Activated -= WindowActivated;
            }
        }
    }
}
