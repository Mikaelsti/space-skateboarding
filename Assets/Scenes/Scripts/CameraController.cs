using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float Xoffset;
    public float Yoffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        transform.position = new Vector3(player.transform.position.x +Xoffset , player.transform.position.y +Yoffset, transform.position.z);
    }
}
