using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
    public MenuDetector detector;

    private enum Trigger
    {
        Left,
        Center,
        Right
    }

    [SerializeField]
    private Trigger trigger;

    private bool left = false;
    private bool right = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hand>() != null)
        {
            if (other.GetComponent<Hand>().whichHand == Hand.Hands.Left)
            {
                left = true;
            }
            else
            {
                right = true;
            }

            Check();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Hand>() != null)
        {
            if (other.GetComponent<Hand>().whichHand == Hand.Hands.Left)
            {
                left = false;
            }
            else
            {
                right = false;
            }

            Check();
        }
    }

    public void Check()
    {
        switch(trigger)
        {
            case Trigger.Left:
                LeftCheck();
                break;
            case Trigger.Center:
                CenterCheck();
                break;
            case Trigger.Right:
                RightCheck();
                break;
        }
    }

    public void LeftCheck()
    {
        if(left)
        {
            detector.leftTrigger = true;
        }
        else
        {
            detector.leftTrigger = false;
        }
    }

    public void CenterCheck()
    {
        if(left && right)
        {
            detector.activated = true;
            detector.activationTime = 3f;
        }
    }

    public void RightCheck()
    {
        if (right)
        {
            detector.rightTrigger = true;
        }
        else
        {
            detector.rightTrigger = false;
        }
    }
}
