using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Stat variables
    public int xp = 10;
    [SerializeField] public float lvl = 1;
    [SerializeField] public float maxHealth;
    [SerializeField] private protected float currentHealth;
    [SerializeField] private protected string Name;
    [SerializeField] private protected float dmg;
    [SerializeField] private protected float ms;


    //Attack variables
    [SerializeField] public float asp = 0.5f;
    [SerializeField] private protected float detectionRange = 2.5f;
    [SerializeField] private protected float attackrange = 0.2f;
    public bool detected = false;
    public float nextattack = 0f;


    //Components
    [SerializeField] private protected Collider2D jumppoint;
    private protected Rigidbody2D rb;
    private protected Collider2D coll;
    [SerializeField] private protected GameObject player;
    [SerializeField] private protected Transform attackpoint;

    private protected RectTransform hpbar;


    //Layers
    [SerializeField] private protected LayerMask EnemyLayers;
    [SerializeField] private protected LayerMask ground;

    public Animator_Enemy anim;
    private protected bool attackended;

    public bool isDead = false;

    public enum Stance { move, attack }
    public Stance stance = Stance.move;

    public List<GameObject> items = new List<GameObject>();

    public StatSys statsys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void Search()
    //{
    //    Stats[] elemek = GameObject.Find("GameObject").GetComponent<Stat_DB>().stats;


    //    //Component elem = GameObject.Find("GameObject").GetComponent<Stat_DB>();

    //    foreach (Stats n in elemek)
    //    {
    //        if (n.name == Name)
    //        {
    //            maxHealth = n.HP * lvl;
    //            dmg = n.Damage * lvl;
    //            asp = n.AttackSpeed * lvl;
    //            ms = n.MovementSpeed * lvl;

    //            break;
    //        }
    //    }

    //}

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        float k = 3.1f * (damage / maxHealth);


        if (hpbar.localScale.x < k)
        {
            k = hpbar.localScale.x;
        }
        hpbar.localScale = new Vector3(hpbar.localScale.x - k, hpbar.localScale.y, hpbar.localScale.z);
        //hurt animation

        if (currentHealth <= 0)
        {
            isDead = true;
            anim.Die();
        }
    }

    private protected void DetectPlayer()
    {
        Collider2D[] Denem = Physics2D.OverlapCircleAll(transform.position, detectionRange, EnemyLayers);

        if (Denem.Length > 0)
        {
            detected = true;
        }
    }

    public void DropItem()
    {
        statsys.xp += xp;
        statsys.lvlup();
        //foreach(GameObject item in items)
        //{
        //    Instantiate(item, transform.position, item.transform.rotation);
            
        //    //player.GetComponent<Player_Controller>
        //}
    }
}
