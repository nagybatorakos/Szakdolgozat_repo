using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool player = true;

    private protected Rigidbody2D rb;
    public bool arrow = true;
    [SerializeField] private protected float speed = 4f;
    [SerializeField] private protected float damage = 20f;


    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //private void Launch()
    //{
    //    rb.velocity = new Vector3(speed * transform.localScale.x, rb.velocity.y);




    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.tag == "Player")
    //    {
    //        return;
    //    }
    //    //on collision destroy
    //    Destroy(gameObject);

    //    //instantiate sprites as children of enemy

    //    //on collision damage
    //    //???whats that?? brackeys 11:45
    //    Enemy enemy = collision.GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        enemy.TakeDamage(damage);
    //    }


    //}
}
