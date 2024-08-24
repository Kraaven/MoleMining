using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    // Update is called once per frame

    public bool toggle = false;
    public GameObject UI;
    private InventoryController _inventoryController;
    public TMP_Text Label;
    public static ClickDetector Instance;
    private void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
        UI.SetActive(toggle);
        Instance = this;
        SetLabel("");
    }

    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hit something
            if (hit.collider != null && hit.collider.TryGetComponent<Item>(out Item IT))
            {
                print(IT.Type);
                var Added =_inventoryController.Insert(new ItemInfo(IT));
                if (Added)
                {
                    Destroy(IT.gameObject);
                }
                else
                {
                    print("Item Not Picked Up");
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle = !toggle;
            UI.SetActive(toggle);
        }
    }

    public static void SetLabel(string Data)
    {
        Instance.Label.text = Data;
    }
}

