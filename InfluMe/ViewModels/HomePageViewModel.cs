﻿using InfluMe.Models;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels
{
    /// <summary>
    /// ViewModel for home page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class HomePageViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<Job> instagramJobs;
        private ObservableCollection<Job> tiktokJobs;

        private Command itemSelectedCommand;

        private string bannerImage;

        private Command viewAllCommand;

        private Command notificationButtonCommand;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with Xamarin.Forms Image, which displays the banner image.
        /// </summary>
        [DataMember(Name = "bannerimage")]
        public string BannerImage
        {
            get { return App.ImageServerPath + this.bannerImage; }
            set { this.bannerImage = value; }
        }        
        

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "instagramjobs")]
        public ObservableCollection<Job> InstagramJobs
        {
            get
            {
                return this.instagramJobs;
            }

            set
            {
                if (this.instagramJobs == value)
                {
                    return;
                }

                this.SetProperty(ref this.instagramJobs, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get
            {
                return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected));
            }
        }

        /// <summary>
        /// Gets the command that is executed when the view all button is clicked.
        /// </summary>
        public Command ViewAllCommand
        {
            get
            {
                return this.viewAllCommand ?? (this.viewAllCommand = new Command(this.ViewAllClicked));
            }
        }

        /// <summary>
        /// Gets the command that will be executed when notification button is selected.
        /// </summary>
        public Command NotificationButtonCommand
        {
            get
            {
                return this.notificationButtonCommand ?? (this.notificationButtonCommand = new Command(this.NotificationButtonClicked));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ItemSelected(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an view all is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ViewAllClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an notification icon is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationButtonClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}