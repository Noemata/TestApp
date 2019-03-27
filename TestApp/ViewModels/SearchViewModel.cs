using System;
using Windows.UI.Core;

using Caliburn.Micro;
using TestApp.Views;
using TestApp.Services;

namespace TestApp.ViewModels
{
    [RegisterCMAttribute(InstanceMode.PerRequest)]
    public class SearchViewModel : Screen, IViewAware
    {
        private SearchView _view;
        private readonly IDialogWindowManager _dialogWindowManager;
        private readonly IUserNotificationService _userNotificationService;
        private CoreDispatcher _dispatcher;

        public SearchViewModel(IDialogWindowManager dialogWindowManager, IUserNotificationService userNotificationService)
        {
            _dialogWindowManager = dialogWindowManager;
            _userNotificationService = userNotificationService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            _view = (SearchView)view;
            _dispatcher = _view.Dispatcher;
        }
    }
}
