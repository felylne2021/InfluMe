﻿using InfluMe.ViewModels;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace InfluMe.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CatalogDataService
    {
        #region fields

        private static CatalogDataService catalogDataService;

        private AdminHomeViewModel catalogPageViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="CatalogDataService"/> class.
        /// </summary>
        private CatalogDataService()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="CatalogDataService"/>.
        /// </summary>
        public static CatalogDataService Instance => catalogDataService ?? (catalogDataService = new CatalogDataService());

        /// <summary>
        /// Gets or sets the value of catalog page view model.
        /// </summary>
        public AdminHomeViewModel CatalogPageViewModel =>
            this.catalogPageViewModel ??
            (this.catalogPageViewModel = PopulateData<AdminHomeViewModel>("tempmodel.json"));

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "InfluMe.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        #endregion
    }
}