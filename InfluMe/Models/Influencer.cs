using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Models {
    public class ForgotPasswordRequest {
        public string influencerEmail { get; set; }
    }

    public class ForgotPasswordResponseBody {
        public ForgotPasswordResponse body { get; set; }
    }

    public class ForgotPasswordResponse {
        public string influencerEmail { get; set; }
    }

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
        public string influencerStatus { get; set; }
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

    public class Enrollment {
        public bool isChecked { get; set; }
        public int influencerId { get; set; }
    }

    public class InfluencerStatusBody {
        public InfluencerIsActive body { get; set; }
    }

    public class InfluencerIsActive {
        public string influencerStatus { get; set; }
    }

}
