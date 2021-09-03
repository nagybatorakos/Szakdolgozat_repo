using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Stat variables
    [SerializeField] public float lvl = 1;
    [SerializeField] public float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private string Name;
    [SerializeField] private float dmg;
    [SerializeField] private float ms;


    //Attack variables
    [SerializeField] private float asp=0.5f;
    [SerializeField] private float detectionRange=2.5f;
    [SerializeField] private float attackrange = 0.2f;
    [SerializeField] private bool detected=false;
    public float nextattack = 0f;


    //Components
    [SerializeField] private Collider2D jumppoint;
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField]private GameObject player;
    [SerializeField] private Transform attackpoint;


    //Layers
    [SerializeField] private LayerMask EnemyLayers;
    [SerializeField] private LayerMask ground;

    public Animator_Enemy anim;
    private bool attackended;

    public enum Stance {move, attack }
    public Stance stance = Stance.move;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        
        //stat inserting
        Name = this.name.Split(char.Parse(" "))[0];
        Search();


        currentHealth = maxHealth;
    }


    void Update()
    {
        

        //Player detection
        Collider2D[] Denem = Physics2D.OverlapCircleAll(transform.position, detectionRange, EnemyLayers);

        if (Denem.Length >0)
        {
            detected = true;
        }

        Movement();
        //Attack();
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
                dmg = n.Damage * lvl;
                asp = n.AttackSpeed * lvl;
                ms = n.MovementSpeed * lvl;

                break;
            }
        }
    
    }

    private void Movement()
    {
        //if (detected == false || stance != 0)
        //{
        //    return;
        //}


        if(detected==false || !anim.isComplete )
        {
            return;
        }

        //transform.localScale = player.transform.localScale;

        if (transform.position.x-player.transform.position.x>0.9f)
        {
            stance = Stance.move;
            rb.velocity = new Vector2(-1*ms, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (transform.position.x - player.transform.position.x < -0.9f) 
        {
            stance = Stance.move;
            rb.velocity = new Vector2(ms, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            stance = Stance.attack;
        }

        if (jumppoint.IsTouchingLayers(ground) && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 6f);
        }

    }

    public void Attack()
    {
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, EnemyLayers);

        if (HitEnemies.Length < 1 || nextattack > Time.time)
        {
            return;
        }

        //stance = Stance.attack;

        nextattack = Time.time + 1f / asp;


        //damage them each
        foreach (Collider2D enemy in HitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
            enemy.GetComponent<Player_Controller>().TakeDamage(dmg);
            
        }


        //stance = Stance.move;
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
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackpoint != null)
        {
            Gizmos.DrawWireSphere(attackpoint.position, attackrange);
        }
    }
}
