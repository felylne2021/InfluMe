﻿using System.Collections.Generic;
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
        public string hasContentApproval { get; set; }

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
        public string hasContentApproval { get; set; }
        public string jobImage { get; set; }


    }

    [Preserve(AllMembers = true)]
    public class JobApplied {
        public int influencerId { get; set; }
        public int jobId { get; set; }
        public string approvalStatus { get; set; }
        public string progressStatus { get; set; }
        public string proofOfWork { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class JobAppliedResponseBody {
        public JobAppliedResponse body { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class JobAppliedResponse {
        public int influencerId { get; set; }
        public int adminId { get; set; }
        public int deliveryId { get; set; }
        public int paymentId { get; set; }
        public int appliedJobId { get; set; }
        public string deliveryStatus { get; set; }
        public string paymentStatus { get; set; }
        public string approvalStatus { get; set; }
        public string progressStatus { get; set; }
        public string proofOfWork { get; set; }
        public string contentDraft { get; set; }
        public string isApply { get; set; }
        public InfluencerResponse influencer { get; set; }
        public JobResponse job { get; set; }
        public Payment payment { get; set; }
        public Delivery delivery { get; set; } 

    }

    public class JobStatsResponseBody {
        public JobStatsResponse body { get; set; }
    }

    public class JobStatsResponse {
        public int influencerId { get; set; }
        public List<JobAppliedResponse> appliedJobList { get; set; }
        public string earnings { get; set; }
    }

    public class PoWSubmission {
        public int influencerId { get; set; }
        public int jobId { get; set; }
        public string proofOfWork { get; set; }
    }

    public class DraftSubmission {
        public int influencerId { get; set; }
        public int jobId { get; set; }
        public string contentDraft { get; set; }
    }

    public class ChangeJobProgressRequest {
        public int influencerId { get; set; }
        public int jobId { get; set; }
        public string progressStatus { get; set; }

    }

    public class Payment {
        public int paymentId { get; set; }
        public string paymentStatus { get; set; }
        public string paymentAmount { get; set; }
        public string paymentDate { get; set; }
    }

    public class Delivery {
        public int deliveryId { get; set; }
        public string deliveryStatus { get; set; }
        public string deliveryCompany { get; set; }
        public string deliveryItemName { get; set; }
    }
}