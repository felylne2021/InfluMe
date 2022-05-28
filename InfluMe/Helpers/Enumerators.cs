using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Helpers {
    public enum ResponseStatus {
        VALID,
        INVALID,
        REGISTERED
    }

    public enum InfluencerStatus {
        INACTIVE,
        ACTIVE,
        ENROLLED
    }

    public enum UserType {
        Influencer,
        Admin
    }

    public enum JobPlatformList {
        Instagram,
        TikTok,
        Both
    }

    public enum JobApplicationStatus {
        False,
        True
    }

    public enum JobStatus {
        OPEN, // registration open
        SELECTION, // waiting for client selection
        ONGOING, // registration closed and approved influencers has been announced
        DONE, // all influencers' proof approved and payment pending (paid jobs only)
        CLOSE // all influencers' proof approved (if no payment), all payment status paid (if any)
    }

    public static class JobProgressStatus {
        public static string Applied = "Applied"; // influencer applied
        public static string NotApproved = "Not Accepted"; // influencer not chosen
        public static string OnDelivery = "On Delivery"; // influencer chosen and product not null
        public static string PendingProof = "Pending Proof"; // influencer accepted, no draft/ draft accepted
        public static string PendingDraft = "Pending Draft"; // if job needs drafting, accepted
        public static string DraftSubmitted = "Draft Submitted"; // if content needs approval (hasContentApproval) and submitted
        public static string ProofSubmitted = "Proof Submitted"; // influencer posted proof of work approved
        public static string PendingPayment = "Pending Payment"; // paid jobs only
        public static string Completed = "Completed"; // payment done (for paid jobs)/PoW approved
    }

    public static class DeliveryStatus {
        public static string PickUp = "Pick Up";
        public static string OnShipping = "On Shipping";
        public static string OnDelivery = "On Delivery";
        public static string Received = "Received";
        public static List<string> DeliveryStatusList = new List<string> {
            DeliveryStatus.PickUp, DeliveryStatus.OnShipping, DeliveryStatus.OnDelivery, DeliveryStatus.Received
        };
    }


}
