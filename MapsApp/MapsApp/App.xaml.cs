using Xamarin.Forms;
using XamarinForms.ViewModels;
using XamarinForms.Views;

namespace MapsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ContentPage
            {
                Content = new ActivityIndicator()
                {
                    IsRunning = true,
                    IsEnabled = true
                }
            };
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            var foursquareViewModel = new FoursquareViewModel();

            await foursquareViewModel.InitDataAsync();

            MainPage = new TabbedPage
            {
                Children =
                {
                    new Views.MainPage(foursquareViewModel),
                    new FoursquareViewPage(foursquareViewModel)
                }
            };
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
