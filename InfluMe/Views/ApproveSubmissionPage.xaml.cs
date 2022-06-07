using System;
using System.Collections.Generic;
using System.Linq;
using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Views;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfluMe.ViewModels;

namespace InfluMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApproveSubmissionPage : ContentPage
    {
        public ApproveSubmissionPage(JobAppliedResponse selected)
        {
            InitializeComponent();
            BindingContext = new ApproveSubmissionViewModel(selected);
        }
    }
}