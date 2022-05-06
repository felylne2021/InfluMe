﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace InfluMe.Models
{
    /// <summary>
    /// Model for pages with product.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Job : INotifyPropertyChanged
    {
        #region Fields

        private bool isFavourite;

        private string previewImage;

        private List<string> previewImages;

        private int totalQuantity;


        private ObservableCollection<Review> reviews = new ObservableCollection<Review>();

        #endregion

        #region Event

        /// <summary>
        /// The declaration of property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public int jobId { get; set; }
        public string jobName { get; set; }
        public string jobBrand { get; set; }
        public string jobRegistrationDeadline { get; set; }
        public string jobDeadline { get; set; }
        public string jobAgeRange { get; set; }
        public string jobGender { get; set; }
        public string jobPlatform { get; set; }
        public string jobDomicile { get; set; }
        public string jobFee { get; set; }
        public string jobProduct { get; set; }
        public string jobSOW { get; set; }
        public string jobAdditionalRequirement { get; set; }
        public string jobStatus { get; set; }
        /// <summary>
        /// Gets or sets the property that holds the product id.
        /// </summary>
        [DataMember(Name = "jobId")]
        public int Id { get; set; }
        

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays the product name.
        /// </summary>
        [DataMember(Name = "jobName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays the product summary.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays the product description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays the job fee.
        /// </summary>
        

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the customer review.
        /// </summary>
        [DataMember(Name = "reviews")]
        public ObservableCollection<Review> Reviews
        {
            get
            {
                return this.reviews;
            }

            set
            {
                this.reviews = value;
                this.NotifyPropertyChanged(nameof(this.Reviews));
            }
        }

        /// <summary>
        /// Gets or sets the property has been bound with SfCombobox which displays selected quantity of product.
        /// </summary>
        [DataMember(Name = "quantities")]
        public List<object> Quantities { get; set; } = new List<object> { 1, 2, 3, 4, 5 };

        /// <summary>
        /// Gets or sets the property that has been bound with SfCombobox, which displays the product variants.
        /// </summary>
        [DataMember(Name = "sizevariants")]
        public List<string> SizeVariants { get; set; } = new List<string> { "XS", "S", "M", "L", "XL" };

        /// <summary>
        /// Gets or sets a value indicating whether the cart is favorite.
        /// </summary>
        [DataMember(Name = "isfavourite")]
        public bool IsFavourite
        {
            get
            {
                return this.isFavourite;
            }

            set
            {
                this.isFavourite = value;
                this.NotifyPropertyChanged(nameof(this.IsFavourite));
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with SfCombobox, which displays the total quantity.
        /// </summary>
        [DataMember(Name = "totalquantity")]
        public int TotalQuantity
        {
            get
            {
                return this.totalQuantity;
            }

            set
            {
                this.totalQuantity = value;
                this.NotifyPropertyChanged(nameof(this.TotalQuantity));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}