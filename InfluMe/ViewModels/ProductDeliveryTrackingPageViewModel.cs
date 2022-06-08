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


        #endregion

        public ProductDeliveryTrackingPageViewModel(JobAppliedResponse selected) {
            this.Job = selected;
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());

        }

        public double ProgressValue { get; set; }

        /// <summary>
        /// Gets or sets the step status.
        /// </summary>
        public StepStatus StepStatus { get; set; }
        public DeliveryData Data { get; set; }

        private List<DelivProg> progresses;
        public List<DelivProg> Progresses {
            get {
                return this.progresses;
            }
            set {
                this.SetProperty(ref this.progresses, value);
            }
        }

        public ObservableCollection<DelivProg> ProgressesCollection { get; set; }

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


        public async void InitializeProperties() {

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
    }
}
