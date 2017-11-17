using GardenHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GardenHelper
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WikkiPage : ContentPage
    {
        public WikkiPage (string title) : this()
        {
            BindingContext = new WikkiViewModel(title);
        }

        public WikkiPage()
        {
            InitializeComponent();
        }
    }
}