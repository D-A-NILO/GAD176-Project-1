using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private int damage = 10;

    private Vector3 moveDirection;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }
    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }
    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by projectile!");
            // Apply damage to player if they have a health script
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.TakeDamage(damage);
                Debug.Log("Player hit by enemy projectile");
            }

            Destroy(gameObject);
        }
    }
}
