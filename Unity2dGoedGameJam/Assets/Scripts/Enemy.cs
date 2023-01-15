using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health=100;
    public string state = "Roaming";
    public Transform sightPoint;
    public LayerMask visiable;
    public LayerMask player;
    private Transform target;
    private float attackTimer;

    private float timer = 0;
    public float speed = 1;
    public LayerMask Wall;

    private Animator animator;
    int currentHealth;
    private void Start()
    {
        animator = GetComponent<Animator>();
        sightPoint = transform.Find("vision");
        currentHealth = health;
    }
    private void Update()   
    {
        if(timer > 0)
        {
            animator.enabled = true;
            if(state == "Standing")
            {
                Transform capture = Physics2D.Raycast(sightPoint.position, new Vector2(speed * 7, 0), 7f, visiable).transform;
                if (capture != null && capture.tag == "Player")
                {
                    target = capture;
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    state = "Alert";    
                }
            }
            if (state == "Roaming")
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                if (Physics2D.OverlapAreaAll(transform.position, transform.position + new Vector3(2, 3, 0), Wall).Length > 0)
                {
                    speed = -speed;
                    transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                }
                Transform capture = Physics2D.Raycast(sightPoint.position, new Vector2(speed * 7, 0), 7f, visiable).transform;
                if (capture != null && capture.tag == "Player")
                {
                    target = capture;
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    state = "Alert";
                }
            }
            if(state == "Alert")
            {
                Transform capture = Physics2D.Raycast(sightPoint.position, new Vector2(speed * 7, 0), 7f, visiable).transform;
                if (capture == null)
                {
                    soundManager.Instance.sdElfWalk.Play();
                    state = "Roaming";
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
                transform.position=Vector3.MoveTowards(transform.position, new Vector3(target.position.x,transform.position.y,0), Time.deltaTime* Mathf.Abs(speed) * 2);
                if (transform.position.x > target.position.x)
                {
                    speed = -1;
                    transform.localScale = new Vector3(-1,1,1);
                }
                else
                {
                    speed = 1;
                    transform.localScale = new Vector3(1,1,1);
                }
                if(Mathf.Abs(target.position.x-transform.position.x)<=2){
                    state = "Attack";
                    GetComponent<SpriteRenderer>().color = Color.red;
                    attackTimer = 2f;
                }
                
            }
            if(state == "Attack")
            {
                if(attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    float distance = target.position.x - transform.position.x;
                    if ((distance >= -2&&distance<0 && speed<0)|| (distance < 2&&distance>0 && speed > 0))                
                        target.GetComponent<characterMovement>().getHurt(1);
                    state = "Rest";
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    attackTimer = 2f;
                }
            }
            if (state == "Rest")
            {
                attackTimer-= Time.deltaTime;
                if(attackTimer < 0)
                {
                    Transform capture = Physics2D.Raycast(sightPoint.position, new Vector2(speed * 7, 0), 7f, visiable).transform;
                    if(capture != null && capture.tag == "Player")
                    {
                        target = capture;
                        GetComponent<SpriteRenderer>().color = Color.yellow;
                        state = "Alert";
                    }
                    else
                    {
                        state = "Roaming";
                        GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
            }
            
            timer -= Time.deltaTime;
        }
        else animator.enabled = false;
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(Events.timeStart, addTimer);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<int>(Events.timeStart, addTimer);
    }
    private void addTimer(int x)
    {
        timer = x;
    }
    public void gotHit(int damage)
    {
        soundManager.Instance.sdHurt.Play();
        //Onhit Animation
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            transform.Rotate(new Vector3(0, 0, 90));
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, new Vector2(7,0));
        
    }
}
