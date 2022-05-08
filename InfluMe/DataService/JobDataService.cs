using InfluMe.Models;
using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfluMe.DataService
{
    public class JobDataService
    {
        #region fields

        private string _hostname = "https://influmebe.herokuapp.com";

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="JobDataService"/> class.
        /// </summary>
        public JobDataService()
        {
        }

        #endregion


        #region Methods

        public async Task<List<JobResponse>> GetAllJob() {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.GetAsync("/job");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                AllJobResponseBody respBody = JsonSerializer.Deserialize<AllJobResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }

        public async Task<JobResponse> GetJobById(string jobId) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            var response = await client.GetAsync($"/job/get/{jobId}");

            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                JobResponseBody respBody = JsonSerializer.Deserialize<JobResponseBody>(jsonString);
                return respBody.body;
            }
            else throw new Exception();
        }


        #endregion
    }
}