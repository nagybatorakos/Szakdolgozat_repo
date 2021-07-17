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
    private float detectionRange=1f;

    private bool detected=false;

    [SerializeField] private LayerMask EnemyLayers;
    private Transform tf;
    private GameObject player;
    void Start()
    {
        tf = GetComponent<Transform>();
        player = GameObject.Find("Player");

        Name = this.name.Split(char.Parse(" "))[0];

        Search();
        currentHealth = maxHealth;
    }


    void Update()
    {
        
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(this.transform.position, detectionRange, EnemyLayers);

        if (HitEnemies != null)
        {
            detected = true;
        }

        Movement();

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

    private void Movement()
    {
        if (detected == false)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.5f * Time.deltaTime);




    }




    public void TakeDamage(float damage) 
    {
        currentHealth -= damage;

        float k = 3.1f * (damage / maxHealth);

        RectTransform hpbar = GameObject.Find("fill").GetComponent<RectTransform>();
        if (hpbar.localScale.x < k)
        {
            k = hpbar.localScale.x;
        }
        hpbar.localScale = new Vector3(hpbar.localScale.x -k, hpbar.localScale.y, hpbar.localScale.z);
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
