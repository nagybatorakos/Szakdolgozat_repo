using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lvl = 1;
    public float maxHealth;
    private float currentHealth;
    private string Name;
    private float attack;
    private float ms;
    private float asp;

    void Start()
    {
        Name = this.name.Split(char.Parse(" "))[0];

        Search();
        currentHealth = maxHealth;
    }


    void Update()
    {

    }


    public void Search()
    { 
        Stats[] elemek = GameObject.Find("GameObject").GetComponent<Stat_DB>().stats;


        //Component elem = GameObject.Find("GameObject").GetComponent<Stat_DB>();

        foreach(Stats n in elemek)
        {
            if (n.name == Name)
            {
                maxHealth = n.HP * lvl;
                attack = n.Damage * lvl;
                asp = n.AttackSpeed * lvl;
                ms = n.MovementSpeed * lvl;

                break;
            }
        }
    
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
