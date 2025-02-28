using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    [Header("References")]
    public AudioSource guardAudio;
    public GameObject player;
    public GameObject noticeMark;
    public Transform childTransform;
    public Animator anim;

    [Header("Waypoints")]
    public Transform[] waypoints;


    NavMeshAgent agent;
    Collider agentCol;
    Vector3 target;

    private int waypointIndex;
    

    private EnemyVision enemyVision;

    private GameObject newObject;
    private int tempMark = 0;
    private bool hasDetected = false;

    
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
            if(!hasDetected)// run coroutine only when player first enters vision
            {
                hasDetected = true;
                anim.SetTrigger("Detected");
                StartCoroutine(DetectedAttack());
            }
            
            agent.SetDestination(player.transform.position);
        }
        else if(!enemyVision.isInAngle)
        {
            if(hasDetected)
            {
                hasDetected = false; // Resets detection when player leaves vision   
            }
        }

        
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
        agent.isStopped = true; 

        if(tempMark == 0)
        {
       
         newObject = Instantiate(noticeMark, childTransform.position, Quaternion.identity); 
         guardAudio.Play();
         newObject.transform.SetParent(childTransform);
         tempMark++;   
         Destroy(newObject, 1.8f);
        }

        yield return new WaitForSeconds(1.8f);

        agent.isStopped = false;
        tempMark = 0;
    }
}
