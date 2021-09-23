using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    public GameObject player;

    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;

    public enum State { Idle, Run, Roll, Rise, Fall, Attack_1, Die }
    public State CurrentState = State.Idle;
    public State NewState = State.Idle;






    [SerializeField] private float xAxis;




    [SerializeField] private bool isComplete;
    [SerializeField] private bool isGrounded;

    [SerializeField] private bool isRisen = false;
    [SerializeField] private bool isAttackPressed;
    [SerializeField] private bool isRollPressed;







    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        coll = player.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();



    }



    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");

        if (coll.IsTouchingLayers(ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //############################

        if (isGrounded)
        {
            if (xAxis == 0)
            {
                //isComplete = true;
                if (CurrentState == State.Run)
                {
                    isComplete = true;
                }
                ChangeAnimationState(State.Idle);

            }

            if (xAxis != 0)
            {
                ChangeAnimationState(State.Run);
                //isComplete = false;
            }

            if (CurrentState == State.Fall)
            {
                isComplete = true;
            }


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeAnimationState(State.Rise);
                isComplete = false;
                isRisen = true;
            }



        }



        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAnimationState(State.Roll);
            isComplete = false;
        }


        if (isRisen)
        {
            if (rb.velocity.y < 0f)
            {
                ChangeAnimationState(State.Fall);
                isRisen = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimationState(State.Attack_1);
            isComplete = false;
        }

    }





    public void AnimationComplete()
    {
        isComplete = true;
    }


    void ChangeAnimationState(State newState)
    {
        if (!isComplete)
        {
            if (((int)newState) > ((int)CurrentState))
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


    public void SpawnA()
    {
        player.GetComponent<Player_Controller>().SpawnArrow();
    }


}
