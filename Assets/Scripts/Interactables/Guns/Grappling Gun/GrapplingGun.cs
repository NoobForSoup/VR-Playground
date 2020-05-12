using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : Gun
{
    public GameObject grapple;

    public override void InstantiateBullet()
    {
        if(grapple != null)
        {
            Destroy(grapple);
        }

        grapple = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
        grapple.GetComponent<Grapple>().gun = this;
    }

    public override void Release()
    {
        if (grapple != null)
        {
            Destroy(grapple);
        }
    }
}
