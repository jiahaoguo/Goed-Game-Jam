using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public float attackSpeed=0.5f;
    public float attackTime;

    public LayerMask enemyMask;

    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.move, moving);
        Messenger.AddListener<int>(Events.attack, attack);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.move, moving);
        Messenger.RemoveListener<int>(Events.attack, attack);
    }

    private void moving(int distance)
    {
        transform.position+=new Vector3(distance, 0, 0);
    }

    private void attack(int damage)
    {
        // Do animation here

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().gotHit(damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
