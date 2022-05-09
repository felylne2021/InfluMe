using InfluMe.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobDetailPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobDetailPage" /> class.
        /// </summary>
        public JobDetailPage(string jobId)
        {
            this.InitializeComponent();
            JobId.Text = jobId;
            BindingContext = new JobDetailPageViewModel(jobId);
        }       


    }
}