using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Interactable {
    public GameObject bulletPrefab;
    public GameObject muzzle;

    public enum firemodes
    {
        Auto,
        Semi
    }

    public firemodes firemode;

    public float shootCooldown = 0.1f;
    public float currentShootCooldown;

    public bool shooting = false;

    protected override void Update()
    {
        base.Update();

        if(shooting == true && currentShootCooldown <= 0f)
        {
            currentShootCooldown = shootCooldown;
            InstantiateBullet();
        }

        if(currentShootCooldown > 0f)
        {
            currentShootCooldown -= Time.deltaTime;

            if(currentShootCooldown <= 0f)
            {
                currentShootCooldown = 0f;
            }
        }
    }

    public void Shoot()
    {
        if(firemode == firemodes.Auto)
        {
            shooting = true;
        }
        else
        {
            InstantiateBullet();
        }
    }

    public void StopShoot()
    {
        shooting = false;
    }

    public virtual void InstantiateBullet()
    {
        GameObject shotBullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
    }
}
