using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;

using Caliburn.Micro;
using TestApp.Views;
using TestApp.Messages;
using TestApp.Services;

namespace TestApp.ViewModels
{
    [RegisterCMAttribute(InstanceMode.PerRequest)]
    public class ShellViewModel : Screen, IViewAware, IHandle<ResumeStateMessage>, IHandle<SuspendStateMessage>
    {
        private ShellView _view;
        private readonly WinRTContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private INavigationService _navigationService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IDialogWindowManager _dialogWindowManager;
        private bool _resume;
        private CoreDispatcher _dispatcher;

        public ShellViewModel(WinRTContainer container, IDialogWindowManager dialogWindowManager, IUserNotificationService userNotificationService, IEventAggregator eventAggregator)
        {
            _container = container;
            _dialogWindowManager = dialogWindowManager;
            _userNotificationService = userNotificationService;
            _eventAggregator = eventAggregator;
        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            _view = (ShellView)view;
            _dispatcher = _view.Dispatcher;
        }

        public void SetupNavigationService(Frame frame)
        {
            if (_container.HasHandler(typeof(INavigationService), null))
                _container.UnregisterHandler(typeof(INavigationService), null);

            _navigationService = _container.RegisterNavigationService(frame);

            if (_resume)
                _navigationService.ResumeState();

            _navigationService.For<HomeViewModel>().Navigate();
        }
                                                             
        private void ItemSelected(Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args != null)
            {
                string option = args.InvokedItem as string;

                if (option != null)
                {
                    switch (option)
                    {
                        case "About":
                            _navigationService.For<AboutViewModel>().Navigate();
                            break;
                        case "Home":
                            _navigationService.For<HomeViewModel>().Navigate();
                            break;
                        case "Search":
                            _navigationService.For<SearchViewModel>().Navigate();
                            break;
                        default:
                            break;
                    }
                }
                else if (args.IsSettingsInvoked)
                    _navigationService.For<SetupViewModel>().Navigate();
            }
        }

        public void Handle(SuspendStateMessage message)
        {
            _navigationService.SuspendState();
        }

        public void Handle(ResumeStateMessage message)
        {
            _resume = true;
        }

        public void ShowFullScreen()
        {
            ApplicationView appView = ApplicationView.GetForCurrentView();

            bool isInFullScreenMode = appView.IsFullScreenMode;

            if (isInFullScreenMode)
                appView.ExitFullScreenMode();
            else
                appView.TryEnterFullScreenMode();
        }
    }
}
