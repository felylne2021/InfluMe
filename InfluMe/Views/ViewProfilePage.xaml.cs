using InfluMe.ViewModels;
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
            this.BindingContext = ViewProfilePageViewModel.BindingContext;
        }
    }
}