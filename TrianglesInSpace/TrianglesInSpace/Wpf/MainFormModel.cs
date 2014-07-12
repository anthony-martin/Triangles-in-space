using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Primitives;
using InputMode = TrianglesInSpace.Primitives.InputMode;

namespace TrianglesInSpace.Wpf
{
    public interface IMainFormModel 
    {
    }
    public class MainFormModel : INotifyPropertyChanged, IMainFormModel
    {
        private readonly ICommand m_Add ;
        private readonly IBus m_Bus;

        private int count = 0;

        public MainFormModel(IBus bus)
        {
            m_Bus = bus;
            m_Add = new RelayCommand(OnAddVessel, null);
        }

        public ICommand OnAdd
        {
            get
            {
                return m_Add;
            }

        }

        private void OnAddVessel()
        {
            m_Bus.Send(new ChangeInputModeMessage(InputMode.Placement));
            //count++;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
