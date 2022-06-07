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
    public class InputPaymentViewModel : BaseViewModel {

        private JobDataService service = new JobDataService();
        private string imageBlob;


        public InputPaymentViewModel() {
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.SaveCommand = new Command(SaveClicked);
        }

        public JobAppliedResponse Selected { get; set; }

        public string ImageBlob {
            get {
                return this.imageBlob;
            }

            set {
                if (this.imageBlob == value) {
                    return;
                }

                this.SetProperty(ref this.imageBlob, value);
            }
        }

        private async void SaveClicked() {
            SubmitPayment req = new SubmitPayment();

            req.influencerId = Selected.influencerId;
            req.jobId = Selected.job.jobId;
            req.progressStatus = JobProgressStatus.Completed.ToString();
            req.paymentAmount = Selected.job.jobFee;
            req.paymentDate = DateTime.Now.ToString("yyyy-MM-dd");
            req.progressStatus = PaymentStatusList.PAID.ToString();
            req.paymentProof = this.ImageBlob;

            try {
                await service.SubmitPaymentProof(req);
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Payment Proof Submitted"));
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
