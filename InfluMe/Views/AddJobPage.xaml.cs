﻿using InfluMe.ViewModels;
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