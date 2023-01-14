using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public float attackSpeed=0.5f;
    public float attackTime;
    private Animator animator;
    private bool hit=false;

    private float moveDistance=0;
    private float timer=0;

    public LayerMask enemyMask;
    public LayerMask Wall;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.move, moving);
        Messenger.AddListener<int>(Events.attack, attack);
        Messenger.AddListener<int>(Events.timeStart, addTimer);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.move, moving);
        Messenger.RemoveListener<int>(Events.attack, attack);
        Messenger.RemoveListener<int>(Events.timeStart, addTimer);
    }
    private void addTimer(int x)
    {
        timer = x;
    }
    private void moving(int distance)
    {
        Collider2D[] hitwalls = Physics2D.OverlapAreaAll(transform.position, transform.position+new Vector3(distance, 1, 0),Wall);
        if(hitwalls.Length > 0)
        {
            moveDistance = Mathf.Infinity;
        }
        else
        {
            moveDistance= distance;
        }        
    }
    private void Update()
    {
        if (moveDistance == Mathf.Infinity && timer > 0)
        {
            //rejection Animation
            timer -= Time.deltaTime;
        }
        else if (moveDistance > 0 && timer > 0)
        {
            float x = moveDistance / timer * Time.deltaTime;
            transform.position += new Vector3(x, 0, 0);
            moveDistance -= x;
            timer -= Time.deltaTime;
        }
        else if (moveDistance<0.2)
        {
            transform.position += new Vector3(moveDistance, 0, 0);
            moveDistance = 0;
            timer = 0;
        }
        if (timer <= 0)
        {
            animator.enabled = false;
            GetComponent<Collider2D>().enabled = true;
            if (hit) transform.Rotate(new Vector3(0, 0,-90));
            hit = false;
        }
            
        else 
            animator.enabled = true;
        
    }

    public void getHurt(int damage)
    {
        timer = 2;
        hit = true;
        moveDistance = Mathf.Infinity;
        transform.Rotate(new Vector3(0 , 0, 90));
        GetComponent<Collider2D>().enabled = false;
        Messenger.Broadcast<int>(Events.timeStart, 2);
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
