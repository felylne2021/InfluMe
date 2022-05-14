using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class JobDetailPageViewModel : BaseViewModel {
        #region Fields
        private JobResponse job;
        private bool isApplied;
        private bool isSubmissionVisible;

        private JobDataService service => new JobDataService();
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="JobDetailPageViewModel" /> class.
        /// </summary>
        public JobDetailPageViewModel() {
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.ApplyJobCommand = new Command<string>(this.ApplyJob);
            this.SubmitCommand = new Command<string>(SubmitClicked);
        }

        #endregion

        #region Public properties

        public bool IsApplied {
            get {
                return this.isApplied;
            }

            set {
                if (this.isApplied == value) {
                    return;
                }

                this.SetProperty(ref this.isApplied, value);
            }
        }

        public bool IsSubmissionVisible {
            get {
                return this.isSubmissionVisible;
            }

            set {
                if (this.isSubmissionVisible == value) {
                    return;
                }

                this.SetProperty(ref this.isSubmissionVisible, value);
            }
        }

        public JobResponse Job {
            get {
                return this.job;
            }

            set {
                if (this.job == value) {
                    return;
                }

                this.SetProperty(ref this.job, value);
            }
        }

        #endregion

        #region Methods


        private async void ApplyJob(string jobId) {
            JobApplied jobApplied = new JobApplied() {
                influencerId = Convert.ToInt32(Application.Current.Properties["UserId"].ToString()),
                jobId = Convert.ToInt32(jobId),
                progressStatus = JobProgressStatus.Applied
            };
            if (IsApplied.Equals(false)) {
                try {
                    JobAppliedResponse jobAppliedResponse = await service.ApplyJob(jobApplied);
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }
                this.IsApplied = true;
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Job Applied"));
            }
            else if (IsApplied.Equals(true)) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorMessagePopupPage("You Have Already Applied"));
            }
        }
        private void SubmitClicked(string jobProgressStatus) {
            Application.Current.MainPage.Navigation.PushAsync(new SubmissionJobContentPage());
        }


        #endregion

        #region Commands
        public Command BackButtonCommand { get; set; }
        public Command ApplyJobCommand { get; set; }
        public Command SubmitCommand { get; set; }
        #endregion

    }
}
