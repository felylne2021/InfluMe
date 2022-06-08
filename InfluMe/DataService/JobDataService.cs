using InfluMe.Helpers;
using InfluMe.Models;
using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfluMe.DataService
{
    public class JobDataService
    {
        #region fields

        private static readonly string _hostname = "https://influmebe.herokuapp.com";
        private static readonly string _delivHostname = "https://api.binderbyte.com";
        private HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(_hostname) };
        private HttpClient delivClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(_delivHostname
            ) };


        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="JobDataService"/> class.
        /// </summary>
        public JobDataService()
        {
            //client.DefaultRequestHeaders.CacheControl.MustRevalidate = true;
            //client.DefaultRequestHeaders.CacheControl.NoCache = false;
        }

        #endregion


        #region Methods

        public async Task<List<Notification>> GetNotifications(string id, string userType) {

            NotificationResponse resp = new NotificationResponse();

            var response = userType == UserType.Admin.ToString() ? await client.GetAsync($"/notification/admin/{id}"): await client.GetAsync($"/notification/influencer/{id}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<NotificationResponse>(jsonString);
                return resp.body;
            }
            else throw new Exception();
        }

        public async Task<List<JobResponse>> GetAllJob() {

            var response = await client.GetAsync("/job");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                AllJobResponseBody respBody = JsonSerializer.Deserialize<AllJobResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        
        public async Task<List<JobAppliedResponse>> GetAllAppliedJobByJobId(int jobId) {

            var response = await client.GetAsync($"/appliedJob/get/job/{jobId}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                AllJobAppliedBody respBody = JsonSerializer.Deserialize<AllJobAppliedBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<JobResponse> GetJobById(string jobId) {

            var response = await client.GetAsync($"/job/get/{jobId}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                JobResponseBody respBody = JsonSerializer.Deserialize<JobResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<JobAppliedResponse> ApplyJob(JobAppliedRequest jobApplied) {

            var response = await client.PostAsJsonAsync("/appliedJob/apply", jobApplied);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                JobAppliedResponseBody respBody = JsonSerializer.Deserialize<JobAppliedResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<JobResponse> AddJob(JobRequest req) {

            var response = await client.PostAsJsonAsync("/job/save", req);

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                JobResponseBody respBody = JsonSerializer.Deserialize<JobResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task SubmitPoW(PoWSubmission submission) {

            var response = await client.PostAsJsonAsync("/appliedJob/submitPOW", submission);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }

        }

        public async Task SubmitDraft(DraftSubmission submission) {

            var response = await client.PostAsJsonAsync("/appliedJob/submitContentDraft", submission);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task ChangeJobProgress(ChangeJobProgressRequest request) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.PostAsJsonAsync($"/appliedJob/changeProgressStatus", request);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task SubmitPaymentProof(SubmitPayment request) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.PostAsJsonAsync($"/payment/save", request);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task DeleteJob(string jobId) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.DeleteAsync($"/job/delete/{jobId}");

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task UpdateJob(JobResponse job) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);
            job.jobDeadline = DateTime.ParseExact(job.jobDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            job.jobRegistrationDeadline = DateTime.ParseExact(job.jobRegistrationDeadline, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            var response = await client.PutAsJsonAsync($"/job/update/{job.jobId}", job);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task SaveDelivery(Delivery delivery){
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.PostAsJsonAsync($"/delivery/save", delivery);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
        }

        public async Task SubmitJobApplicants(List<Applicant> applicants, int jobId) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.PostAsJsonAsync($"/appliedJob/changeProgressStatus/bulk", applicants);

            if (!response.IsSuccessStatusCode) {
                throw new Exception();
            }
            else {
                var waRes = await client.GetAsync($"/job/sendClosedJobWA/{jobId}");

                if (!waRes.IsSuccessStatusCode) {
                    throw new Exception();
                }
            }
        }

        public async Task<DeliveryData> TrackDelivery(string courier, string awb) {

            var response = await delivClient.GetAsync($"/v1/track?api_key=83a01043e7d02757d94c9d517e6e6e8caccf5a8fc44ee63f1116e09b5d64f557&courier={courier}&awb={awb}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                DeliveryTrackingResponse respBody = JsonSerializer.Deserialize<DeliveryTrackingResponse>(jsonString);
                return respBody.data;
            }
            else throw new Exception();
        }

        #endregion
    }
}