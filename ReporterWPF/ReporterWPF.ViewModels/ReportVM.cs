using ReporterWPF.Models.ApiModels;
using ReporterWPF.ViewModels.Core;

namespace ReporterWPF.ViewModels
{
    public class ReportVM : BindableBase
    {
        private Report report;

        public ReportVM(Report report)
        {
            this.report = report;
        }
    }
}
