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
        static string NotAccepted = "Not Accepted";
        static string Ongoing = "Ongoing";
        static string ProofSubmitted = "Proof Submitted";
        static string Completed = "Complete";
    }

    public static class DeliveryStatus {
        static string PickUp = "Pick Up";
        static string OnShipping = "On Shipping";
        static string OnDelivery = "On Delivery";
        static string Received = "Received";
    }
}
