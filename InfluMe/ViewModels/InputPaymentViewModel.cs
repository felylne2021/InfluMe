using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfluMe.ViewModels {
    public class InputPaymentViewModel {

        private JobDataService service = new JobDataService();

        public InputPaymentViewModel() {
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.SaveCommand = new Command(SaveClicked);
        }
        
        public JobAppliedResponse Selected { get; set; }
        private async void SaveClicked() {
            ChangeJobProgressRequest req = new ChangeJobProgressRequest();

            req.influencerId = Selected.influencerId;
            req.jobId = Selected.job.jobId;
            req.progressStatus = JobProgressStatus.Completed.ToString();

            try {
                await service.ChangeJobProgress(req);
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Submission Approved"));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        public Command BackButtonCommand { get; set; }

        public Command SaveCommand { get; set; }
    }
}
