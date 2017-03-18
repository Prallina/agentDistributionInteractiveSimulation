
namespace Pheromones_in_Networks
{
    public static class AcoConstants
    {
        //Var
        // influence of pheromone on direction
        private static double _alpha = 3.0;
        // influence of adjacent node distance
        private static double _beta = 0.02;
        // pheromone decrease factor (evaporation rate)
        private static double _rho = 0.01;
        // pheromone increase factor
        private static double _Q = 2.0;
        // added pheromone when an edge ist traversed 
        private static double _tau0 = 2.0;
        //
        private static double _eta0 = 1.0;
        // number of nodes
        private static int _numOfNodes = 10;
        // number of edges
        private static int _numOfEdges = _numOfNodes * (_numOfNodes - 1) / 2;


        public static double Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }
        public static double Beta
        {
            get { return _beta; }
            set { _beta = value; }
        }
        public static double Rho
        {
            get { return _rho; }
            set
            {
                if(value > 1)
                {
                    _rho = 1;
                }
                else if(value <= 0)
                {
                    _rho = 0.00000001;
                }
                else
                {
                    _rho = value;
                }
                
            }
        }
        public static double Q
        {
            get { return _Q; }
            set { _Q = value; }
        }
        public static double InitialTauValue
        {
            get { return _tau0; }
            set { _tau0 = value; }
        }
        public static double InitialEtaValue
        {
            get { return _eta0; }
            set { _eta0 = value; }
        }
        public static double AddTau
        {
            get { return _tau0; }
            set { _tau0 = value; }
        }
        public static double AddEta
        {
            get { return _eta0; }
            set { _eta0 = value; }
        }
        public static int NumOfNodes
        {
            get { return _numOfNodes; }
            set { _numOfNodes = value; }
        }
        public static int NumOfEdges
        {
            get { return _numOfEdges; }
            //set { _numOfEdges = value; }
        }
    }
}
