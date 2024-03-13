using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetpackOnChara : MonoBehaviour
{
    public Transform body;
    public Transform chara;
    private Vector3 jetpackPos;
    private Vector3 rot;
    private SpriteRenderer spriteRenderer;
    public Sprite jetpack1Sprite;
    public Sprite jetpack2Sprite;
    public Sprite jetpack3Sprite;
    public Sprite jetpack4Sprite;
    public CharacterController controller;


    private void Start()
    {
        jetpackPos.x = gameObject.transform.position.x;
        jetpackPos.y = gameObject.transform.position.y;

        rot.z = 20;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (controller.isRotatingLeft == true)
        {
            jetpackPos.x = body.position.x;
            jetpackPos.y = body.position.y;
            //rot.z = chara.rotation.z;

            //gameObject.transform.position = new Vector3(jetpackPos.x, jetpackPos.y, 0);
           // gameObject.transform.rotation = Quaternion.AngleAxis(rot.z, rot);
        }
        else
        {
            jetpackPos.x = body.position.x-0.5f;
            jetpackPos.y = body.position.y;

          //  gameObject.transform.position = new Vector3(jetpackPos.x, jetpackPos.y, 0);
           // gameObject.transform.rotation = Quaternion.Euler(0, 0, rot.z);
        }

      CheckJetpackSprite();
    }

    void CheckJetpackSprite()
    {
        switch (UpgradeController.jetpackLevel)
        {
            case 1:
                spriteRenderer.sprite = jetpack1Sprite;
                break;
            case 2:
                spriteRenderer.sprite = jetpack2Sprite;
                break;
            case 3:
                spriteRenderer.sprite = jetpack3Sprite;
                break;
            case 4:
                spriteRenderer.sprite = jetpack4Sprite;
                break;
        }
    }
}
