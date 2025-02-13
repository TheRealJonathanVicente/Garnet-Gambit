using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardTestMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform testLoc;
    public Transform Loc1;
    public Transform Loc2;

    public bool firstMove = true;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       // NavMeshHit hit;
       agent.SetDestination(testLoc.position);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        agent.SetDestination(Loc2.position);
    }

    
}
