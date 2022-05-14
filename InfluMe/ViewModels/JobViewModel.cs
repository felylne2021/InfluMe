using InfluMe.DataService;
using InfluMe.Models;
using InfluMe.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using InfluMe.Helpers;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for home page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class JobViewModel : BaseViewModel {
        #region Fields
        private ObservableCollection<JobResponse> jobs;
        private ObservableCollection<JobResponse> allJobs;
        private ObservableCollection<JobResponse> instagramJobs;
        private ObservableCollection<JobResponse> tiktokJobs;
        private JobResponse selectedJob;
        private string jobPlatformFilter;

        private JobDataService service => new JobDataService();

        #endregion

        #region Constructor
        public JobViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.ViewAllCommand = new Command<string>(this.ViewAllClicked);
            this.NotificationButtonCommand = new Command(this.NotificationButtonClicked);
            this.FilterJobByPlatformCommand = new Command(this.FilterJobByPlatform);
        }
        #endregion

        #region Public properties
        public JobResponse SelectedJob {
            get { return selectedJob; }
            set {
                if (selectedJob != value && value != null) {
                    selectedJob = value;
                    ItemSelected();
                }
            }
        }

        public ObservableCollection<JobResponse> Jobs {
            get {
                return this.jobs;
            }

            set {
                if (this.jobs == value) {
                    return;
                }

                this.SetProperty(ref this.jobs, value);
            }
        }

        public ObservableCollection<JobResponse> AllJobs {
            get {
                return this.allJobs;
            }

            set {
                if (this.allJobs == value) {
                    return;
                }

                this.SetProperty(ref this.allJobs, value);
            }
        }

        public string JobPlatformFilter {
            get {
                return this.jobPlatformFilter;
            }

            set {
                if (this.jobPlatformFilter == value) {
                    return;
                }

                this.SetProperty(ref this.jobPlatformFilter, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<JobResponse> InstagramJobs {
            get {
                return this.instagramJobs;
            }

            set {
                if (this.instagramJobs == value) {
                    return;
                }

                this.SetProperty(ref this.instagramJobs, value);
            }
        }

        public ObservableCollection<JobResponse> TikTokJobs {
            get {
                return this.tiktokJobs;
            }

            set {
                if (this.tiktokJobs == value) {
                    return;
                }

                this.SetProperty(ref this.tiktokJobs, value);
            }
        }

        #endregion

        #region Command

        public Command BackButtonCommand {get; set; }        

        /// <summary>
        /// Gets the command that is executed when the view all button is clicked.
        /// </summary>
        public Command ViewAllCommand { get; set; }

        /// <summary>
        /// Gets the command that will be executed when notification button is selected.
        /// </summary>
        public Command NotificationButtonCommand {get; set; }

        public Command FilterJobByPlatformCommand {get; set; }

        #endregion

        #region Methods

        private async void InitializeProperties() {
            List<JobResponse> jobs = new List<JobResponse>();

            try {
                jobs = await service.GetAllJob();
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            this.AllJobs = new ObservableCollection<JobResponse>(jobs);
            this.Jobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals(JobPlatformFilter) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())));
            this.InstagramJobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals(JobPlatformList.Instagram.ToString()) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())).Take(3));
            this.TikTokJobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals(JobPlatformList.TikTok.ToString()) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())).Take(3));


        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ItemSelected() {
            Application.Current.MainPage.Navigation.PushAsync(new JobDetailPage(SelectedJob, false));
        }

        /// <summary>
        /// Invoked when an view all is selected.
        /// </summary>
        private void ViewAllClicked(string jobPlatform) {
            Application.Current.MainPage.Navigation.PushAsync(new JobListPage(jobPlatform, this));
        }

        private void FilterJobByPlatform(object obj) {
            if (!string.IsNullOrEmpty(JobPlatformFilter))
                this.Jobs = new ObservableCollection<JobResponse>(this.AllJobs.Where(x => x.jobPlatform.Equals(JobPlatformFilter) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())));
            else this.Jobs = this.AllJobs;
        }
        /// <summary>
        /// Invoked when an notification icon is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationButtonClicked(object obj) {
            // Do something
        }
    }
    

    #endregion
}
