using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Jewelry
{
    public override void Initialize(string name, Color color)
    {
        Type = ItemType.Ring;
        Name = name;
        Color = color;
        SR.sprite = ResourceManager.Instance.GetRingSprite(Name);
        SR.color = Color;
    }

    protected override void UpdateAppearance()
    {
        // Update the ring's appearance based on the attached gem
    }
}
