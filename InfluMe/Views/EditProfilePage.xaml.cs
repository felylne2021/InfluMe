﻿using Xamarin.Forms.Internals;
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
        public EditProfilePage()
        {
            this.InitializeComponent();
            this.ProfileImage.Source = App.ImageServerPath + "ProfileImage11.png";
        }
    }
}