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
    public partial class ManageJobPage : ContentPage {

        JobViewModel _viewModel;
        public ManageJobPage() {
            InitializeComponent();
            var jobViewModel = new JobViewModel();
            BindingContext = _viewModel = jobViewModel;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}