using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for contacts page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ApproveEnrollmentViewModel : BaseViewModel {
        #region Fields

        private List<InfluencerResponse> influencerList;
        private ObservableCollection<InfluencerResponse> enrolledList;
        private InfluMeService service => new InfluMeService();
        private InfluencerResponse selectedInf;
        private bool isEnrollEmpty;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="InfluencerPageViewModel"/> class.
        /// </summary>
        public ApproveEnrollmentViewModel() {
            this.InitializeProperties();
            this.Deactivate = new Command<int>(DeactivateClicked);
            this.ClosePopupCommand = new Command(_ => Application.Current.MainPage.Navigation.PopPopupAsync());
        }

        #endregion

        #region Properties

       

        public bool IsEnrollEmpty {
            get {
                return this.isEnrollEmpty;
            }

            set {
                if (this.isEnrollEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isEnrollEmpty, value);
            }
        }
        public InfluencerResponse SelectedInf {
            get { return selectedInf; }
            set {
                if (value != null) {
                    selectedInf = value;
                    ItemSelected();
                }
            }
        }


        public List<InfluencerResponse> InfluencerList {
            get {
                return this.influencerList;
            }

            set {
                if (this.influencerList == value) {
                    return;
                }

                this.SetProperty(ref this.influencerList, value);
            }
        }

        public ObservableCollection<InfluencerResponse> EnrolledList {
            get {
                return this.enrolledList;
            }

            set {
                if (this.enrolledList == value) {
                    return;
                }

                this.SetProperty(ref this.enrolledList, value);
            }
        }

       

        #endregion

        #region Methods

        private async void InitializeProperties() {

            try {
                this.InfluencerList = await service.GetInfluencers();
                this.EnrolledList = new ObservableCollection<InfluencerResponse>(InfluencerList.Where(x => x.influencerStatus.Equals(InfluencerStatus.ENROLLED.ToString())));

                this.IsEnrollEmpty = EnrolledList.Count == 0;
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }



        }

        private async void ItemSelected() {
            //await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmationPopupPage(this));
        }

        private async void DeactivateClicked(int influencerId) {
            string newStatus = SelectedInf.influencerStatus.Equals(InfluencerStatus.ACTIVE.ToString()) ? InfluencerStatus.INACTIVE.ToString() : InfluencerStatus.ACTIVE.ToString();
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            try {
                await service.UpdateInfluencerStatus(influencerId.ToString(), newStatus);
                this.InitializeProperties();
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Influencer status updated."));
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        #endregion

        #region commands
        public Command Deactivate { get; set; }
        public Command ClosePopupCommand { get; set; }
        #endregion
    }
}