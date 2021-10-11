using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera_Controller cam;
    public GameObject player;
    public Animator transition;
    public GameObject Speech;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera_Controller>();
        transition = GameObject.Find("Canvas").transform.Find("transition").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        player = cam.player;
        player.GetComponent<Player_Controller>().enabled = false;
    }


    public void btn()
    {
        Speech.SetActive(false);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);

        Scene active = SceneManager.GetActiveScene();
        //Scene next = SceneManager.GetSceneByName(collision.gameObject.name);

        SceneManager.LoadScene("Village", LoadSceneMode.Additive);

        foreach (GameObject go in cam.transfer)
        {
            
            SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName("Village"));


        }

        yield return null;


        player.transform.position = new Vector3(30f, -2f, player.transform.position.z);
         

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Village"));


        transition.SetTrigger("End");
        player.GetComponent<Player_Controller>().enabled = true;
        //yield return new WaitForSeconds(3f);
        SceneManager.UnloadScene(active);

    }

}
