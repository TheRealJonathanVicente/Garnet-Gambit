using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    private bool isInteracting;

    private PlayerControls controls; // Ensure that PlayerControls is defined and linked to input system
    
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls(); // Initialize PlayerControls
        
        controls.Gameplay.Interact.performed += ctx => isInteracting = true;
        controls.Gameplay.Interact.canceled += ctx => isInteracting = false;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable(); // Enable input actions
    }

    void OnDisable()
    {
        controls.Gameplay.Disable(); // Disable input actions
    }

    // Update is called once per frame
    void Update()
    {
        // Check both the E key and isInteracting for interaction
        if (Input.GetKeyDown(KeyCode.E) || isInteracting)
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactOBJ))
                {
                    interactOBJ.Interact(); // Perform the interaction
                }
            }
        }
    }
}
