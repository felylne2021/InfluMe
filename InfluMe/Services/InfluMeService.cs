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

        private readonly string _hostname = "https://influmebe.herokuapp.com";
        public InfluMeService() {
        }

        public async Task<LoginResponse> Login(string username, string password) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

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

        public async Task<InfluencerResponse> GetInfluencerById(string influencerId) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.GetAsync($"/influencer/get/{influencerId}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                InfluencerResponseBody respBody = JsonSerializer.Deserialize<InfluencerResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<OTPResponse> GetOTP(string email) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

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

        public async Task<OTPVerificationResponse> GetOTPVerification(string email, string otp) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.PostAsJsonAsync("/influencer/save", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                InfluencerResponseBody respBody = JsonSerializer.Deserialize<InfluencerResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();

        }

        public async Task<JobStatsResponse> GetInfluencerStats(string id) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            JobStatsResponseBody resp = new JobStatsResponseBody();

            var response = await client.GetAsync($"/appliedJob/get/statistic/{id}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<JobStatsResponseBody>(jsonString);
                return resp.body;
            }
            else throw new Exception();
        }
    }
}
