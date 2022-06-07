﻿using InfluMe.Models;
using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OngoingJobPage : ContentPage {
        public OngoingJobPage(JobResponse selectedJob) {
            InitializeComponent();
            BindingContext = new JobApplicantsViewModel(selectedJob);
        }
    }
}