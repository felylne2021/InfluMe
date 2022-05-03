using InfluMe.Models.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using InfluMe.Helpers;

namespace InfluMe.Services {
    
    public class InfluMeService{

        private readonly string _hostname;
        public InfluMeService() {
            _hostname = "https://influmebe.herokuapp.com";
        }

        public async Task<OTPResponse> GetOTP(string email) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_hostname);

            OTPRequest req = new OTPRequest(){
                influencerEmail = email
            };

            OTPResponse resp = new OTPResponse();
            try {
                var response = await client.PostAsJsonAsync("/mail/send", req);

                if (response.IsSuccessStatusCode) {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    OTPResponseBody respBody = JsonSerializer.Deserialize<OTPResponseBody>(jsonString);
                    return respBody.body;
                }
            }
            catch (Exception ex) {
                resp.otpStatus = OTPStatusEnum.UNKNOWN.ToString();
                return resp;
            }
            return null;
        }

    }
}
