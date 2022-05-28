using InfluMe.Models;
using InfluMe.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPopupPage : PopupPage {
        public ConfirmationPopupPage(InfluencerPageViewModel vm) {
            InitializeComponent();
            BindingContext = vm;
        }

    }
}