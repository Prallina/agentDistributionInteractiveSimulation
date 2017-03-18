using System.Collections.Generic;
using Pheromones_in_Networks;

namespace AgentDistribution
{
    class NodeContainer
    {
        private static NodeContainer instance;
        private List<GraphNode> allNodes;

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

        public void addEdge(int id)
        {
            allNodes.Add(new GraphNode(id));
        }

        public GraphNode getEdge(int id)
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