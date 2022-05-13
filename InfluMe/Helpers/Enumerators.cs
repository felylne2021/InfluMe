using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Helpers {
    public enum ResponseStatus {
        VALID,
        INVALID,
        REGISTERED
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
        public static string Ongoing = "Ongoing"; // influencer selected
        public static string ContentSubmitted = "Content Submitted"; // if content needs approval (hasContentApproval)
        public static string ProofSubmitted = "Proof Submitted"; // influencer posted proof of work
        public static string PendingPayment = "Pending Payment"; // paid jobs only
        public static string Completed = "Completed"; // payment done (for paid jobs)/PoW approved
    }

    public static class DeliveryStatus {
        public static string PickUp = "Pick Up";
        public static string OnShipping = "On Shipping";
        public static string OnDelivery = "On Delivery";
        public static string Received = "Received";
    }
}
