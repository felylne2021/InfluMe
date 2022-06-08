using InfluMe.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// Page to show chat profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditProfilePage" /> class.
        /// </summary>
        public EditProfilePage(ViewProfilePageViewModel viewProfilePageViewModel)
        {
            this.InitializeComponent();
            BindingContext = viewProfilePageViewModel;
        }

        private async void Upload_Clicked(object sender, System.EventArgs e) {

            var res = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 25
            });

            if (res != null) {

                byte[] imageArray = System.IO.File.ReadAllBytes($@"{res.Path}");

                EditProfileImage.Source = ImageSource.FromStream(() => {
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