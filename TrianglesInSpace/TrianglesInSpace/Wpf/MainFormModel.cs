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
using TrianglesInSpace.Objects;
using TrianglesInSpace.World;

namespace TrianglesInSpace.Wpf
{
    public interface IMainFormModel 
    {
    }
    public class MainFormModel : INotifyPropertyChanged, IMainFormModel
    {
        private readonly ICommand m_Add ;
        private readonly IBus m_Bus;
        private IPlayerId m_Id;

        private Guid m_PlayerOneId = Guid.NewGuid();
        private Guid m_PlayerTwoId = Guid.NewGuid();
        private readonly ICommand m_PlayerOne;
        private readonly ICommand m_PlayerTwo;

        private readonly ICommand m_Attack;

        private int count = 0;

        public MainFormModel(IBus bus, 
                             IPlayerId id,
                             IFieldDisplay field)
        {
            m_Bus = bus;
            m_Id = id;
            m_Id.Id = m_PlayerOneId;
            m_Add = new RelayCommand(OnAddVessel, null);
            m_PlayerOne = new RelayCommand(SetPlayerOne, null);
            m_PlayerTwo = new RelayCommand(SetPlayerTwo, null);

            m_Attack = new RelayCommand(OnAttackMode, null);
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

        public ICommand OnAttack
        {
            get
            {
                return m_Attack;
            }
        }

        private void OnAttackMode()
        {
            m_Bus.Send(new ChangeInputModeMessage(InputMode.Attack));
            //count++;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OnPlayerOne
        {
            get
            {
                return m_PlayerOne;
            }
        }

        private void SetPlayerOne()
        {
            m_Id.Id = m_PlayerOneId;
        }

        public ICommand OnPlayerTwo
        {
            get
            {
                return m_PlayerTwo;
            }
        }

        private void SetPlayerTwo()
        {
            m_Id.Id = m_PlayerTwoId;
        }

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
