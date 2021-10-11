using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    //Class Selected
    public bool sword;
    public bool bow;
    public bool staff;


    //Components
    public Rigidbody2D rb;
    public Transform tf;
    public Collider2D coll;


    //Move and jump floats
    public float MovementSpeed = 5f;
    public float Jumpheight = 5f;


    //Attack variables
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.5f;
    public float AttackDamage = 20f;
    public float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private GameObject projectile;


    //Layers
    [SerializeField] private LayerMask EnemyLayers;
    [SerializeField] private LayerMask ground;


    //Health
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currentHealth;


    public Inventory inv;
    public StatSys statsys;
    public AnimatorController anim;
    //[SerializeField] private GameObject go;

    //movement bools
    [SerializeField] private bool run = false;
    [SerializeField] private bool rise = false;

    public Camera_Controller cam;
    public Animator transition;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        coll = GetComponent<Collider2D>();

        //AttackPoint = GameObject.Find("attackpoint").GetComponent<Transform>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


    }


    // Update is called once per frame

    private void Update()
    {
        InputDetection();
    }

    void FixedUpdate()
    {
        Movement();


    }


    private void Movement()
    {
        if (run)
        {
            rb.velocity = new Vector2(MovementSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        }

        if (rise)
        {
            //rb.velocity = new Vector2(rb.velocity.x, Jumpheight);
        }

    }

    private void InputDetection()
    {

        //Horizontal Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            run = true;
            //rb.velocity = new Vector2(MovementSpeed * -1, rb.velocity.y);
            tf.localScale = new Vector2(-1, 1);

        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            run = true;
            //rb.velocity = new Vector2(MovementSpeed, rb.velocity.y);
            tf.localScale = new Vector2(1, 1);

        }
        else
        {
            run = false;
        }


        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && coll.IsTouchingLayers(ground))
        {
            rise = true;

            rb.velocity = new Vector2(rb.velocity.x, Jumpheight);

        }
        else
        {
            rise = false;
        }


        //Attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
    }

    public void Attack()
    {
        //need code for hunter, mage too

        if (sword)
        {

            //enemy detection and storing
            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            //damage them each
            foreach (Collider2D enemy in HitEnemies)
            {
                if (enemy.tag != "Enemy") { continue; }
                Debug.Log("we hit " + enemy.name);
                enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            }
        }

    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth);
        Debug.Log("player took dmg");
    }

    public void SpawnArrow()
    {
        projectile.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        if (projectile.name == "fireball")
        {
            //projectile.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y)*2;
            GameObject fire = Instantiate(projectile, AttackPoint.position, projectile.transform.rotation);
            fire.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.x) * 2;
            //fire.transform.localScale = fire.transform.localScale * transform.localScale.x;
        }
        else
        {
            Instantiate(projectile, AttackPoint.position, transform.rotation);

        }
        projectile.GetComponent<Projectile>().player = true;
        Debug.Log("arrow");
    }

    //[System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.tag == "SceneSwap")
        {

            StartCoroutine(ChangeScene(collision));


        }
        else if (collision.gameObject.name.StartsWith("Coin"))
        {
            collision.gameObject.GetComponent<Pickup>().pickup();
            inv.coins += collision.gameObject.GetComponent<Pickup>().value;
            //itt inv.coins++
        }
        else if (collision.tag == "Item")
        {
            //string[] st = collision.gameObject.name.Split(' ');

            inv.AddtoInv(collision.gameObject);
            Destroy(collision.gameObject);

        }

    }

    private void StartTransition()
    {

    }

    IEnumerator ChangeScene(Collider2D collision)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        Dictionary<string, Vector3> positions = new Dictionary<string, Vector3>();
        positions.Add("Village0", new Vector3(30f, -2f, tf.position.z));
        positions.Add("Village1", new Vector3(83f, -1.8f, tf.position.z));
        positions.Add("Cave", new Vector3(-132f, -0.8f, tf.position.z));
        positions.Add("ForestBridge1", new Vector3(-108f, -0.8f, tf.position.z));
        positions.Add("ForestBridge0", new Vector3(20f, -1.6f, tf.position.z));
        positions.Add("Dungeon", new Vector3(97f, -1.8f, tf.position.z));

        Scene active = SceneManager.GetActiveScene();
        Scene next = SceneManager.GetSceneByName(collision.gameObject.name);

        SceneManager.LoadScene(collision.gameObject.name, LoadSceneMode.Additive);

        foreach (GameObject go in cam.transfer)
        {
            Debug.Log(next.name);
            SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(collision.gameObject.name));


        }

        yield return null;

        if (collision.gameObject.name == "ForestBridge")
        {
            if (collision.gameObject.transform.position.x < -125f)
            {
                transform.position = positions[collision.gameObject.name + "1"];
            }
            else
            {
                transform.position = positions[collision.gameObject.name + "0"];
            }
        }
        else if (collision.gameObject.name == "Village")
        {
            if (collision.gameObject.transform.position.x < 30f)
            {
                transform.position = positions[collision.gameObject.name + "0"];
            }
            else
            {
                transform.position = positions[collision.gameObject.name + "1"];
            }
        }
        else
        {
            transform.position = positions[collision.gameObject.name];
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(collision.gameObject.name));

        SceneManager.UnloadScene(active);
        yield return new WaitForSeconds(3f);
        transition.SetTrigger("End");
    }

    //draws hitbox
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint != null)
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }

}
