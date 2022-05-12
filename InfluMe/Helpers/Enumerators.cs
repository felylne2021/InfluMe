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

    public enum JobPlatform {
        Instagram,
        TikTok
    }

    public enum JobApplicationStatus {
        False,
        True
    }

    public static class JobProgressStatus {
        public static string NotAccepted = "Not Accepted";
        public static string Ongoing = "Ongoing";
        public static string ProofSubmitted = "Proof Submitted";
        public static string Completed = "Completed";
    }

    public static class DeliveryStatus {
        public static string PickUp = "Pick Up";
        public static string OnShipping = "On Shipping";
        public static string OnDelivery = "On Delivery";
        public static string Received = "Received";
    }
}
