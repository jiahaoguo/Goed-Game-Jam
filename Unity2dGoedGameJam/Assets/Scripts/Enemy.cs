using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health=100;
    int currentHealth;
    private void Start()
    {
        currentHealth = health;
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
