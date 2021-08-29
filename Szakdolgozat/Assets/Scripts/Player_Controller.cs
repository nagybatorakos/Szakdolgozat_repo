using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tf;
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float Jumpheight = 5f;


    //attack
    private Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.5f;
    [SerializeField] private float AttackDamage = 20f;

    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;

    [SerializeField] private LayerMask EnemyLayers;

    public bool sword = false;
    public bool bow = true;
    [SerializeField] private GameObject projectile;
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currentHealth;
    //public GameObject hp;


    private enum State { idle, run, attack, roll, die }
    private State stance = State.idle;

    [SerializeField] private Animator anim;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        

        AttackPoint = GameObject.Find("attackpoint").GetComponent<Transform>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        anim.SetInteger("state", (int)stance);

    }


    private void Movement() 
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(MovementSpeed * -1, rb.velocity.y);
            //tf.position = new Vector3(tf.position.x - 0.2f, tf.position.y, tf.position.z);
            tf.localScale = new Vector2(-1, 1);
            stance = State.run;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(MovementSpeed, rb.velocity.y);
            tf.localScale = new Vector2(1, 1);
            stance = State.run;
        }



        //if (Input.GetKeyUp(KeyCode.LeftArrow) | Input.GetKeyUp(KeyCode.RightArrow))
        //{
            //rb.velocity = new Vector2(0f, rb.velocity.y);
        //}

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumpheight);
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stance = State.attack;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void Attack() 
    {
        //need code for hunter, mage too

        if (sword)
        {
        //anim.SetTrigger("Attack");

        //enemy detection and storing
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        //damage them each
        foreach (Collider2D enemy in HitEnemies) 
        {
            Debug.Log("we hit "+ enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }
        }
        else if (bow)
        {
            //shoot+ on hit destroy
            projectile.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            Instantiate(projectile, AttackPoint.position, transform.rotation);
            //damage collided
            //Debug.Log("we hit " + enemy.name);
            //enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth);
        Debug.Log("player took dmg");
    }


    //draws hitbox
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint != null) 
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }

}
