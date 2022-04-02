﻿using InfluMe.ViewModels;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace InfluMe.DataService {
    /// <summary>
    /// Data service for contacts page to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactsDataService {
        #region fields 

        private static ContactsDataService contactsDataService;

        private ManageInfluencerPageViewModel contactsViewModel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="ContactsDataService"/>.
        /// </summary>
        public static ContactsDataService Instance => contactsDataService ?? (contactsDataService = new ContactsDataService());

        /// <summary>
        /// Gets or sets the value of contacts page view model.
        /// </summary>
        public ManageInfluencerPageViewModel ManageInfluencerPageViewModel =>
            this.contactsViewModel ??
            (this.contactsViewModel = PopulateData<ManageInfluencerPageViewModel>("navigation.json"));

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName) {
            var file = "InfluMe.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file)) {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        #endregion
    }
}