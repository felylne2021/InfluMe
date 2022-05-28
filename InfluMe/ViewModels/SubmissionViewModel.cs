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
    public class SubmissionViewModel:BaseViewModel {

        private string titleText;
        private string submissionLinkText;
        private int jobId;
        private JobDataService service => new JobDataService();

        public SubmissionViewModel() {
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.SubmitCommand = new Command<string>(SubmitClicked);
        }

        public Command BackButtonCommand { get; set; }
        public Command SubmitCommand { get; set; }
        public string TitleText {
            get {
                return this.titleText;
            }

            set {
                this.SetProperty(ref this.titleText, value);
            }
        }

        public string SubmissionLinkText {
            get {
                return this.submissionLinkText;
            }

            set {
                this.SetProperty(ref this.submissionLinkText, value);
            }
        }

        public int JobId {
            get {
                return this.jobId;
            }

            set {
                this.SetProperty(ref this.jobId, value);
            }
        }

        

        public async void SubmitClicked(string links) {
            if(this.TitleText == "Content Draft") {
                DraftSubmission submission = new DraftSubmission() {
                    influencerId = Convert.ToInt32(Application.Current.Properties["UserId"].ToString()),
                    jobId = JobId,
                    contentDraft = links
                };

                try {
                    await service.SubmitDraft(submission);
                    try {
                        ChangeJobProgressRequest request = new ChangeJobProgressRequest() {
                            influencerId = Convert.ToInt32(Application.Current.Properties["UserId"].ToString()),
                            jobId = JobId,
                            progressStatus = JobProgressStatus.DraftSubmitted
                        };
                        await service.ChangeJobProgress(request);
                    }
                    catch (Exception) {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                    }
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }

                
            }
            else if (this.TitleText == "Proof of Work") {
                PoWSubmission submission = new PoWSubmission() {
                    influencerId = Convert.ToInt32(Application.Current.Properties["UserId"].ToString()),
                    jobId = JobId,
                    proofOfWork = links
                };
                try {
                    await service.SubmitPoW(submission);
                    try {
                        ChangeJobProgressRequest request = new ChangeJobProgressRequest() {
                            influencerId = Convert.ToInt32(Application.Current.Properties["UserId"].ToString()),
                            jobId = JobId,
                            progressStatus = JobProgressStatus.ProofSubmitted
                        };
                        await service.ChangeJobProgress(request);
                    }
                    catch (Exception) {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                    }

                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }

                
            }

            await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Submission Success."));
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PopAsync();

        }

    }
}
