
using System.Threading.Tasks;

namespace InfluMe.Validators
{
    /// <summary>
    /// this interface used for validation 
    /// </summary>
    /// <typeparam name="T">The validation rule parameter</typeparam>
    public interface IValidationRuleAsync<T>
    {
        #region Property

        /// <summary>
        /// Gets or sets the Validation Message.
        /// </summary>
        string ValidationMessage { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// method for check the email
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>returns bool value</returns>
        Task<bool> Check(T value);

        #endregion
    }
}