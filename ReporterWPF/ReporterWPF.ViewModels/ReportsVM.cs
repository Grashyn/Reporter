using ReporterWPF.DataAccess;
using ReporterWPF.Models;
using ReporterWPF.Models.ApiModels;
using ReporterWPF.ViewModels.Core;
using System;
using System.Collections.ObjectModel;

namespace ReporterWPF.ViewModels
{
    public class ReportsVM : BindableBase
    {
        private ObservableCollection<Report> reports;

        public ReportsVM()
        {
            this.SelectReportCommand = new DelegateCommand<Report>(this.SelectReportExecute);
            this.Initialize();
        }

        public DelegateCommand<Report> SelectReportCommand { get; }

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

        public async void Initialize()
        {
            var result = await ApiProvider.Instance.GetReportsAsync();
            this.Reports = new ObservableCollection<Report>(result);
        }

        private void SelectReportExecute(Report selectedReport)
        {
            MainVM.Instance.CurrentVM = new ReportVM(selectedReport);
            MainVM.Instance.State = WindowState.Report;
        }
    }
}
