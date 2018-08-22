using System.Threading;

namespace ReporterWPF.Models
{
    public class PostRequestData : RequestData
    {
        public object Data { get; set; }

        public CancellationToken CancellationToken { get; set; }
    }
}
