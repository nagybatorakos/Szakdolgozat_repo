using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;
    public bool arrow= true;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float damage = 20f;

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
        //on collision destroy
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
