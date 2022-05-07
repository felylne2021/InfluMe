using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace InfluMe.Models
{

    [Preserve(AllMembers = true)]
    public class JobRequest {
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

    }

    public class AllJobResponseBody {
        public List<JobResponse> body { get; set; }
    }

    public class JobResponseBody {
        public JobResponse body { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class JobResponse
    {
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

    }
}