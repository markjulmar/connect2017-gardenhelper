using System;
using XamarinUniversity.Infrastructure;

namespace GardenHelper.ViewModels
{
    public class WikkiViewModel : SimpleViewModel
    {
        const string Prefix = "https://en.wikipedia.org/wiki/";

        public string Title { get; set; }
        public Uri Url { get; set; }

        public WikkiViewModel(string title)
        {
            Title = title;
            Url = new Uri(Prefix + title);
        }
    }
}
