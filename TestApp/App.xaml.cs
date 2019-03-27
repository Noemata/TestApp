using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;

using Caliburn.Micro;
using TestApp.Messages;
using TestApp.Services;
using TestApp.ViewModels;

namespace TestApp
{
    /// <summary>
    /// Class App. This class cannot be inherited.
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// The container
        /// </summary>
        private WinRTContainer _container;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container.Singleton<IDialogWindowManager, DialogWindowManager>();
            _container.Singleton<IUserNotificationService, UserNotificationService>();

            this.RegistInstances(this._container);

            _eventAggregator = _container.GetInstance<IEventAggregator>();
        }

        private void RegistInstances(SimpleContainer _container)
        {
            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes
                .Select(t => new { T = t, Mode = t.GetCustomAttribute<RegisterCMAttributeAttribute>()?.Mode })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types)
            {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton)
                {
                    _container.RegisterSingleton(type, null, type);
                }
                else if (t.Mode == InstanceMode.PerRequest)
                {
                    _container.RegisterPerRequest(type, null, type);
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="E:Launched" /> event.
        /// </summary>
        /// <param name="args">The <see cref="LaunchActivatedEventArgs"/> instance containing the event data.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Note we're using DisplayRootViewFor (which is view model first)
            // this means we're not creating a root frame and just directly
            // inserting ShellView as the Window.Content

            DisplayRootViewFor<ShellViewModel>();

            // It's kinda of weird having to use the event aggregator to pass 
            // a value to ShellViewModel, could be an argument for allowing
            // parameters or launch arguments

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                _eventAggregator.PublishOnUIThread(new ResumeStateMessage());
            }
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            _eventAggregator.PublishOnUIThread(new SuspendStateMessage(e.SuspendingOperation));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
