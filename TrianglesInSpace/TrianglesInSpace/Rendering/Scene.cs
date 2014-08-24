using System;
using System.Collections.Generic;
using System.Linq;
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
            m_Bus.Subscribe<SelectedObjectMessage>(OnSelected).AddTo(m_Disposer);
            m_Bus.Subscribe<DeselectedObjectMessage>(OnDeselected).AddTo(m_Disposer);
        }

        private void OnAdd(AddObjectMessage message)
        {
            Add(message.Name, message.Shape);
        }

        public void Add(string name, string shape)
        {
            var entity = m_SceneManager.CreateEntity(name, shape);
            var node = m_SceneManager.RootSceneNode.CreateChildSceneNode(name);
            node.AttachObject(entity);

            using(var material = MaterialManager.Singleton.GetByName("triangle/white"))
            {
                entity.SetMaterial(material);
            }

            m_SceneNodes.Add(new NodeWithPosition(node, new CombinedMotion(DefaultMotion())));
        }

        public void Add(string name, string pathId, string shape, string materialName = "triangle/white")
        {
            var entity = m_SceneManager.CreateEntity(name, shape);
            var node = m_SceneManager.RootSceneNode.CreateChildSceneNode(name);
            node.AttachObject(entity);

            using (var material = MaterialManager.Singleton.GetByName(materialName))
            {
                entity.SetMaterial(material);
            }

            m_SceneNodes.Add(new NodeWithPosition(node, pathId, new CombinedMotion(DefaultMotion())));
        }

        public void OnSelected(SelectedObjectMessage message)
        {
            foreach (var node in m_SceneNodes.Where(x => x.Name == message.SelectedName))
            {
                node.OnSelected(m_SceneManager, message.Owned);
            }
        }

        public void OnDeselected(DeselectedObjectMessage message)
        {
            foreach (var node in m_SceneNodes.Where(x => x.Name == message.DeselectedName))
            {
                node.OnDeselected(m_SceneManager);
            }
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
            foreach( var node in m_SceneNodes)
            {
                if (node.PathId == message.Name)
                {
                    node.Motion = message.Motion;
                }
            }
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
