using InfluMe.DataService;
using InfluMe.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    /// <summary>
    /// Page to display the Contacts list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageInfluencerPage {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageInfluencerPage" /> class.
        /// </summary>
        public ManageInfluencerPage() {
            this.InitializeComponent();
            this.BindingContext = new InfluencerPageViewModel();
        }

        protected override void OnAppearing() {
            InfluencerList.SelectedItem = null;

        }
    }
}