using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        GameObject.Find("SettingsMenu").SetActive(false);
    }

    public void Esc()
    {
        GameObject.Find("SettingsMenu").SetActive(false);
    }
}
