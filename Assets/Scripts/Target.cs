using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;

public class Target : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public PathFinding[] guardAI;
    public Transform playerTarget;
    
    void Start()
    {
        guardAI = FindObjectsOfType<PathFinding>();
    }
    
   public void Hit()
   {
        Debug.Log("Target hit");

        for(int i = 0; i < guardAI.Length; i++)
        {
           if(guardAI[i] != null)
           {
                guardAI[i].enabled = false;
           }
           
        }
        agent.SetDestination(playerTarget.position);
   }
}
