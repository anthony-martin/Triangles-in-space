using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Objects;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Vessels;
using TrianglesInSpace.Primitives;


namespace TrianglesInSpace.Rendering
{
    public interface ITargetedVesselRenderer
    {
    }
    public class TargetedVesselRenderer : IDisposable, ITargetedVesselRenderer
    {
        private readonly IBus m_Bus;
        private readonly ISelectableObjectRepository m_Targets;

        private Disposer m_Disposer; 

        private IRenderer m_Renderer;
        private IPlayerId m_Id;
        private IVesselRepository m_VesselRepository;

        private string m_CurrentTarget;

        private string m_CurrentSelection;

        private List<Attack> m_Attacks;


        public TargetedVesselRenderer(IBus bus,
                                      ISelectableObjectRepository targets,
                                      IRenderer renderer,
                                      IPlayerId id,
                                      IVesselRepository vesselRepository)
        {
            m_Disposer = new Disposer();
            m_Bus = bus;
            m_Targets = targets;

            m_Renderer = renderer;
            m_Id = id;
            m_VesselRepository = vesselRepository;

            m_Attacks = new List<Attack>();

            m_Bus.Subscribe<HighlightTargetMessage>(OnHighlight).AddTo(m_Disposer);
            m_Bus.Subscribe<AttackTargetMessage>(OnAttack).AddTo(m_Disposer);
            m_Bus.Subscribe<SelectedObjectMessage>(OnSelection).AddTo(m_Disposer);
            m_Bus.Subscribe<TimeUpdateMessage>(OnTick).AddTo(m_Disposer);
        }

        private void OnHighlight(HighlightTargetMessage message)
        {
            var target = m_Targets.GetObjectAt(message.WorldPosition, message.Time).FirstOrDefault();
            if (target == null && m_CurrentTarget != null)
            {
                m_Renderer.Scene.Remove(m_CurrentTarget + "target");
                m_CurrentTarget = null;
            }
            else if (target != null && m_CurrentTarget != target.Name)
            {
                if (m_CurrentTarget != null)
                {
                    m_Renderer.Scene.Remove(m_CurrentTarget + "target");
                }
                m_CurrentTarget = target.Name;

                m_Renderer.Scene.Add(m_CurrentTarget + "target", m_CurrentTarget, "square", "target_highlight");
                m_Bus.Send(new RequestPathMessage(m_CurrentTarget));
            }
            
        }


        private void OnAttack(AttackTargetMessage message)
        {
            if (m_CurrentTarget != null )
            {
                m_Renderer.Scene.Remove(m_CurrentTarget + "target");
                
                var vessel = m_VesselRepository.GetByName(m_CurrentSelection);
                var target = m_VesselRepository.GetByName(m_CurrentTarget);
                if (vessel != null && target != null)
                {
                    // get vessel position
                    Vector vesselPositon = vessel.GetPosition(message.Time);
                    
                    // get the targets position
                    Vector targetPosition = target.GetPosition(message.Time);
                    
                    // zero around vessel
                    Vector vesselToTarget = targetPosition - vesselPositon;
                    
                    // get the facing so we can check weapons.
                    Facing facingWeapons = vessel.GetHeading(message.Time).ToFacing(vesselToTarget);
                    
                    // get weapons
                    var weapons =  vessel.AvailableWeapons(facingWeapons, vesselToTarget.Length);
                    // post firing.
                    // send???
                    if (weapons.Any())
                    {
                        m_Attacks.Add(new Attack(message.Time, m_CurrentTarget, vesselPositon));

                        m_Renderer.Scene.Add("Attack" + message.Time, m_CurrentTarget, "square", "target_highlight");
                        m_Bus.Send(new RequestPathMessage(m_CurrentTarget));
                    }
                }

                m_CurrentTarget = null;

                
            }
        }

        private class Attack
        {
            //private List<IWeaponSystem> m_Weapons;
            private ulong m_FiringTime;
            private string m_Target;
            private Vector m_FiringPosition;
            public Attack(ulong firingTime, string target, Vector firingPosition)
            {
                m_FiringTime = firingTime;
                m_Target = target;
                m_FiringPosition = firingPosition;
            }

            public ulong FiringTime
            {
                get
                {
                    return m_FiringTime;
                }
            }

            public string Target
            {
                get
                {
                    return m_Target;
                }
            }

            public Vector FiringPosition
            {
                get
                {
                    return m_FiringPosition;
                }
            }
        }

        public void OnTick(TimeUpdateMessage message)
        {
            var remove = new List<Attack>();

            foreach(var attack in m_Attacks)
            {
                var target = m_VesselRepository.GetByName(attack.Target);
                if (target != null)
                {
                    Vector targetPosition = target.GetPosition(message.Time);

                    var distance = (targetPosition - attack.FiringPosition).Length;

                    var travelTime = distance  / BaseConstants.WeaponSpeed   * 1000;

                    double elapsedTime = (double)( message.Time - attack.FiringTime);

                    if( travelTime <= elapsedTime)
                    {
                        // we hit explode things here
                        m_Renderer.Scene.Remove("Attack" + attack.FiringTime);
                        remove.Add(attack);
                    }
                }
                else
                {
                    remove.Add(attack);
                }
            }

            foreach (var attack in remove)
            {
                m_Attacks.Remove(attack);
            }
        }

        private void OnSelection(SelectedObjectMessage message)
        {
            if (message.Owned)
            {
                m_CurrentSelection = message.SelectedName;
            }
        }

        public virtual void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
