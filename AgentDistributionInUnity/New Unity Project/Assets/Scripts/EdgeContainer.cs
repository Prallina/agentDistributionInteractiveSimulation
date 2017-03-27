using System.Collections.Generic;

namespace AgentDistribution
{
    class EdgeContainer
    {
        private static EdgeContainer instance;
        private List<GraphEdge> allEdges;

        private EdgeContainer()
        {
            allEdges = new List<GraphEdge>();
        }

        public static EdgeContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EdgeContainer();
                }
                return instance;
            }
        }

        public void addEdge(int id)
        {
            allEdges.Add(new GraphEdge(id));
        }

        public GraphEdge getEdge(int id)
        {
            foreach (GraphEdge e in allEdges)
            {
                if (e.ID == id)
                {
                    return e;
                }
            }
            return null;
        }


        public List<GraphEdge> getEdgeSet(int[] ids)
        {
            List<GraphEdge> tmp = new List<GraphEdge>(ids.Length);
            foreach (int i in ids)
            {
                foreach (GraphEdge g in allEdges)
                {
                    if (g.ID == i)
                    {
                        tmp.Add(g);
                        break;
                    }
                }
            }
            return tmp;
        }
        // override
        public void updateEdges(GraphEdge e_new)
        {
            foreach (GraphEdge e in allEdges)
            {
                if (e_new.ID == e.ID)
                {
                    e.Tau = e_new.Tau;
                    e.Eta = e_new.Eta;
                }
            }

        }
    }
}