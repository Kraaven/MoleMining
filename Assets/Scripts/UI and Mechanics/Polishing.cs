using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Polishing : MonoBehaviour, InteractiveMenu
{
    public RectTransform ItemHolder; // Slot where the item to be polished is placed
    public RectTransform ResultHolder; // Slot where the polished item will appear
    public InventoryItem InputItem; // The item currently being polished
    public bool HoldingItem; // Whether an item is currently held for polishing

    // Method to take an item from the inventory and place it in the polishing station
    public bool TakeItem(int ID)
    {
        if (HoldingItem)
        {
            Debug.LogWarning("Already holding an item for polishing.");
            return false;
        }

        InventoryItem item = InventoryController.Singleton.SampleItemList[ID].GetComponent<InventoryItem>();
        if (item != null && item.ItemInformation.Category == ItemCategory.Crystal)
        {
            // Remove the item from the inventory and place it in the ItemHolder
            //InventoryController.DeleteItem(ID);
            var Rect = item.gameObject.GetComponent<RectTransform>();
            
            // Update item state and move to ItemHolder
            item.IsInInventory = false;
            item.transform.SetParent(ItemHolder);
            item.transform.localPosition = Vector3.zero;

            InputItem = item;
            HoldingItem = true;
            Debug.Log("Item placed in polishing station.");
            return true;
        }

        Debug.LogWarning("Invalid item or item category.");
        return false;
    }

    public void ClearSlot()
    {
        if (ItemHolder.transform.childCount == 1)
        {
            print("Moving from 1st Slot to inv");
            HoldingItem = false;
            InventoryController.Singleton.Insert(InputItem.ItemInformation);
            Destroy(InputItem.gameObject);
            InputItem = null;
        }

        if (ResultHolder.transform.childCount == 1)
        {
            print("Moving Crafted Item to inv");
            var I = ResultHolder.transform.GetChild(0).GetComponent<InventoryItem>();
            InventoryController.Singleton.Insert(I.ItemInformation);
            Destroy(I.gameObject);
        }
        
    }

    // Method to polish an item into a Princess cut gem
    public void PolishPrincess()
    {
        PolishGem("Princess");
    }

    public void PolishRound()
    {
        PolishGem("Round");
    }
    public void PolishHeart()
    {
        PolishGem("Heart");
    }
    public void PolishTearDrop()
    {
        PolishGem("TearDrop");
    }
    public void PolishPear()
    {
        PolishGem("Pear");
    }
    
    
    // Method to polish an item into a Brilliant cut gem
    public void PolishBrilliant()
    {
        PolishGem("Brilliant");
    }

    // Method to polish an item into an Octagon cut gem
    public void PolishOctagon()
    {
        PolishGem("Octagon");
    }

    // General method to handle the polishing process
    // private void PolishGem(string cutType)
    // {
    //     if (InputItem == null || !HoldingItem)
    //     {
    //         Debug.LogWarning("No item to polish.");
    //         return;
    //     }
    //
    //     // Create the polished gem with the specified cut type
    //     ItemInfo polishedGem = new ItemInfo(ItemCategory.Gem, cutType, InputItem.ItemInformation.ItemMaterial);
    //     
    //     // Add the polished gem to the inventory
    //     bool addedToInventory = InventoryController.Singleton.Insert(polishedGem);
    //
    //     if (addedToInventory)
    //     {
    //         // Find the newly added item in the inventory
    //         InventoryItem polishedItem = InventoryController.Singleton.SampleItemList.Find(x => x.GetComponent<InventoryItem>().ItemInformation.Name == polishedGem.Name).GetComponent<InventoryItem>();
    //
    //         if (polishedItem != null)
    //         {
    //             // Move the polished item to the ResultHolder
    //             polishedItem.IsInInventory = false;
    //             polishedItem.transform.SetParent(ResultHolder);
    //             polishedItem.transform.localPosition = Vector3.zero;
    //
    //             Debug.Log($"{cutType} gem polished and placed in ResultHolder.");
    //         }
    //         else
    //         {
    //             Debug.LogError("Failed to find polished item in inventory.");
    //         }
    //
    //         // Clear the input holder and reset the polishing station
    //         Destroy(InputItem.gameObject);
    //         HoldingItem = false;
    //         InputItem = null;
    //     }
    //     else
    //     {
    //         Debug.LogError("Inventory is full. Could not add polished gem.");
    //     }
    // }
    
    private void PolishGem(string cutType)
    {
        if (InputItem == null || !HoldingItem)
        {
            Debug.LogWarning("No item to polish.");
            return;
        }
        
        // Create the polished gem with the specified cut type
        ItemInfo polishedGem = new ItemInfo(ItemCategory.Gem, cutType, InputItem.ItemInformation.ItemMaterial);
        
        var MajorSprite = ResourceManager.Instance.GetSprite(polishedGem.Category, polishedGem.ObjectType);
        var I = Instantiate(FindObjectOfType<InventoryController>().SampleItem);
        var IM = I.GetComponent<Image>();
        IM.sprite = MajorSprite;
        IM.color = Materials.GetMaterialColor(polishedGem.ItemMaterial);
        IM.GetComponent<InventoryItem>().ItemInformation = polishedGem;
        IM.GetComponent<InventoryItem>().IsInInventory = false;
        I.transform.SetParent(ResultHolder);
        I.transform.localPosition = Vector3.zero;
        I.transform.localScale *= 0.4f;
        
        Destroy(InputItem.gameObject);
        HoldingItem = false;
    }
}
