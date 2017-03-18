using System;
using Pheromones_in_Networks;


namespace AgentDistribution
{
    class Program
    {
        static void Main(string[] args)
        {


            Network network = new Network(AcoConstants.NumOfNodes);

            //Gebe IDs der Kanten vom Knoten mit der ID 4 aus 
            int[] _result = network.getEdgesIDs(4);
            Console.WriteLine("\n--\n");
            foreach (int i in _result)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}