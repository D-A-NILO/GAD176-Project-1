using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] protected float fireRate = 0.5f;
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float bulletSpeed = 20f;

    private float lastFireTime;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    public virtual void Fire()
    {
        if (Time.time >= lastFireTime + fireRate && currentAmmo > 0)
        {
            lastFireTime = Time.time;
            currentAmmo--;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;

            Debug.Log("Gun fired! Ammo left: " + currentAmmo);
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Reload required.");
        }
    }

    public virtual void Reload()
    {
        Debug.Log("Reloading...");
        currentAmmo = maxAmmo;
    }
}
