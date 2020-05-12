using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand m_ActiveHand = null;

    public bool resettable = true;
    public float resetHeight = -1;

    public List<Collider> collidersToToggle;
    private bool collidersOn = true;
    
    protected virtual void Update()
    {
        if(m_ActiveHand != null && collidersOn)
        {
            foreach(Collider col in collidersToToggle)
            {
                col.enabled = false;
            }

            collidersOn = false;

        }
        else
        if(!collidersOn)
        {
            foreach (Collider col in collidersToToggle)
            {
                col.enabled = true;
            }

            collidersOn = true;
        }

        if(resettable)
        {
            if (transform.position.y < resetHeight)
            {
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
        }
    }

    public virtual void Release()
    {

    }
}
