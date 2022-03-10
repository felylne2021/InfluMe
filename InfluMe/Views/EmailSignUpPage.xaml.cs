using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailSignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainLoginPage" /> class.
        /// </summary>
        public EmailSignUpPage()
        {
            this.InitializeComponent();
        }
    }
}