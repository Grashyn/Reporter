using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ReporterWPF.Models
{
    public static class WebRequestExtensions
    {
        public static Task<T> GetResponseAsync<T>(this WebRequest request, JsonSerializerSettings settings = null)
        {
            return request.GetResponseAsyncInternal<T>(settings);
        }

        public static Task<byte[]> GetBytesResponseAsync(this WebRequest request)
        {
            return request.GetBytesResponseAsyncInternal();
        }

        private static async Task<T> GetResponseAsyncInternal<T>(this WebRequest request, JsonSerializerSettings settings = null)
        {
            var data = default(T);
            async Task GetResponseInternal()
            {
                using (var response = await request.GetResponseAsync())
                {
                    if (response != null)
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var responseStreamReader = new StreamReader(responseStream))
                                {
                                    var responseJson = await responseStreamReader.ReadToEndAsync();
                                    data = JsonConvert.DeserializeObject<T>(responseJson, settings);
                                }
                            }
                        }
                    }
                }
            }

            await GetResponseInternal();
            return data;
        }

        private static async Task<byte[]> GetBytesResponseAsyncInternal(this WebRequest request)
        {
            var result = default(byte[]);
            using (var response = await request.GetResponseAsync())
            {
                if (response != null)
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                var buffer = new byte[16 * 1024];
                                int bytesRead;
                                while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    memoryStream.Write(buffer, 0, bytesRead);
                                }

                                result = memoryStream.ToArray();
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
