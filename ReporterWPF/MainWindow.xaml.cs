using System.Windows;

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

        //                    using (var responseStreamReader = new StreamReader(responseStream))
        //                    {
        //                        var responseJson = await responseStreamReader.ReadToEndAsync();
        //                        var resu = responseJson.Remove(0, 1);
        //                        resu = resu.Remove(resu.Length - 1, 1);
        //                        var request2 = WebRequest.CreateHttp(new Uri("http://localhost:58589/api/Report"));
        //                        request2.Headers[HttpRequestHeader.Authorization] = $"Bearer {resu}";
        //                        request2.ContentType = "application/json";
        //                        request2.Method = "GET";
        //                        using (var response2 = await request2.GetResponseAsync())
        //                        {
        //                            if (response2 != null)
        //                            {
        //                                using (var responseStream2 = response2.GetResponseStream())
        //                                {
        //                                    if (responseStream2 != null)
        //                                    {
        //                                        using (var responseStreamReader2 = new StreamReader(responseStream2))
        //                                        {
        //                                            var responseJson2 = await responseStreamReader2.ReadToEndAsync();

        //                                        }
        //                                    }
        //                                }
    }
}
