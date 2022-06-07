using InfluMe.Models;
using InfluMe.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputPaymentPage : ContentPage {

        public InputPaymentPage(JobAppliedResponse selected) {
            InitializeComponent();
            BindingContext = new InputPaymentViewModel() { Selected = selected };
        }

        private async void Upload_Clicked(object sender, System.EventArgs e) {

            var res = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 25
            });

            if (res != null) {

                byte[] imageArray = System.IO.File.ReadAllBytes($@"{res.Path}");

                imageView.Source = ImageSource.FromStream(() => {
                    var stream = res.GetStream();
                    res.Dispose();
                    return stream;
                });

                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                imageBlob.Text = base64ImageRepresentation;
            }

        }
    }
}