using UnityEngine;
using System.Collections.Generic;
using AgentDistribution;

public class SetNodesOnTerrain : MonoBehaviour
{
    /*
     * 
     * deprecated 
     * 
     */

    // Node with corresponding Postion
    private Dictionary<GraphNode, Vector3> dict;
    private GameObject node = null;
    private AdjacencyMatrix matrix;

    //NodeContainer nc;
    //EdgeContainer ec;

    // Use this for initialization
    void Start()
    {
        initNodes(AcoConstants.NumOfNodes);
        AdjacencyMatrix mat = new AdjacencyMatrix(AcoConstants.NumOfNodes);
        connectNodes(AcoConstants.NumOfNodes, mat);


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initNodes(int n)
    {
        int dim = (int) Mathf.Sqrt(n);
        int counter = 1;

        for (int row = 0; row < dim; row++)
        {
            for (int column = 0; column < dim; column++)
            {
                node = GameObject.CreatePrimitive(PrimitiveType.Cube);
                node.transform.position = new Vector3((column+1) * 10, 0, (row+1) * 10);
                node.GetComponent<Renderer>().material.color = Color.red;

                NodeContainer.Instance.createNewNode(counter, node.transform.position);

                counter++;
            }
        }
        
    }
 
    private void connectNodes(int n, AdjacencyMatrix mat)
    {
        int dim = (int)Mathf.Sqrt(n);

        for (int i = 0; i < n; i++)
        {
            
            // in unterster Reihe...
            if(i == n - 1)
            {
                // letzter Knoten
                mat.setEdge(i, i, 0);
            }
            else if((n-(i+1))< dim)
            {
                // unterste Knoten bis auf den Letzten
                mat.setEdge(i, i + 1, 1);
            }
            else
            {
                // in letzter Spalte
                if ((i+1)%dim == 0)
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
}
