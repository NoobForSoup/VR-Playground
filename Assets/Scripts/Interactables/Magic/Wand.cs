using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wand : Interactable
{
    public Transform muzzle;

    public abstract void Action();
    public abstract void Release();
    public abstract void Dropped();
}
