using InfluMe.Models;
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
        public JobDetailPage(JobResponse selectedJob, bool isApplied, bool isSubmissionVisible)
        {
            this.InitializeComponent();
            JobId.Text = selectedJob.jobId.ToString();
            BindingContext = new JobDetailPageViewModel() { Job = selectedJob, IsApplied = isApplied, IsSubmissionVisible = isSubmissionVisible};

        }       


    }
}