using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun
{
    public override void InstantiateBullet()
    {
        GameObject shotBullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);

        shotBullet.GetComponent<Rigidbody>().AddExplosionForce(10f, muzzle.transform.position, 1f);
    }
}
