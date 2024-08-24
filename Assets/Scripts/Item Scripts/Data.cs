using System;
using System.Collections;
using System.Collections.Generic;
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
    public static Color Silver => Color.HSVToRGB(188, 3, 100);
    public static Color Gold => Color.HSVToRGB(49, 88, 95);

    public static Color Peridot => new Color(101,154,40);
    public static Color Amethyst => new Color(74, 19, 84);
    public static Color Pink_Tormaline => new Color(139, 24, 58);



}
