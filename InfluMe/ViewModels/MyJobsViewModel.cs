using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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
    public class MyJobsViewModel : BaseViewModel {
        #region Fields

        private InfluMeService service = new InfluMeService();
        private List<JobAppliedResponse> myJobs;
        private List<JobAppliedResponse> allMyJobs;
        private JobAppliedResponse selectedJob;
        private bool isEmpty;

        private string jobStatusFilter;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ViewProfilePageViewModel" /> class.
        /// </summary>
        public MyJobsViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => 
            Application.Current.MainPage.Navigation.PopAsync());
            this.FilterJobByStatusCommand = new Command(FilterJobByStatus);
        }
        #endregion

        #region Public Properties
        public JobAppliedResponse SelectedJob {
            get { return selectedJob; }
            set {
                if(value != null) {
                    selectedJob = value;
                    ItemSelected();
                }
            }
        }

        public string JobStatusFilter {
            get {
                return this.jobStatusFilter;
            }

            set {
                if (this.jobStatusFilter == value) {
                    return;
                }

                this.SetProperty(ref this.jobStatusFilter, value);
            }
        }

        public bool IsEmpty {
            get {
                return this.isEmpty;
            }

            set {
                if (this.isEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isEmpty, value);
            }
        }

        public List<JobAppliedResponse> MyJobs {
            get {
                return this.myJobs;
            }

            set {
                if (this.myJobs == value) {
                    return;
                }

                this.SetProperty(ref this.myJobs, value);
            }
        }

        public List<JobAppliedResponse> AllMyJobs {
            get {
                return this.allMyJobs;
            }

            set {
                if (this.allMyJobs == value) {
                    return;
                }

                this.SetProperty(ref this.allMyJobs, value);
            }
        }

        #endregion


        #region Commands
        public Command BackButtonCommand { get; set; }
        public Command FilterJobByStatusCommand { get; set; }


        #endregion

        #region Methods
       
        public async void InitializeProperties() {

            try {

                JobStatsResponse resp = await service.GetInfluencerStats(Application.Current.Properties["UserId"].ToString());

                this.AllMyJobs = resp.appliedJobList;

                
                this.MyJobs = AllMyJobs;

                this.IsEmpty = this.AllMyJobs.Count == 0;

            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        private void FilterJobByStatus(object obj) {
            if (!string.IsNullOrEmpty(JobStatusFilter))
                this.MyJobs = this.AllMyJobs.Where(x => x.progressStatus.Equals(JobStatusFilter)).ToList();
            else this.MyJobs = this.AllMyJobs;
        }

        private async void ItemSelected() {
            switch (this.SelectedJob.progressStatus) {
                case JobProgressStatus.OnDelivery:
                    await Application.Current.MainPage.Navigation.PushAsync(new ProductDeliveryTrackingPage());
                    break;

                case JobProgressStatus.Completed:
                    if(!this.SelectedJob.job.jobFee.Equals("0.00"))
                        await Application.Current.MainPage.Navigation.PushAsync(new PaymentStatusPage(this.SelectedJob));
                    break;

                default:
                    await Application.Current.MainPage.Navigation.PushAsync(new JobDetailPage(SelectedJob.job, true, SelectedJob.progressStatus));
                    break;
            }
            
        }

        #endregion
    }
}