using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //Class Selected
    public bool sword = false;
    public bool bow = true;


    //Components
    public Rigidbody2D rb;
    public Transform tf;
    public Collider2D coll;


    //Move and jump floats
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float Jumpheight = 5f;


    //Attack variables
    private Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.5f;
    [SerializeField] private float AttackDamage = 20f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private GameObject projectile;


    //Layers
    [SerializeField] private LayerMask EnemyLayers;
    [SerializeField] private LayerMask ground;


    //Health
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currentHealth;



    public AnimatorController anim;
    //[SerializeField] private GameObject go;


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        coll = GetComponent<Collider2D>();

        AttackPoint = GameObject.Find("attackpoint").GetComponent<Transform>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();


    }


    private void Movement() 
    {

        //Horizontal Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(MovementSpeed * -1, rb.velocity.y);
            tf.localScale = new Vector2(-1, 1);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(MovementSpeed, rb.velocity.y);
            tf.localScale = new Vector2(1, 1);

        }


        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumpheight);

        }


        //Attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
            //projectile.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            //Instantiate(projectile, AttackPoint.position, transform.rotation);
            //SpawnArrow();


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

    public void SpawnArrow()
    {
        projectile.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        Instantiate(projectile, AttackPoint.position, transform.rotation);
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
