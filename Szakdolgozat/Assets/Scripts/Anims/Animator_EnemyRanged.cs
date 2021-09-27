using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_EnemyRanged : Animator_Enemy
{
    public EnemyRanged sc;
    private void Start()
    {
        sc = enemy.GetComponent<EnemyRanged>();

        rb = enemy.GetComponent<Rigidbody2D>();
        coll = enemy.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();


        //sc = enemy.GetComponent<EnemyRanged>();

        isComplete = true;
    }
    private void Update()
    {

        Debug.Log(isComplete);
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
            if (isComplete)
            {
                if (rb.velocity.x != 0)
                {
                    ChangeAnimationState(State.Run);
                    //sc.nextattackal valami h varjon
                }

                else if (sc.nextattack < Time.time && sc.detected)
                {

                    //isComplete = false;
                    ChangeAnimationState(State.Attack_1);

                }
            }


        }

    }
    public void SpawnArrow()
    {
        sc.SpawnArrow();
    }


    public void ScAttack()
    {
        sc.Attack();
    }
}