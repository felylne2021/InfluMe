using InfluMe.DataService;
using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Validators;
using InfluMe.Validators.Rules;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for add contact page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AddJobPageViewModel : BaseViewModel {
        #region Fields
        private DateTime today = DateTime.Now;
        private DateTime tomorrow = DateTime.Now.AddDays(1);

        private string date = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        private string fee = "0.00";
        private string jobDeadline = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
        private string imageBlob;
        private bool hasContentApproval;

        private JobDataService service => new JobDataService();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="AddJobPageViewModel" /> class.
        /// </summary>
        public AddJobPageViewModel() {
            this.InitializeProperties();
            this.AddValidationRules();
            this.SubmitCommand = new Command(this.SubmitButtonClicked);
        }

        #endregion

        #region Property

        public DateTime Today {
            get {
                return this.today;
            }

            set {
                if (this.today == value) {
                    return;
                }

                this.SetProperty(ref this.today, value);
                OnPropertyChanged();
            }
        }

        public DateTime Tomorrow {
            get {
                return this.tomorrow;
            }

            set {
                if (this.tomorrow == value) {
                    return;
                }

                this.SetProperty(ref this.tomorrow, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the first name from user in the Add Contact page.
        /// </summary>
        public ValidatableObject<string> JobName { get; set; }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the last name from user in the Add Contact page.
        /// </summary>
        public ValidatableObject<string> Brand { get; set; }

        /// <summary>
        /// Gets or sets the property that bounds with a date picker that gets the date from user in the Add Contact page.
        /// </summary>
        public string RegistrationDeadline {
            get {
                return this.date;
            }

            set {
                if (this.date == value) {
                    return;
                }

                this.SetProperty(ref this.date, value);
            }
        }

        public string JobDeadline {
            get {
                return this.jobDeadline;
            }

            set {
                if (this.jobDeadline == value) {
                    return;
                }

                this.SetProperty(ref this.jobDeadline, value);
            }
        }

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

        /// <summary>
        /// Gets or sets the property that bounds with a combo box that gets the gender from user in the Add Contact page.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the Domicile comma separated values.
        /// </summary>
        public string Domicile { get; set; }

        /// <summary>
        /// Gets or sets the AgeRange for the job.
        /// </summary>
        public string AgeRange { get; set; }

        public bool HasContentApproval {
            get => hasContentApproval;
            set {
                hasContentApproval = value;
                OnPropertyChanged();
            }
        }

        public string Fee {
            get {
                return this.fee;
            }

            set {
                if (this.fee == value) {
                    return;
                }

                this.SetProperty(ref this.fee, value);
            }
        }
        /// <summary>
        /// Gets or sets the AgeRange for the job.
        /// </summary>
        public string SOW { get; set; }

        /// <summary>
        /// Gets or sets the AgeRange for the job.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the Additional Requirements (like ER, min. followers, niche) for the job.
        /// </summary>
        public string AdditionalRequirements { get; set; }

        #endregion

        #region Comments

        /// <summary>
        /// Gets or sets the command that will be executed when the submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// check the fields are valid or not
        /// </summary>
        /// <returns>returns bool value</returns>
        public bool AreFieldsValid() {
            bool isJobNameValid = this.JobName.Validate();
            bool isBrandNameValid = this.Brand.Validate();
            return isJobNameValid && isBrandNameValid;
        }

        /// <summary>
        /// Intialize the methods for validation
        /// </summary>
        private void InitializeProperties() {
            this.JobName = new ValidatableObject<string>() { Value = "" };
            this.Brand = new ValidatableObject<string>() { Value = "" };
            this.ImageBlob = "upload";
            this.Platform = null;
            this.Gender = null;
            this.SOW = "";
            this.AdditionalRequirements = "";
            this.Product = "";
            this.AgeRange = "";
            this.HasContentApproval = false;
            this.Domicile = "";
        }

        /// <summary>
        /// Validation Rules for firstname and lastname
        /// </summary>
        private void AddValidationRules() {
            this.JobName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Job Name Required" });
            this.Brand.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Brand Name Required" });
        }

        /// <summary>
        /// Invoked when the submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SubmitButtonClicked(object obj) {
            if (this.AreFieldsValid()) {
                // Do Something
                JobRequest req = new JobRequest() {
                    jobImageBlob = ImageBlob,
                    jobName = JobName.Value,
                    jobBrand = Brand.Value,
                    jobRegistrationDeadline = DateTime.ParseExact(RegistrationDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                    jobDeadline = DateTime.ParseExact(JobDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                    jobAgeRange = AgeRange ?? "Any Age"
                    jobStatus = JobStatus.OPEN.ToString(),
                    jobGender = Gender ?? "Any Gender",
                    jobPlatform = Platform ?? "Both",
                    jobDomicile = String.IsNullOrEmpty(Domicile) ? "Anywhere" : Domicile,
                    jobFee = Fee,
                    jobProduct = String.IsNullOrEmpty(Product) ? "No Product" : Product,
                    jobSOW = SOW,
                    jobAdditionalRequirement = AdditionalRequirements,
                    hasContentApproval = HasContentApproval ? "true" : "false"
                };
                try {
                    JobResponse resp = await service.AddJob(req);
                    InitializeProperties();
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Add Job Success"));
                }
                catch (Exception) {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
                }
            }
        }

       

        #endregion
    }
}
