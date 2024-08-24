using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public GameObject inv;

    private bool isinvOpen = false;

    public playermovement pm;
    // Start is called before the first frame update
    void Start()
    {
        inv.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isinvOpen)
        {
            inv.SetActive(true);
            isinvOpen = true;
            pm.enabled = false;

        }
        else if (Input.GetKeyDown(KeyCode.E) && isinvOpen)
        {
            inv.SetActive(false);
            isinvOpen = false;
            pm.enabled = true;
        }
    }
}

public class invList : MonoBehaviour
{
    public List<ItemInfo> inventoryList = new List<ItemInfo>();
    public GameObject player;
    private void Update()
    {
        var T = new Item();
        inventoryList.Add(new ItemInfo(T));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

