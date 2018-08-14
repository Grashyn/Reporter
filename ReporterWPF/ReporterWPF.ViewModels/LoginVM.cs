using ReporterWPF.DataAccess;
using ReporterWPF.ViewModels.Core;
using System.Security;
using System.Windows.Controls;

namespace ReporterWPF.ViewModels
{
    public class LoginVM : BindableBase
    {
        private string email = "spiderdk90@gmail.com";
        private bool isBusy;

        public LoginVM()
        {
            this.LoginCommand = new DelegateCommand<object>(this.LoginExecute, this.LoginCanExecute);
        }

        public DelegateCommand<object> LoginCommand { get; }

        public string Email
        {
            get => this.email;
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private bool LoginCanExecute(object Object)
        {
            return !this.isBusy;
        }

        private async void LoginExecute(object Object)
        {
            this.isBusy = true;
            this.LoginCommand.RaiseCanExecuteChanged();

            if (Object is PasswordBox passwordBox)
            {
                await ApiProvider.Instance.Login(this.Email, passwordBox.SecurePassword);
            }

            this.isBusy = false;
            this.LoginCommand.RaiseCanExecuteChanged();
        }
    }
}
