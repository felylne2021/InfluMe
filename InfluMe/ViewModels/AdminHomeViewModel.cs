using InfluMe.Models;
using InfluMe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class AdminHomeViewModel : BaseViewModel
    {
        #region Fields

        

        private Command itemSelectedCommand;

        private Command sortCommand;

        private Command filterCommand;

        private Command addToCartCommand;

        private Command cardItemCommand;
        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="AdminHomeViewModel" /> class.
        /// </summary>
        public AdminHomeViewModel()
        {
            this.LogOutCommand = new Command(LogOutClicked);
            this.NotificationCommand = new Command(NotificationClicked);
        }

        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        [DataMember(Name = "jobs")]
        public ObservableCollection<JobResponse> Jobs {
            get; set;
        }

        

        #endregion

        #region Command
        public Command LogOutCommand { get; set; }
        public Command NotificationCommand { get; set; }

        #endregion

        #region Methods

        public void LogOutClicked(object obj) {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["UserId"] = "";
            Application.Current.Properties["UserType"] = "";
            App.Current.SavePropertiesAsync();
            Application.Current.MainPage = new NavigationPage(new MainLoginPage());
        }

       private void NotificationClicked() {
            Application.Current.MainPage.Navigation.PushAsync(new NotificationListPage(new JobViewModel().Notifications));
        }

        #endregion
    }
}