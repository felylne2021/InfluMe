using InfluMe.DataService;
using InfluMe.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobListPage" /> class.
        /// </summary>
        public JobListPage(string jobPlatform, JobViewModel jobViewModel)
        {
            this.InitializeComponent();
            jobViewModel.JobPlatformFilter = jobPlatform;
            BindingContext =jobViewModel;
        }
    }
}