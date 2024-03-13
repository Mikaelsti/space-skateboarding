using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private GameObject Player;
    private AudioSource audioSource;
    private SpriteRenderer sprite;
    private Collider2D coll;
    private void Start()
    {
        Player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            Debug.Log("pelaaja t�rm�si, nyt ��nt�");
            audioSource.Play();

            coll.enabled = false;
            sprite.enabled = false;
            
            Destroy(gameObject, audioSource.clip.length);
            Debug.Log("objecti tuhottu faktoilla ja logiikalla");
        }
    }

}
