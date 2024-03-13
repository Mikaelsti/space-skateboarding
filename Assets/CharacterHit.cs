using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHit : MonoBehaviour
{
    public GameObject gameplayUI;
    public GameObject runEndMenu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ramp")
        {
            Debug.Log("Kuoli lol");
            Time.timeScale = 0;
            runEndMenu.SetActive(true);
            gameplayUI.SetActive(false);
        }
    }
}
