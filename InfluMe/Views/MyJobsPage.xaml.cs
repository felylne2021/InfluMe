using InfluMe.Services;
using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyJobsPage : ContentPage {
        public MyJobsPage() {
            InitializeComponent();
            BindingContext = new MyJobsViewModel();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            InfluMeService service = new InfluMeService();
            MyJobsViewModel viewModel = (MyJobsViewModel)this.BindingContext;

            viewModel.InitializeProperties();
        }
    }
}