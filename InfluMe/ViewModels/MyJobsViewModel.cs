﻿using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.Services;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.ViewModels {
    /// <summary>
    /// ViewModel for health profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class MyJobsViewModel : BaseViewModel {
        #region Fields

        private InfluMeService service = new InfluMeService();
        private List<JobAppliedResponse> myJobs;
        private List<JobAppliedResponse> allMyJobs;

        private string jobStatusFilter;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ViewProfilePageViewModel" /> class.
        /// </summary>
        public MyJobsViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.FilterJobByStatusCommand = new Command(FilterJobByStatus);
        }

        public string JobStatusFilter {
            get {
                return this.jobStatusFilter;
            }

            set {
                if (this.jobStatusFilter == value) {
                    return;
                }

                this.SetProperty(ref this.jobStatusFilter, value);
            }
        }

        #endregion

        #region Public properties

        public List<JobAppliedResponse> MyJobs {
            get {
                return this.myJobs;
            }

            set {
                if (this.myJobs == value) {
                    return;
                }

                this.SetProperty(ref this.myJobs, value);
            }
        }

        public List<JobAppliedResponse> AllMyJobs {
            get {
                return this.allMyJobs;
            }

            set {
                if (this.allMyJobs == value) {
                    return;
                }

                this.SetProperty(ref this.allMyJobs, value);
            }
        }

        #endregion


        #region Commands
        public Command BackButtonCommand { get; set; }  
        public Command FilterJobByStatusCommand { get; set; }

        #endregion

        #region Methods

        private async void InitializeProperties() {

            try {
               
                JobStatsResponse resp = await service.GetInfluencerStats(Application.Current.Properties["UserId"].ToString());

                this.AllMyJobs = resp.appliedJobList;
                this.MyJobs = AllMyJobs;

            }
            catch (Exception) {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage());
            }
        }

        private void FilterJobByStatus(object obj) {
            if (!string.IsNullOrEmpty(JobStatusFilter)) {
                switch (JobStatusFilter) {
                    case "Applied":
                        break;

                    case "Ongoing":
                        break;

                    case "Content Approved":
                        break;

                    case "Admin Approved":
                        break;

                    case "Pending Payment":
                        break;

                    case "Completed":
                        break;

                    case "Not Approved":
                        break;

                    default:
                        this.MyJobs = this.AllMyJobs;
                        break;
                };
            }
            else this.MyJobs = this.AllMyJobs;
        }

        #endregion
    }
}