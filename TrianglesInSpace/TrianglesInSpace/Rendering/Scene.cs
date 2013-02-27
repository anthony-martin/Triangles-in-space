using System;
using System.Collections.Generic;
using Mogre;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;
using Math = Mogre.Math;

namespace TrianglesInSpace.Rendering
{
    public class Scene : IDisposable 
    {
        private readonly IBus m_Bus;
        private readonly SceneManager m_SceneManager;
        private readonly Disposer m_Disposer ;

        private readonly List<NodeWithPosition> m_SceneNodes;

        public Scene(IBus bus, SceneManager sceneManager)
        {
            m_Bus = bus;
            m_SceneManager = sceneManager;
            m_SceneNodes = new List<NodeWithPosition>();
            m_Disposer = new Disposer();

            m_Bus.Subscribe<PathMessage>(UpdateMotion).AddTo(m_Disposer);
        }

        public void Add(string name, string shape)
        {
            var entity = m_SceneManager.CreateEntity(name, shape);
            var node = m_SceneManager.RootSceneNode.CreateChildSceneNode(name);
            node.AttachObject(entity);
            
            m_SceneNodes.Add(new NodeWithPosition(node, new CombinedMotion(DefaultMotion())));
        }

        private IEnumerable<IMotion> DefaultMotion()
        {
            return new List<IMotion>
                {
                    new CircularMotion(0, 50, new Primitives.Angle(0), new Primitives.Angle(Math.PI/10), 20, Vector.Zero)
                };
        }

        private void UpdateMotion(PathMessage message)
        {
            NodeWithPosition node = m_SceneNodes.Find(x => x.Name == message.Name);
            node.Motion = message.Motion;
        }

        public void UpdatePosition(ulong time)
        {
            foreach (var node in m_SceneNodes)
            {
                node.UpdatePosition(time);
            }
        }


        public void Dispose()
        {
            m_SceneManager.RootSceneNode.RemoveAndDestroyAllChildren();
        }
    }
}
