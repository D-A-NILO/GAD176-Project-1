using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyScript
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private float shootCooldown = 2f;
    private float lastShotTime;

    protected override void Update()
    {
        base.Update();

        if (player && Vector3.Distance(transform.position, player.position) <= shootRange)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time >= lastShotTime + shootCooldown)
        {
            Vector3 shootDirection = (player.position - shootPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(shootDirection));
            projectile.GetComponent<Rigidbody>().velocity = shootDirection * 10f; // Adjust speed as needed
            lastShotTime = Time.time;
        }
    }
}
