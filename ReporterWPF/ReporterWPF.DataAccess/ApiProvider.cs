using ReporterWPF.Models;
using ReporterWPF.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace ReporterWPF.DataAccess
{
    public class ApiProvider
    {
        private static readonly Lazy<ApiProvider> LazyInstance = new Lazy<ApiProvider>(() => new ApiProvider());
        private const string ServerUrl = "http://localhost:58589/api/";

        public event Action<bool> AuthorizationStateChanged = status => { };

        private ApiProvider()
        {
        }

        public static ApiProvider Instance => LazyInstance.Value;

        public bool IsAuthorized { get; private set; }

        public async Task Login(string email, SecureString password)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(password);
                var pass = Marshal.PtrToStringUni(unmanagedString);
                var loginUri = new Uri($"{ServerUrl}Account/Login");
                var request = await RequestComposer.Instance.CreateRequest(new PostRequestData
                {
                    Uri = loginUri,
                    RequestType = RequestType.POST,
                    Data = new LoginData
                    {
                        Email = email,
                        Password = pass
                    }
                });

                var result = await request.GetResponseAsync<User>();
                RequestComposer.Instance.Token = $"Bearer {result.Token}";
                this.IsAuthorized = true;
                this.AuthorizationStateChanged.Invoke(true);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public async Task<IEnumerable<Report>> GetReportsAsync()
        {
            var loginUri = new Uri($"{ServerUrl}Report");
            var request = await RequestComposer.Instance.CreateRequest(new PostRequestData
            {
                Uri = loginUri,
                RequestType = RequestType.GET
            });

            return await request.GetResponseAsync<List<Report>>();
        }
    }
}
