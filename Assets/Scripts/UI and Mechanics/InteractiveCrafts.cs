using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractiveCrafts : MonoBehaviour
{
    public List<RectTransform> Menus;
    public InteractiveMenu Menu;
    public static InteractiveCrafts Singleton;
    public static bool OpenMenu;



    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        
        Menu = Menus[1].GetComponent<InteractiveMenu>();

        InteractiveCrafts.ClearFocus();
    }


    public static void FocusFurnace()
    {
        int focus = 0;

        for (int i = 0; i < Singleton.Menus.Count; i++)
        {
            if (i == focus)
            {
                Singleton.Menu = Singleton.Menus[focus].GetComponent<InteractiveMenu>();
                Singleton.Menus[focus].gameObject.SetActive(true);
                OpenMenu = true;
            }
            else {
                Singleton.Menus[i].gameObject.SetActive(false);
            }
        }  
    }

    public static void FocusPolish() {
        int focus = 1;

        for (int i = 0; i < Singleton.Menus.Count; i++)
        {
            if (i == focus)
            {
                Singleton.Menu = Singleton.Menus[focus].GetComponent<InteractiveMenu>();
                Singleton.Menus[focus].gameObject.SetActive(true);
                OpenMenu = true;
            }
            else
            {
                Singleton.Menus[i].gameObject.SetActive(false);
            }
        }
    }


    public static void ClearFocus() {
        for (int i = 0; i < Singleton.Menus.Count; i++)
        {
            Singleton.Menus[i].gameObject.SetActive(false);
        }
        Singleton.Menu = null;
        OpenMenu = false;
    }
    
    
}

public interface InteractiveMenu
{
    public bool TakeItem(int ID);
    public void ClearSlot();
}
