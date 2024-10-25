using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace JIRA_PMS_ConsoleAPP
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;

    public class Class1
       
    {

        private string email = "irfan007lohar@gmail.com";
        private string apiToken = "ATATT3xFfGF0NqdtoTKrOzAHZjoiTghbPladtPomaqpzSt45je5dZTiFeLYVCK5XK7eqQNfob4JKb40jd5aK47_vh0kxg3MFIn9a1NMwQZsTm4W72Rm_JB4duQ2HoO7yrrp1Dd1QpIRTiiCCpTDbwpdR8kRfWx6uMpmez5mXRG0qHQTyfKWnCvY=C0645BA2";

        public void GETData()
        {
            string api = "https://irfan007lohar.atlassian.net/rest/api/3/user?accountId=712020:ea3704c2-a8a9-4cad-a04b-b5fe4bcbef31";

            WebRequest requestObjectGet = WebRequest.Create(api);

            requestObjectGet.Method = "GET";

            HttpWebResponse responseObjectGet = null;

            responseObjectGet = (HttpWebResponse)requestObjectGet.GetResponse();

            string strResult = null;

            using (Stream stream = responseObjectGet.GetResponseStream())
            {

                StreamReader sr = new StreamReader(stream);
                strResult = sr.ReadToEnd();
                sr.Close();

            }
        }

        public async Task<string> GetResponse(string API_URL, HttpContent? requestBody)
        {
            using var client = new HttpClient();

            // Add the Authorization header with Base64 encoded email and API token
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
