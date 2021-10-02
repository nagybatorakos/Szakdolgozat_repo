using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private Transform playertf;
    [SerializeField] private Transform camtf;
    [SerializeField] private float speed;
    private float diff;
    

    void Start()
    {
        //playertf = GameObject.Find("Player").GetComponent<Transform>();
        camtf = GetComponent<Transform>();
        diff = math.abs(transform.position.y - playertf.position.y);
    }

    
    void Update()
    {
        //target = new Vector3();

        //camtf.position = new Vector3(playertf.position.x, camtf.position.y, camtf.position.z);
        float step = speed * Time.deltaTime; // calculate distance to move
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.Distance(transform.position, playertf.position), step);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playertf.position.x, playertf.position.y+diff, transform.position.z), step);
    }



}
