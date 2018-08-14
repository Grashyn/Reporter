using Newtonsoft.Json;
using ReporterWPF.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.DataAccess
{
    public class RequestComposer
    {
        private static readonly Lazy<RequestComposer> LazyInstance = new Lazy<RequestComposer>(() => new RequestComposer());

        public string Token;

        private RequestComposer()
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 999;
        }

        public static RequestComposer Instance => LazyInstance.Value;

        internal async Task<HttpWebRequest> CreateRequest(RequestData requestData, WebHeaderCollection headers = null)
        {
            var request = WebRequest.CreateHttp(requestData.Uri);
            if (headers == null)
            {
                var requestHeaders = new WebHeaderCollection();
                if (!string.IsNullOrEmpty(this.Token))
                {
                    requestHeaders.Add(HttpRequestHeader.Authorization, this.Token);
                }

                request.Headers = requestHeaders;
            }
            else
            {
                request.Headers = headers;
            }

            request.Accept = "application/json";
            request.KeepAlive = true;

            switch (requestData.RequestType)
            {
                case RequestType.GET:
                    request.Method = HttpMethod.Get.ToString();
                    break;

                case RequestType.POST:
                    request.Method = HttpMethod.Post.ToString();
                    await WriteRequestBodyAsync(request, (PostRequestData)requestData);
                    break;

                case RequestType.DELETE:
                    request.Method = HttpMethod.Delete.ToString();
                    break;

                case RequestType.PUT:
                    request.Method = HttpMethod.Put.ToString();
                    await WriteRequestBodyAsync(request, (PostRequestData)requestData);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return request;
        }

        private static async Task WriteRequestBodyAsync(HttpWebRequest request, PostRequestData requestData)
        {
            request.ContentType = "application/json";
            request.SendChunked = true;

            using (var requestStream = request.GetRequestStream())
            {
                var postData = JsonConvert.SerializeObject(requestData.Data);
                var postDataBytes = Encoding.UTF8.GetBytes(postData);
                await requestStream.WriteAsync(postDataBytes, 0, postDataBytes.Length, requestData.CancellationToken);
            }
        }
    }
}
