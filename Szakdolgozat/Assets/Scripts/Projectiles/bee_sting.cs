using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class bee_sting : Projectile
{
    // Start is called before the first frame update
    [SerializeField] Transform pl;
    private float ips;
    private float ipu;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = GameObject.FindGameObjectWithTag("Player").transform;
        ips = pl.position.y - 0.8f;

        if (transform.localScale.x==-1)
        {
            ipu = pl.position.x - 0.8f;

        }
        else
        {
            ipu = pl.position.x + 0.8f;
        }

    }


    // Update is called once per frame
    void Update()
    {
        Launch();
        if (transform.position == Vector3.MoveTowards(transform.position, new Vector3(ipu, ips, pl.position.z), speed * Time.deltaTime)) { Destroy(gameObject); }
    }


    private void Launch()
    {

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(ipu, ips, pl.position.z), speed * Time.deltaTime);




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
