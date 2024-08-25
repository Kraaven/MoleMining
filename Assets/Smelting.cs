using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smelting : MonoBehaviour, InteractiveMenu
{
    public RectTransform ItemHolder; // Slot where the item to be polished is placed
    public RectTransform ResultHolder; // Slot where the polished item will appear
    public InventoryItem InputItem; // The item currently being polished
    public bool HoldingItem; // Whether an item is currently held for polishing

    [Header("Smelting Objects")]
    public GameObject Fire1;
    public GameObject Fire2;
    public GameObject Fire3;

    public int Fuel = 3;

    // Method to take an item from the inventory and place it in the polishing station

    private void Start()
    {
        Fuel = 0;

        Fire1.SetActive(false);
        Fire2.SetActive(false);
        Fire3.SetActive(false);
    }

    public bool TakeItem(int ID)
    {
        if (HoldingItem)
        {
            Debug.LogWarning("Already holding an item for polishing.");
            return false;
        }
        

        InventoryItem item = InventoryController.Singleton.SampleItemList[ID].GetComponent<InventoryItem>();
        
        if (item != null && item.ItemInformation.Category == ItemCategory.Ore && !item.ItemInformation.ObjectType.Equals("Coal") )
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

    public void StartSmelting()
    {
        Material IngotMat;
        switch (InputItem.ItemInformation.ObjectType)
        {
            case "Gold":
                IngotMat = Material.Gold;
                break;
            case "Silver":
                IngotMat = Material.Silver;
                break;
            case "Platinum":
                IngotMat = Material.Platinum;
                break;
            default:
                IngotMat = Material.Default;
                break;
        }
        
        
        SmeltOre(IngotMat);
    }


    private void SmeltOre(Material Metal)
    {
        print($"Smelting {Metal}");
        if (InputItem == null || !HoldingItem)
        {
            Debug.LogWarning("No item to polish.");
            return;
        }
        
        // Create the polished gem with the specified cut type
        ItemInfo SmeltedIngot = new ItemInfo(ItemCategory.Metal, "Ingot", Metal);
        
        var MajorSprite = ResourceManager.Instance.GetSprite(SmeltedIngot.Category, SmeltedIngot.ObjectType);
        var I = Instantiate(FindObjectOfType<InventoryController>().SampleItem);
        var IM = I.GetComponent<Image>();
        IM.sprite = MajorSprite;
        IM.color = Materials.GetMaterialColor(SmeltedIngot.ItemMaterial);
        IM.GetComponent<InventoryItem>().ItemInformation = SmeltedIngot;
        IM.GetComponent<InventoryItem>().IsInInventory = false;
        I.transform.SetParent(ResultHolder);
        I.transform.localPosition = Vector3.zero;
        I.transform.localScale *= 0.006f;
        
        Destroy(InputItem.gameObject);
        HoldingItem = false;
        
    }
    
}
