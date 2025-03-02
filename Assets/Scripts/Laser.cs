using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 8f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private UnityEvent OnHitPlayer;

    public Transform player; // Reference to the player's position

    private RaycastHit rayHit;
    private Ray ray;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out rayHit, laserDistance, ~ignoreMask))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, rayHit.point);

            // If the laser hits the player, alert all enemies
            if (rayHit.collider.CompareTag("Player"))
            {
                OnHitPlayer?.Invoke();
                AlertAllEnemies();
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);
        }
    }

    private void AlertAllEnemies()
    {
        NavMeshAgent[] enemies = FindObjectsOfType<NavMeshAgent>();

        foreach (NavMeshAgent agent in enemies)
        {
            if (agent.CompareTag("Guard")) // Ensure only enemies are affected
            {
                agent.SetDestination(player.position);
            }
        }
    }
}
