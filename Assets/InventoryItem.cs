using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler 
{

    public ItemInfo ItemInformation;

    public int InventorySlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ClickDetector.SetLabel(ItemInformation.Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClickDetector.SetLabel("");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController.HighLightSlot(InventorySlot);
    }
}
