using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Enemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject deadsprite;
    private protected Rigidbody2D rb;
    private protected Collider2D coll;
    [SerializeField] private protected Animator anim;
    [SerializeField] private protected LayerMask ground;

    public enum State { Idle, Run, Jump, Fall, Attack_1, Die, Dash }
    public State CurrentState = State.Idle;
    public State NewState = State.Idle;






    //[SerializeField] private float xAxis;




    public bool isComplete = true;
    [SerializeField] private protected bool isGrounded;

    //[SerializeField] private bool isRisen = false;
    //[SerializeField] private bool isAttackPressed;
    //[SerializeField] private bool isRollPressed;


    //public EnemyRanged sc;
    //public object sc;



    void Start()
    {

    }



    private void Update()
    {
        
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
