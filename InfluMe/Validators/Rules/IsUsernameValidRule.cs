using Xamarin.Forms.Internals;
using System.ComponentModel.DataAnnotations;

namespace InfluMe.Validators.Rules
{
    /// <summary>
    /// Validation rule for check the email has empty or null.
    /// </summary>
    /// <typeparam name="T">Not null or empty rule parameter</typeparam>
    [Preserve(AllMembers = true)]
    public class IsUsernameValidRule<T> : IValidationRule<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the validation Message.
        /// </summary>
        public string ValidationMessage { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Check the Email format
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>returns bool value</returns>
        public bool Check(T value)
        {
            if(value == null)
                return false;
            return !value.ToString().StartsWith("@");
        }

        #endregion
    }
}
