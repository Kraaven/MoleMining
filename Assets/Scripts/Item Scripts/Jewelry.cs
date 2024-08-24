using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Jewelry : Item
{
    public Gem AttachedGem { get; protected set; }

    public virtual void AttachGem(Gem gem)
    {
        AttachedGem = gem;
        UpdateAppearance();
    }

    protected abstract void UpdateAppearance();
}
