using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    public GameObject runEndMenu;
    public GameObject pauseMenuUi;
    public GameObject gameplayUI;
    public GameObject rampControlButtons;
    public GameObject airControlButtons;
    public GameObject jetpackButton;
    public GameObject boostButton;

    public static bool gameStateFroze = false;
    public static bool testboolean = false;
    private bool loopEnder = true;

    // Start is called before the first frame update
    void Awake()
    {
        this.enabled = false;
        //button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x < 1 /*&& CharacterController.isFloored*/)
        {
            gameStateFroze = true;
            testboolean = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            //gamestatefroze = true> vaihe 1
            //Debug.Log("buttoncontroller gamestatefroze on" + gameStateFroze);
            CharacterController.isFlying = false;
            

            if (loopEnder)
            {
                Debug.Log("Gamestate froze: " + gameStateFroze);
                //Distancesaved = false> Vaihe 3
                GameManager.distanceSaved = false;
                loopEnder = false;
            }

            runEndMenu.SetActive(true);
            gameplayUI.SetActive(false);
            //Time.timeScale = 0;
        }

        if (gameStateFroze)
        {
            Debug.Log("buttoncontroller gamestatefroze on" + gameStateFroze);
        }
        else
        {
            //loopender = true> Vaihe 2
            loopEnder = true;
        }

        if (CharacterController.isFlying == true)
        {
            rampControlButtons.SetActive(false);


            if (UpgradeController.jetpackOwned)
            {
                jetpackButton.SetActive(true);
            }

            if (UpgradeController.boosterOwned)
            {
                boostButton.SetActive(true);
            }
        }
    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        gameplayUI.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        gameplayUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        //gameStateFroze = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Shop()
    {

        SceneManager.LoadScene("ShopScene");
    }
}
