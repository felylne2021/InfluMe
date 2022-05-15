﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Models {
    public class InfluencerRequest {
        public string influencerEmail { get; set; }
        public string influencerPassword { get; set; }
        public string influencerName { get; set; }
        public string influencerGender { get; set; }
        public string influencerDOB { get; set; }
        public string influencerAddress { get; set; }
        public string influencerInstagramId { get; set; }
        public string influencerTiktokId { get; set; }
        public string whatsappNumber { get; set; }
        public string bankName { get; set; }
        public string bankAccountNumber { get; set; }
    }

    public class InfluencerResponseAll {
        public List<InfluencerResponse> body { get; set; }
    }

    public class InfluencerResponseBody {
        public InfluencerResponse body { get; set; }
    }
    public class InfluencerResponse {
        public int influencerId { get; set; }
        public string influencerEmail { get; set; }
        public string influencerPassword { get; set; }
        public string influencerName { get; set; }
        public string influencerGender { get; set; }
        public string influencerDOB { get; set; }
        public string influencerAddress { get; set; }
        public string influencerInstagramId { get; set; }
        public string instagramFollowersCount { get; set; }
        public string influencerTiktokId { get; set; }
        public string tiktokFollowersCount { get; set; }
        public string whatsappNumber { get; set; }
        public string bankName { get; set; }
        public string bankAccountNumber { get; set; }
        public string influencerColorHex { get; set; }
        public List<JobResponse> appliedJobList { get; set; }
        public List<Notification> notificationList { get; set; }
    }

    public class InfluencerUpdateRequest {
        public string influencerId { get; set; }
        public string influencerEmail { get; set; }
        public string influencerPassword { get; set; }
        public string influencerAddress { get; set; }
        public string influencerInstagramId { get; set; }
        public string influencerTiktokId { get; set; }
        public string whatsappNumber { get; set; }
        public string bankName { get; set; }
        public string bankAccountNumber { get; set; }
    }

}
