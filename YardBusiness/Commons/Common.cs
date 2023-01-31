using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Business.Commons
{
    public static class YardExtensions
    {
        public static bool IsNull(this string data)
        {
            return string.IsNullOrEmpty(data?.Trim() ?? string.Empty);
        }

        public static DateTime TrimTime(this DateTime data)
        {
            return DateTime.Parse(data.ToShortDateString());
        }

        public static bool Like(this string field, string condition)
        {
            return condition != null ? (field?.Contains(condition) ?? false) : true;
        }

        public static bool IsBetween(this DateTime condition, DateTime? field_from, DateTime? field_to, bool isNullable = false)
        {
            return field_from.HasValue && field_to.HasValue ? condition.IsBetween(field_from.Value, field_to.Value) : isNullable;
        }

        public static bool IsBetween(this DateTime condition, DateTime field_from, DateTime field_to)
        {
            return condition >= field_from && condition <= field_to;
        }

        public static bool DateBetweenField(this DateTime? field_from, DateTime? field_to, DateTime? condition)
        {
            return condition.HasValue ? (field_from.HasValue ? (field_to.HasValue ? condition >= field_from && condition <= field_to : field_from.Equals(condition)) : false) : true;
        }

        public static bool DateBetweenCondition(this DateTime field, DateTime? condition_from, DateTime? condition_to, bool trimTime = false)
        {
            return condition_from.HasValue ? (condition_to.HasValue ? condition_from <= (trimTime ? field.TrimTime() : field) && condition_to >= (trimTime ? field.TrimTime() : field) : (trimTime ? field.TrimTime() : field).Equals(condition_from)) : true;
        }

        public static bool DateBetweenCondition(this DateTime? field, DateTime? condition_from, DateTime? condition_to)
        {
            return condition_from.HasValue ? (field.HasValue ? (condition_to.HasValue ? condition_from <= field && condition_to >= field : field.Equals(condition_from)) : false) : true;
        }

        public static bool IsEquals<T>(this T? source, T? data) where T : struct
        {
            return data.HasValue ? Nullable.Equals(source, data) : true;
        }

        public static bool IsEquals<T>(this T source, T? data) where T : struct
        {
            return data.HasValue ? source.Equals(data) : true;
        }

        public static bool IsContains(this Guid source, List<Guid> data)
        {
            return (data?.Count ?? 0) > 0 ? data.Contains(source) : true;
        }
    }

    public static class Utils
    {
        public static void NullCoalescing<T>(ref T source, dynamic assignedSource)
        {
            if (source == null)
            {
                source = assignedSource;
            }
        }

        public static string GUID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
        public static T SendDataApi<T>(string url, string data, string authorization = "")
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(authorization))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                }

                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                var contentResult = result.Content.ReadAsStringAsync().Result;
                T model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(contentResult);
                return model;
            }
        }

        public static T SendDataApi_release_manifest<T>(string url, string data, string authorization)
        {
            Logtxt LoggingService = new Logtxt();
            using (var client = new HttpClient())
            {
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest"+ DateTime.Now.ToString("yyyy-MM-dd"), "Start SendDataApi_release_manifest" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest"+ DateTime.Now.ToString("yyyy-MM-dd"), "Start url : "+url + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest"+ DateTime.Now.ToString("yyyy-MM-dd"), "Start data : " + data  + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest"+ DateTime.Now.ToString("yyyy-MM-dd"), "Start authorization : " + authorization  + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                client.Timeout = TimeSpan.FromMinutes(60);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(authorization))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                }

                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest" + DateTime.Now.ToString("yyyy-MM-dd"), "result : " + result);
                var contentResult = result.Content.ReadAsStringAsync().Result;
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest" + DateTime.Now.ToString("yyyy-MM-dd"), "contentResult : " + contentResult);
                T model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(contentResult);
                LoggingService.DataLogLines("SendDataApi_release_manifest", "SendDataApi_release_manifest" + DateTime.Now.ToString("yyyy-MM-dd"), "model : " + model);
                return model;
            }
        }

        public class betweenDate
        {
            public DateTime start { get; set; }

            public DateTime end { get; set; }
        }

        public static betweenDate toBetweenDate(this string date)
        {
            try
            {
                CultureInfo cultureInfo = new CultureInfo("en-US");
                betweenDate betweenDate = new betweenDate();
                date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                DateTime dateTime1 = DateTime.Parse(date, (IFormatProvider)cultureInfo);
                DateTime dateTime2 = DateTime.Parse(date, (IFormatProvider)cultureInfo);
                dateTime2 = dateTime2.AddHours(23.0);
                dateTime2 = dateTime2.AddMinutes(59.0);
                betweenDate.start = dateTime1;
                betweenDate.end = dateTime2;
                return betweenDate;
            }
            catch (Exception ex)
            {
                return (betweenDate)null;
            }
        }

        public static T GetDataApi<T>(string url, string data, string authorization = "")
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(30);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(authorization))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                }

                //var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.GetAsync(url + "/" + data).Result;
                var contentResult = result.Content.ReadAsStringAsync().Result;
                T model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(contentResult);
                return model;
            }
        }

        public static T SendDataApi<T>(string url, string data, out string contentResult)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromMinutes(30);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                contentResult = result.Content.ReadAsStringAsync().Result;
                T model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(contentResult);
                return model;
            }
        }
        public static void OneWaySendDataApi(string url, string data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(data, Encoding.GetEncoding("TIS-620"), "application/json");
                client.PostAsync(url, content);
            }
        }
        public static string SerializeObject(dynamic model)
        {
            return JsonConvert.SerializeObject(model);
        }
        public static string SendDataApiAuthorization(string url, string data, string authorization)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                var contentResult = result.Content.ReadAsStringAsync().Result;
                return contentResult;
            }
        }
        public static T SendDataGoogleApi<T>(string url, string data, out string contentResult)
        {
            using (var client = new HttpClient())
            {
                var keystring = "AIzaSyDZmuCtFcZSQcg2FVMpP5K4vP2AnHkx4Hc";
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromMinutes(30);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "=" + keystring);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                contentResult = result.Content.ReadAsStringAsync().Result;
                T model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(contentResult);
                return model;
            }
        }

        internal static List<T> SendDataApi<T>(object p, string v)
        {
            throw new NotImplementedException();
        }
    }

    public class AppSettingConfig
    {
        public string GetUrl(string key)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);
            var configuration = builder.Build();
            return configuration.GetConnectionString(key).ToString();
        }
    }

    public class ConfigurationManager1
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager1()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();


            //string value1 = ConfigurationManager.AppSetting["GrandParent_Key:Parent_Key:Child_Key"];
            //string value2 = ConfigurationManager.AppSetting["Parent_Key:Child_Key"];
            //string value3 = ConfigurationManager.AppSetting["Child_Key"];

        }

    }
}
