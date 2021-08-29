using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;
    public GameObject player;
    public Player_Controller pc;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask ground;

    public enum State { idle, run, attack, roll, die, rise, fall }
    public State stance = State.idle;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        coll = player.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        AnimationState();
        anim.SetInteger("state", (int)stance);

    }


    private void AnimationState()
    {
        if (stance == State.attack)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stance = State.attack;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            stance = State.rise;
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                stance = State.roll;
            }
            else
            {
                stance = State.run;
            }

        }




        if (stance == State.rise)
        {
            if (rb.velocity.y < .2f)
            {
                stance = State.fall;
            }
        }
        else if (stance == State.fall)
        {
            if (coll.IsTouchingLayers(ground))
            {
                stance = State.idle;
                //audio.landing();
            }
        }

        //if (Mathf.Abs(rb.velocity.x) > .2f)
        //{
        //    if (stance == State.roll)
        //    {
        //        return;
        //    }
        //    stance = State.run;
        //}
        else if (Mathf.Abs(rb.velocity.x) < .2f)
        {
            stance = State.idle;
        }
    }

    private void SetIdle()
    {
        stance = State.idle;
    }

    public void SpawnArrow()
    {
        pc.SpawnArrow();
    }

    public void Roll()
    {

    }

}
