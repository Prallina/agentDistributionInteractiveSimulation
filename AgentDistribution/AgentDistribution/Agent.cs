using Pheromones_in_Networks;

namespace AgentDistribution
{
    class Agent
    {
        private int _id;
        private GraphNode _currentNode;
        private int _coverage;
        private Network network;

        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public GraphNode CurrentNode
        {
            get
            {
                return _currentNode;
            }

            set
            {
                _currentNode = value;
            }
        }

        public int Coverage
        {
            get
            {
                return _coverage;
            }

            set
            {
                _coverage = value;
            }
        }
        public Agent(int id, GraphNode currentNode, int coverage)
        {
            ID = id;
            CurrentNode = currentNode;
            Coverage = coverage;
        }

        public void calculateNextStep()
        {
            CurrentNode = CurrentNode.findBestEdge(EdgeContainer.Instance.getEdgeSet(network.getEdgesIDs(CurrentNode.ID)));
            
        }
    }
}
