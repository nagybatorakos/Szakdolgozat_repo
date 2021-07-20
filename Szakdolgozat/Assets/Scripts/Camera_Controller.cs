using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private Transform playertf;
    [SerializeField] private Transform camtf;


    void Start()
    {
        //playertf = GameObject.Find("Player").GetComponent<Transform>();
        camtf = GetComponent<Transform>();
    }

    
    void Update()
    {
        camtf.position = new Vector3(playertf.position.x, camtf.position.y, camtf.position.z);
    }



}
