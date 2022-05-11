using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for health profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ViewProfilePageViewModel : BaseViewModel {
        #region Fields

        private InfluMeService service = new InfluMeService();
        private InfluencerResponse influencer;
        private string influencerAgeSex;
        private string profileColor;
        /// <summary>
        /// To store the health profile data collection.
        /// </summary>
        private string profileImage;

        private ObservableCollection<HealthProfile> cardItems;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ViewProfilePageViewModel" /> class.
        /// </summary>
        public ViewProfilePageViewModel() {
            this.InitializeProperties();
            this.LogOutCommand = new Command(LogOutClicked);
            this.EditProfileCommand = new Command(EditProfileClicked);
        }

        #endregion

        #region Public properties

        public string InfluencerAgeSex {
            get {
                return this.influencerAgeSex;
            }
            set {
                if (this.influencerAgeSex == value) {
                    return;
                }

                this.SetProperty(ref this.influencerAgeSex, value);
            }
        }

        public string ProfileColor {
            get {
                return this.profileColor;
            }
            set {
                this.profileColor = value;
            }

        }

        public InfluencerResponse Influencer {
            get {
                return this.influencer;
            }

            set {
                if (this.influencer == value) {
                    return;
                }

                this.SetProperty(ref this.influencer, value);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of health profile view model.
        /// </summary>
        //public static ViewProfilePageViewModel BindingContext =>
        //    healthProfileViewModel = PopulateData<ViewProfilePageViewModel>("profile.json");

        /// <summary>
        /// Gets or sets the health profile items collection.
        /// </summary>
        [DataMember(Name = "cardItems")]
        public ObservableCollection<HealthProfile> CardItems {
            get {
                return this.cardItems;
            }

            set {
                this.SetProperty(ref this.cardItems, value);
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        [DataMember(Name = "profileImage")]
        public string ProfileImage {
            get {
                return App.ImageServerPath + this.profileImage;
            }

            set {
                this.profileImage = value;
            }
        }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        [DataMember(Name = "profileName")]
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the completed jobs.
        /// </summary>
        [DataMember(Name = "completedJobs")]
        public string CompletedJobs { get; set; }

        /// <summary>
        /// Gets or sets the ongoing jobs.
        /// </summary>
        [DataMember(Name = "ongoingJobs")]
        public string OngoingJobs { get; set; }

        /// <summary>
        /// Gets or sets the earnings.
        /// </summary>
        [DataMember(Name = "earnings")]
        public string Earnings { get; set; }

       

        #endregion

        #region Commands

        public Command LogOutCommand { get; set; }
        public Command EditProfileCommand { get; set; }

        #endregion

        #region Methods

        private async void InitializeProperties() {

            this.ProfileColor = Helpers.ColorHelper.RandomColor();
            try {
                this.Influencer = await service.GetInfluencerById(Application.Current.Properties["UserId"].ToString());
                
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            this.InfluencerAgeSex = CalculateAge(this.Influencer.influencerDOB) + " y.o | " + Influencer.influencerGender;
        }

        private string CalculateAge(string birthdate) {
            // Save today's date.
            var today = DateTime.Today;
            var Birthdate = DateTime.ParseExact(birthdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            // Calculate the age.
            var age = today.Year - Birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (Birthdate.Date > today.AddYears(-age)) age--;
            return age.ToString();
        }

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName) {
            var file = "InfluMe.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file)) {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        
        private void LogOutClicked(object obj) {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["UserId"] = "";
            Application.Current.Properties["UserType"] = "";
            Application.Current.MainPage = new MainLoginPage();
            //Application.Current.MainPage.Navigation.PopToRootAsync();

        }

        private void EditProfileClicked(object obj)
        {

        }

        #endregion
    }
}