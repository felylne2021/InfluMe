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
using System;
using System.Linq;

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
            this.OTP = "";
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
                try {
                    LoginResponse resp = await service.Login(this.Email.Value, this.Password.Value);
                    bool isLoginValid = resp.status.Equals(ResponseStatusEnum.VALID.ToString()) ? true : false;

                    if (isLoginValid) {
                        var previousPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
                        Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                        Application.Current.Properties["UserId"] = resp.userId.ToString();
                        Application.Current.Properties["UserType"] = resp.userType;

                        if (resp.userType.Equals(UserTypeEnum.Influencer.ToString()))
                            await Application.Current.MainPage.Navigation.PushAsync(new HomePage(resp.userId.ToString()));
                        else if (resp.userType.Equals(UserTypeEnum.Admin.ToString())) 
                            await Application.Current.MainPage.Navigation.PushAsync(new AdminHomePage());

                        Application.Current.MainPage.Navigation.RemovePage(previousPage);
                        
                    }
                    else {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new InvalidCredentialPopupPage());
                    }
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
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
                try {
                    OTPResponse resp = await service.GetOTP(this.Email.Value);
                    if (resp.otpStatus.Equals(ResponseStatusEnum.REGISTERED.ToString())) {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EmailRegisteredPopupPage());
                    }
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }
            }

        }

        private async void VerifyEmailClicked(object obj) {
            if (IsEmailValid()) {
                try {
                    OTPResponse resp = await service.GetOTP(this.Email.Value);
                    if (resp.otpStatus.Equals(ResponseStatusEnum.REGISTERED.ToString())) {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EmailRegisteredPopupPage());
                    }
                    else
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EmailOTPPopupPage(this.Email.Value));
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }
            }

        }

        private async void SubmitOTPClicked(string email) {
            if (!string.IsNullOrEmpty(this.OTP) && this.OTP.Length == 6) {
                try {
                    OTPVerificationResponse resp = await service.GetOTPVerification(email, OTP);
                    if (resp.body.Equals(ResponseStatusEnum.VALID.ToString())) {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage(email));
                    }
                    else if (resp.body.Equals(ResponseStatusEnum.INVALID.ToString())) {
                        IsOTPErrorMessageVisible = true;
                    }
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }
            }
            else
                IsOTPErrorMessageVisible = true;

        }

        #endregion
    }
}