using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable
{
    public Tip m_Tip;

    public Enums.Skills skill;
    public int experience;

    protected virtual void Update()
    {
        base.Update();

        if (m_Tip != null)
        {
            GetComponent<Collider>().enabled = false;

            transform.position = m_Tip.transform.position;
            transform.rotation = m_Tip.transform.rotation * Quaternion.Euler(0, 90, 0);

            if (m_ActiveHand != null)
            {
                m_Tip = null;
            }
        }
    }
}
