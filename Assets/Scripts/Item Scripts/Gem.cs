using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Item
{
    public string GemType;
    public Color gemcolor;

    public override void INIT()
    {
        SR.sprite = ResourceManager.ItemManager.GetGemSprite(GemType);
        SR.color = gemcolor;
    }
}
