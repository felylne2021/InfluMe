using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
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

        private string imageBlob = "";
        private InfluMeService service = new InfluMeService();
        private InfluencerResponse influencer;
        private JobStatsResponse jobStats;
        private bool _isOTPErrorMessageVisible;
        private int completedJobs;
        private int ongoingJobs;
        private string influencerAgeSex;
        private string maskedEmail;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ViewProfilePageViewModel" /> class.
        /// </summary>
        public ViewProfilePageViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.LogOutCommand = new Command(LogOutClicked);
            this.EditProfileCommand = new Command(EditProfileClicked);
            this.SaveChangesCommand = new Command(SaveChanges);
            this.ResendOTPCommand = new Command(this.ResendOTPClicked);
            this.SubmitOTPCommand = new Command(this.SubmitOTPClicked);
            this.SetOTPErrorInvisible = new Command(_ => IsOTPErrorMessageVisible = false);
            this.OTP = "";
        }

        #endregion

        #region Public properties
        public string OTP { get; set; }

        public string ImageBlob {
            get {
                return this.imageBlob;
            }

            set {
                if (this.imageBlob == value) {
                    return;
                }

                this.SetProperty(ref this.imageBlob, value);
            }
        }

        public bool IsOTPErrorMessageVisible {
            get => _isOTPErrorMessageVisible;
            set {
                _isOTPErrorMessageVisible = value;
                OnPropertyChanged();
            }
        }

        public int CompletedJobs {
            get {
                return this.completedJobs;
            }
            set {
                if (this.completedJobs == value) {
                    return;
                }

                this.SetProperty(ref this.completedJobs, value);
            }
        }
        public int OngoingJobs {
            get {
                return this.ongoingJobs;
            }
            set {
                if (this.ongoingJobs == value) {
                    return;
                }

                this.SetProperty(ref this.ongoingJobs, value);
            }
        }

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

        public string MaskedEmail {
            get {
                return this.maskedEmail;
            }
            set {
                if (this.maskedEmail == value) {
                    return;
                }

                this.SetProperty(ref this.maskedEmail, value);
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

        public JobStatsResponse JobStats {
            get {
                return this.jobStats;
            }

            set {
                if (this.jobStats == value) {
                    return;
                }

                this.SetProperty(ref this.jobStats, value);
            }
        }

        #endregion


        #region Commands
        public Command BackButtonCommand { get; set; }
        public Command LogOutCommand { get; set; }
        public Command EditProfileCommand { get; set; }
        public Command SaveChangesCommand { get; set; }
        public Command ResendOTPCommand { get; set; }
        public Command SubmitOTPCommand { get; set; }
        public Command SetOTPErrorInvisible { get; set; }

        #endregion

        #region Methods

        private async void InitializeProperties() {

            try {
                this.Influencer = await service.GetInfluencerById(Application.Current.Properties["UserId"].ToString());

                this.JobStats = await service.GetInfluencerStats(Application.Current.Properties["UserId"].ToString());


                this.OngoingJobs = JobStats.appliedJobList.Count(x => !x.progressStatus.Equals(JobProgressStatus.Applied) && !x.progressStatus.Equals(JobProgressStatus.NotApproved) && !x.progressStatus.Equals(JobProgressStatus.Completed));
                this.CompletedJobs = JobStats.appliedJobList.Count(x => x.progressStatus.Equals(JobProgressStatus.Completed));

            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
            if (this.Influencer.bankAccountNumber == null)
                this.Influencer.bankAccountNumber = "";

            if (this.Influencer.appliedJobList == null)
                this.Influencer.appliedJobList = new List<JobResponse>();

            this.Influencer.influencerDOB = DateTime.ParseExact(this.Influencer.influencerDOB, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            this.InfluencerAgeSex = CalculateAge(this.Influencer.influencerDOB) + " y.o | " + Influencer.influencerGender;
            this.Influencer.influencerPassword = "";
            var email = this.Influencer.influencerEmail;
            this.MaskedEmail = string.Format("{0}*****{1}", email[0], email.Substring(email.IndexOf('@') - 1));
        }

        private string CalculateAge(string birthdate) {
            // Save today's date.
            var today = DateTime.Today;
            var Birthdate = DateTime.ParseExact(birthdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            // Calculate the age.
            var age = today.Year - Birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (Birthdate.Date > today.AddYears(-age)) age--;
            return age.ToString();
        }


        private async void LogOutClicked(object obj) {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties["UserId"] = "";
            Application.Current.Properties["UserType"] = "";
            await App.Current.SavePropertiesAsync();
            Application.Current.MainPage = new NavigationPage(new MainLoginPage());

        }

        private async void EditProfileClicked() {
            await Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage(this));
        }

        private async void SaveChanges() {
            try {
                this.OTP = "";
                OTPResponse resp = await service.GetOTPEditProfile(this.Influencer.influencerEmail);

                await Application.Current.MainPage.Navigation.PushPopupAsync(new EditProfileOTPPopupPage(this));
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        private async void ResendOTPClicked(object obj) {
            try {
                OTPResponse resp = await service.GetOTPEditProfile(this.Influencer.influencerEmail);
            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        private async void SubmitOTPClicked() {
            if (!string.IsNullOrEmpty(this.OTP) && this.OTP.Length == 6) {
                try {
                    OTPVerificationResponse resp = await service.GetOTPVerification(this.Influencer.influencerEmail, OTP);
                    if (resp.body.Equals(ResponseStatus.VALID.ToString())) {
                        InfluencerUpdateRequest req = new InfluencerUpdateRequest() {
                            influencerColorHex = this.ImageBlob,
                            influencerId = this.Influencer.influencerId.ToString(),
                            influencerEmail = this.Influencer.influencerEmail,
                            influencerAddress = this.Influencer.influencerAddress,
                            influencerPassword = this.Influencer.influencerPassword,
                            influencerInstagramId = this.Influencer.influencerInstagramId,
                            influencerTiktokId = this.Influencer.influencerTiktokId,
                            whatsappNumber = this.Influencer.whatsappNumber,
                            bankName = this.Influencer.bankName,
                            bankAccountNumber = this.Influencer.bankAccountNumber
                        };
                        await service.UpdateProfile(req);
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Application.Current.MainPage.Navigation.PopPopupAsync();

                        await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Update Profile Success"));
                        this.InitializeProperties();
                    }
                    else if (resp.body.Equals(ResponseStatus.INVALID.ToString())) {
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