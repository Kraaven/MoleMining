using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class InventoryController : MonoBehaviour
{
    public RectTransform SampleItem;
    public List<ItemInfo> Inventory;
    public List<RectTransform> InvetorySlots;
    public List<RectTransform> SampleItemList;
    public static InventoryController Singleton;
    public RectTransform SlotSelected;
    public InventoryItem SelectedItem;
    
    
    //public Sprite 

    private void Start()
    {
        // for (int i = 0; i < 20; i++)
        // {
        //     var I = Instantiate(SampleItem);
        //     Insert(I);
        //     print("Inserted New Sample");
        // }

        Singleton = this;
        SlotSelected.GetComponent<Image>().enabled = false;
    }

    public void Insert(RectTransform Item)
    {
        int NewSlot = SampleItemList.Count;
            SampleItemList.Add(Item);
            //Item.transform.position = InvetorySlots[NewSlot].position;
            Item.transform.SetParent(InvetorySlots[NewSlot]);
            Item.transform.localPosition = new Vector3(0,0,0);
            Item.transform.localScale *= 0.006f;
        

    }
    
    public bool Insert(ItemInfo Item)
    { 
        int NewSlot = SampleItemList.Count;
        if (NewSlot >= 24)
        {
            print("Inventory Full");
            return false;

        }
        
        var MajorSprite = ResourceManager.Instance.GetSprite(Item.Category, Item.ObjectType);
        var I = Instantiate(SampleItem);
        var IM = I.GetComponent<Image>();
        IM.sprite = MajorSprite;
        IM.color = Materials.GetMaterialColor(Item.ItemMaterial);
        IM.GetComponent<InventoryItem>().ItemInformation = Item;
        IM.GetComponent<InventoryItem>().InventorySlot = NewSlot;
        IM.GetComponent<InventoryItem>().IsInInventory = true;
        
        Insert(I);
        return true;


    }
    
    public static void SelectItem(int slotID)
    {
        Singleton.SelectedItem = Singleton.SampleItemList[slotID].GetComponent<InventoryItem>();
        HighLightSlot(slotID);
    }

    public static void HighLightSlot(int ID)
    {

        Singleton.SlotSelected.SetParent(Singleton.InvetorySlots[ID]); 
        Singleton.SlotSelected.localPosition = Vector3.zero;
        Singleton.SlotSelected.SetAsFirstSibling();
        Singleton.SlotSelected.GetComponent<Image>().enabled = true;
    }

    
    public static bool DeleteSelectedItem()
    {
        if (Singleton.SelectedItem == null)
        {
            Debug.Log("No item selected to delete.");
            return false;
        }

        int slotID = Singleton.SelectedItem.InventorySlot;

        // Now use the existing delete logic
        return DeleteItem(slotID);
    }
    
    public static void DeselectItem()
    {
        Singleton.SelectedItem = null;
        Singleton.SlotSelected.GetComponent<Image>().enabled = false;
    }

    // Existing DeleteItem method
    public static bool DeleteItem(int slotID)
    {
        if (slotID < 0 || slotID >= Singleton.SampleItemList.Count)
        {
            Debug.Log("Invalid slot ID");
            return false;
        }

        RectTransform itemToDelete = Singleton.SampleItemList[slotID];

        if (itemToDelete != null)
        {
            Singleton.SampleItemList.RemoveAt(slotID);
            Destroy(itemToDelete.gameObject);

            for (int i = slotID; i < Singleton.SampleItemList.Count; i++)
            {
                Singleton.SampleItemList[i].GetComponent<InventoryItem>().InventorySlot = i;
                Singleton.SampleItemList[i].SetParent(Singleton.InvetorySlots[i]);
                Singleton.SampleItemList[i].localPosition = Vector3.zero;
            }

            Debug.Log("Item deleted from slot: " + slotID);

            DeselectItem(); // Deselect after deletion

            return true;
        }
        else
        {
            Debug.Log("No item found in the specified slot");
            return false;
        }
    }
}
