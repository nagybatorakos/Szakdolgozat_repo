using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_P : Projectile
{

    public float special_damage;
    public bool special;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }


    private void Launch()
    {
        rb.velocity = new Vector3(speed * transform.localScale.x, rb.velocity.y);




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" || collision.gameObject.name == "see")
        {
            return;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (special)
            {
                if (arrow)
                {

                    Enemy en = collision.GetComponent<Enemy>();
                    if (en != null)
                    {
                        en.TakeDamage(special_damage);
                    }
                }
                else
                {
                    damage = special_damage;

                }

            }
            else
            {




                //on collision destroy
                Debug.Log(collision.gameObject.name);
                Destroy(gameObject);

                //instantiate sprites as children of enemy

                //on collision damage
                //???whats that?? brackeys 11:45
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }


        }
        else
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
