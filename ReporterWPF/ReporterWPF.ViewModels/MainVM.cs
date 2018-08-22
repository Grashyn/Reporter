using ReporterWPF.Models;
using ReporterWPF.ViewModels.Core;
using System;

namespace ReporterWPF.ViewModels
{
    public class MainVM : BindableBase
    {
        private static readonly Lazy<MainVM> LazyInstance = new Lazy<MainVM>(() => new MainVM());

        private WindowState state;
        private BindableBase currentVM = new LoginVM();

        private MainVM()
        {
        }

        public static MainVM Instance => LazyInstance.Value;

        public WindowState State
        {
            get => this.state;
            set
            {
                if (this.state != value)
                {
                    this.state = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public BindableBase CurrentVM
        {
            get => this.currentVM;
            set
            {
                if (this.currentVM != value)
                {
                    this.currentVM = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
