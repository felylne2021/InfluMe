using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfluMe.ViewModels {
    public class ApproveSubmissionViewModel {

        private JobDataService service = new JobDataService();
        private JobAppliedResponse selected;

        public ApproveSubmissionViewModel(JobAppliedResponse selected) {
            this.Selected = selected;
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
        }

        public string Header { get; set; }
        public string JobDetail { get; set; }

        public JobAppliedResponse Selected {
            get {
                return this.selected;
            }

            set {
                if (value != null) {
                    selected = value;
                }
            }
        }

        public void InitializeProperties() {
            this.Header = $"{Selected.influencer.influencerName}'s\n";
            this.Header += (Selected.progressStatus.Equals(JobProgressStatus.DraftSubmitted.ToString())) ? "content draft\nlink submission" : "proof of work\nlink submission";
            this.JobDetail = Selected.job.jobName + " from " + Selected.job.jobBrand;
        }

        private async Task Approve() {
            ChangeJobProgressRequest req = new ChangeJobProgressRequest();

            req.influencerId = Selected.influencerId;
            req.jobId = Selected.job.jobId;

            if (Selected.progressStatus.Equals(JobProgressStatus.DraftSubmitted.ToString()))
                req.progressStatus = JobProgressStatus.PendingProof.ToString();
            else { // proof submitted approved
                req.progressStatus = Selected.job.jobFee JobProgressStatus.PendingProof.ToString();
            }

            try {
                await service.ChangeJobProgress(req);
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Delivery Saved"));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        public Command BackButtonCommand { get; set; }
        public Command ApproveCommand { get; set; }
        public Command RejectCommand { get; set; }
    }
}
