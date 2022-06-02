using InfluMe.ViewModels;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    /// <summary>
    /// Page to show the health profile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewProfilePage : ContentPage {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProfilePage" /> class.
        /// </summary>
        public ViewProfilePage() {
            this.InitializeComponent();
            BindingContext = new ViewProfilePageViewModel();
            ViewProfilePageViewModel vm = (ViewProfilePageViewModel)this.BindingContext;
        }
    }
}