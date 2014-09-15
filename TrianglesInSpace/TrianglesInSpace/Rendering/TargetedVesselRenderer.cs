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

            m_Bus.Subscribe<HighlightTargetMessage>(OnHighlight).AddTo(m_Disposer);
            m_Bus.Subscribe<AttackTargetMessage>(OnAttack).AddTo(m_Disposer);
            m_Bus.Subscribe<SelectedObjectMessage>(OnSelection).AddTo(m_Disposer);
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
                }

                m_CurrentTarget = null;

                
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
