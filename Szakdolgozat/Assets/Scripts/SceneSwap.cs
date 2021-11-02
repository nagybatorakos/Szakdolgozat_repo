using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSwap : MonoBehaviour
{
    Player_Controller player;

    public Animator transition;
    public GameObject gosign;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Main Camera").GetComponent<Camera_Controller>().player.GetComponent<Player_Controller>();
            
        }
    }


    public IEnumerator ChangeScene(Collider2D collision)
    {
        player.locker = true;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        Dictionary<string, Vector3> positions = new Dictionary<string, Vector3>();
        positions.Add("Village0", new Vector3(30f, -2f, player.tf.position.z));
        positions.Add("Village1", new Vector3(83f, -1.8f, player.tf.position.z));
        positions.Add("Cave", new Vector3(-132f, -0.8f, player.tf.position.z));
        positions.Add("ForestBridge1", new Vector3(-108f, -0.8f, player.tf.position.z));
        positions.Add("ForestBridge0", new Vector3(20f, -1.6f, player.tf.position.z));
        positions.Add("Dungeon", new Vector3(97f, -1.8f, player.tf.position.z));

        Scene active = SceneManager.GetActiveScene();
        Scene next = SceneManager.GetSceneByName(collision.gameObject.name);

        SceneManager.LoadScene(collision.gameObject.name, LoadSceneMode.Additive);

        foreach (GameObject go in player.cam.transfer)
        {
            //Debug.Log(next.name);
            SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(collision.gameObject.name));


        }

        yield return null;

        if (collision.gameObject.name == "ForestBridge")
        {
            if (collision.gameObject.transform.position.x < -125f)
            {
                player.tf.position = positions[collision.gameObject.name + "1"];
            }
            else
            {
                player.tf.position = positions[collision.gameObject.name + "0"];
            }
        }
        else if (collision.gameObject.name == "Village")
        {
            if (collision.gameObject.transform.position.x < 30f)
            {
                player.tf.position = positions[collision.gameObject.name + "0"];
            }
            else
            {
                player.tf.position = positions[collision.gameObject.name + "1"];
            }
        }
        else
        {
            player.tf.position = positions[collision.gameObject.name];
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(collision.gameObject.name));

        SceneManager.UnloadScene(active);
        yield return new WaitForSeconds(3f);
        transition.SetTrigger("End");
        player.locker = false;
    }


    public IEnumerator GameOver()
    {
        player.locker = true;
        gosign.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);

        Scene active = SceneManager.GetActiveScene();

        gosign.SetActive(false);
        transition.SetTrigger("End");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive); 
        
        yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("StartMenu"));

        SceneManager.UnloadScene(active);
        //yield return new WaitForSeconds(3f);
        player.locker = false;
    }

}
