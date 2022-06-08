using InfluMe.Models;
using InfluMe.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    /// <summary>
    /// Page to show the payment success.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentStatusPage : ContentPage {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatusPage" /> class.
        /// </summary>
        public PaymentStatusPage(JobAppliedResponse selected) {
            this.InitializeComponent();
            this.BindingContext = new PaymentViewModel(selected);
        }
    }
}