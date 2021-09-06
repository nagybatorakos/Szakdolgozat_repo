using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Enemy : MonoBehaviour
{

    public GameObject enemy;
    public GameObject deadsprite;
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;

    public enum State { Idle, Run, Jump, Fall, Attack_1, Die }
    public State CurrentState = State.Idle;
    public State NewState = State.Idle;






    //[SerializeField] private float xAxis;




    public bool isComplete = true;
    [SerializeField] private bool isGrounded;

    //[SerializeField] private bool isRisen = false;
    //[SerializeField] private bool isAttackPressed;
    //[SerializeField] private bool isRollPressed;

    public Enemy sc;





    void Start()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
        coll = enemy.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sc = enemy.GetComponent<Enemy>();

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

    public void ScAttack()
    {
        sc.Attack();
    }

    public void AnimationStart()
    {
        isComplete = false;
    }

    public void AnimationComplete()
    {
        isComplete = true;
        ChangeAnimationState(State.Idle);
        //sc.stance = Enemy.Stance.move;
        Debug.Log($"iscomplete changed to: {isComplete}");
    }


    public void Delete()
    {
        deadsprite.transform.position = transform.position;

        deadsprite.transform.localScale = enemy.transform.localScale;
        Instantiate(deadsprite);
        Destroy(enemy);
    }
    public void Die()
    {
        ChangeAnimationState(State.Die);
        isComplete = false;

    }

    public void ChangeAnimationState(State newState)
    {
        if (!isComplete)
        {
            if (((int)newState) >= ((int)CurrentState))
            {
                anim.Play(newState.ToString());
                CurrentState = newState;
            }
            else
            {
                return;
            }

        }
        else
        {
            anim.Play(newState.ToString());
            CurrentState = newState;
        }

    }

}
