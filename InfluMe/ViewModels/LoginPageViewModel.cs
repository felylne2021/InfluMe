using InfluMe.Validators;
using InfluMe.Validators.Rules;
using InfluMe.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using FreshMvvm;
using FreshMvvm.Popups;
using Rg.Plugins.Popup.Extensions;
using InfluMe.Services;
using InfluMe.Models;
using InfluMe.Helpers;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : BaseViewModel {
        #region Fields

        private ValidatableObject<string> password;
        private ValidatableObject<string> email;
        private bool _isOTPErrorMessageVisible;
        private InfluMeService service => new InfluMeService();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel() {
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.BackPopupButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopPopupAsync());
            this.VerifyEmailCommand = new Command(this.VerifyEmailClicked);
            this.ResendOTPCommand = new Command(this.ResendOTPClicked);
            this.SubmitOTPCommand = new Command<string>(this.SubmitOTPClicked);
            this.SetOTPErrorInvisible = new Command(_ => IsOTPErrorMessageVisible = false);
        }

        #endregion

        #region property
        public string OTP { get; set; }

        public bool IsOTPErrorMessageVisible {
            get => _isOTPErrorMessageVisible;
            set {
                _isOTPErrorMessageVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password {
            get {
                return this.password;
            }

            set {
                if (this.password == value) {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        public ValidatableObject<string> Email {
            get {
                return this.email;
            }

            set {
                if (this.email == value) {
                    return;
                }

                this.SetProperty(ref this.email, value);
            }
        }


        #endregion

        #region Command

        ///// <summary>
        ///// Gets or sets the command that is executed when the Log In button is clicked.
        ///// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        public Command BackButtonCommand { get; set; }
        public Command BackPopupButtonCommand { get; set; }

        public Command VerifyEmailCommand { get; set; }

        public Command ResendOTPCommand { get; set; }

        public Command SubmitOTPCommand { get; set; }

        public Command SetOTPErrorInvisible { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Check the login credentials
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid() {
            bool isEmailValid = this.Email.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isEmailValid && isPasswordValid;
        }

        public bool IsEmailValid() {
            return this.Email.Validate();
        }

        public bool IsPasswordValid() {
            return this.Password.Validate();
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties() {
            this.Email = new ValidatableObject<string>();
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for login
        /// </summary>
        private void AddValidationRules() {
            this.Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email Required" });
            this.Email.Validations.Add(new IsEmailValidRule<string> { ValidationMessage = "Email Format Invalid" });
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj) {
            if (this.AreFieldsValid()) {
                LoginResponse resp = await service.Login(this.Email.Value, this.Password.Value);
                bool isLoginValid = resp.status.Equals(ResponseStatusEnum.VALID.ToString()) ? true : false;

                if (isLoginValid) {
                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
                else {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new InvalidCredentialPopupPage());
                }
            }


        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj) {
            await Application.Current.MainPage.Navigation.PushAsync(new EmailSignUpPage());
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj) {
            // Do something
        }

        private async void ResendOTPClicked(object obj) {
            if (IsEmailValid()) {
                OTPResponse resp = await service.GetOTP(this.Email.Value);
            }

        }

        private async void VerifyEmailClicked(object obj) {
            if (IsEmailValid()) {
                OTPResponse resp = await service.GetOTP(this.Email.Value);
                await Application.Current.MainPage.Navigation.PushPopupAsync(new EmailOTPPopupPage(this.Email.Value));
            }

        }

        private async void SubmitOTPClicked(string email) {
            if (this.OTP.Length == 6) {
                OTPVerificationResponse resp = await service.GetOTPVerification(email, OTP);
                if (resp.body.Equals(ResponseStatusEnum.VALID.ToString())) {
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage(email));
                }
                else if (resp.body.Equals(ResponseStatusEnum.INVALID.ToString())) {
                    IsOTPErrorMessageVisible = true;
                }
            }
            else 
                IsOTPErrorMessageVisible = true;

        }

        #endregion
    }
}