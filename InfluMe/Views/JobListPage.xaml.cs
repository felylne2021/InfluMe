using InfluMe.DataService;
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
        public JobListPage(string jobPlatform)
        {
            this.InitializeComponent();
            JobPlatform.Text = jobPlatform;
            UserId.Text = "";
        }
    }
}