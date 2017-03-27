using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AgentDistribution
{
    public class GraphNode
    {
        private bool _isCurrentNode;
        private int _id;
        private Vector3 _position;

        public bool IsCurrentNode
        {
            get { return _isCurrentNode; }
            set { _isCurrentNode = value; }
        }
        public int ID
        {
            get { return _id; }
            //set { _id = value; }
        }
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public GraphNode(int id,Vector3 position)
        {
            _id = id;
            Position = position;
        }

        // calculates the probabilites for all edges
        public void calcProbabilites(ref List<GraphEdge> edges)
        {
            double sum = SumOfAllEgdes(ref edges);
            foreach (GraphEdge e in edges)
            {
                e.Probability = (System.Math.Pow(e.Tau, AcoConstants.Alpha) * System.Math.Pow(e.Eta, AcoConstants.Beta)) / sum;
            }
            // sort the probabilites ascending 
            edges = edges.OrderBy(o => o.Probability).ToList();
        }

        //
        public double SumOfAllEgdes(ref List<GraphEdge> edges)
        {
            double sum = 0;
            foreach (GraphEdge e in edges)
            {
                sum += System.Math.Pow(e.Tau, AcoConstants.Alpha) * System.Math.Pow(e.Eta, AcoConstants.Beta);
            }

            return sum;
        }


        // probabilistic approach
        public GraphEdge chooseEdgeProbabilistic(List<GraphEdge> edges)
        {
            calcProbabilites(ref edges);
            GraphNode selectedNode = null;
            System.Random r = new System.Random();
            double diceRoll = r.NextDouble();

            double cumulative = 0.0;
            foreach (GraphEdge g in edges)
            {
                cumulative += g.Probability;
                if (diceRoll < cumulative)
                {
                    g.updateTau(true);
                    selectedNode = g;
                    break;
                }
                g.updateTau(false);
            }
            return g;
        }

        // deterministic approach, get edge with minimal pheromone
        private GraphEdge chooseEdgeDeterministic(List<GraphEdge> edges)
        {
            GraphEdge selectedEdge = null;
            double min = double.MaxValue;
            foreach (GraphEdge g in edges)
            {
                if (g.Probability < min)
                {
                    min = g.Probability;
                    selectedEdge = g;
                }
            }
            foreach (GraphEdge g in edges)
            {
                if (g.Probability == min)
                {
                    g.IsChoosen = true;
                    g.updateTau(true);

                }
                g.updateTau(false);
            }
            return selectedEdge;
        }
    }

}