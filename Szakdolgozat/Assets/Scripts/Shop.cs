using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclick(GameObject go)
    {
        int value = Int32.Parse(go.transform.Find("value").GetComponent<TextMeshProUGUI>().text);
        if(inv.coins>= value)
        {
            inv.AddtoInv(go);
            inv.coins -= value;
        }
    }
}
