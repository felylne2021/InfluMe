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
        public HomePage(int influencerId)
        {
            this.InitializeComponent();
            this.BindingContext = HomeDataService.Instance.HomePageViewModel;
            InfluencerId.Text = influencerId.ToString();
        }

        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);

        //    if (width < height)
        //    {
        //        if (this.listView.LayoutManager is GridLayout)
        //        {
        //            (this.listView.LayoutManager as GridLayout).SpanCount = 2;
        //        }
        //    }
        //    else
        //    {
        //        if (this.listView.LayoutManager is GridLayout)
        //        {
        //            (this.listView.LayoutManager as GridLayout).SpanCount = 4;
        //        }
        //    }
        //}
    }
}