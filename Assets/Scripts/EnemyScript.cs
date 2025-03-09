using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] protected int health = 50;
    [SerializeField] protected float speed = 3.5f;
    [SerializeField] protected int damage = 10;

    protected Transform player;
    protected PlayerManager playerManager;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = player.GetComponent<PlayerManager>();
    }

    protected virtual void Update()
    {
        if (player)
        {
            MoveTowardsPlayer();
        }
    }

    protected void MoveTowardsPlayer()
    {
        // Calculate direction vector from enemy to player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep movement horizontal

        if (direction.magnitude > 0.1f) // Avoid jittering when close
        {
            Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
            transform.position += moveVector; // Apply movement
            transform.forward = direction.normalized; // Face the player
        }
    }
 
    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject); // Destroy the enemy
    }

    public int GetHealth()
    {
        return health;
    }
}
