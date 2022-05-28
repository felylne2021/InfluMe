using Xamarin.Forms.Internals;
using System.ComponentModel.DataAnnotations;
using InfluMe.Services;
using System.Threading.Tasks;

namespace InfluMe.Validators.Rules
{
    /// <summary>
    /// Validation rule for check the email has empty or null.
    /// </summary>
    /// <typeparam name="T">Not null or empty rule parameter</typeparam>
    [Preserve(AllMembers = true)]
    public class IsTTExists<T> : IValidationRuleAsync<T>
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
        public async Task<bool> Check(T value)
        {
            InfluMeService service = new InfluMeService();
            
            bool TT = await service.GetTikTok(value.ToString());
            return TT;
        }

        #endregion
    }
}
