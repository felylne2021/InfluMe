using InfluMe.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddJobPage : ContentPage {
        public AddJobPage() {
            this.InitializeComponent();

            
        }

        private void Reload(object sender, System.EventArgs e) {
            JobNameEntry.Text = string.Empty;
            BrandEntry.Text = string.Empty;
            AgeEntry.Text = string.Empty;
            Fee.Text = "0.00";
            SOW.Text = string.Empty;
            AdditionalRequirementsEntry.Text = string.Empty;

            GenderPicker.SelectedValue = null;
            PlatformPicker.SelectedValue = null;
        }

        private void DatePicker_Clicked(object sender, System.EventArgs e) {
            datePicker.IsOpen = true;
        }

        private void SecDatePicker_Clicked(object sender, System.EventArgs e) {
            secDatePicker.IsOpen = true;
        }

        private void DatePicker_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e) {
            pickerButton.Text = string.Format("{0:dd/MM/yyyy}", e.NewValue);
        }

        private void SecDatePicker_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e) {
            secondPickerButton.Text = string.Format("{0:dd/MM/yyyy}", e.NewValue);
        }

        private async void Upload_Clicked(object sender, System.EventArgs e) {
           
            var res = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 15
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