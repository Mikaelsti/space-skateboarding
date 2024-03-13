using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptDoNotUse : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text posX;
    [SerializeField]
    private TMPro.TMP_Text posY;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posX.text = "X speed: " + (float)Mathf.Round(rb.velocity.x * 100f) / 100f;
        posY.text = "Y speed: " + (float)Mathf.Round(rb.velocity.y * 100f) / 100f;
    }
}
