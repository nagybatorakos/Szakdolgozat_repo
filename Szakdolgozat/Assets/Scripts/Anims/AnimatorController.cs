using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    public GameObject player;
    private Transform tf;
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;

    public enum State { Idle, Run, Roll, Rise, Fall, Attack_1, Special, Die }
    public State CurrentState = State.Idle;
    public State NewState = State.Idle;

    public AudioClip[] audios = new AudioClip[3];
    public AudioSource audio;
   
    




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
        tf = player.GetComponent<Transform>();


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

        if (player.GetComponent<Player_Controller>().special)
        {
            ChangeAnimationState(State.Special);
            isComplete = false;
           
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAnimationState(State.Roll);
            rb.velocity = new Vector2(5f, rb.velocity.y);
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

    public void footstep()
    {
        audio.clip = audios[0];
        audio.Play();
    }

    public void attack_sound()
    {
        audio.clip = audios[1];
        audio.Play();
    }

    public void spec_ended()
    {
       player.GetComponent<Player_Controller>().special = false;
        player.layer = 10;
    }

    public void war_special()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Player_Controller>().special_range * tf.localScale.x, rb.velocity.y);
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

    public void Slash()
    {
        player.GetComponent<Player_Controller>().Attack();
    }

    public void SpawnA()
    {
        player.GetComponent<Player_Controller>().SpawnArrow();
    }


}
