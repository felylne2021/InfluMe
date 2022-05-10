using InfluMe.DataService;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// Page to display product home page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            this.InitializeComponent();
            InfluencerId.Text = Application.Current.Properties["UserId"].ToString();
        }      
    }
}