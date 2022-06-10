using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for ProductDeliveryTracking page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProductDeliveryTrackingPageViewModel : BaseViewModel {
        #region Fields
        private JobAppliedResponse job;

        private InfluMeService service => new InfluMeService();
        private JobDataService jobService => new JobDataService();
        private List<DelivProg> progresses;
        private DeliveryData data;
        private ObservableCollection<DelivProg> progressesCollection;


        #endregion

        public ProductDeliveryTrackingPageViewModel(JobAppliedResponse selected) {
            this.Job = selected;
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.ReceivedCommand = new Command(Receive);
        }

        public double ProgressValue { get; set; }

        /// <summary>
        /// Gets or sets the step status.
        /// </summary>
        public StepStatus StepStatus { get; set; }
        public DeliveryData Data {
            get {
                return this.data;
            }
            set {
                this.SetProperty(ref this.data, value);
            }
        }

        public List<DelivProg> Progresses {
            get {
                return this.progresses;
            }
            set {
                this.SetProperty(ref this.progresses, value);
            }
        }

        public ObservableCollection<DelivProg> ProgressesCollection {
            get {
                return this.progressesCollection;
            }
            set {
                if (this.progressesCollection == value) {
                    return;
                }
                this.SetProperty(ref this.progressesCollection, value);
            }
        }

        public class DelivProg {
            public string date { get; set; }
            public string status { get; set; }
            public StepStatus stepStatus { get; set; }
            public int progressVal { get; set; }

        }

        private static string Wrap(string v, int size) {
            v = v.TrimStart();
            if (v.Length <= size) return v;
            var nextspace = v.LastIndexOf(' ', size);
            if (-1 == nextspace) nextspace = Math.Min(v.Length, size);
            return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
            "" : "\n" + Wrap(v.Substring(nextspace), size));
        }


        private async void InitializeProperties() {

            try {
                this.Data = await jobService.TrackDelivery(this.Job.delivery.deliveryCompany, this.Job.delivery.deliveryReceipt);
                var status = this.Data.summary.status;

                this.Progresses = new List<DelivProg>();

                foreach (History history in Data.history) {

                    this.Progresses.Add(new DelivProg() {
                        date = history.date,
                        status = Wrap(history.desc, 30),
                        stepStatus = StepStatus.Completed,
                        progressVal = 100
                    });


                }

                this.ProgressesCollection = new ObservableCollection<DelivProg>(Progresses);

            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        private async void Receive() {
            try {
                ChangeJobProgressRequest req = new ChangeJobProgressRequest();
                req.influencerId = Job.influencerId;
                req.jobId = Job.job.jobId;
                req.progressStatus = Job.job.hasContentApproval.ToLower().Equals("true") ? JobProgressStatus.PendingDraft : JobProgressStatus.PendingProof;

                await jobService.ChangeJobProgress(req);
                await Application.Current.MainPage.Navigation.PopAsync();

                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Product Accepted"));
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        #region Properties

        public JobAppliedResponse Job {
            get {
                return this.job;
            }

            set {
                this.SetProperty(ref this.job, value);
            }
        }


        #endregion

        public Command BackButtonCommand { get; set; }
        public Command ReceivedCommand { get; set; }
    }
}
