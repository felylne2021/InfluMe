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

        private JobDataService service => new JobDataService();
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="JobDetailPageViewModel" /> class.
        /// </summary>
        public JobDetailPageViewModel()
        {
            this.InitializeProperties();
        }

        #endregion

        #region Public properties
        public string JobId { get; set; }

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
                this.Job = await service.GetJobById(JobId);
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            
        }

        #endregion

    }
}
