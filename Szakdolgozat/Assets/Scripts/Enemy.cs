using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(float damage) 
    {
        currentHealth -= damage;
        //hurt animation

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    void Die() 
    {
        Debug.Log("Enemy died");
        //die anim
        //disable enemy
    }

}
