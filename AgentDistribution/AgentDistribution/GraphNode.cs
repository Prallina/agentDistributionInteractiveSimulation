using System.Collections.Generic;

namespace Pheromones_in_Networks
{
    public class GraphNode
    {
        private bool _isCurrentNode;
        private int _id;

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
        public GraphNode(int id)
        {
            _id = id;
        }

        // calculates the probabilites for all edges
        public void calcProbabilites(ref List<GraphEdge> edges)
        {
            double sum = SumOfAllEgdes(ref edges);
            foreach (GraphEdge e in edges)
            {
                e.Probability = (System.Math.Pow(e.Tau, AcoConstants.Alpha) * System.Math.Pow(e.Eta, AcoConstants.Beta)) / sum;
            }
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


        //
        public GraphNode findBestEdge( List<GraphEdge> edges)
        {
            calcProbabilites(ref edges);
            //    GraphNode selectedNode = null;
            //    System.Random r = new System.Random();
            //    double diceRoll = r.NextDouble();

            //    double cumulative = 0.0;
            //    foreach(GraphEdge g in this.Edges)
            //    {
            //        cumulative += g.Probability;
            //        if (diceRoll < cumulative)
            //        {
            //            g.updateTau(true);
            //            selectedNode = g.Tail;
            //            break;
            //        }
            //        g.updateTau(false);
            //    }
            //    return selectedNode;


            //GraphEdge best = null;
            //double min = double.MaxValue;
            //foreach (GraphEdge g in this.Edges)
            //{
            //    if (g.Probability < min)
            //    {
            //        min = g.Probability;
            //        best = g;
            //    }
            //}
            //foreach (GraphEdge g in this.Edges)
            //{
            //    if (g.Probability == min)
            //    {
            //        g.IsChoosen = true;
            //        g.updateTau(true);

            //    }
            //    g.updateTau(false);
            //}
            return null;
        }

        public void updateEdgeContainer()
        {

        }
    }

}
