using UnityEngine;

public class interact : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public LayerMask interactableLayer; // Layer mask for interactable objects

    void Start()
    {
        // If the main camera is not assigned, get the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, interactableLayer);

            if (hit.collider != null)
            {
                // If an interactable object is clicked, call its Interact method
                hit.collider.GetComponent<Interactable>()?.Interact();
            }
        }
    }
}