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
        private List<Applicant> applicants;
        private List<JobAppliedResponse> jobApplieds;
        private bool isEmpty;
        private JobDataService service => new JobDataService();


        public JobApplicantsViewModel(JobResponse selectedJob) {
            this.SelectedJob = selectedJob;
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.CopyCommand = new Command(CopyClicked);
            this.SubmitCommand = new Command(SubmitClicked);

        }

        public JobResponse SelectedJob { get; set; }

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

        private async void InitializeProperties() {
            try {
                 this.JobApplieds = await service.GetAllAppliedJobByJobId(SelectedJob.jobId);

                this.Applicants = this.JobApplieds.ToList().Select(x => new Applicant { influencerId = x.influencerId, jobId = SelectedJob.jobId, isChecked = false }).ToList();


                this.IsEmpty = Applicants.Count == 0;
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
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
