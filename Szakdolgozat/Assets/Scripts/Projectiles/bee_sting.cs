using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee_sting : Projectile
{
    // Start is called before the first frame update
    [SerializeField] Transform pl;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }


    private void Launch()
    {
        
        transform.position = Vector3.MoveTowards(transform.position,pl.position, speed * Time.time);




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            return;
        }
        //on collision destroy
        Destroy(gameObject);

        //instantiate sprites as children of enemy

        //on collision damage
        //???whats that?? brackeys 11:45
        Player_Controller enemy = collision.GetComponent<Player_Controller>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

    }
}
