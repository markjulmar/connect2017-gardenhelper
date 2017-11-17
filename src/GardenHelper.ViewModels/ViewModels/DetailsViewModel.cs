using GardenHelper.Interfaces;
using GardenHelper.Models;
using System.IO;
using System.Threading.Tasks;
using XamarinUniversity.Infrastructure;
using XamarinUniversity.Interfaces;
using XamarinUniversity.Services;

namespace GardenHelper.ViewModels
{
    public class DetailsViewModel : SimpleViewModel
    {
        Plant plant;
        Stream imageStream;
        private double _price;

        public string Name => plant.Name;
        public double Price { get => _price; private set => SetPropertyValue(ref _price, value); }
        public string Description => plant.Description;
        public Stream ImageStream => imageStream;

        public IAsyncDelegateCommand ShowWikkiPage { get; private set; }

        public bool HasLicense => !string.IsNullOrEmpty(plant.License);
        public string AttributionText => plant.License;
        public string AttributionUrl => plant.LicenseUrl;

        public DetailsViewModel(Plant plant, Stream imageStream)
        {
            this.plant = plant;
            this.imageStream = imageStream;

            ShowWikkiPage = new AsyncDelegateCommand(OnShowWikkiPage, () => !string.IsNullOrEmpty(Name));
            LoadPricesAsync();
        }

        async void LoadPricesAsync()
        {
            Price = await XamUInfrastructure.ServiceLocator
                .Get<IPricingDetails>().GetPriceFromId(plant.Id);
        }

        async Task OnShowWikkiPage()
        {
            await XamUInfrastructure.ServiceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Wikki, Name);
        }
    }
}

