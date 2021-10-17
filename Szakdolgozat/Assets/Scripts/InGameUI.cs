using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public Inventory invy;
    public GameObject inv;
    public GameObject sett;
    public GameObject shop;
    public TextMeshProUGUI tmp;
    public bool interact=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tmp.text != invy.coins.ToString())
        {
            tmp.text = invy.coins.ToString();
        }

        if (interact)
        {
            OpenInventory();
            inv.transform.Find("StatsMenu").gameObject.SetActive(false);
            shop.SetActive(true);

        }


    }

    private void CloseActive()
    {
        interact = false;

        List<GameObject> ls = new List<GameObject> { inv, sett, shop };
        foreach(GameObject l in ls)
        {
            if (l.activeSelf)
            {
                l.SetActive(false);
            }
        }
    }

    public void OpenInventory()
    {
        CloseActive();
        inv.SetActive(true);
        inv.transform.Find("StatsMenu").gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        CloseActive();
        //inv.SetActive(false);

        //gameObject.transform.Find("healthbar").Find("fill").GetComponent<RectTransform>();
    }

    public void OpenSettings()
    {
        CloseActive();
        sett.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        sett.SetActive(false);
    }

    public void Esc()
    {
        sett.SetActive(false);
    }

}
