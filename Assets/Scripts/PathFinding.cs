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
    public GameObject noticeMark;
    public Transform childTransform;
    public Animator anim;

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
            //play notice mark above guard head
            agent.SetDestination(player.transform.position);
           // GameObject newObject Instantiate(noticeMark, childTransform.position, Quaternion.identity); //make coroutine once alltrue = true, spawn exclamation, wait for 1 seconds, destroy it , wait for seconds (?) change detected to false 
            return; // Skip further execution to avoid conflicting movements
        }
      /*  else if(enemyVision.isInRange && enemyVision.allTrue == false)
        {
            agent.isStopped = true;
            StartCoroutine(DetectedAttack());

        }*/
        


        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            IterateIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;// target destination changes based on array index
        agent.SetDestination(target);// updates destination
        
    }

    void IterateIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) { //infinite cycle 
            waypointIndex = 0;
        }
    }
    IEnumerator DetectedAttack()
    {
        anim.SetTrigger("Detected");
        yield return new WaitForSeconds(1);
        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position);
    }
}
