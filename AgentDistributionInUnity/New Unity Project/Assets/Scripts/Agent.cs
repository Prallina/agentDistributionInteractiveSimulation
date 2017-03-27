using UnityEngine;

namespace AgentDistribution
{
    class Agent : MonoBehaviour
    {
        private int _id;
        private GraphNode _currentNode;
        private GraphNode _nextNode;
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
        public GraphNode NextNode
        {
            get
            {
                return _nextNode;
            }

            set
            {
                _nextNode = value;
            }
        }

        public Agent(int id, GraphNode currentNode)
        {
            ID = id;
            CurrentNode = currentNode;
            NextNode = null;
        }

        public void calculateNextStep()
        {
            if (NextNode != null)
            {
                CurrentNode = NextNode;
            }
            NextNode = CurrentNode.findBestEdge(EdgeContainer.Instance.getEdgeSet(network.getOutgoingEdges(CurrentNode.ID)));

        }
    }
}