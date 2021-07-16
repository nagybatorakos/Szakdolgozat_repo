using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tf;
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float Jumpheight = 5f;

    private Animator anim;

    //attack
    private Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.5f;
    [SerializeField] private float AttackDamage = 20f;

    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;

    [SerializeField] private LayerMask EnemyLayers;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        AttackPoint = GameObject.Find("attackpoint").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }


    private void Movement() 
    {
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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumpheight);
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void Attack() 
    {
        //need code for hunter, mage too

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



    //draws hitbox
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint != null) 
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }

}
