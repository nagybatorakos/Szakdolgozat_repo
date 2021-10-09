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

    // Start is called before the first frame update
    void Start()
    {
        info.text = $"Level: {lvl}   XP: {xp}/{xpborder}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add1()
    {
        point[0] += 1;
        pointstr[0].text = point[0].ToString();
        setvisible();

        player.maxHealth += 20;
        player.currentHealth = player.maxHealth;
        player.healthBar.SetMaxHealth(player.maxHealth);
    }

    public void add2()
    {
        point[1] += 1;
        pointstr[1].text = point[1].ToString();
        setvisible();

        player.MovementSpeed += 0.3f;
        player.Jumpheight += 0.2f;
    }
    public void add3()
    {
        point[2] += 1;
        pointstr[2].text = point[2].ToString();
        setvisible();

        player.AttackDamage += 5f;
    }
    public void add4()
    {
        point[3] += 1;
        pointstr[3].text = point[3].ToString();
        setvisible();

        player.attackRate += 0.2f;
    }
    public void add5()
    {
        point[4] += 1;
        pointstr[4].text = point[4].ToString();
        setvisible();
    }

    private void setvisible()
    {
        foreach(TextMeshProUGUI mesh in pointstr)
        {
            GameObject button = mesh.transform.GetChild(1).gameObject;
            if(button.activeSelf)
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
            setvisible();
        }
        info.text = $"Level: {lvl}   XP: {xp}/{xpborder}";
    }

}
