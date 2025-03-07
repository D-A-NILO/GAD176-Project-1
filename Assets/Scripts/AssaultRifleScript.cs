using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleScript : WeaponScript
{
    private bool isFiring;

    void Start()
    {
        fireRate = 0.1f; // Fast fire rate for automatic gun
        maxAmmo = 30; // Standard AR magazine
        currentAmmo = maxAmmo;
        bulletSpeed = 35f; // Faster bullet speed
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isFiring = true;
            InvokeRepeating(nameof(Fire), 0f, fireRate);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
            CancelInvoke(nameof(Fire));
        }
    }

    public override void Fire()
    {
        if (currentAmmo > 0)
        {
            lastFireTime = Time.time;
            currentAmmo--;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;

            Debug.Log("Assault Rifle fired! Ammo left: " + currentAmmo);
        }
        else
        {
            Debug.Log("Assault Rifle out of ammo! Reload needed.");
            CancelInvoke(nameof(Fire)); // Stop firing if out of ammo
        }
    }
}
