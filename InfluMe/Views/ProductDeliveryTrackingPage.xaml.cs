using InfluMe.DataService;
using InfluMe.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    /// <summary>
    /// Page to show the product delivery tracking.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDeliveryTrackingPage {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDeliveryTrackingPage" /> class.
        /// </summary>
        public ProductDeliveryTrackingPage() {
            this.InitializeComponent();
            this.BindingContext = new ProductDeliveryTrackingPageViewModel();
        }
    }
}