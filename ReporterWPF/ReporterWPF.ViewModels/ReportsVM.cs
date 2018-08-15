using ReporterWPF.DataAccess;
using ReporterWPF.Models.ApiModels;
using ReporterWPF.ViewModels.Core;
using System.Collections.ObjectModel;

namespace ReporterWPF.ViewModels
{
    public class ReportsVM : BindableBase
    {
        private ObservableCollection<Report> reports;

        public ReportsVM()
        {
            ApiProvider.Instance.AuthorizationStateChanged += this.OnAuthorizationStateChanged;
        }

        public ObservableCollection<Report> Reports
        {
            get => this.reports;
            set
            {
                if (this.reports != value)
                {
                    this.reports = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private async void OnAuthorizationStateChanged(bool isAuthorized)
        {
            if (isAuthorized)
            {
                var result = await ApiProvider.Instance.GetReportsAsync();
                this.Reports = new ObservableCollection<Report>(result);
            }
        }
    }
}
