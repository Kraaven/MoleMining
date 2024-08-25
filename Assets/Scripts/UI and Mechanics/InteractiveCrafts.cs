using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCrafts : MonoBehaviour
{
    public List<RectTransform> Menus;
    public InteractiveMenu Menu;
    public static InteractiveCrafts Singleton;

    private void Start()
    {
        Singleton = this;
        Menu = Menus[0].GetComponent<InteractiveMenu>();
    }
    
    
}

public interface InteractiveMenu
{
    public bool TakeItem(int ID);
    public void ClearSlot();
}
