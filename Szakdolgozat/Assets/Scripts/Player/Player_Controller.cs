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
    public float MovementSpeed = 5f;
    public float Jumpheight = 5f;


    //Attack variables
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.5f;
    public float AttackDamage = 20f;
    public float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private GameObject projectile;


    //Layers
    [SerializeField] private LayerMask EnemyLayers;
    [SerializeField] private LayerMask ground;


    //Health
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currentHealth;


    public Inventory inv;
    public StatSys statsys;
    public AnimatorController anim;
    //[SerializeField] private GameObject go;

    //movement bools
    [SerializeField] private bool run = false;
    [SerializeField] private bool rise = false;



    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        coll = GetComponent<Collider2D>();

        //AttackPoint = GameObject.Find("attackpoint").GetComponent<Transform>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


    }


    // Update is called once per frame

    private void Update()
    {
        InputDetection();
    }

    void FixedUpdate()
    {
        Movement();


    }


    private void Movement()
    {
        if (run)
        {
            rb.velocity = new Vector2(MovementSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        }

        if (rise)
        {
            //rb.velocity = new Vector2(rb.velocity.x, Jumpheight);
        }

    }

    private void InputDetection()
    {

        //Horizontal Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            run = true;
            //rb.velocity = new Vector2(MovementSpeed * -1, rb.velocity.y);
            tf.localScale = new Vector2(-1, 1);

        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            run = true;
            //rb.velocity = new Vector2(MovementSpeed, rb.velocity.y);
            tf.localScale = new Vector2(1, 1);

        }
        else
        {
            run = false;
        }


        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && coll.IsTouchingLayers(ground))
        {
            rise = true;

            rb.velocity = new Vector2(rb.velocity.x, Jumpheight);

        }
        else
        {
            rise = false;
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
                Debug.Log("we hit " + enemy.name);
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
        projectile.GetComponent<Projectile>().player = true;
        Debug.Log("arrow");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Item")
        {
            return;
        }

        if (collision.gameObject.name.StartsWith("Coin"))
        {
            collision.gameObject.GetComponent<Pickup>().pickup();
            inv.coins+= collision.gameObject.GetComponent<Pickup>().value;
            //itt inv.coins++
        }
        else
        {
            //string[] st = collision.gameObject.name.Split(' ');

            inv.AddtoInv(collision.gameObject);
            Destroy(collision.gameObject);

        }

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
