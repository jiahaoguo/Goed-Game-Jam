using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health=100;
    public string state = "Roaming";
    private float timer = 0;
    public float speed = 1;
    public LayerMask Wall;
    private Transform sightPoint;
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
            Debug.Log(Physics2D.Raycast(transform.position, sightPoint.position, 7f, 6));
            if (state == "Roaming")
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                if (Physics2D.OverlapAreaAll(transform.position, transform.position + new Vector3(2, 1, 0), Wall).Length > 0)
                {
                    speed = -speed;
                    transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                }
                timer -= Time.deltaTime;
            }            
            if(Physics2D.Raycast(transform.position, sightPoint.position,7f,6))
            {
                
                GetComponent<SpriteRenderer>().color = Color.red;
            }
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
        //Onhit Animation
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            //death Animation
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
