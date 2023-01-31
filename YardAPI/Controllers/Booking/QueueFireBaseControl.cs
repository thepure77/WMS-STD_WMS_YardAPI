using Microsoft.Extensions.Configuration;
using System; 
using System.IO; 
using System.Net;
using System.Text; 

namespace API.Controllers
{
    public class QueueFireBaseControl
    {
        private string urlFireBase = "";
        private string authFireBaseRealtimebase = "";
        private string urlFireBaseFCM = "";
        private string authFireBaseFCM = "";


        public QueueFireBaseControl(IConfiguration configuration)
        {
            urlFireBase = configuration["firebase:real_time_url"];
            authFireBaseRealtimebase = configuration["firebase:real_time_auth"];
            urlFireBaseFCM = configuration["firebase:fcm_url"];
            authFireBaseFCM = configuration["firebase:fcm_auth"];
        }

        public string callQueue(string userId)
        { 
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"{urlFireBase}driver/{userId}/queue/.json?auth={authFireBaseRealtimebase}");

                var json = new
                {
                    call_date = DateTime.Now
                };

                httpWebRequest.Method = "PATCH"; 
                byte[] byteArray = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(json));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = byteArray.Length;

                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                try
                {
                    response = (HttpWebResponse)httpWebRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return "";
                }
                else
                {
                    return $"error http status {response.StatusCode} {response.StatusDescription}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"(can't connect to realtime base) ExceptionMessage : {ex.Message}");
            }
        }
    }
}
