using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float Xlength, Xstartpos;
    private float Ylength, Ystartpos;

    public GameObject cam;
    [SerializeField] private float parallaxEff;
    public bool verticality; 

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        Ystartpos = transform.position.y;
        Ylength = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;

        Xstartpos = transform.position.x;
        Xlength = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //vertical scrolling
        if (verticality)
        {

         float Ytemp = (cam.transform.position.y * (1 - parallaxEff));
         float Ydist = (cam.transform.position.y * parallaxEff);

         transform.position = new Vector3(transform.position.x, Ystartpos + Ydist, transform.position.z);

         if (Ytemp > Ystartpos + Ylength) { (Ystartpos ) += Ylength; }
         else if (Ytemp < Ystartpos - Ylength) { Ystartpos -= Ylength; }
        }

        //horizontal scrolling
        float temp = (cam.transform.position.x * (1 - parallaxEff));
        float dist = (cam.transform.position.x * parallaxEff);

        transform.position = new Vector3(Xstartpos + dist, transform.position.y, transform.position.z);

        if (temp > Xstartpos + Xlength) { Xstartpos += Xlength; }
        else if (temp < Xstartpos - Xlength) { Xstartpos -= (Xlength); }
    }
}
