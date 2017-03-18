using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentDistribution
{
    class Network
    {
        
        private EdgeContainer ec;
        private NodeContainer nc;
        private AdjacencyMatrix m;


        public Network(int n)
        {
            ec = EdgeContainer.Instance;
            nc = NodeContainer.Instance;
            m = new AdjacencyMatrix(n);

        }

        public int[] getEdgesIDs(int id)
        {
           return  m.getEdgesIDs(id);
        }
    }
}
