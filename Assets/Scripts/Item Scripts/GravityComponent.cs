using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    /*private float Gravity = 1;
    private RaycastHit2D DownRay;
    
    // Layer mask to ignore the current game object's layer
    private LayerMask IgnoreLayerMask;

    private void Start()
    {
        // Set the layer mask to ignore the layer this game object is on
        IgnoreLayerMask = LayerMask.NameToLayer("Item");
        IgnoreLayerMask = ~(1 << gameObject.layer);

        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        


    }

    public void Update()
    {
            *//*transform.Translate(Vector3.down * (Gravity * Time.deltaTime));
            Gravity *= 1.01f;*//*
    }*/



    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {

            gameObject.AddComponent<BobbingComponent>();
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            Destroy(this);
        }
    }*/
}