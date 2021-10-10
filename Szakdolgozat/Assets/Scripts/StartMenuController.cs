using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject[] infopanels = new GameObject[3];
    public GameObject choosewindow;
    private int selected;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        choosewindow.SetActive(true);
        //SceneManager.LoadScene("ForestSpawn");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void warriorbtn()
    {
        infopanels[0].SetActive(true);
        infopanels[1].SetActive(false);
        infopanels[2].SetActive(false);
        selected = 0;

    }

    public void hunterbtn()
    {
        infopanels[0].SetActive(false);
        infopanels[1].SetActive(true);
        infopanels[2].SetActive(false);
        selected = 1;
    }

    public void magebtn()
    {
        infopanels[0].SetActive(false);
        infopanels[1].SetActive(false);
        infopanels[2].SetActive(true);
        selected = 2;
    }
    public void selectbtn() 
    {
        Camera_Controller.player = selected;
        SceneManager.LoadScene("ForestBridge");
    }

}
