using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace C3Spectra.APPositioningApp.Common
{
    public class HttpUtilities
    {


        public static TResponse Get<TResponse>(string requestURL, string sessionId = null, string authToken = null, string protocolToken = null)
        {
            HttpWebRequest web_request = GetHttpWebRequest(requestURL, "GET", sessionId: sessionId, authToken: authToken, protocolToken: protocolToken);

            string json_data = string.Empty;
            var response = (HttpWebResponse)web_request.GetResponse();
            using (var stream_reader = new StreamReader(response.GetResponseStream()))
            {
                json_data = stream_reader.ReadToEnd();
            }

            return json_data.HasSomething() ? JsonConvert.DeserializeObject<TResponse>(json_data) : default(TResponse);
        }

        public static TResponse Post<TResponse>(string requestURL, object data, string sessionId = null, string token = null, string protocolToken = null)
        {
            HttpWebRequest web_request = GetHttpWebRequest(requestURL, "POST", sessionId: sessionId, authToken: token, protocolToken: protocolToken);

            // Serialize to JSON
            var json_data = JsonConvert.SerializeObject(data,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                DefaultValueHandling = DefaultValueHandling.Ignore  // Ignores the premitive properties during serialization if they are set to default values
                            });

            using (var streamWriter = new StreamWriter(web_request.GetRequestStream()))
            {
                streamWriter.Write(json_data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            string response_data = string.Empty;
            var response = (HttpWebResponse)web_request.GetResponse();
            using (var stream_reader = new StreamReader(response.GetResponseStream()))
            {
                response_data = Convert.ToString(stream_reader.ReadToEnd());
            }

            return response_data.HasSomething() ? JsonConvert.DeserializeObject<TResponse>(response_data) : default(TResponse);
        }

        public static TResponse Delete<TResponse>(string requestURL, string sessionId = null, string authToken = null, string protocolToken = null)
        {
            HttpWebRequest web_request = GetHttpWebRequest(requestURL, "DELETE", sessionId: sessionId, authToken: authToken, protocolToken: protocolToken);

            string json_data = string.Empty;
            var response = (HttpWebResponse)web_request.GetResponse();
            using (var stream_reader = new StreamReader(response.GetResponseStream()))
            {
                json_data = stream_reader.ReadToEnd();
            }

            return json_data.HasSomething() ? JsonConvert.DeserializeObject<TResponse>(json_data) : default(TResponse);
        }

        private static HttpWebRequest GetHttpWebRequest(string requestURL, string method, string sessionId = null, string authToken = null, string protocolToken = null)
        {
            var uri = new Uri(requestURL);

            HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create(uri);
            web_request.ContentType = "application/json";
            web_request.Method = method;

            if (sessionId.HasSomething())
            {
                var cookie = new Cookie("ASP.NET_SessionId", sessionId, "", uri.Host);
                CookieContainer container = new CookieContainer();
                container.Add(cookie);

                web_request.CookieContainer = container;
            }

            if (authToken.HasSomething())
            {
                web_request.Headers.Add(HttpRequestHeader.Authorization, authToken);
            }

            if (protocolToken.HasSomething())
            {
                web_request.Headers.Add("ProtocolToken", protocolToken);
            }

            return web_request;
        }
    }
}
