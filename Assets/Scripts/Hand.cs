using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public enum Hands
    {
        Left,
        Right
    }

    public Hands whichHand;

    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Boolean m_PinchAction = null;

    public SteamVR_Action_Vibration m_HapticAction = null;

    [HideInInspector]
    public SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractables = new List<Interactable>();

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        if(m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }
        
        if(m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Up");
            Drop();
        }

        if(m_PinchAction.GetStateDown(m_Pose.inputSource))
        {
            Action();
        }

        if (m_CurrentInteractable == null)
        {
            if (m_GrabAction.GetStateDown(m_Pose.inputSource))
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward);
                if(Physics.Raycast(ray, out hit, 3f))
                {
                    Debug.Log(hit.transform);
                    if (hit.transform != null && hit.transform.GetComponent<Interactable>() != null)
                    {
                        hit.transform.position = transform.position;
                        hit.transform.rotation = transform.rotation;
                        Pickup(hit.transform.GetComponent<Interactable>());
                    }
                }
            }
        }

        if (m_CurrentInteractable != null && m_CurrentInteractable.GetComponent<Gun>() != null)
        {
            if(m_PinchAction.GetStateDown(m_Pose.inputSource))
            {
                m_CurrentInteractable.GetComponent<Gun>().Shoot();
            }
            else
            if (m_PinchAction.GetStateUp(m_Pose.inputSource))
            {
                m_CurrentInteractable.GetComponent<Gun>().StopShoot();
            }
        }


        if (m_CurrentInteractable != null && m_CurrentInteractable.GetComponent<Wand>() != null)
        {
            if (m_PinchAction.GetStateDown(m_Pose.inputSource))
            {
                m_CurrentInteractable.GetComponent<Wand>().Action();
            }
            else
            if (m_PinchAction.GetStateUp(m_Pose.inputSource))
            {
                m_CurrentInteractable.GetComponent<Wand>().Release();
            }
        }        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(!collider.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Add(collider.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Remove(collider.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        Pickup(GetNearestInteractable());
    }

    public void Pickup(Interactable interactable)
    {
        m_CurrentInteractable = interactable;

        if (!m_CurrentInteractable)
            return;

        if (m_CurrentInteractable.m_ActiveHand != null)
        {
            m_CurrentInteractable.m_ActiveHand.Drop();
        }

        //m_CurrentInteractable.transform.position = transform.position;
        //m_CurrentInteractable.transform.rotation = transform.rotation;

        Rigidbody targetbody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetbody;

        m_CurrentInteractable.m_ActiveHand = this;

        Vibrate(0.1f, 0.5f);
    }

    public void Drop()
    {
        if (!m_CurrentInteractable)
        {
            return;
        }

        if (m_CurrentInteractable.GetComponent<Gun>() != null)
        {
            m_CurrentInteractable.GetComponent<Gun>().StopShoot();
        }

        if (m_CurrentInteractable.GetComponent<Wand>() != null)
        {
            m_CurrentInteractable.GetComponent<Wand>().Dropped();
        }

        Rigidbody targetbody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetbody.velocity = m_Pose.GetVelocity();
        targetbody.angularVelocity = m_Pose.GetAngularVelocity();

        m_Joint.connectedBody = null;

        m_CurrentInteractable.Release();

        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;

        m_ContactInteractables.Clear();
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if(distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }

    public void Action()
    {
        if(m_CurrentInteractable != null && m_CurrentInteractable.GetComponentsInChildren<Tip>().Length > 0)
        {
            foreach(Tip tip in m_CurrentInteractable.GetComponentsInChildren<Tip>())
            {
                tip.Release();
            }
        }

        if (m_CurrentInteractable != null && m_CurrentInteractable.GetComponent<Sword>() != null)
        {
            if (m_PinchAction.GetStateDown(m_Pose.inputSource))
            {
                m_CurrentInteractable.GetComponent<Sword>().Action();
            }
        }
    }

    public void Vibrate(float length, float force)
    {
        SteamVR_Input_Sources input = SteamVR_Input_Sources.Any;

        switch(whichHand)
        {
            case Hands.Left:
                input = SteamVR_Input_Sources.LeftHand;
                break;
            case Hands.Right:
                input = SteamVR_Input_Sources.RightHand;
                break;
        }

        m_HapticAction.Execute(0, length, 160, force, input);
    }
}
