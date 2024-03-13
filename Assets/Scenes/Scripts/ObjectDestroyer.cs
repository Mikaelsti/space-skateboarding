using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Sphere end overlap
 */

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    float minDist = 50f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    void Update ()
    {
        if((player.transform.position.x - this.transform.position.x) > minDist)
        {

            Destroy(gameObject);
        }
    }
}

/*
    default value 100
    player movement +1 +1 +1 +1
    tile distance -1 -1 -1 -1
    tmp player pos - current player pos
        0               +1
        100             +1
 
    Mathf.Sqrt(x2 -x1)^2 + (y2-y1)^2
 
 
 
 
 
 */
