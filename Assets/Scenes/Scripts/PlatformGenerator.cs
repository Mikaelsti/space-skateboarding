using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;
    public float spawnPointY;

    private float platformWidth;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, spawnPointY, transform.position.z);

            Instantiate(platform, transform.position, transform.rotation);
        }
    }
}
