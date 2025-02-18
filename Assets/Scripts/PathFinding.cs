using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
        UpdateDestination();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) <= agent.stoppingDistance + 0.5f)
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
