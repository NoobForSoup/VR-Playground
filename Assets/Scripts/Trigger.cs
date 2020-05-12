using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject resourcePrefab;

    public float resourceChance = 10;

    public enum TriggerTypes
    {
        fish,
        ore
    }

    public TriggerTypes triggerType;

    private void OnTriggerEnter(Collider other)
    {
        string tipTag = "";

        switch(triggerType)
        {
            case TriggerTypes.fish:
                tipTag = "HarpoonTip";
                break;
            case TriggerTypes.ore:
                tipTag = "PickaxeTip";
                break;
            default:
                tipTag = "HarpoonTip";
                break;
        }

        GetResource(tipTag, other);
    }

    public void GetResource(string tipTag, Collider other)
    {
        if (other.gameObject.CompareTag(tipTag))
        {
            float chance = Random.Range(0, 100);

            if (chance < resourceChance && other.GetComponent<Tip>().resource == null && other.GetComponent<Tip>().interactable.m_ActiveHand != null)
            {
                GameObject resource = Instantiate(resourcePrefab);
                resource.GetComponent<Resource>().m_Tip = other.GetComponent<Tip>();
                other.GetComponent<Tip>().resource = resource.GetComponent<Resource>();
                other.GetComponent<Tip>().interactable.m_ActiveHand.Vibrate(0.1f, 0.5f);
            }
        }
    }
}
