using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Camera_Controller : MonoBehaviour
{
    public AudioClip[] music = new AudioClip[4];
    public GameObject[] chars = new GameObject[3];
    [SerializeField] private Transform playertf;
    [SerializeField] private Transform camtf;
    [SerializeField] private float speed;
    private float diff;
    public CinemachineVirtualCamera vcam;
    public static int p;
    public GameObject player;
    public GameObject[] transfer = new GameObject[5];
    public AudioSource audio;
    public GameObject volume;


    void Start()
    {
     

        //vcam = gameObject.GetComponent<CinemachineVirtualCamera>();

        player = chars[p];
        //GameObject.Find(player).SetActive(true);
        if (p < 3)
        {
            player.SetActive(true);
            playertf = player.transform;
            p = 5;
        }
        //playertf = GameObject.Find("Player").GetComponent<Transform>();
        camtf = GetComponent<Transform>();
        diff = math.abs(transform.position.y - playertf.position.y);
        vcam.Follow = player.transform;
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("Village"))
        {
            audio.clip = music[0];
        }
        else if (SceneManager.GetActiveScene().name.StartsWith("Forest"))
        {
            audio.clip = music[2];
        }
        audio.volume = volume.GetComponent<Slider>().value;
        audio.Play();
        ////target = new Vector3();

        ////camtf.position = new Vector3(playertf.position.x, camtf.position.y, camtf.position.z);
        //float step = speed * Time.deltaTime; // calculate distance to move
        ////transform.position = Vector3.MoveTowards(transform.position, Vector3.Distance(transform.position, playertf.position), step);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(playertf.position.x, playertf.position.y + diff, transform.position.z), step);
    }



}
