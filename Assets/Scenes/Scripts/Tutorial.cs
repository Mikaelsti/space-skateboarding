using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Tutorial : MonoBehaviour
{

    public GameObject tutorial;
    public Transform obstacle;
    public GameObject hand;
    public GameObject obstacle2spawn;
    GameObject player;
    public Animator HandAnimator;
    public Animator RotButton;
    public Animator RotButton2;
    bool hasJumped = false;
    bool hasPlayedTutorial; //needs to be saved
    private GameObject tutorialObs;

    public JSONSaveLoad jsl;

    string path;
    public string savedProgressDirectory = "savedprogress";

    void Start()
    {

        jsl = new JSONSaveLoad();


        #if UNITY_ANDROID
        path = Application.persistentDataPath;
        #endif
        #if UNITY_EDITOR
                path = Application.dataPath;
        #endif
        player = GameObject.Find("Player");
        StartCoroutine("TeachAcc");



        if (File.Exists(path + "/" + savedProgressDirectory + "/" + "1" + "tutorialdata" + ".json"))
        {
            
        }else
        {
            //GameProgress p = new GameProgress(id, name, isBought);
            //jsl.SaveProgress(p);

            SaveTut(1, "tutorial", false);
        }


        SaveTutorial t = jsl.LoadTutorial(1);

        if(t.done == true)
        {
            tutorial.SetActive(false);
        }
       else
        {
            tutorialObs = Instantiate(obstacle2spawn, obstacle);
        }
    }

    private void Update()
    {
        float dist2obs =  Vector2.Distance(player.transform.position, tutorialObs.transform.position);
        if (dist2obs <= 15 && hasJumped == false)
        {
            Debug.Log("opeta hyppy");
            StartCoroutine("TeachJump");
        }
    }
    IEnumerator TeachAcc()
    {
        HandAnimator.Play("PointingHand2");
        yield return new WaitForSeconds(2);
    }

    IEnumerator TeachJump()
    {
        HandAnimator.Play("PointingHand1");
        Time.timeScale = 0.5f;
        hasJumped = true;
        yield return new WaitForSeconds(0.5f);
        hand.SetActive(false);
        StartCoroutine("OnJump");
    }

    IEnumerator OnJump()
    {
        RotButton2.Play("Blink");
        RotButton.Play("Blink");
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        hasPlayedTutorial = true;
        tutorial.SetActive(false);

        SaveTutorial t = new SaveTutorial(1, "tutorial", true);
        jsl.SaveTutorial(t);
    }

    public void SaveTut(int id, string name, bool done)
    {
        SaveTutorial t = new SaveTutorial(id, name, done);
        jsl.SaveTutorial(t);
    }
}