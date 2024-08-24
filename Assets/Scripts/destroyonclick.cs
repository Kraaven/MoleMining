using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    public float radius = 2f;  // Radius within which objects will be destroyed

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 0 is the left mouse button
        {
            // Convert the mouse position to a world position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Find all colliders within the radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            // Loop through all colliders found
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Player" && collider.gameObject.tag == "ground")
                {
                    // Destroy the game object associated with the collider
                    Destroy(collider.gameObject);
                }

            }
        }
    }

    // This will draw the radius in the Scene view for debugging
    private void OnDrawGizmosSelected()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
