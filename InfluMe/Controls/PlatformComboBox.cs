using InfluMe.Models;
using Syncfusion.XForms.ComboBox;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.Controls {
    [Preserve(AllMembers = true)]
    public class PlatformComboBox : SfComboBox, INotifyPropertyChanged {
        #region Fields


        
        /// <summary>
        /// Gets or sets string property that represents mask format for phone number. 
        /// </summary>
        private string mask = string.Empty;

        #endregion

        #region Constructor

        public PlatformComboBox() {
            this.BindingContext = this;

            this.Platforms = new List<PlatformModel>();
            this.Platforms.Add(new PlatformModel() {
                Platform = "Instagram"              
            });
            this.Platforms.Add(new PlatformModel() {
                Platform = "TikTok"
            });
            this.Platforms.Add(new PlatformModel() {
                Platform = "Both"
            });

            this.DataSource = this.Platforms;
            this.SetBinding(SfComboBox.SelectedItemProperty, "Platform", BindingMode.TwoWay);
            this.DisplayMemberPath = "Platform";
            this.Watermark = "Select Platform";
            this.VerticalOptions = LayoutOptions.CenterAndExpand;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

      

        

        /// <summary>
        /// Gets or sets array collection that contains states based on country selection. 
        /// </summary>        

        public string Mask {
            get {
                return this.mask;
            }

            set {
                this.mask = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the collection property, which contains the countries data. 
        /// </summary>
        public List<PlatformModel> Platforms { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method used to rest State and City and update PhoneNumber format. 
        /// </summary>
        

        #endregion
    }
}