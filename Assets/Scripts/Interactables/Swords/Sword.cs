using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Interactable
{
    public void Action()
    {
        GetComponent<Animator>().SetTrigger("Switch");
    }
}
