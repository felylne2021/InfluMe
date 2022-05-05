using InfluMe.Validators;
using InfluMe.Validators.Rules;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for add contact page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AddJobPageViewModel : BaseViewModel {
        #region Fields

        private DateTime date = DateTime.Now;
        private string fee = "0.00";

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
        public DateTime RegistrationDeadline { 
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

        public string Fee
        {
            get
            {
                return this.fee;
            }

            set
            {
                if (this.fee == value)
                {
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
            bool isFirstNameValid = this.JobName.Validate();
            bool isLastNameValid = this.Brand.Validate();
            return isFirstNameValid && isLastNameValid;
        }

        /// <summary>
        /// Intialize the methods for validation
        /// </summary>
        private void InitializeProperties() {
            this.JobName = new ValidatableObject<string>();
            this.Brand = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation Rules for firstname and lastname
        /// </summary>
        private void AddValidationRules() {
            this.JobName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name Required" });
            this.Brand.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name Required" });
        }

        /// <summary>
        /// Invoked when the submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SubmitButtonClicked(object obj) {
            if (this.AreFieldsValid()) {
                // Do Something
            }
        }

        #endregion
    }
}
