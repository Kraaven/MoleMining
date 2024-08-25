using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private InventoryController _inventoryController;
    
    public float moveSpeed;

    public Vector2 _movedirection;

    public InputActionReference move;

    private SpriteRenderer moleSprite;

    private bool isFlipped = false;

    public Animator anim;
    
    private Vector2 lastSafePosition; // To store the player's last safe position
    //public string triggerTag = "ground"; // Tag for the trigger zone (e.g., a pitfall)
    

    public float climbSpeed = 5f;  // Speed of climbing
    private bool isClimbing = false;

    public GameObject smithTableUI;
    public GameObject polishTableUI;

    public GameObject inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        moleSprite = GetComponent<SpriteRenderer>();
        lastSafePosition = transform.position;
        _inventoryController = FindObjectOfType<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        _movedirection = move.action.ReadValue<Vector2>();
        if (_movedirection.x == 1 && !isFlipped)
        {
            Flip();
            anim.SetInteger("walking",(int) _movedirection.x);
        }
        else if (_movedirection.x == -1 && isFlipped)
        {
            Flip();
            anim.SetInteger("walking",(int) _movedirection.x);
        }
        else
        {
            anim.SetInteger("walking",(int) _movedirection.x);
        }
        
        transform.Translate(new Vector2(_movedirection.x * moveSpeed * Time.deltaTime,0f));
        
        jumpandgravity();
        ladderclimb();

        if (isGrounded && Input.GetMouseButton(0))
        {
            anim.SetBool("onclick",true);
        }
        else
        {
            anim.SetBool("onclick",false);
            anim.SetInteger("walking",(int) _movedirection.x);
        }
        
        if (isGrounded)
        {
            lastSafePosition = transform.position;
        }
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
            gravityScale = 0f;
            jumpForce = 0f;// Disable gravity while climbing
            anim.SetBool("climbing",true);
        }
        else
        {
            gravityScale = 9.8f; // Restore gravity when not climbing
            jumpForce = 5f;
            anim.SetBool("climbing",false);
        }
    }
void jumpandgravity()
{
    // Set the origin point slightly below the player for ground detection
    Vector2 origin = new Vector2(transform.position.x, transform.position.y - originminus);

    // Raycast downward to check if the player is grounded
    RaycastHit2D groundHit = Physics2D.Raycast(origin, Vector2.down, 0.4f, groundLayer);
    isGrounded = groundHit.collider != null;

    // Raycast to check for horizontal collisions (left and right)
    RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 0.4f, groundLayer);
    RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 0.4f, groundLayer);

    // Raycast to check for vertical collisions (above and below)
    RaycastHit2D topHit = Physics2D.Raycast(transform.position, Vector2.up, 0.4f, groundLayer);
    RaycastHit2D bottomHit = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, groundLayer);

    bool isBlockedLeft = leftHit.collider != null;
    bool isBlockedRight = rightHit.collider != null;
    bool isBlockedAbove = topHit.collider != null;
    bool isBlockedBelow = bottomHit.collider != null && !isGrounded;

    if (isGrounded)
    {
        // If grounded, reset vertical velocity and handle jump input
        verticalVelocity = 0f;
        anim.SetBool("grounded", true);

        if (Input.GetButtonDown("Jump") && gravityScale == 9.8f)
        {
            verticalVelocity = jumpForce;
            anim.SetBool("grounded", false);
        }
    }
    else
    {
        // Apply gravity if not grounded
        verticalVelocity -= gravityScale * Time.deltaTime;

        // Prevent the player from moving through ceilings
        if (isBlockedAbove && verticalVelocity > 0)
        {
            verticalVelocity = 0;
        }

        anim.SetBool("grounded", false);
    }
    
    // Horizontal collision check
    if ((isBlockedLeft && _movedirection.x < 0) || (isBlockedRight && _movedirection.x > 0))
    {
        // If there is a horizontal collision, prevent movement in that direction
        moveSpeed = 0;
    }
    else
    {
        moveSpeed = 5;
    }

    // Prevent the player from getting stuck on the ground
    if (isBlockedBelow && verticalVelocity < 0)
    {
        verticalVelocity = 0;
    }

    // Apply movement (horizontal and vertical)
    transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
}
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = true;  // Start climbing when entering the ladder trigger
        }

        if (other.TryGetComponent<Item>(out Item IT))
        {
            print(IT.Type);
            var Added =_inventoryController.Insert(new ItemInfo(IT));
            if (Added)
            {
                _inventoryController.SampleItemList.Last().GetComponent<InventoryItem>().IsInInventory = true;
                Destroy(IT.gameObject);
            }
            else
            {
                print("Item Not Picked Up");
            }
        }

        if (other.CompareTag("Smith"))
        {
            smithTableUI.SetActive(true);
            inventoryUI.SetActive(true);
        }
        else if (other.CompareTag("polish"))
        {
            polishTableUI.SetActive(true);
            inventoryUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = false;  // Stop climbing when exiting the ladder trigger
            gravityScale = 9.8f;  // Restore gravity just in case
        }
        if (other.CompareTag("Smith"))
        {
            smithTableUI.SetActive(false);
            inventoryUI.SetActive(false);
        }
        else if (other.CompareTag("polish"))
        {
            polishTableUI.SetActive(false);
            inventoryUI.SetActive(false);
        }
    }
}
