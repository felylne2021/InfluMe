using InfluMe.Models;
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
        }

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e) {
            var cb = (CheckBox)sender;
            InfluencerPageViewModel vm = (InfluencerPageViewModel)this.BindingContext;
            vm.Enrollments.Where(x => x.influencerId.Equals(cb.BindingContext)).Select(c => { c.isChecked = !c.isChecked; return c; }).ToList();
            
        }
    }
}