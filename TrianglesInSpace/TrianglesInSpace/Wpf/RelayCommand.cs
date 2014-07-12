using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrianglesInSpace.Wpf
{
    class RelayCommand : ICommand
    {
        private Action m_Execute;
        private Func<bool> m_CanExecute;
        public RelayCommand(Action onExecute, Func<bool> canExecute)
        {
            m_Execute = onExecute;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_CanExecute != null ? m_CanExecute() : true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            m_Execute();
        }
    }
}
