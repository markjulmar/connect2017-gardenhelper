using GardenHelper.Interfaces;
using GardenHelper.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinUniversity.Interfaces;
using XamarinUniversity.Services;

namespace GardenHelper
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            // Register the default services + service locator.
            var ds = XamUInfrastructure.Init();

            //  Register the required ViewModel services
            Services.Register(ds);

            // Register our pages
            var navService = XamUInfrastructure.ServiceLocator.Get<INavigationPageService>();
            navService.RegisterPage(AppPages.Main, () => new MainPage());
            navService.RegisterPage(AppPages.Details, () => new DetailsPage());
            navService.RegisterPage(AppPages.Wikki, title => new WikkiPage(title?.ToString()));

            // Setup the main page
            MainPage = new NavigationPage(new GardenHelper.MainPage()) { BarBackgroundColor = Color.DarkGreen, BarTextColor = Color.White };
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
