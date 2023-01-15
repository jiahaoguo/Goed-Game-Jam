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
    public GameObject hitParticle;
    private bool hit=false;
    private int direction = 1;

    private float moveDistance=0;
    private float timer=0;
    private cardDisplay interativeCard;

    public LayerMask enemyMask;
    public LayerMask Wall;
    public LayerMask Platform;

    private deckHolder panel;

    public cards[] addOnCards;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {            
            if(panel != null)
            {
                interativeCard = panel.addCard(addOnCards[0]);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(panel.deck.Contains(interativeCard)) panel.deck.Remove(interativeCard);
        if(panel.hands.Contains(interativeCard)) panel.hands.Remove(interativeCard);
        if(panel.discards.Contains(interativeCard)) panel.discards.Remove(interativeCard);
        if(interativeCard!=null)interativeCard.gameObject.SetActive(false);
    }
    private void Start()
    {
        panel = FindObjectOfType<deckHolder>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.move, moving);
        Messenger.AddListener<int>(Events.attack, attack);
        Messenger.AddListener<int>(Events.equip, equiping);
        Messenger.AddListener<int>(Events.timeStart, addTimer);
        Messenger.AddListener(Events.turn, turning);
        Messenger.AddListener<int>(Events.up,goingUp);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.move, moving);
        Messenger.RemoveListener<int>(Events.attack, attack);
        Messenger.RemoveListener<int>(Events.equip, equiping);
        Messenger.RemoveListener<int>(Events.timeStart, addTimer);
        Messenger.RemoveListener(Events.turn, turning);
        Messenger.RemoveListener<int>(Events.up, goingUp);
    }
    private void addTimer(int x)
    {
        timer = x;
    }
    private void moving(int distance)
    {
        soundManager.Instance.sdPlayerWalk.Play();
        Collider2D[] hitwalls = Physics2D.OverlapAreaAll(transform.position, transform.position+new Vector3(distance*direction, 1, 0),Wall);
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
        if (Physics2D.OverlapCircleAll(transform.position,1.7f, Platform).Length <= 0)
        {
            transform.position += new Vector3(0, -6, 0)*Time.deltaTime;
        }
        if (moveDistance == Mathf.Infinity && timer > 0)
        {
            //rejection Animation
        }
        else if (moveDistance > 0 && timer > 0)
        {
            float x = moveDistance / timer * Time.deltaTime;
            transform.position += new Vector3(x*direction, 0, 0);
            moveDistance -= x;
            
        }
        if (moveDistance < 0.2 && moveDistance != 0)
        {
            transform.position += new Vector3(moveDistance*direction, 0, 0);
            moveDistance = 0;
        }
        if (timer <= 0)
        {
            soundManager.Instance.sdPlayerWalk.Stop();
            animator.enabled = false;
            GetComponent<Collider2D>().enabled = true;
            if (hit) transform.Rotate(new Vector3(0, 0,-90));
            hit = false;
        }            
        else
        {
            timer -= Time.deltaTime;
            animator.enabled = true;
        }        
    }

    public void getHurt(int damage)
    {
        soundManager.Instance.sdHurt.Play();
        timer = 2;
        hit = true;
        moveDistance = Mathf.Infinity;
        transform.Rotate(new Vector3(0 , 0, 90));
        GetComponent<Collider2D>().enabled = false;
        Messenger.Broadcast<int>(Events.timeStart, 2);
    }

    private void attack(int damage)
    {
        if (attackRange == -1)
        {
            RaycastHit2D hitEnemy = Physics2D.Raycast(attackPoint.position, new Vector2(2,0),20f, enemyMask);
            if(hitEnemy.transform != null)
            {
                hitEnemy.transform.GetComponent<Enemy>().gotHit(damage);
                Instantiate(hitParticle, hitEnemy.transform);
            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().gotHit(damage);
                Instantiate(hitParticle, enemy.transform);
            }
        }
        
        // Do animation here    


        soundManager.Instance.sdHit.Play();
    }
    private void equiping(int equip)
    {
        attackRange = equip;
        if(equip == 3)
        {
            soundManager.Instance.sdEquipSword.Play();
            transform.Find("Sword").gameObject.SetActive(true);
            transform.Find("gun").gameObject.SetActive(false);
        }
        if(equip == -1)
        {
            transform.Find("Sword").gameObject.SetActive(false);
            transform.Find("gun").gameObject.SetActive(true);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
    private void turning()
    {
        soundManager.Instance.sdTurn.Play();
        direction = -direction;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }
    private void goingUp(int distance)
    {
        transform.position+=new Vector3(0, distance, 0);
    }
}
