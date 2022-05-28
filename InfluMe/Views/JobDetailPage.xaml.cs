using InfluMe.Models;
using InfluMe.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using InfluMe.Helpers;

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
        public JobDetailPage(JobResponse selectedJob, bool isApplied, string jobProgressStatus)
        {
            this.InitializeComponent();
            JobId.Text = selectedJob.jobId.ToString();
            BindingContext = new JobDetailPageViewModel() { Job = selectedJob, IsApplied = isApplied, ThisJobProgressStatus = jobProgressStatus, IsSubmissionVisible = (jobProgressStatus.Equals(JobProgressStatus.PendingDraft) || jobProgressStatus.Equals(JobProgressStatus.PendingProof)), ImageURL = new System.Uri(selectedJob.jobImage) };
            //((ListView)sender).SelectedItem = null;

        }       


    }
}