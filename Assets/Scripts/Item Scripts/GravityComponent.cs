using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    private float Gravity = 1;
    private RaycastHit2D DownRay;
    
    // Layer mask to ignore the current game object's layer
    private LayerMask IgnoreLayerMask;

    private void Start()
    {
        // Set the layer mask to ignore the layer this game object is on
        IgnoreLayerMask = LayerMask.NameToLayer("Item");
        IgnoreLayerMask = ~(1 << gameObject.layer);
    }

    public void Update()
    {
        DownRay = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, IgnoreLayerMask);

        if (DownRay.collider != null && DownRay.collider.CompareTag("Ground"))
        {
            transform.position = DownRay.point + new Vector2(0,0.8f);
            Destroy(this);
        }
        else
        {
            transform.Translate(Vector3.down * (Gravity * Time.deltaTime));
            Gravity *= 1.01f;
        }
    }
}