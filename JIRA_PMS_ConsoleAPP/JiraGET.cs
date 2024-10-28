using System.Text;


namespace JIRA_PMS_ConsoleAPP
{
    using System;
    using System.Net.Http.Headers;

    public class Class1
       
    {
        private readonly string email;
        private readonly string apiToken;

        public Class1(string email, string apiToken)
        {
            this.email = email;
            this.apiToken = apiToken;
        }

        public async Task<string> GetResponse(string API_URL, HttpContent? requestBody)
        {
            using var client = new HttpClient();

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{apiToken}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            HttpResponseMessage response;

            if (requestBody == null)
            {
                response = await client.GetAsync(API_URL);
            }
            else
            {
                response = await client.PostAsync(API_URL, requestBody);
            }

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }

}
