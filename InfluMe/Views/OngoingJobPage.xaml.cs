using InfluMe.Models;
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
    public partial class OngoingJobPage : ContentPage {

        JobApplicantsViewModel _viewModel;
        public OngoingJobPage(JobResponse selectedJob) {
            InitializeComponent();
            BindingContext = _viewModel = new JobApplicantsViewModel(selectedJob);
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}