using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehavior : Enemy
{
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private GameObject projectile;
    public float dist;
    public float hoverHeight;
    public float hoverForce;


    void Start()
    {
        hpbar = gameObject.transform.Find("healthbar").Find("fill").GetComponent<RectTransform>();
        jumppoint = gameObject.transform.Find("see").gameObject.GetComponent<Collider2D>();
        attackpoint = gameObject.transform.Find("attackpoint");
        

        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        //stat inserting
        Name = this.name.Split(char.Parse(" "))[0];
        //Search();


        currentHealth = maxHealth;
    }


    void Update()
    {
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


        if (player == null)
        {
            player = GameObject.Find("Main Camera").GetComponent<Camera_Controller>().player;
        }



        //Cast a ray in the direction specified in the inspector.
        //RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down*hoverHeight);
        //Debug.DrawRay(transform.position, Vector2.down * hoverHeight);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, -5* hoverHeight));
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, -1*hoverHeight) , 1 << LayerMask.NameToLayer("ground"));
        
        //If something was hit.
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            float hoverError = hoverHeight - hit.distance;
            //Debug.Log("hit", hit.collider);
            float distance = transform.position.y - hit.point.y;
            //Debug.Log($"{distance}, {hit.collider.gameObject.name}");
            // Only apply a lifting force if the object is too low (ie, let
            // gravity pull it downward if it is too high).
            if (distance <= hoverHeight)
            {

                //Debug.Log("adding force");
                // Subtract the damping from the lifting force and apply it to
                // the rigidbody.
                float upwardSpeed = rb.velocity.y;
                float lift = hoverError * hoverForce;// - upwardSpeed;// * hoverDamp;
                rb.AddForce(lift * Vector2.up);

            }

            //If the object hit is less than or equal to 6 units away from this object.
            if (hit.distance <= hoverHeight)
            {
                rb.AddForce(new Vector2(0, hoverForce));
                //Debug.Log("Enemy In Range!");
            }
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
            //transform.localScale = new Vector2(player.transform.localScale.x * -1, 1);
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



        if (player.transform.position.y != transform.position.y || nextattack > Time.time)
        {
            return;
        }

        //stance = Stance.attack;

        nextattack = Time.time + 1f / asp;



        SpawnArrow();

        //stance = Stance.move;
    }

    public void SpawnArrow()
    {

        projectile.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        Instantiate(projectile, AttackPoint.position, transform.rotation);
        projectile.GetComponent<Projectile>().player = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackpoint != null)
        {
            Gizmos.DrawWireSphere(attackpoint.position, attackrange);
        }
    }
}