using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager ItemManager { get; private set; }

    private void Awake()
    {
        ItemManager = this;
    }

    [Header("Crystals")]
    public List<ItemEntry> CrystalVarients;
    [Header("Jewelry")]
    public List<ItemEntry> GemCuts;
    public List<ItemEntry> Rings;
    
    

    public Sprite GetGemSprite(string Gname)
    {
        foreach (var cut in GemCuts)
        {
            if (cut.name.Equals(Gname))
            {
                return cut.ItemSprite;
            }
        }

        print("Gem not found");
        return null;
    }
}
