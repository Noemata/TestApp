using Windows.UI.Xaml.Controls;

namespace TestSimplified
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        public void InvokeCall(object sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args != null)
            {
                string option = args.InvokedItem as string;

                if (option != null)
                {
                    switch (option)
                    {
                        case "About":
                            Result.Text = "About";
                            break;
                        case "Home":
                            Result.Text = "Home";
                            break;
                        case "Search":
                            Result.Text = "Search";
                            break;
                        default:
                            break;
                    }
                }
                else if (args.IsSettingsInvoked)
                    Result.Text = "Setup";
            }
        }
    }
}
