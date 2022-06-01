using InfluMe.Models;
using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditJobPage : ContentPage {
        public EditJobPage(JobResponse selectedJob) {
            InitializeComponent();
            var bytes = Convert.FromBase64String(selectedJob.jobImageBlob);
            imageView.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            BindingContext = new EditJobViewModel() { SelectedJob = selectedJob };
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
            FileResult res = await MediaPicker.PickPhotoAsync(new MediaPickerOptions {
                Title = "Pick job thumbnail image"
            });

            if (res != null) {
                Stream stream = await res.OpenReadAsync();
                
                imageView.Source = ImageSource.FromStream(() => stream);


                byte[] imageArray = System.IO.File.ReadAllBytes($@"{res.FullPath}");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                imageBlob.Text = base64ImageRepresentation;
            }

        }
    }
}