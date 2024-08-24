using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Item
{
    public Color GEMTYPE;
    public string NAME;

    private void Start()
    {
        Initialize(NAME, GEMTYPE);
    }
    
    public override void Initialize(string name, Color color)
    {
        Type = ItemType.Metal;
        Name = name;
        Color = color;
        SR.sprite = ResourceManager.Instance.GetMetalSprite(Name);
        SR.color = Color;
    }
}
