using InfluMe.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnrollmentsPage : ContentPage {
        public EnrollmentsPage() {
            InitializeComponent();
            this.BindingContext = new InfluencerPageViewModel();
            //EnrollmentList.ItemTapped += ListTapped();
        }

        private ItemTappedEventHandler ListTapped() {
            throw new NotImplementedException();
        }
    }
}