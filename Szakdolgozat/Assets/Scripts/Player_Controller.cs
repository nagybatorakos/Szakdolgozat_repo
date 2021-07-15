using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tf;
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float Jumpheight = 5f;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

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
    }


}
