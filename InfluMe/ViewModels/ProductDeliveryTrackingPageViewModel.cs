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

        private List<DelivProg> progresses;
        public List<DelivProg> Progresses {
            get {
                return this.progresses;
            }
            set {
                this.SetProperty(ref this.progresses, value);
            }
        }

        public ObservableCollection<DelivProg> ProgressesCollection { get; set;}

        public class DelivProg{
            public string status { get; set; }
            public StepStatus stepStatus { get; set; }
            public int progressVal { get; set; }

        }


        

        public async void InitializeProperties() {
            try {
                var status = this.Job.delivery.deliveryStatus;
                
                this.Progresses = new List<DelivProg>();

                foreach (string delivStat in DeliveryStatus.DeliveryStatusList) {
                    
                    if (status.Equals(delivStat, StringComparison.OrdinalIgnoreCase)) {
                        this.Progresses.Add(new DelivProg() {
                            status = delivStat,
                            stepStatus = delivStat == DeliveryStatus.OnShipping || status == DeliveryStatus.OnDelivery ? StepStatus.InProgress : status == DeliveryStatus.Received ? StepStatus.Completed : StepStatus.NotStarted,
                            progressVal = 100
                        });
                        break;
                    }
                    else {
                        this.Progresses.Add(new DelivProg() {
                            status = delivStat,
                            stepStatus = delivStat == DeliveryStatus.OnShipping || status == DeliveryStatus.OnDelivery ? StepStatus.InProgress : status == DeliveryStatus.Received ? StepStatus.Completed : StepStatus.NotStarted,
                            progressVal = 100
                        });
                    }
                        
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
