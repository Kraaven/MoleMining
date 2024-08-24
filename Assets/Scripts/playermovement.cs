using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    public float gravityScale = 9.8f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    private float verticalVelocity;
    private bool isGrounded;
    private Vector2 origin;
    [SerializeField] private float originminus = 0.4f;
    
    public float moveSpeed;

    public Vector2 _movedirection;

    public InputActionReference move;

    private SpriteRenderer moleSprite;

    private bool isFlipped = false;

    public float climbSpeed = 5f;  // Speed of climbing
    private bool isClimbing = false; 
    // Start is called before the first frame update
    void Start()
    {
        moleSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _movedirection = move.action.ReadValue<Vector2>();
        if (_movedirection.x == 1 && !isFlipped)
        {
            Flip();
        }
        else if(_movedirection.x == -1 && isFlipped)
        {
            Flip();
        }
        
        transform.Translate(new Vector2(_movedirection.x * moveSpeed * Time.deltaTime,0f));
        
        jumpandgravity();
        ladderclimb();
    }
    
    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip horizontally
        transform.localScale = localScale;
    }

    void ladderclimb()
    {
        if (isClimbing)
        {
            // Handle vertical movement on the ladder
            transform.Translate(new Vector2(0, _movedirection.y * climbSpeed * Time.deltaTime));
            gravityScale = 0f;  // Disable gravity while climbing
        }
        else
        {
            gravityScale = 9.8f;  // Restore gravity when not climbing
        }
    }
    void jumpandgravity()
    {
        origin = new Vector2(transform.position.x, transform.position.y - originminus);
        // Check if the object is grounded using Raycast
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded)
        {
            // If grounded, reset vertical velocity and check for jump input
            verticalVelocity = 0f;

            if (Input.GetButtonDown("Jump") && gravityScale == 9.8f)
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = true;  // Start climbing when entering the ladder trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = false;  // Stop climbing when exiting the ladder trigger
            gravityScale = 9.8f;  // Restore gravity just in case
        }
    }
}
