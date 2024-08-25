using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ItemInfo ItemInformation;
    public int InventorySlot;
    public bool IsInInventory;

    private float lastClickTime = 0f;       // To track the time of the last click
    private const float doubleClickDelay = 0.3f; // Max delay time to detect double-click

    public void OnPointerEnter(PointerEventData eventData)
    {
        ClickDetector.SetLabel(ItemInformation.Name);
        if (IsInInventory)
        {
            InventoryController.SelectItem(InventorySlot);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClickDetector.SetLabel("");
        if (IsInInventory)
        {
            InventoryController.DeselectItem();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Time.time - lastClickTime < doubleClickDelay)
            {
                // Double-click detected
                Debug.Log("Double click detected on slot: " + InventorySlot);
                OnDoubleClick();
            }
            else
            {
                if (IsInInventory)
                {
                    InventoryController.SelectItem(InventorySlot);
                }
            }
            lastClickTime = Time.time;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (IsInInventory)
            {
                InventoryController.DeleteItem(InventorySlot);
            }
        }
    }

    // Custom method to handle double-click event
    private void OnDoubleClick()
    {
        if (IsInInventory)
        {
            var added = InteractiveCrafts.Singleton.Menu.TakeItem(InventorySlot);
            
            if(!added) return;
            InventoryController.Singleton.SampleItemList.RemoveAt(InventorySlot);
            
            for (int i = InventorySlot; i < InventoryController.Singleton.SampleItemList.Count; i++)
            {
                InventoryController.Singleton.SampleItemList[i].GetComponent<InventoryItem>().InventorySlot = i;
                InventoryController.Singleton.SampleItemList[i].SetParent(InventoryController.Singleton.InvetorySlots[i]);
                InventoryController.Singleton.SampleItemList[i].localPosition = Vector3.zero;
            }
            
        }
        else
        {
            InteractiveCrafts.Singleton.Menu.ClearSlot();
        }
    }

    // Add the item to the inventory
    private void AddToInventory()
    {
        bool added = InventoryController.Singleton.Insert(ItemInformation);
        if (added)
        {
            IsInInventory = true;
            Debug.Log("Item added to inventory.");
            // Additional code to update the UI if needed
        }
        else
        {
            Debug.Log("Inventory is full, could not add item.");
        }
    }
}
