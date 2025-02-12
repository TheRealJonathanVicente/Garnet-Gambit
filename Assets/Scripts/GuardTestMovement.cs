using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardTestMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform testLoc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(testLoc.position);
    }
}
