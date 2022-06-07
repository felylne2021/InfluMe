using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InfluMe.ViewModels
{
    class JobApplicantsViewModel : BaseViewModel
    {
        private JobAppliedResponse selected;
        private List<Applicant> applicants;
        private List<JobAppliedResponse> jobApplieds;
        private List<JobAppliedResponse> approveds;
        private bool isAppliEmpty;
        private bool isApprovedsEmpty;
        private JobDataService service => new JobDataService();


        public JobApplicantsViewModel(JobResponse selectedJob) {
            this.SelectedJob = selectedJob;
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.CopyCommand = new Command(CopyClicked);
            this.SubmitCommand = new Command(SubmitClicked);

        }

        public JobResponse SelectedJob { get; set; }

        public JobAppliedResponse Selected{
            get {
                return this.selected;
            }

            set {
                if (value != null) {
                    selected = value;
                    ItemSelected();
                }
            }
        }

        public List<Applicant> Applicants {
            get {
                return this.applicants;
            }

            set {
                if (this.applicants == value) {
                    return;
                }

                this.SetProperty(ref this.applicants, value);
            }
        }

        public List<JobAppliedResponse> JobApplieds {
            get {
                return this.jobApplieds;
            }

            set {
                if (this.jobApplieds == value) {
                    return;
                }

                this.SetProperty(ref this.jobApplieds, value);
            }
        }

        public List<JobAppliedResponse> Approveds {
            get {
                return this.approveds;
            }

            set {
                if (this.approveds == value) {
                    return;
                }

                this.SetProperty(ref this.approveds, value);
            }
        }


        public bool IsAppliEmpty {
            get {
                return this.isAppliEmpty;
            }

            set {
                if (this.isAppliEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isAppliEmpty, value);
            }
        }

        public bool IsApprovedsEmpty {
            get {
                return this.isApprovedsEmpty;
            }

            set {
                if (this.isApprovedsEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isApprovedsEmpty, value);
            }
        }


        private async void InitializeProperties() {
            try {
                 this.JobApplieds = await service.GetAllAppliedJobByJobId(SelectedJob.jobId);

                if (SelectedJob.jobStatus.Equals(JobStatus.SELECTION.ToString())) {
                    this.Applicants = this.JobApplieds.Select(x => new Applicant { influencerId = x.influencerId, jobId = SelectedJob.jobId, isChecked = false }).ToList();

                    this.IsAppliEmpty = Applicants.Count == 0;
                }

                else {
                    this.Approveds
                        = this.JobApplieds.Where(x => x.progressStatus != JobProgressStatus.NotApproved.ToString()).ToList();

                    this.IsApprovedsEmpty = Approveds.Count == 0;
                }
                    


                
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        private async void ItemSelected() {
            switch (selected.progressStatus) {
                case (JobProgressStatus.OnDelivery):
                    await Application.Current.MainPage.Navigation.PushAsync(new InputDeliveryReceiptPage(Selected));
                    break;

                case (JobProgressStatus.DraftSubmitted):
                    await Application.Current.MainPage.Navigation.PushAsync(new ApproveSubmissionPage(Selected));
                    break;

                case (JobProgressStatus.ProofSubmitted):
                    await Application.Current.MainPage.Navigation.PushAsync(new ApproveSubmissionPage(Selected));
                    break;

                case (JobProgressStatus.PendingPayment):
                    
                    break;
            }
        }

        private async void CopyClicked() {
            string text = $"{SelectedJob.jobBrand}: {SelectedJob.jobName}\n";

            text += new String('=', text.Length);
            text += "\n";

            for (int i = 0; i < JobApplieds.Count; i++) {
                text += $"{i+1}. {JobApplieds[i].influencer.influencerName}\n";

                if (!SelectedJob.jobPlatform.Equals(JobPlatformList.TikTok.ToString()))
                    text += $"Instagram: https://instagram.com/{JobApplieds[i].influencer.influencerInstagramId}/\n";

                if (!SelectedJob.jobPlatform.Equals(JobPlatformList.Instagram.ToString()))
                    text += $"TikTok: https://www.tiktok.com/@{JobApplieds[i].influencer.influencerTiktokId}\n";

                if (!String.IsNullOrEmpty(SelectedJob.jobProduct)) {
                    text += $"Address: {JobApplieds[i].influencer.influencerAddress}\n" +
                        $"Phone Number: {JobApplieds[i].influencer.whatsappNumber}\n";
                }
                text += "\n";
            }

            await Clipboard.SetTextAsync(text);
        }

        private async void SubmitClicked() {
            try {
                await service.SubmitJobApplicants(Applicants);

                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Job Applicants Submitted"));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        public Command BackButtonCommand { get; set; }
        public Command CopyCommand { get; set; }
        public Command SubmitCommand { get; set; }

    }
}
