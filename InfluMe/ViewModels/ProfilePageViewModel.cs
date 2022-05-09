using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Validators;
using InfluMe.Validators.Rules;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfilePageViewModel : BaseViewModel {
        #region Fields
        private ValidatableObject<string> influencerPassword;
        private ValidatableObject<string> influencerName;
        private ValidatableObject<string> influencerGender;
        private ValidatableObject<string> influencerAddress;
        private ValidatableObject<string> influencerInstagramId;
        private ValidatableObject<string> influencerTiktokId;

        private InfluMeService service = new InfluMeService();
        private bool _isPasswordConfirmationErrorMessageVisible;
        private bool _isBirthdateErrorMessageVisible;

        private DateTime influencerDOB = DateTime.Now;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class
        /// </summary>
        public ProfilePageViewModel() {
            this.InitializeProperties();
            this.AddValidationRules();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.EditProfileCommand = new Command(this.EditButtonClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.NotificationCommand = new Command(this.NotificationOptionClicked);
            this.SetPasswordConfirmationErrorInvisible = new Command(_ => IsPasswordConfirmationErrorMessageVisible = false);
        }

        #endregion

        #region Property

        public string influencerEmail { get; set; }
        
        public string passwordConfirmation { get; set; }

        public DateTime InfluencerDOB {
            get {
                return this.influencerDOB;
            }

            set {
                if (this.influencerDOB == value) {
                    return;
                }

                this.SetProperty(ref this.influencerDOB, value);
                OnPropertyChanged();
            }
        }

        public bool IsPasswordConfirmationErrorMessageVisible {
            get => _isPasswordConfirmationErrorMessageVisible;
            set {
                _isPasswordConfirmationErrorMessageVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsBirthdateErrorMessageVisible {
            get => _isBirthdateErrorMessageVisible;
            set {
                _isBirthdateErrorMessageVisible = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> InfluencerPassword {
            get {
                return this.influencerPassword;
            }

            set {
                if (this.influencerPassword == value) {
                    return;
                }

                this.SetProperty(ref this.influencerPassword, value);
            }
        }
        public ValidatableObject<string> InfluencerName {
            get {
                return this.influencerName;
            }

            set {
                if (this.influencerName == value) {
                    return;
                }

                this.SetProperty(ref this.influencerName, value);
            }
        }
        public ValidatableObject<string> InfluencerGender {
            get {
                return this.influencerGender;
            }

            set {
                if (this.influencerGender == value) {
                    return;
                }

                this.SetProperty(ref this.influencerGender, value);
            }
        }
        public ValidatableObject<string> InfluencerAddress {
            get {
                return this.influencerAddress;
            }

            set {
                if (this.influencerAddress == value) {
                    return;
                }

                this.SetProperty(ref this.influencerAddress, value);
            }
        }
        public ValidatableObject<string> InfluencerInstagramId {
            get {
                return this.influencerInstagramId;
            }

            set {
                if (this.influencerInstagramId == value) {
                    return;
                }

                this.SetProperty(ref this.influencerInstagramId, value);
            }
        }
        public ValidatableObject<string> InfluencerTiktokId {
            get {
                return this.influencerTiktokId;
            }

            set {
                if (this.influencerTiktokId == value) {
                    return;
                }

                this.SetProperty(ref this.influencerTiktokId, value);
            }
        }


        #endregion

        #region Command

        public Command BackButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the available status is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the notification option is clicked.
        /// </summary>
        public Command NotificationCommand { get; set; }

        public Command SetPasswordConfirmationErrorInvisible { get; set; }

        #endregion

        #region Methods

        public bool AreFieldsValid() {
            bool isPasswordValid = this.InfluencerPassword.Validate();
            bool isNameValid = this.InfluencerName.Validate();
            bool isGenderValid = this.InfluencerGender.Validate();
            bool isAddressValid = this.InfluencerAddress.Validate();
            bool isInstagramIdValid = this.InfluencerInstagramId.Validate();
            bool isTiktokIdValid = this.InfluencerTiktokId.Validate();

            var today = DateTime.Today;
            var age = today.Year - this.InfluencerDOB.Year;
            if (this.InfluencerDOB.Date > today.AddYears(-age)) age--;

            this.IsBirthdateErrorMessageVisible = age > 12 ? false : true;

            if (isPasswordValid)
                this.IsPasswordConfirmationErrorMessageVisible = !this.passwordConfirmation.Equals(this.InfluencerPassword.Value);

            return isPasswordValid && isNameValid && isGenderValid && isAddressValid && isInstagramIdValid && isTiktokIdValid && !this.IsBirthdateErrorMessageVisible && !this.IsPasswordConfirmationErrorMessageVisible;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties() {
            this.InfluencerPassword = new ValidatableObject<string>();
            this.InfluencerName = new ValidatableObject<string>();
            this.InfluencerGender = new ValidatableObject<string>();
            this.InfluencerAddress = new ValidatableObject<string>();
            this.InfluencerInstagramId = new ValidatableObject<string>();
            this.InfluencerTiktokId = new ValidatableObject<string>();
        }

        private void AddValidationRules() {

            this.InfluencerPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            this.InfluencerPassword.Validations.Add(new IsLengthValidRule<string> { ValidationMessage = "Password Must be At Least 8 Characters" });
            this.InfluencerName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name Required" });
            this.InfluencerGender.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Gender Required" });
            this.InfluencerAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Address Required" });
            this.InfluencerInstagramId.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Instagram Username Required" });
            this.InfluencerInstagramId.Validations.Add(new IsUsernameValidRule<string> { ValidationMessage = "Username Without '@'" });
            this.InfluencerTiktokId.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Tiktok Username Required" });
            this.InfluencerTiktokId.Validations.Add(new IsUsernameValidRule<string> { ValidationMessage = "Username Without '@'" });
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj) {
            // Do something
        }

        private async void SignUpClicked(object obj) {
            if (AreFieldsValid()) {
                InfluencerRequest signUpRequest = new InfluencerRequest() {
                    influencerEmail = this.influencerEmail,
                    influencerName = this.InfluencerName.Value,
                    influencerPassword = this.InfluencerPassword.Value,
                    influencerGender = this.InfluencerGender.Value,
                    influencerAddress = this.InfluencerAddress.Value,
                    influencerDOB = this.influencerDOB.ToString("yyyy-MM-dd"),
                    influencerInstagramId = this.InfluencerInstagramId.Value,
                    influencerTiktokId = this.InfluencerTiktokId.Value
                };

                try {                    
                    InfluencerResponse resp = await service.SignUp(signUpRequest);
                    await Application.Current.MainPage.Navigation.PopAsync();
                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage(resp.influencerId.ToString()));
                    Application.Current.MainPage.Navigation.RemovePage(new MainLoginPage());
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }
            }
        }

        /// <summary>
        /// Invoked when the notification option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void NotificationOptionClicked(object obj) {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
        }

        #endregion
    }
}
