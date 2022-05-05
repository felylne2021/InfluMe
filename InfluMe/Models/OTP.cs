using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfluMe.Models {

    public class OTPRequest {
        public string influencerEmail { get; set; }
    }

    public class OTPResponseBody {
        public OTPResponse body { get; set; }
    }

    public class OTPResponse {
        public string otpNumber { get; set; }
        public string otpStatus { get; set; }
    }

    public class OTPVerificationResponse {
        public string body { get; set; }
    }
}
