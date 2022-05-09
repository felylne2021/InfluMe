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

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for home page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class JobViewModel : BaseViewModel {
        #region Fields
        private ObservableCollection<JobResponse> jobs;
        private ObservableCollection<JobResponse> instagramJobs;
        private ObservableCollection<JobResponse> tiktokJobs;
        private JobResponse selectedJob;

        private JobDataService service => new JobDataService();

        #endregion

        #region Constructor
        public JobViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.ViewAllCommand = new Command<string>(this.ViewAllClicked);
            this.NotificationButtonCommand = new Command(this.NotificationButtonClicked);
        }
        #endregion

        #region Public properties
        public JobResponse SelectedJob {
            get { return selectedJob; }
            set {
                if (selectedJob != value) {
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
            this.Jobs = new ObservableCollection<JobResponse>(jobs);
            this.InstagramJobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals("Instagram") || x.jobPlatform.Equals("Both")).Take(3));
            this.TikTokJobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals("TikTok") || x.jobPlatform.Equals("Both")).Take(3));


        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ItemSelected() {
            Application.Current.MainPage.Navigation.PushAsync(new JobDetailPage(SelectedJob.jobId.ToString()));
        }

        /// <summary>
        /// Invoked when an view all is selected.
        /// </summary>
        private void ViewAllClicked(string jobPlatform) {
            // TODO:pass jobPlatform param to determine view model, save in label
            Application.Current.MainPage.Navigation.PushAsync(new JobListPage(jobPlatform));
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
