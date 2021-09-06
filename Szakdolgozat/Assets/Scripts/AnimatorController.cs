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

    public enum State { Idle_bow, Run_bow, Roll_bow, Rise_bow, Fall_bow, Attack_bow, Die_bow }
    public State CurrentState = State.Idle_bow;
    public State NewState = State.Idle_bow;






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
                if (CurrentState == State.Run_bow)
                {
                    isComplete = true;
                }
                ChangeAnimationState(State.Idle_bow);

            }

            if (xAxis != 0)
            {
                ChangeAnimationState(State.Run_bow);
                //isComplete = false;
            }

            if (CurrentState == State.Fall_bow)
            {
                isComplete = true;
            }


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeAnimationState(State.Rise_bow);
                isComplete = false;
                isRisen = true;
            }



        }



        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAnimationState(State.Roll_bow);
            isComplete = false;
        }


        if (isRisen)
        {
            if (rb.velocity.y < 0f)
            {
                ChangeAnimationState(State.Fall_bow);
                isRisen = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimationState(State.Attack_bow);
            isComplete = false;
        }

    }

    private void FixedUpdate()
    {

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


    public void SpawnArrow()
    {
        player.GetComponent<Player_Controller>().SpawnArrow();
    }


}
