using System.Threading.Tasks;
using XamarinUniversity.Infrastructure;

namespace GardenHelper.ViewModels
{
    public class ErrorViewModel : SimpleViewModel
    {
        private string _errorText;
        public bool HasError { get { return !string.IsNullOrEmpty(Error); } }
        public string Error
        {
            get => _errorText;
            set
            {
                if (SetPropertyValue(ref _errorText, value))
                {
                    RaisePropertyChanged(nameof(HasError));
                    Task.Delay(8000).ContinueWith(tr => Error = null);
                }
            }
        }
    }
}
