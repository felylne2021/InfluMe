using System;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
    /// <summary>
    /// Page to show chat profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage(string email) {
            this.InitializeComponent();
            InfluencerEmail.Text = email;
        }

        private void DatePicker_Clicked(object sender, System.EventArgs e) {
            birthdateErrorMessage.IsVisible = false;
            birthdatePicker.IsOpen = true;
        }

        private void DatePicker_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e) {
            birthdatePickerButton.Text = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(e.NewValue).Date);
        }
    }
}