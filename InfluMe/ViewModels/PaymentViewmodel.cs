using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for Payment page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class PaymentViewModel : BaseViewModel {
        #region Fields

        private JobAppliedResponse job;
        private string imageSource;
        private string jobString;
        private InfluMeService service => new InfluMeService();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="PaymentViewModel"/> class.
        /// </summary>
        public PaymentViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
        }

        #endregion

        #region Properties

        public JobAppliedResponse Job {
            get {
                return this.job;
            }

            set {
                this.SetProperty(ref this.job, value);
            }
        }
        public string ImageSource {
            get { return this.imageSource; }
            set { this.SetProperty(ref this.imageSource, value); }
        }
        public string JobString {
            get { return this.jobString; }
            set { this.SetProperty(ref this.jobString, value); }
        }

        #endregion

        #region Commands

        public Command BackButtonCommand { get; set; }


        #endregion

        #region Methods
        public async void InitializeProperties() {
            try {
                this.Job = await service.GetDummyAppliedJob();
                this.JobString = $"{Job.job.jobName} from {Job.job.jobBrand}";
                var payment = Job.payment;
                if (payment.paymentStatus.Equals("Paid")) {
                    this.ImageSource = "checked";
                }
                else this.ImageSource = "pending";

            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }


        #endregion
    }
}
