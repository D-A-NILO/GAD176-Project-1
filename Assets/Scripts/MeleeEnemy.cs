using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyScript
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    protected override void Update()
    {
        base.Update();

        if (player && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Melee Enemy Attacks!");
            // If player has a health script, call: player.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
