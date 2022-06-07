using InfluMe.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using InfluMe.Models;
using Rg.Plugins.Popup.Extensions;
using InfluMe.Views;

namespace InfluMe.ViewModels {
    public class OngoingJobViewModel : BaseViewModel {

        private InfluMeService service => new InfluMeService();
        private List<JobAppliedResponse> applications;

        public OngoingJobViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
        }

        public async void InitializeProperties() {
            this.Applications = new List<JobAppliedResponse>();
            try {

                //this.Applications = await service.GetDummyAppliedJob();
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }
        public List<JobAppliedResponse> Applications {
            get {
                return this.applications;
            }

            set {
                if (this.applications == value) {
                    return;
                }

                this.SetProperty(ref this.applications, value);
            }
        }

        public Command BackButtonCommand { get; set; }
    }
}
