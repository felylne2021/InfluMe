﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfluMe.Models.ServiceResponse {

    public class LoginRequest {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse {
        public string username { get; set; }
        public string password { get; set; }
        public string status { get; set; }
    }
}
