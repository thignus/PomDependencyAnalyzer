using System;
using System.Windows.Input;

namespace PomDependencyAnalyzer.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public void Execute(object paramater)
        {
            this.action();
        }

        public bool CanExecute(object paramater)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
