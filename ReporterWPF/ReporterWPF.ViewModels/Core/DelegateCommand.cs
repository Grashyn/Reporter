using System;
using System.Windows.Input;

namespace ReporterWPF.ViewModels.Core
{
    public class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;
        private bool canExecuteCache;

        public event EventHandler CanExecuteChanged = delegate { };

        public DelegateCommand(Action execute)
        {
            this.execute = execute;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public bool CanExecute(object parameter)
        {
            var canExecuteFinal = this.canExecute == null || this.canExecute();
            if (this.canExecuteCache != canExecuteFinal)
            {
                this.canExecuteCache = canExecuteFinal;
                CanExecuteChanged(this, EventArgs.Empty);
            }

            return this.canExecuteCache;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;
        private bool canExecuteCache;

        public event EventHandler CanExecuteChanged = delegate { };

        public DelegateCommand(Action<T> execute)
        {
            this.execute = execute;
        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            var canExecuteFinal = this.canExecute == null || this.canExecute((T)parameter);
            if (this.canExecuteCache != canExecuteFinal)
            {
                this.canExecuteCache = canExecuteFinal;
                CanExecuteChanged(this, EventArgs.Empty);
            }

            return this.canExecuteCache;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
