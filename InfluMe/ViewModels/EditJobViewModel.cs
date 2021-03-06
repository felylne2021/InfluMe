using InfluMe.DataService;
using InfluMe.Models;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfluMe.ViewModels {
    public class EditJobViewModel {

        private JobResponse selectedJob;
        private JobDataService service => new JobDataService();

        public EditJobViewModel() {
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.SaveCommand = new Command(Save);
            this.DeleteCommand = new Command(Delete);
        }

        public JobResponse SelectedJob {
            get { 
               
                return selectedJob; 
            }
            set {
                if (value != null) {
                    selectedJob = value;
                }
            }
        }

        public async void Save() {
            try {

                await service.UpdateJob(selectedJob);

                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Job Updated"));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        public async void Delete() {
            try {
                await service.DeleteJob(selectedJob.jobId.ToString());
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Job Deleted"));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        public Command BackButtonCommand { get; set; }
        public Command SaveCommand { get; set; }
        public Command DeleteCommand { get; set; }
    }
}
