using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfluMe.Models;
using InfluMe.Views;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfluMe.ViewModels;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputDeliveryReceiptPage : ContentPage {
        public InputDeliveryReceiptPage(JobAppliedResponse selected) {
            InitializeComponent();
            BindingContext = new InputDeliveryViewModel() { Selected = selected };
        }
    }
}