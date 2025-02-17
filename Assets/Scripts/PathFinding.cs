using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) <= 1)
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
