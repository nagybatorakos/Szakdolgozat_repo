using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float lvl = 1;
    [SerializeField] public float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private string Name;
    [SerializeField] private float attack;
    [SerializeField] private float ms;
    [SerializeField] private float asp;
    [SerializeField] private float detectionRange=2.5f;

    [SerializeField] private bool detected=false;

    [SerializeField] private LayerMask EnemyLayers;
    private Rigidbody2D rb;
    private GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        Name = this.name.Split(char.Parse(" "))[0];

        Search();
        currentHealth = maxHealth;
    }


    void Update()
    {
        
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRange, EnemyLayers);

        if (HitEnemies.Length >0)
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

        //transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.5f * Time.deltaTime);
        if (transform.position.x-player.transform.position.x>1f)
        {
            rb.velocity = new Vector2(-1*ms, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (transform.position.x - player.transform.position.x < -1f) 
        {
            rb.velocity = new Vector2(ms, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

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

    private void OnDrawGizmosSelected()
    {
        if (transform.position != null)
        {
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}
