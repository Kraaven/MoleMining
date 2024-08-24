using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;


public enum ItemCategory
{
    Ore,
    Metal,
    Gem,
    EmptyPiece
}

public class Materials
{

    public static Color GetMaterialColor(Material Mat)
    {
        switch (Mat)
        {
            case Material.Gold:
                return FromHex("e8b923");
            case Material.Silver:
                return FromHex("c2c4ce");
            case Material.Amethyst:
                return FromHex("de7dbe");
            case Material.Pink_Tormaline:
                return FromHex("FC5A96");
            case Material.Peridot:
                return FromHex("B4C424");
            default:
                Debug.Log("Default Color");
                return new Color(0, 0, 0);
        }
    }
    
    public static Color FromHex(string hex)
    {
        if (hex.Length<6)
        {
            throw new System.FormatException("Needs a string with a length of at least 6");
        }

        hex = hex.ToLower();

        var r = hex.Substring(0, 2);
        var g = hex.Substring(2, 2);
        var b = hex.Substring(4, 2);
        string alpha;
        if (hex.Length >= 8)
            alpha = hex.Substring(6, 2);
        else
            alpha = "FF";

        return new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
            (int.Parse(g, NumberStyles.HexNumber) / 255f),
            (int.Parse(b, NumberStyles.HexNumber) / 255f),
            (int.Parse(alpha, NumberStyles.HexNumber) / 255f));
    }

}

public enum Material
{
    Silver,
    Gold,
    Peridot,
    Amethyst,
    Pink_Tormaline
}

[Serializable]
public class ItemInfo
{
    public string Name;
    public ItemCategory Category;
    public string ObjectType;
    public Material ItemMaterial;
    public ItemInfo Attached;

    public ItemInfo(Item PickedUpitem)
    {
        Category = PickedUpitem.Category;
        ObjectType = PickedUpitem.Type;
        ItemMaterial = PickedUpitem.Material;

        switch (Category)
        {
            case ItemCategory.Gem:
                Name = $"{ObjectType} Cut {ItemMaterial.ToString()}".Replace("_"," ");
                break;
        }
    }

}
