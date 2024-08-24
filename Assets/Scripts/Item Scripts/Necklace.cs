using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : Jewelry
{
    public override void Initialize(string name, Color color)
    {
        Type = ItemType.Necklace;
        Name = name;
        Color = color;
        SR.sprite = ResourceManager.Instance.GetNecklaceSprite(Name);
        SR.color = Color;
    }

    protected override void UpdateAppearance()
    {
        // Update the necklace's appearance based on the attached gem
    }
}
