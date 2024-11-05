using System.Text;


namespace JIRA_PMS_ConsoleAPP
{
    using System;
    using System.Net.Http.Headers;
    using System.Runtime.InteropServices.Marshalling;

    public class FetchAPI
       
    {
        private readonly string email;
        private readonly string apiToken;

        public FetchAPI(string email, string apiToken)
        {
            this.email = email;
            this.apiToken = apiToken;
        }

        public async Task<string> GetResponse(string API_URL)
        {
            using var client = new HttpClient();

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{apiToken}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            HttpResponseMessage response;

            response = await client.GetAsync(API_URL);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var responseBodyContent =  response.Content.GetHashCode();

            return responseBody;
        }
    }

}
