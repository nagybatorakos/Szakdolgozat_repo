using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float Jumpheight = 5f;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }


    private void Movement() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(MovementSpeed * -1, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(MovementSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumpheight);
        }
    }


}
