using GardenHelper.Interfaces;
using GardenHelper.Services.Azure;
using XamarinUniversity.Interfaces;

namespace GardenHelper.ViewModels
{
    public static class Services
    {
        public static void Register(IDependencyService serviceLocator)
        {
            //serviceLocator.Register<IIdentifyPicture, GardenHelper.Services.Mocks.MockIdentifyPicture>();
            //serviceLocator.Register<IPlantDetails, GardenHelper.Services.Mocks.MockPlantDetails>();
            //serviceLocator.Register<IPricingDetails, GardenHelper.Services.Mocks.MockPricingDetails>();

            // Register our services
            serviceLocator.Register<IIdentifyPicture, AzureVisionService>();
            serviceLocator.Register<IPlantDetails, BingEntitySearch>();
            serviceLocator.Register<IPricingDetails, AzurePricingService>();
        }
    }
}
