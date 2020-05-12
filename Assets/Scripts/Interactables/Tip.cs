using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{ 
    public Interactable interactable;

    public Resource resource;

    public void Release()
    {
        resource.m_Tip = null;
        resource.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        resource.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;
        resource = null;
    }
}
