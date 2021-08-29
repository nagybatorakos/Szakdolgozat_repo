using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;
    public GameObject player;
    public enum State { idle, run, attack, roll, die, jump, fall }
    public State stance = State.idle;
    public Player_Controller pc;
    [SerializeField] private Animator anim;




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
        //if (stance == State.jump)
        //{
        //    if (rb.velocity.y < .2f)
        //    {
        //        stance = State.fall;
        //    }
        //}
        //else if (stance == State.fall)
        //{
        //    if (coll.IsTouchingLayers(ground))
        //    {
        //        stance = State.idle;
        //        //audio.Landing();
        //    }
        //}

        if (Mathf.Abs(rb.velocity.x) > .2f)
        {
            stance = State.run;
        }
        else if (stance != State.attack)
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

}
