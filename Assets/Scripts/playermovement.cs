using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public Vector2 _movedirection;

    public InputActionReference move;
    public InputActionReference jump;

    private SpriteRenderer moleSprite;

    private bool isFlipped = false;
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
    }
    
    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip horizontally
        transform.localScale = localScale;
    }
    


}
