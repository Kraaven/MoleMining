using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public ItemType itemType;
    void Start()
    {
        gameObject.AddComponent<GravityComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
