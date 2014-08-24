using Mogre;
using TrianglesInSpace.Motion;
using System.Collections.Generic;

namespace TrianglesInSpace.Rendering
{
    public class NodeWithPosition
    {
        private readonly SceneNode m_SceneNode;
        private CombinedMotion m_Motion;

        private List<SceneNode> m_SelectedNodes;
        private bool m_Selected = false;

        private readonly string m_PathId;


        public NodeWithPosition(SceneNode sceneNode, CombinedMotion startingMotion)
        {
            m_SceneNode = sceneNode;
            m_PathId = m_SceneNode.Name;
            m_Motion = startingMotion;

            m_SelectedNodes = new List<SceneNode>();
            m_SelectedNodes.Add(sceneNode);
        }

        public NodeWithPosition(SceneNode sceneNode, string pathid, CombinedMotion startingMotion)
        {
            m_SceneNode = sceneNode;
            m_PathId = pathid;
            m_Motion = startingMotion;

            m_SelectedNodes = new List<SceneNode>();
            m_SelectedNodes.Add(sceneNode);
        }

        public string Name
        {
            get
            {
                return m_SceneNode.Name;
            }
        }

        public string PathId
        {
            get
            {
                return m_PathId;
            }
        }

        public CombinedMotion Motion
        {
            get
            {
                return m_Motion;
            }
            set
            {
                m_Motion = value;
            }
        }

        public void UpdatePosition(ulong time)
        {
            ulong count = 0;
            foreach (var node in m_SelectedNodes)
            {
                ulong currentTime = time + 1500* count;
                count++;
                var currenmtMotion = m_Motion.GetCurrentMotion(currentTime);
                var currentPositon = currenmtMotion.GetCurrentPosition(currentTime);
                node.Position = VectorConversions.ToOgreVector(currentPositon);

                var rotation = new Primitives.Angle(currenmtMotion.GetVelocity(currentTime));
                rotation.ReduceAngle();

                Quaternion quat = new Quaternion(new Radian(rotation.Value), new Vector3(0, -1, 0));
                node.Orientation = quat;
            }
        }

        public void OnSelected(SceneManager manager, bool owned)
        {
            var entity = manager.GetEntity(m_SceneNode.Name);

            string materialName;

            if (owned)
            {
                materialName = "triangle/red";
            }
            else
            {
                materialName = "triangle/blue";
            }

            using (var material = MaterialManager.Singleton.GetByName(materialName))
            {
                entity.SetMaterial(material);
            }
            if (!m_Selected)
            {
                m_Selected = true;
                using (var material = MaterialManager.Singleton.GetByName("triangle/white"))
                {
                    for(int i = 0; i < 10; i++)
                    {
                        string markerName = m_SceneNode.Name+"markerName"+ i;
                        var pathMarker = manager.CreateEntity(markerName, "triangle");
                        var node = manager.RootSceneNode.CreateChildSceneNode(markerName);
                        node.AttachObject(pathMarker);
                    
                        node.SetScale(0.5,0.5,0.5);
                        m_SelectedNodes.Add(node);

                        pathMarker.SetMaterial(material);
                    }
                }
            }
        }

        public void OnDeselected(SceneManager manager)
        {
            var entity = manager.GetEntity(m_SceneNode.Name);

            using (var material = MaterialManager.Singleton.GetByName("triangle/white"))
            {
                entity.SetMaterial(material);
            }
            if (m_Selected)
            {
                m_Selected = false;
                m_SelectedNodes.RemoveAt(0);
                foreach(var node in m_SelectedNodes)
                {
                    manager.DestroyEntity(node.Name);
                    node.RemoveAndDestroyAllChildren();
                    manager.RootSceneNode.RemoveAndDestroyChild(node.Name);
                }
                m_SelectedNodes.Clear();
                m_SelectedNodes.Add(m_SceneNode);
            }
        }
    }
}
