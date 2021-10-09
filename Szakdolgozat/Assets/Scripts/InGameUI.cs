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
    public TextMeshProUGUI tmp;
    
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
    }

    private void CloseActive()
    {
        List<GameObject> ls = new List<GameObject> { inv, sett };
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
    }

    public void CloseInventory()
    {
        
        inv.SetActive(false);

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
