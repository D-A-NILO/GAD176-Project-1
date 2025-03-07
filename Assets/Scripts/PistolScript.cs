using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : WeaponScript
{
    void Start()
    {
        fireRate = 0.3f; // Slower fire rate
        maxAmmo = 12; // Typical pistol magazine
        currentAmmo = maxAmmo;
        bulletSpeed = 25f; // Slightly faster bullet
    }

    public override void Fire()
    {
        if (Time.time >= lastFireTime + fireRate && currentAmmo > 0)
        {
            lastFireTime = Time.time;
            currentAmmo--;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;

            Debug.Log("Pistol fired! Ammo left: " + currentAmmo);
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Pistol out of ammo! Reload needed.");
        }
    }
}
