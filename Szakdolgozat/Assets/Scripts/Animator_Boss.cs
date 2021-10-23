using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Boss : Animator_Enemy
{
    public Boss sc;

    private void Start()
    {

        sc = enemy.GetComponent<Boss>();



        rb = enemy.GetComponent<Rigidbody2D>();
        coll = enemy.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();


        //sc = enemy.GetComponent<EnemyRanged>();

        isComplete = true;
    }
    private void Update()
    {

        //Debug.Log(isComplete);
        if (coll.IsTouchingLayers(ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            if (isComplete && sc.detected)
            {
                //if (sc.next_dash < Time.time && sc.chp < sc.maxHealth / 2)
                //{
                    
                //    anim.Play("Dash");
                //    sc.next_dash = Time.time + 2f / sc.asp;
                //    //isComplete = false;
                //    //ChangeAnimationState(State.Dash);
                //}
                if (rb.velocity.x != 0)
                {
                    ChangeAnimationState(State.Run);
                    //sc.nextattackal valami h varjon
                }






                else if (sc.nextattack < Time.time)
                {
                    sc.nextattack = Time.time + 1f / sc.asp;
                    //isComplete = false;
                    ChangeAnimationState(State.Attack_1);

                }
            }


        }

    }
    public void drop()
    {
        sc.DropItem();
    }
    public void start_dash()
    {
        sc.dash = true;
    }
    public void end_dash()
    {
        sc.dash = false;
    }

    public void ScAttack()
    {
        sc.Attack();
    }
}
