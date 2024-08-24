using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Item
{
    public override void Initialize(string name, Color color)
    {
        Type = ItemType.Crystal;
        Name = name;
        Color = color;
        SR.sprite = ResourceManager.Instance.GetCrystalSprite(Name);
        SR.color = Color;
    }
}
