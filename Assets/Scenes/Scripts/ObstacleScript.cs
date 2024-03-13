using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRb;
    public float decForce;
    public GameObject textPrefab;
    Animator animator;
    Collider2D collider;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string[] phrases = new string[] { "Oof!", "Yikers!", ":(", "Not Good", };

        if (col.transform.name == "Character"/* && col.transform.name == "Board"*/)
        {
            //playerRb.AddForce(transform.right * -decForce * Time.deltaTime);
            playerRb.velocity = (transform.right * /*decForce * Time.deltaTime / */playerRb.velocity / decForce);
            Debug.Log("Esteeseen osu" + playerRb.velocity);
            collider.enabled = false;

            string randomPhrase = phrases[Random.Range(0, phrases.Length)];
            textPrefab.GetComponent<TextMeshPro>().text = randomPhrase;
            var Ftext=  Instantiate(textPrefab, new Vector3(player.transform.position.x + 10, player.transform.position.y+5, 0), Quaternion.identity);
            Ftext.GetComponent<TextMeshPro>().color = Color.red;

            animator.Play("Destruction_Obstacle_YellaRed_Anim");
            Debug.Log("tuhoutumis animaation");

            Destroy(gameObject, 0.2f);
        }
    }
}
