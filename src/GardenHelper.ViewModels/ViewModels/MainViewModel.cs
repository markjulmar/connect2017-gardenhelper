using GardenHelper.Interfaces;
using Microsoft.ProjectOxford.Vision;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using XamarinUniversity.Infrastructure;
using XamarinUniversity.Interfaces;
using XamarinUniversity.Services;

namespace GardenHelper.ViewModels
{
    public class MainViewModel : ErrorViewModel
    {
        bool initializedCamera, isBusy;
        public IAsyncDelegateCommand TakePhoto { get; private set; }
        public IAsyncDelegateCommand PickPhoto { get; private set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (SetPropertyValue(ref isBusy, value))
                {
                    TakePhoto?.RaiseCanExecuteChanged();
                    PickPhoto?.RaiseCanExecuteChanged();
                }
            }
        }

        public MainViewModel()
        {
            TakePhoto = new AsyncDelegateCommand(() => OnTakeOrPickPhotoAsync(false), () => !IsBusy);
            PickPhoto = new AsyncDelegateCommand(() => OnTakeOrPickPhotoAsync(true), () => !IsBusy);

#if TEST_CLOUD
            Xamarin.Forms.MessagingCenter.Subscribe<object,string>(this, 
                nameof(BackdoorTests.TakePicture), OnTestWithPicture);
#endif

        }

#if TEST_CLOUD
        // Backdoor function for UITest to avoid the camera.
        private async void OnTestWithPicture(object sender, string pictureType)
        {
            string url = null;
            pictureType = pictureType?.ToLower() ?? "";
            switch (pictureType)
            {
                case "rose":
                    url = "https://i.pinimg.com/736x/fc/95/88/fc95887d0b1ab9f8d12fc468d1ff861e--rose-jewelry-rose-tattoos.jpg";
                    break;
                case "tulip":
                    url = "http://www.woodenshoe.com/media/attila-graffiti-tulip.jpg";
                    break;
                case "orchid":
                    url = "http://www.aos.org/AOS/media/Content-Images/Orchids/orchid-care-phal.jpg";
                    break;
                default:
                    Error = "No picture found.";
                    break;
            }

            if (url != null)
            {
                try
                {
                    Func<Stream> getImage = () => new System.Net.Http.HttpClient().GetStreamAsync(url).Result;
                    await OnProcessPhotoStream(getImage);
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
#endif

        public async Task OnTakeOrPickPhotoAsync(bool existingPhoto)
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                var mediaFile = (existingPhoto)
                    ? await PickPhotoAsync()
                    : await TakePhotoAsync();

                if (mediaFile != null)
                {
                    await OnProcessPhotoStream(mediaFile.GetStream);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OnProcessPhotoStream(Func<Stream> mediaFile)
        {
            IDependencyService locator = XamUInfrastructure.ServiceLocator;

            string id = await locator.Get<IIdentifyPicture>().IdentifyAsync(mediaFile);
            if (string.IsNullOrEmpty(id))
            {
                Error = "Sorry, I was unable to identify that plant. Please try again.";
            }

            var plant = await locator.Get<IPlantDetails>().GetPlantFromIdAsync(id);
            if (plant != null)
            {
                await locator.Get<INavigationService>().NavigateAsync(AppPages.Details, new DetailsViewModel(plant, mediaFile.Invoke()));
            }
            else
            {
                Error = $"Sorry, I could not find any information on {id}.";
            }
        }

        async Task<MediaFile> PickPhotoAsync()
        {
            if (!initializedCamera)
            {
                await CrossMedia.Current.Initialize();
                initializedCamera = true;
            }

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                return await CrossMedia.Current.PickPhotoAsync(
                    new PickMediaOptions { PhotoSize = PhotoSize.Small });
            }

            return null;
        }

        async Task<MediaFile> TakePhotoAsync()
        {
            if (!initializedCamera)
            {
                await CrossMedia.Current.Initialize();
                initializedCamera = true;
            }

            if (CrossMedia.Current.IsCameraAvailable
                && CrossMedia.Current.IsTakePhotoSupported)
            {
                return await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions {
                        AllowCropping = true,
                        Name = $"{DateTime.UtcNow.Ticks}.jpg",
                        Directory = "GardenPhotos",
                        PhotoSize = PhotoSize.Small,
                    });
            }

            return null;
        }
    }
}
