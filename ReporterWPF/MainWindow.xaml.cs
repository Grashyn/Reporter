using Newtonsoft.Json;
using ReporterWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReporterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginClicked(object sender, RoutedEventArgs e)
        {
            var data = new LoginData
            {
                Email = this.Email.Text,
                Password = this.Password.Text
            };

            var request = WebRequest.CreateHttp(new Uri("http://localhost:58589/api/Account/Login"));
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var requestStream = request.GetRequestStream())
            {
                var postData = JsonConvert.SerializeObject(data);
                var postDataBytes = Encoding.UTF8.GetBytes(postData);
                await requestStream.WriteAsync(postDataBytes, 0, postDataBytes.Length);
            }
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
                                var resu = responseJson.Remove(0, 1);
                                resu = resu.Remove(resu.Length - 1, 1);
                                var request2 = WebRequest.CreateHttp(new Uri("http://localhost:58589/api/Report"));
                                request2.Headers[HttpRequestHeader.Authorization] = $"Bearer {resu}";
                                request2.ContentType = "application/json";
                                request2.Method = "GET";
                                using (var response2 = await request2.GetResponseAsync())
                                {
                                    if (response2 != null)
                                    {
                                        using (var responseStream2 = response2.GetResponseStream())
                                        {
                                            if (responseStream2 != null)
                                            {
                                                using (var responseStreamReader2 = new StreamReader(responseStream2))
                                                {
                                                    var responseJson2 = await responseStreamReader2.ReadToEndAsync();

                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}
