using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float length, startposx, startposy;
    [SerializeField] GameObject cam;
    [SerializeField] float effect;
    public float yeffect;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startposx = transform.position.x+length/2;

        startposy = transform.position.y;
    }


    void Update()
    {
        float temp = (cam.transform.position.x * (1 - effect));
        float dist = (cam.transform.position.x * effect);

        float diff = cam.transform.position.y*yeffect;
        float b = (startposy + diff);

        transform.position = new Vector3(startposx + dist, startposy + diff, transform.position.z);

        if (temp > startposx + length) { startposx += length; }
        else if (temp < startposx - length) { startposx -= length; }
        //transform.position.y
    }
}

