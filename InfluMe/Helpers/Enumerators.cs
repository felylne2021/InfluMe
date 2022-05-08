using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Helpers {
    public enum ResponseStatusEnum {
        VALID,
        INVALID,
        REGISTERED
    }

    public enum UserTypeEnum {
        Influencer,
        Admin
    }

    public enum JobPlatform {
        Instagram,
        TikTok,
        Both
    }
}
