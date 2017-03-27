using System;
using System.Collections.Generic;
using UnityEngine;

namespace AgentDistribution 
{
    class Network : MonoBehaviour
    {
        public GameObject guardprefab;
        public GameObject playerprefab;

        private AdjacencyMatrix mat;
        private GameObject node = null;
        private List<int> occupied;

        // Use this for initialization
        void Start()
        {
            initNetwork(AcoConstants.NumOfNodes);
            initAgents(AcoConstants.NumOfGuards, AcoConstants.NumOfPlayer);   
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void initNetwork(int numOfNodes)
        {
            initNodes(numOfNodes);
            mat = new AdjacencyMatrix(numOfNodes);
            initAdjcencyMatrix(numOfNodes);
            initEdges(numOfNodes);
        }

        private void initNodes(int n)
        {
            int counter = 1;
            int dim = (int)Mathf.Sqrt(n);

            for (int row = 0; row < dim; row++)
            {
                for (int column = 0; column < dim; column++)
                {
                    node = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    node.transform.position = new Vector3((column + 1) * 10, 0, (row + 1) * 10);
                    node.GetComponent<Renderer>().material.color = Color.red;

                    NodeContainer.Instance.createNewNode(counter, node.transform.position);

                    counter++;
                }
            }
        }

        private void initAdjcencyMatrix(int n)
        {
            int dim = (int)Mathf.Sqrt(n);

            for (int i = 0; i < n; i++)
            {

                // in unterster Reihe...
                if (i == n - 1)
                {
                    // letzter Knoten
                    mat.setEdge(i, i, 0);
                }
                else if ((n - (i + 1)) < dim)
                {
                    // unterste Knoten bis auf den Letzten
                    mat.setEdge(i, i + 1, 1);
                }
                else
                {
                    // in letzter Spalte
                    if ((i + 1) % dim == 0)
                    {
                        mat.setEdge(i, i + dim, 1);
                    }
                    else
                    {
                        mat.setEdge(i, i + 1, 1);
                        mat.setEdge(i, i + dim, 1);
                    }
                }

            }

        }

        private void initEdges(int n)
        {
            int dim = (int)Mathf.Sqrt(n);
            int idCounter = 1;

            for (int row = 0; row < dim; row++)
            {
                for (int column = 0; column < dim; column++)
                {
                    if (mat.getEdge(row, column) == 1)
                    {
                        mat.setEdge(row, column, idCounter);
                        EdgeContainer.Instance.addEdge(idCounter);

                        idCounter++;
                    }

                }
            }
        }

        private void initAgents(int numOfAgents, int numOfPlayer)
        {
            if (numOfAgents + numOfPlayer >= AcoConstants.NumOfNodes)
            {
                throw new System.Exception("To many Agents and Player");
            }
            occupied = new List<int>(numOfAgents + numOfPlayer);
            distributeAgents(numOfAgents, guardprefab);
            distributeAgents(numOfPlayer, playerprefab);

        }

        private void distributeAgents(int num, GameObject prefab)
        {
            GraphNode[] tmp = getStartNodes(num);

            for (int i= 0; i < num; i++)
            {
                Instantiate(prefab, tmp[i].Position, Quaternion.identity);
            }
        }       

        private GraphNode[] getStartNodes(int num)
        {
            int index = 0;
            System.Random rnd = new System.Random();
            GraphNode[] list = new GraphNode[num];
            
            for (int i = 0; i < num; i++)
            {
                index = rnd.Next(0, NodeContainer.Instance.Count);

                if (!occupied.Contains(index))
                {
                    occupied.Add(index);
                    list[i] = NodeContainer.Instance.getNode(index);
                }
                else
                {
                    // try again
                    i--;
                }
            }
            return list;
        }

        public int[] getOutgoingEdges(int id)
        {
            return mat.getEdgesIDs(id);
        }
    }
}