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
using System.Globalization;

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
        private List<Notification> notifications;
        private JobResponse selectedJob;
        private string jobPlatformFilter;
        private bool isEmpty;
        private bool isIGEmpty;
        private bool isTTEmpty;

        private JobDataService service => new JobDataService();

        #endregion

        #region Constructor
        public JobViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.ViewAllCommand = new Command<string>(this.ViewAllClicked);
            this.NotificationButtonCommand = new Command(this.NotificationButtonClicked);
            this.FilterJobByPlatformCommand = new Command(this.FilterJobByPlatform);
            this.LogOutCommand = new Command(LogOutClicked);

        }
        #endregion

        #region Public properties
        public JobResponse SelectedJob {
            get { return selectedJob; }
            set {
                if (value != null) {
                    selectedJob = value;
                    ItemSelected();
                }
                else selectedJob = null;
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

        public bool IsIGEmpty {
            get {
                return this.isIGEmpty;
            }

            set {
                if (this.isIGEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isIGEmpty, value);
            }
        }

        public bool IsTTEmpty {
            get {
                return this.isTTEmpty;
            }

            set {
                if (this.isTTEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isTTEmpty, value);
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

        public List<Notification> Notifications {
            get {
                return this.notifications;
            }

            set {
                if (this.notifications == value) {
                    return;
                }

                this.SetProperty(ref this.notifications, value);
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

        public Command LogOutCommand { get; set; }


        #endregion

        #region Methods

        private async void InitializeProperties() {
            List<JobResponse> jobs = new List<JobResponse>();

            try {
                jobs = await service.GetAllJob();
                jobs = jobs.Select(x => { x.jobDeadline = DateTime.ParseExact(x.jobDeadline, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"); return x; }).ToList();
                jobs = jobs.Select(x => { x.jobRegistrationDeadline = DateTime.ParseExact(x.jobRegistrationDeadline, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"); return x; }).ToList();

                this.Notifications = await service.GetNotifications(Application.Current.Properties["UserId"].ToString(), Application.Current.Properties["UserType"].ToString());
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            if(Application.Current.Properties["UserType"].Equals(UserType.Influencer.ToString()))
                jobs = jobs.Where(x => x.jobStatus.Equals(JobStatus.OPEN.ToString())).ToList();

            
            this.AllJobs = new ObservableCollection<JobResponse>(jobs);
            if (JobPlatformFilter != null)
                this.Jobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals(JobPlatformFilter) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())));
            else if (JobPlatformFilter == null)
                this.Jobs = this.AllJobs;

            this.InstagramJobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals(JobPlatformList.Instagram.ToString()) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())).Take(3));
            this.TikTokJobs = new ObservableCollection<JobResponse>(jobs.Where(x => x.jobPlatform.Equals(JobPlatformList.TikTok.ToString()) || x.jobPlatform.Equals(JobPlatformList.Both.ToString())).Take(3));

            this.IsEmpty = this.AllJobs.Count == 0;
            this.IsIGEmpty = this.InstagramJobs.Count == 0;
            this.IsTTEmpty = this.TikTokJobs.Count == 0;
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ItemSelected() {
            if(Application.Current.Properties["UserType"].ToString() == UserType.Admin.ToString()) {
                switch (selectedJob.jobStatus) {
                    case nameof(JobStatus.OPEN):
                        await Application.Current.MainPage.Navigation.PushAsync(new EditJobPage(SelectedJob));
                        break;
                    case nameof(JobStatus.SELECTION):
                        // list of influencers who applied
                        await Application.Current.MainPage.Navigation.PushAsync(new ViewJobApplicantsPage(SelectedJob));
                        break;
                    case nameof(JobStatus.ONGOING):
                        // list of influencers with ongoing Job Status ref: my jobs
                        await Application.Current.MainPage.Navigation.PushAsync(new OngoingJobPage(SelectedJob));
                        break;
                    
                    case nameof(JobStatus.COMPLETE):
                        // admin can close when all done
                        break;
                }
                
            }
                

            else if(Application.Current.Properties["UserType"].ToString() == UserType.Influencer.ToString())
                await Application.Current.MainPage.Navigation.PushAsync(new JobDetailPage(SelectedJob, false, ""));
        }

        /// <summary>
        /// Invoked when an view all is selected.
        /// </summary>
        private void ViewAllClicked(string jobPlatform) {
            this.SelectedJob = null;
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
            Application.Current.MainPage.Navigation.PushAsync(new NotificationListPage(this.Notifications));
        }

        public void LogOutClicked(object obj) {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["UserId"] = "";
            Application.Current.Properties["UserType"] = "";
            App.Current.SavePropertiesAsync();
            Application.Current.MainPage = new NavigationPage(new MainLoginPage());
        }

        public void OnAppearing() {
            IsBusy = true;
            SelectedJob = null;
            InitializeProperties();
        }
    }
    

    #endregion
}
