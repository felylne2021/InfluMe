using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Models {
    public class Influencer {
        public string influencerId { get; set; }
        public string influencerEmail { get; set; }
        public string influencerPassword { get; set; }
        public string influencerName { get; set; }
        public string influencerGender { get; set; }
        public DateTime influencerDOB => DateTime.Now;
        public string influencerAddress { get; set; }
        public string influencerInstagramId => "@";
        public string influencerTiktokId => "@";
        public List<Job> appliedJobList { get; set; }
        public List<Notification> notificationList { get; set; }

    }
}
