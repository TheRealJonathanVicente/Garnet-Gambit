using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    NavMeshAgent agent;
    Collider agentCol;
    public Transform[] waypoints;
    private int waypointIndex;
    Vector3 target;
    public GameObject player;

    private EnemyVision enemyVision;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyVision = GetComponent<EnemyVision>();
        
        waypointIndex = 0;
        UpdateDestination();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // If enemy sees player, chase the player
        if (enemyVision.allTrue)
        {
            agent.SetDestination(player.transform.position);
            return; // Skip further execution to avoid conflicting movements
        }
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            IterateIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        
    }

    void IterateIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) { 
            waypointIndex = 0;
        }
    }
}
