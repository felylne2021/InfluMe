using InfluMe.DataService;
using InfluMe.Models;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class JobDetailPageViewModel : BaseViewModel
    {
        #region Fields
        private JobResponse job;
        private string currentJobId;

        private JobDataService service => new JobDataService();
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="JobDetailPageViewModel" /> class.
        /// </summary>
        public JobDetailPageViewModel(string jobId)
        {
            this.CurrentJobId = jobId;
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
        }

        #endregion

        #region Public properties
        public string CurrentJobId {
            get {
                return this.currentJobId;
            }

            set {
                if (this.currentJobId == value) {
                    return;
                }

                this.SetProperty(ref this.currentJobId, value);
            }
        }

        public JobResponse Job {
            get {
                return this.job;
            }

            set {
                if (this.job == value) {
                    return;
                }

                this.SetProperty(ref this.job, value);
            }
        }

        #endregion

        #region Methods

        private async void InitializeProperties() {            

            try {
                this.Job = await service.GetJobById(this.CurrentJobId);
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            
        }

        #endregion

        #region Commands
        public Command BackButtonCommand { get; set; }
        #endregion

    }
}
