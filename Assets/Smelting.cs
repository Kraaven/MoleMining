using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Smelting : MonoBehaviour, InteractiveMenu
{
    public RectTransform ItemHolder; // Slot where the item to be polished is placed
    public RectTransform ResultHolder; // Slot where the polished item will appear
    public InventoryItem InputItem; // The item currently being polished
    public bool HoldingItem; // Whether an item is currently held for polishing
    public TMP_Text ButtonLabel;

    [Header("Smelting Objects")]
    public GameObject Fire1;
    public GameObject Fire2;
    public GameObject Fire3;

    public int Fuel = 3;

    private void Start()
    {
        Fuel = 3; // Start with all fires active

        Fire1.SetActive(true);
        Fire2.SetActive(true);
        Fire3.SetActive(true);

        UpdateButtonLabel();
    }

    public bool TakeItem(int ID)
    {
        if (HoldingItem)
        {
            Debug.LogWarning("Already holding an item for smelting.");
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
            Debug.Log("Item placed in smelting station.");
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
        if (Fuel <= 0)
        {
            Refuel();
            return;
        }

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
            Debug.LogWarning("No item to smelt.");
            return;
        }
        
        // Create the smelted ingot
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

        Fuel--;
        UpdateFires();
        UpdateButtonLabel();
    }

    private void UpdateFires()
    {
        Fire1.SetActive(Fuel > 0);
        Fire2.SetActive(Fuel > 1);
        Fire3.SetActive(Fuel > 2);
    }

    private void UpdateButtonLabel()
    {
        if (Fuel <= 0)
        {
            ButtonLabel.text = "Refuel";
        }
        else
        {
            ButtonLabel.text = "Smelt";
        }
    }

    private void Refuel()
    {
        // Look for coal in the inventory
        for (int i = 0; i < InventoryController.Singleton.SampleItemList.Count; i++)
        {
            var item = InventoryController.Singleton.SampleItemList[i].GetComponent<InventoryItem>();
            if (item != null && item.ItemInformation.ObjectType.Equals("Coal"))
            {
                InventoryController.DeleteItem(i); // Delete coal from inventory
                Fuel = 3; // Restore fuel
                UpdateFires();
                UpdateButtonLabel();
                Debug.Log("Refueled with coal.");
                return;
            }
        }
        
        Debug.LogWarning("No coal available for refueling.");
    }
}
