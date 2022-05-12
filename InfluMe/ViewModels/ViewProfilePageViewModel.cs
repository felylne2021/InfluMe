using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for health profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ViewProfilePageViewModel : BaseViewModel {
        #region Fields

        private InfluMeService service = new InfluMeService();
        private InfluencerResponse influencer;
        private JobStatsResponse jobStats;
        private int completedJobs;
        private int ongoingJobs;
        private string influencerAgeSex;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ViewProfilePageViewModel" /> class.
        /// </summary>
        public ViewProfilePageViewModel() {
            this.InitializeProperties();
            this.LogOutCommand = new Command(LogOutClicked);
            this.EditProfileCommand = new Command(EditProfileClicked);
        }

        #endregion

        #region Public properties

        public int CompletedJobs {
            get {
                return this.completedJobs;
            }
            set {
                if (this.completedJobs == value) {
                    return;
                }

                this.SetProperty(ref this.completedJobs, value);
            }
        }
        public int OngoingJobs {
            get {
                return this.ongoingJobs;
            }
            set {
                if (this.ongoingJobs == value) {
                    return;
                }

                this.SetProperty(ref this.ongoingJobs, value);
            }
        }

        public string InfluencerAgeSex {
            get {
                return this.influencerAgeSex;
            }
            set {
                if (this.influencerAgeSex == value) {
                    return;
                }

                this.SetProperty(ref this.influencerAgeSex, value);
            }
        }

        public InfluencerResponse Influencer {
            get {
                return this.influencer;
            }

            set {
                if (this.influencer == value) {
                    return;
                }

                this.SetProperty(ref this.influencer, value);
            }
        }

        public JobStatsResponse JobStats {
            get {
                return this.jobStats;
            }

            set {
                if (this.jobStats == value) {
                    return;
                }

                this.SetProperty(ref this.jobStats, value);
            }
        }

        #endregion
        

        #region Commands

        public Command LogOutCommand { get; set; }
        public Command EditProfileCommand { get; set; }

        #endregion

        #region Methods

        private async void InitializeProperties() {

            try {
                this.Influencer = await service.GetInfluencerById(Application.Current.Properties["UserId"].ToString());
                this.JobStats = await service.GetInfluencerStats(Application.Current.Properties["UserId"].ToString());

                this.OngoingJobs = JobStats.appliedJobList.Count(x => x.progressStatus.Equals(JobProgressStatus.Ongoing));
                this.CompletedJobs = JobStats.appliedJobList.Count(x => x.progressStatus.Equals(JobProgressStatus.Completed));

            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            this.InfluencerAgeSex = CalculateAge(this.Influencer.influencerDOB) + " y.o | " + Influencer.influencerGender;
        }

        private string CalculateAge(string birthdate) {
            // Save today's date.
            var today = DateTime.Today;
            var Birthdate = DateTime.ParseExact(birthdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            // Calculate the age.
            var age = today.Year - Birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (Birthdate.Date > today.AddYears(-age)) age--;
            return age.ToString();
        }

        
        private void LogOutClicked(object obj) {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["UserId"] = "";
            Application.Current.Properties["UserType"] = "";
            Application.Current.MainPage = new MainLoginPage();

        }

        private void EditProfileClicked(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage());
        }

        #endregion
    }
}