using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public float gravityScale = 9.8f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    private float verticalVelocity;
    private bool isGrounded;
    private Vector2 origin;
    [SerializeField] private float originminus = 0.4f;
    

    void Update()
    {
        origin = new Vector2(transform.position.x, transform.position.y - originminus);
        // Check if the object is grounded using Raycast
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded)
        {
            // If grounded, reset vertical velocity and check for jump input
            verticalVelocity = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            // Apply gravity if not grounded
            verticalVelocity -= gravityScale * Time.deltaTime;
        }

        // Apply vertical movement
        transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
    }
}
