using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSelectionPage : ContentPage {
        public JobSelectionPage() {
            this.InitializeComponent();
            BindingContext = new InfluencerPageViewModel();
        }
    }
}