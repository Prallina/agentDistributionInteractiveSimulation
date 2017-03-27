namespace AgentDistribution
{
    public class GraphEdge
    {
        private int _id;

        private bool _isChoosen;

        // amount of pheromone  
        private double _tau;
        // desirability of state transition from _head to _tail
        private double _eta;
        // probability of state transition from _head to _tail
        private double _probability;


        // Getter/Setter
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool IsChoosen
        {
            get { return _isChoosen; }
            set { _isChoosen = value; }
        }
        public double Tau
        {
            get { return _tau; }
            set { _tau = value; }
        }
        public double Eta
        {
            get { return _eta; }
            set { _eta = value; }
        }

        public double Probability
        {
            get { return _probability; }
            set { _probability = value; }
        }
        public GraphEdge(int id)
        {
            ID = id;
            IsChoosen = false;
            Tau = AcoConstants.InitialTauValue; // initial Pheromone
            Eta = AcoConstants.InitialEtaValue; // change to a method
        }

        // update Tau if this edge is choosen from the guard agent
        public void updateTau(bool isbest)
        {
            double delta = 0;
            if (isbest)
            {
                delta = AcoConstants.AddTau;
            }
            this.Tau = (1 - AcoConstants.Rho) * this.Tau + AcoConstants.Rho * delta;
        }

        public override bool Equals(object obj)
        {
            var e = obj as GraphEdge;
            if (object.ReferenceEquals(e, null))
                return false;
            return ID == e.ID && Tau == e.Tau && Eta == e.Eta;
        }

        public override int GetHashCode()
        {
            var hc = 0;
            hc = ID.GetHashCode();
            hc = unchecked((hc * 7) ^ (int)Tau);
            hc = unchecked((hc * 21) ^ (int)Eta);
            return hc;
        }
    }
}