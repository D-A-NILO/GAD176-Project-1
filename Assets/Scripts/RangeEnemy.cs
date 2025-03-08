using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyScript
{
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    private float lastFireTime;
    private bool isShooting = false;

    protected override void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= shootingRange)
        {
            // If within range, stop moving and shoot
            Shoot();
        }
        else if (!isShooting)
        {
            // Move toward the player only if not shooting
            base.Update();
        }
    }

    private void Shoot()
    {
        if (Time.time >= lastFireTime + fireRate)
        {
            isShooting = true; // Stop movement while shooting
            AimAtPlayer(); // Rotate towards player before shooting

            Debug.Log("Ranged Enemy Shoots!");

            if (projectilePrefab != null && firePoint != null)
            {
                // Spawn projectile and direct it toward the player
                GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Vector3 shootDirection = (player.position - firePoint.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = shootDirection * 15f; // Bullet speed
            }
            else
            {
                Debug.LogError("projectile or firepoint is missing");
            }


            lastFireTime = Time.time;
            Invoke(nameof(ResumeMovement), 1f); // Delay before moving again
        }
    }

    private void AimAtPlayer()
    {
        // Look at the player's position (only adjust Y rotation)
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Keep enemy upright
        transform.forward = directionToPlayer;

        // Rotate firePoint to aim directly at the player
        firePoint.LookAt(player.position);
    }

    private void ResumeMovement()
    {
        isShooting = false; // Allow movement again
    }
}
