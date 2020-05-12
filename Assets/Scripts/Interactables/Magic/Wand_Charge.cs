using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_Charge : Wand
{
    public float maxCharge = 1f;
    public float chargeSpeed = 0.2f;
    [HideInInspector]
    public float charge;

    public GameObject prefab;

    private bool spawned = false;
    private bool maxed = false;

    private MagicCharge currentCharge;

    private bool charging = false;

    protected override void Update()
    {
        base.Update();

        if(charging == true)
        {
            charge += chargeSpeed * Time.deltaTime;

            if (charge > maxCharge)
            {
                charge = maxCharge;

                if (maxed == false)
                {
                    m_ActiveHand.Vibrate(0.1f, 0.5f);
                }

                maxed = true;
            }
        }
    }

    public void InstantiateCharge()
    {
        GameObject obj = PhotonNetwork.Instantiate("MagicCharge", muzzle.position, muzzle.rotation, 0, new object[] { GetComponent<PhotonView>().GetInstanceID() });
        obj.GetComponent<MagicCharge>().wand = this;
        currentCharge = obj.GetComponent<MagicCharge>();
    }

    public override void Action()
    {
        charging = true;
        InstantiateCharge();
    }

    public override void Release()
    {
        charging = false;
        charge = 0f;
        currentCharge.Release(m_ActiveHand.m_Pose.GetVelocity());
        currentCharge = null;
    }

    public override void Dropped()
    {
        Destroy(currentCharge);
    }
}
