using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace InfluMe.ViewModels {
    public class JobListViewModel : BaseViewModel {

        private ObservableCollection<JobResponse> jobs;
        private JobDataService service => new JobDataService();

        public JobListViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
        }

        #region Public properties
        public int UserId;
        public string selectedFilter = "All";

        public string SelectedFilter {
            get => selectedFilter;
            set {
                if (SetProperty(ref selectedFilter, value)) ;
            }
        }

        public ObservableCollection<string> FilterOptions = new ObservableCollection<string>
        {
            "All",
            JobPlatform.Instagram.ToString(),
            JobPlatform.TikTok.ToString()
        };

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
        #endregion

        #region Command

        public Command BackButtonCommand { get; set; }

        #endregion

        #region Methods

        //private void FilterItems(ObservableCollection<JobResponse> listJobs) {
        //    listJobs.ToList().Where(x => x.jobPlatform.Equals(SelectedFilter) || SelectedFilter == "All");
        //}

        private async void InitializeProperties() {
            List<JobResponse> jobs = new List<JobResponse>();

            try {
                jobs = await service.GetAllJob();
            }
            catch (Exception) {
                throw new Exception();
            }
            this.Jobs = new ObservableCollection<JobResponse>(jobs);
        }

        #endregion
    }
}