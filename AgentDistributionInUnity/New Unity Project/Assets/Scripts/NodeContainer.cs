using System.Collections.Generic;
using UnityEngine;

namespace AgentDistribution
{
    class NodeContainer
    {
        private static NodeContainer instance;
        private List<GraphNode> allNodes;

        public int Count
        {
            get { return allNodes.Count; }
            //set { _capacity = value; }
        }

        private NodeContainer()
        {
            allNodes = new List<GraphNode>();
        }

        public static NodeContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NodeContainer();
                }
                return instance;
            }
        }

        public void createNewNode(int id, Vector3 position)
        {
            allNodes.Add(new GraphNode(id, position));
        }

        public GraphNode getNode(int id)
        {
            foreach (GraphNode n in allNodes)
            {
                if (n.ID == id)
                {
                    return n;
                }
            }
            return null;
        }


    }
}