using InfluMe.Models;
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

        private static ViewProfilePageViewModel healthProfileViewModel;

        /// <summary>
        /// To store the health profile data collection.
        /// </summary>
        private string profileImage;

        private ObservableCollection<HealthProfile> cardItems;

        private Command<object> itemTappedCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ViewProfilePageViewModel" /> class.
        /// </summary>
        public ViewProfilePageViewModel() {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of health profile view model.
        /// </summary>
        public static ViewProfilePageViewModel BindingContext =>
            healthProfileViewModel = PopulateData<ViewProfilePageViewModel>("profile.json");

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
        /// Gets or sets the bio.
        /// </summary>
        [DataMember(Name = "bio")]
        public string Bio { get; set; }      

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

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand {
            get {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        #endregion

        #region Methods

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

        /// <summary>
        /// Invoked when an item is selected from the health profile page.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem) {
            // Do something
        }

        #endregion
    }
}