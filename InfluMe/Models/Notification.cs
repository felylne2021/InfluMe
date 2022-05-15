using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Models {
    public class Notification {
        public int notificationId { get; set; }
        public string notificationDate { get; set; }
        public string notificationMessage { get; set; }
    }

    public class NotificationResponse {
        public List<Notification> body { get; set; }
    }
}
