using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Item
{
    public string CutType { get; private set; }

    public Color GEMTYPE;
    public string NAME;
    public string CUT;

    private void Start()
    {
        CutType = CUT;
        Initialize(NAME, GEMTYPE);
    }

    public override void Initialize(string name, Color color)
    {
        Type = ItemType.Gem;
        Name = name;
        Color = color;
        SR.sprite = ResourceManager.Instance.GetGemSprite(CutType);
        SR.color = Color;
    }

    public void SetCut(string cutType)
    {
        CutType = cutType;
        SR.sprite = ResourceManager.Instance.GetGemSprite(CutType);
    }
}
