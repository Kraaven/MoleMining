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
            Item.transform.localScale *= 0.012f;
        

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
        
        Insert(I);
        return true;


    }

    public static void HighLightSlot(int ID)
    {
       Singleton.SlotSelected.SetParent(Singleton.InvetorySlots[ID]); 
       Singleton.SlotSelected.localPosition = Vector3.zero;
       Singleton.SlotSelected.GetComponent<Image>().enabled = true;
    }
}
