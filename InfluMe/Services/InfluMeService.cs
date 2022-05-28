using InfluMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using InfluMe.Helpers;
using Xamarin.Forms;

namespace InfluMe.Services {

    public class InfluMeService {

        private static readonly string _hostname = "https://influmebe.herokuapp.com";
        private static readonly string _pyHostname = "https://influmebe-python.herokuapp.com";
        private HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(_hostname) };
        private HttpClient pyClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(_pyHostname) };

        public InfluMeService() {
            //pyClient.DefaultRequestHeaders.CacheControl.MinFresh = TimeSpan.FromDays(1);
            //pyClient.DefaultRequestHeaders.CacheControl.NoCache = false;
        }

        public async Task<JobAppliedResponse> GetDummyAppliedJob() {
            
            var response = await client.GetAsync("appliedJob/get/83");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                JobAppliedResponseBody respBody = JsonSerializer.Deserialize<JobAppliedResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<LoginResponse> Login(string username, string password) {
           
            LoginRequest req = new LoginRequest() {
                email = username,
                password = password
            };

            var response = await client.PostAsJsonAsync("/login", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                LoginResponseBody respBody = JsonSerializer.Deserialize<LoginResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<ForgotPasswordResponse> SendForgotPasswordMail(ForgotPasswordRequest req) {

            var response = await client.PostAsJsonAsync("/mail/resetPassword", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                ForgotPasswordResponseBody respBody = JsonSerializer.Deserialize<ForgotPasswordResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<InfluencerResponse> GetInfluencerById(string influencerId) {

            var response = await client.GetAsync($"/influencer/get/{influencerId}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                InfluencerResponseBody respBody = JsonSerializer.Deserialize<InfluencerResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task UpdateInfluencerStatus(string influencerId, string status) {
            var response = await client.GetAsync($"/influencer/updateStatus?influencerId={influencerId}&influencerStatus={status}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task<List<InfluencerResponse>> GetInfluencers() {

            var response = await client.GetAsync($"/influencer/get");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                InfluencerResponseAll respBody = JsonSerializer.Deserialize<InfluencerResponseAll>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<OTPResponse> GetOTP(string email) {

            OTPRequest req = new OTPRequest() {
                influencerEmail = email
            };


            var response = await client.PostAsJsonAsync("/mail/send", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                OTPResponseBody respBody = JsonSerializer.Deserialize<OTPResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<OTPResponse> GetOTPEditProfile(string email) {

            OTPRequest req = new OTPRequest() {
                influencerEmail = email
            };


            var response = await client.PostAsJsonAsync("/mail/editProfile", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                OTPResponseBody respBody = JsonSerializer.Deserialize<OTPResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<OTPVerificationResponse> GetOTPVerification(string email, string otp) {

            OTPVerificationResponse resp = new OTPVerificationResponse();

            var response = await client.GetAsync($"/mail/activate?email={email}&otp_code={otp}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<OTPVerificationResponse>(jsonString);
                return resp;
            }
            else throw new Exception();
        }

        public async Task<InfluencerResponse> SignUp(InfluencerRequest req) {

            var response = await client.PostAsJsonAsync("/influencer/save", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                InfluencerResponseBody respBody = JsonSerializer.Deserialize<InfluencerResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();

        }

        public async Task<JobStatsResponse> GetInfluencerStats(string id) {

            JobStatsResponseBody resp = new JobStatsResponseBody();

            var response = await client.GetAsync($"/appliedJob/get/statistic/{id}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<JobStatsResponseBody>(jsonString);
                return resp.body;
            }
            else throw new Exception();
        }        

        public async Task UpdateProfile(InfluencerUpdateRequest req) {

            var response = await client.PostAsJsonAsync("/influencer/updateProfile", req);

            if (!response.IsSuccessStatusCode) {

                throw new Exception();
            }

        }

        public async Task SubmitEnrollments(List<Enrollment> req) {

            var response = await client.PostAsJsonAsync("/influencer/updateStatusBulk", req);

            if (!response.IsSuccessStatusCode) {

                throw new Exception();
            }

        }

        public async Task<bool> GetInfluencerActiveStatus(int id) {

            InfluencerStatusBody resp = new InfluencerStatusBody();

            var response = await client.GetAsync($"/influencer/getInfluencerStatus/{id}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<InfluencerStatusBody>(jsonString);
                return resp.body.influencerStatus.Equals(Helpers.InfluencerStatus.ACTIVE.ToString());
            }
            else throw new Exception();
        }


        public async Task<bool> GetInstagram(string username) {

            try {
                var response = await pyClient.GetAsync($"/instagramFollowerCount?username={username}");

                return (response.IsSuccessStatusCode);
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<bool> GetTikTok(string username) {
            try {
                var response = await pyClient.GetAsync($"/tiktokFollowerCount?username={username}");

                return (response.IsSuccessStatusCode);
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
