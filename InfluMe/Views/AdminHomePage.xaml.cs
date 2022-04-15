using InfluMe.DataService;
using Syncfusion.ListView.XForms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// The Category Tile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminHomePage" /> class.
        /// </summary>
        public AdminHomePage()
        {
            this.InitializeComponent();
            this.BindingContext = CategoryDataService.Instance.AdminHomePageViewModel;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width < height)
            {
                if (this.MenuTile.LayoutManager is GridLayout)
                {
                    (this.MenuTile.LayoutManager as GridLayout).SpanCount = 2;
                }
            }
            else
            {
                if (this.MenuTile.LayoutManager is GridLayout)
                {
                    (this.MenuTile.LayoutManager as GridLayout).SpanCount = 3;
                }
            }
        }
    }
}