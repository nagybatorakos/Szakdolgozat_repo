using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] private float dist;

    void Start()
    {
        hpbar = gameObject.transform.Find("healthbar").Find("fill").GetComponent<RectTransform>();

        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        //stat inserting
        Name = this.name.Split(char.Parse(" "))[0];
        //Search();


        currentHealth = maxHealth;
    }


    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Main Camera").GetComponent<Camera_Controller>().player;
        }

        if (isDead)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        else
        {
            //Player detection
            DetectPlayer();

            Movement();
            //Attack();
        }

    }


    private void Movement()
    {

        if (detected == false || !anim.isComplete)
        {
            return;
        }

        if (Vector2.Distance(player.transform.position, transform.position) < dist)
        {
            rb.velocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        if (transform.position.x - player.transform.position.x > 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //stance = Stance.move;
            rb.velocity = new Vector2(-1 * ms, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (transform.position.x - player.transform.position.x < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //stance = Stance.move;
            rb.velocity = new Vector2(ms, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //stance = Stance.attack;
        }

        if (jumppoint.IsTouchingLayers(ground) && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 6f);
        }

    }

    public void Attack()
    {
        
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, EnemyLayers);
        Debug.Log(HitEnemies.Length);


        //damage them each
        foreach (Collider2D enemy in HitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
            enemy.GetComponent<Player_Controller>().TakeDamage(dmg);

        }
        nextattack = Time.time + 1f / asp;

        //stance = Stance.move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name.StartsWith("Slime"))
        {
            if (collision.gameObject.name.StartsWith("Player"))
            {
                collision.gameObject.GetComponent<Player_Controller>().TakeDamage(dmg);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackpoint != null)
        {
            Gizmos.DrawWireSphere(attackpoint.position, attackrange);
        }
    }
}
