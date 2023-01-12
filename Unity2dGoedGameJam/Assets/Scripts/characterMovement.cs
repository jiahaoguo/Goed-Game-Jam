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
    public LayerMask Wall;

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
        Collider2D[] hitwalls = Physics2D.OverlapAreaAll(transform.position, transform.position+new Vector3(distance, 1, 0),Wall);
        if(hitwalls.Length > 0)
        {
            //Rejection Animation
        }
        else
        {
            transform.position += new Vector3(distance, 0, 0);
        }        
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
        Gizmos.DrawCube(transform.position+ new Vector3(2, 0, 0), new Vector3(4, 1, 0));
    }
}
