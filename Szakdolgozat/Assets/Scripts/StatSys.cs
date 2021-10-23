using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatSys : MonoBehaviour
{
    //public GameObject[] stats = new GameObject[5];

    public Player_Controller player;
    public GameObject sign;
    public TextMeshProUGUI info;

    public int lvl = 1;
    public int xpborder = 50;
    public int xp = 0;
    //public int pointstodivide = 0;
    public TextMeshProUGUI[] pointstr = new TextMeshProUGUI[5];
    public int[] point = new int[5];

    public float attack_up = 5f;
    public float movesp_up=0.3f;
    public float jump_up=0.2f;
    public float nextattack_up=0.2f;
    public float special_up=5f;
    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        info.text = $"Level: {lvl}   XP: {xp}/{xpborder}";
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Main Camera").GetComponent<Camera_Controller>().player.GetComponent<Player_Controller>();
    }

    public void add1()
    {
        point[0] += 1;
        pointstr[0].text = point[0].ToString();
        setvisible();

        player.maxHealth += 20;
        player.currentHealth = player.maxHealth;
        player.healthBar.SetMaxHealth(player.maxHealth);
        points -= 1;
    }

    public void add2()
    {
        point[1] += 1;
        pointstr[1].text = point[1].ToString();
        setvisible();

        player.MovementSpeed += movesp_up;
        player.Jumpheight += jump_up;
        points -= 1;
    }
    public void add3()
    {
        point[2] += 1;
        pointstr[2].text = point[2].ToString();
        setvisible();

        player.AttackDamage += attack_up;
        points -= 1;
    }
    public void add4()
    {
        point[3] += 1;
        pointstr[3].text = point[3].ToString();
        setvisible();

        player.attackRate += nextattack_up;
        points -= 1;
    }
    public void add5()
    {
        point[4] += 1;
        pointstr[4].text = point[4].ToString();
        setvisible();
        player.specialdamage=special_up;
        points -= 1;
    }

    private void setvisible()
    {
        foreach(TextMeshProUGUI mesh in pointstr)
        {
            GameObject button = mesh.transform.GetChild(1).gameObject;
            if (button.activeSelf && points == 0)
            { button.SetActive(false); sign.SetActive(false); }
            else { button.SetActive(true); sign.SetActive(true); }
        }
    }
    public void lvlup()
    {
        if (xp>=xpborder)
        {
            lvl+=1;
            xp = xp - xpborder;
            xpborder = lvl * 100 / 2;
            points += 1;
            setvisible();
        }
        info.text = $"Level: {lvl}   XP: {xp}/{xpborder}";
    }

}
