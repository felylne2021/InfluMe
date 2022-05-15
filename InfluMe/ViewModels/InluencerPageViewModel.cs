using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for contacts page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class InfluencerPageViewModel : BaseViewModel {
        #region Fields

        private ObservableCollection<InfluencerResponse> influencerList;
        private InfluMeService service => new InfluMeService();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="InfluencerPageViewModel"/> class.
        /// </summary>
        public InfluencerPageViewModel() {
            this.InitializeProperties();
        }

        #endregion

        #region Properties

        public ObservableCollection<InfluencerResponse> InfluencerList {
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

        #endregion

        #region Methods

        private async void InitializeProperties() {
            
            try {
                this.InfluencerList = new ObservableCollection<InfluencerResponse>( await service.GetInfluencers());
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
           


        }

        #endregion
    }
}