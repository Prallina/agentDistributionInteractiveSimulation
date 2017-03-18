using UnityEngine;
using UnityEngine.UI;
using Pheromones_in_Networks;
using System.Collections.Generic;
using System;

public class GuardBehaviourScript : MonoBehaviour
{
    private List<GraphNode> nodePositions;
    private GraphNode tmp_n;
    private NavMeshAgent agent;
    private GraphNode nextNode = null;
    public Text currPos;
    public Text transText;
    public GameObject playerObject;

    private bool stopCalculation;
    private bool isHunting;
    private GuardStates currentState;
    

    

    /**watch for Player**/
    // angle in degree 
    private float fieldOfViewRange = 45;
    // distance to detect the player 
    //private int minPlayerDetectDistance = 2;

    private Vector3 lastKnownPostionsOfPlayer;
    private Vector3 lastPositionOnPatrol;

    //New---
    public Vector3 LastKnownPostionsOfPlayer
    {
        get
        {
            return lastKnownPostionsOfPlayer;
        }

        set
        {
            lastKnownPostionsOfPlayer = value;
        }
    }

    public GuardStates CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }
    //---
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentState = GuardStates.Patrol;
        initNetwork();

        // (15, 0, 5) = current Starting Position
        nextNode = mapVector3ToNode(new Vector3(15, 0, 5));
        nextNode.IsCurrentNode = true;

        agent.destination = nextNode.Position;
        lastPositionOnPatrol = agent.transform.position;
  
    }
    

    // Update is called once per frame
    void Update()
    {
        //New if clauses
        //Behaviour
        //look for the Player all the time, deterimines the State
        
        //New
        if (CurrentState != GuardStates.GameOver)
        {
           //currentState = watchForPlayer();
        }
        //---
        // depending on the state, do actions 
        if (CurrentState == GuardStates.Patrol)
        {
            patrolWithPheromones();
        }
        //New---
        if(CurrentState == GuardStates.CameraDetection)
        {
            checkLastKnownPostionOfPlayer();
        }
        //---
        if (CurrentState == GuardStates.Hunt)
        {
            //huntPlayer();
        }
        if (CurrentState == GuardStates.GoBack)
        {
            //goBackToPatrol();
        }
        if(CurrentState == GuardStates.GameOver)
        {
            //New
            //gameOver();
        }

    }

    private void initNetwork()
    {
        initConstants();
        initNodes();
        initEdges();
    }
    private void initConstants()
    {
        AcoConstants.Alpha = 3;
        AcoConstants.Beta = 0.02;

    }
    private void initNodes()
    {
        nodePositions = new List<GraphNode>();
        //start 
        tmp_n = new GraphNode(new Vector3(15, 0, 5));
        nodePositions.Add(tmp_n);

        //1
        tmp_n = new GraphNode(new Vector3(3, 0, 10));
        nodePositions.Add(tmp_n);

        //2
        tmp_n = new GraphNode(new Vector3(3, 0, 25));
        nodePositions.Add(tmp_n);

        //3
        tmp_n = new GraphNode(new Vector3(35, 0, 35));
        nodePositions.Add(tmp_n);

    }
    private void initEdges()
    {
        //1<->2
        mapVector3ToNode(new Vector3(3, 0, 10)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(3, 0, 10)), mapVector3ToNode(new Vector3(3, 0, 25))));
        mapVector3ToNode(new Vector3(3, 0, 25)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(3, 0, 25)), mapVector3ToNode(new Vector3(3, 0, 10))));

        //2<->3
        mapVector3ToNode(new Vector3(3, 0, 25)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(3, 0, 25)), mapVector3ToNode(new Vector3(35, 0, 35))));
        mapVector3ToNode(new Vector3(35, 0, 35)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(35, 0, 35)), mapVector3ToNode(new Vector3(3, 0, 25))));

        //2<->Start
        mapVector3ToNode(new Vector3(3, 0, 25)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(3, 0, 25)), mapVector3ToNode(new Vector3(15, 0, 5))));
        mapVector3ToNode(new Vector3(15, 0, 5)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(15, 0, 5)), mapVector3ToNode(new Vector3(3, 0, 25))));

        //3<->Start
        mapVector3ToNode(new Vector3(35, 0, 35)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(35, 0, 35)), mapVector3ToNode(new Vector3(15, 0, 5))));
        mapVector3ToNode(new Vector3(15, 0, 5)).addEdge(new GraphEdge(mapVector3ToNode(new Vector3(15, 0, 5)), mapVector3ToNode(new Vector3(35, 0, 35))));


    }
    private void goToNextNode()
    {      
        agent.destination = nextNode.Position;
    }
    Vector3 getStartPosition()
    {
        return GameObject.FindGameObjectWithTag("target_start").transform.position;  
    }   
    public GraphNode mapVector3ToNode(Vector3 v)
    {
        foreach( GraphNode n in nodePositions)
        {
            if(n.Position == v){
                return n;
            }
        }
        return null;
    }
    // method called every frame when the guard have to  partol
    public void patrolWithPheromones()
    {
        // input :nextnode
        if (agent.remainingDistance < 1.0f)
        {
            stopCalculation = !stopCalculation;
        }
        //agent arrives at next target
        if (agent.remainingDistance == 0.0f && stopCalculation == false)
        {
            // calculation for next destination...
            System.Console.WriteLine("dist == 0");

            if (nextNode == null)
            {
                Debug.Log("NextNode == null");
            }
            else
            {
                Debug.Log("NextNode != null");
            }

            nextNode.calcProbabilites();
            nextNode.findBestEdge();
            nextNode.IsCurrentNode = false;

            // other (new) nextNode is the target...
            nextNode = nextNode.getDestinationOfBestEdge();
            nextNode.IsCurrentNode = true;
            stopCalculation = true;

            // debug
            goToNextNode();
            currPos.text = "Target:" + nextNode.Position.ToString() + " and Current" + agent.transform.position;

            // ouput :  nextnode <- new destination
        }
        // buffer last position
        lastPositionOnPatrol = agent.transform.position;
    }
    public GuardStates watchForPlayer()
    {
        RaycastHit hit;
        Vector3 rayDirection = playerObject.transform.position - transform.position;

        // Detect if player is within the field of view
        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfViewRange){ 
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    //Debug.Log("HUNT");
                    isHunting = true;
                    return GuardStates.Hunt;
                }
                else
                {
                    if (CurrentState == GuardStates.Hunt)
                    {
                        //Debug.Log("GoBack");
                        isHunting = false;
                        return GuardStates.GoBack;
                    }
                    if (CurrentState == GuardStates.Patrol)
                    {
                        //Debug.Log("PATROL");
                        return GuardStates.Patrol;
                    }
                }
            }
            //Debug.Log("Status quo(H): " + CurrentState.ToString());
            return CurrentState;
        }
        else
        {
            if (CurrentState == GuardStates.Hunt)
            {
                //Debug.Log("GOBACK");
                isHunting = false;
                return GuardStates.GoBack;
            }
            if (CurrentState == GuardStates.Patrol)
            {
                //Debug.Log("PATROL");
                return GuardStates.Patrol;
            }

            //status quo
            //Debug.Log("Status quo(V): "+ CurrentState.ToString());
            return CurrentState;
        }      
    }
    public void huntPlayer()
    {
        // set the current position of the player as new destination
        agent.destination = playerObject.transform.position;
        //New
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);
        //New
        if (distance < 2.0f)
        {
            //player catched -> simulation is over
            CurrentState = GuardStates.GameOver;      
        }
    }
    //New---
    private void checkLastKnownPostionOfPlayer()
    {
        //checkLast Position
        agent.SetDestination(LastKnownPostionsOfPlayer);


        //if player is not there, return to patrol
        if (agent.pathStatus == NavMeshPathStatus.PathComplete){
            currentState = GuardStates.GoBack;
        }
    }
    //---
    public void goBackToPatrol()
    {
        // last positions on the path is the new destination
        agent.destination = lastPositionOnPatrol;
        currPos.text = "CP: "+ agent.transform.position;
        transText.text = "DP: " + agent.destination;

        Vector3 distance = agent.transform.position - agent.destination;
        // if back on path...
        if (distance.magnitude < 2.0f)
        {
            // ...continue patrolling
            CurrentState = GuardStates.Patrol;
            goToNextNode();
        }
    }

    public void gameOver()
    {
        agent.Stop();
        Debug.Log("Game OVER");
    }
}
