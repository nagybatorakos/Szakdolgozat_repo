using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public string name;
    public float HP = 100;
    public float MovementSpeed=2f;
    public float AttackSpeed=0.75f;
    public float Damage= 10f;

    //public List<Stats> stats = new List<Stats>();

    public Stats(string newname, float newhp, float newm, float newas, float newdmg)
    {
        this.name = newname;
        this.HP = newhp;
        this.MovementSpeed = newm;
        this.AttackSpeed = newas;
        this.Damage = newdmg;
    }

    

}


