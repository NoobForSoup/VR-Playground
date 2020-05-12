using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GrapplingGun gun;
    public LineRenderer line;

    public bool stuck = false;

    private void Update()
    {
        line.SetPosition(0, gun.muzzle.transform.position);
        line.SetPosition(1, transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        GetComponent<ConstantMotion>().enabled = false;
        stuck = true;
    }
}
