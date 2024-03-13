using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public JSONSaveLoad jsl;
    public static bool jetpackOwned;
    public static bool boosterOwned;
    public static int boardLevel;
    public int boardLevelInspc;

    public TMPro.TMP_Text boostersOwned;

    public Sprite ItemLockedImage;
    public Sprite ItemOwnedImage;

    public Image board1StateImage;
    public Image board2StateImage;
    public Image board3StateImage;
    public Image board4StateImage;
    public Image board5StateImage;

    public Image board1IsUsingImage;
    public Image board2IsUsingImage;
    public Image board3IsUsingImage;
    public Image board4IsUsingImage;
    public Image board5IsUsingImage;

    public static int jetpackLevel;
    public int jetpackLevelInspc;
    public Button jetpackBtn;
    public Sprite jetpackLvl1Ímage;
    public Sprite jetpackLvl2Image;
    public Sprite jetpackLvl3Image;
    public Sprite jetpackLvl4Image;


    public Image Jetpack1StateImage;
    public Image Jetpack2StateImage;
    public Image Jetpack3StateImage;
    public Image Jetpack4StateImage;

    bool inShop;

    private void Start()
    {
        jsl = new JSONSaveLoad();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "ShopScene")
        {
            inShop = true;
            CheckBoardOwned();
        }
    }

    private void Update()
    {
        boardLevelInspc = boardLevel;
        jetpackLevelInspc = jetpackLevel;
        CheckJetpackLevel();

        CheckBoardLevel();
        CheckBoardOwned();
        CheckBoostersOwned();
    }
    void CheckBoostersOwned()
    {
        SaveBoosts b = jsl.LoadBoost(6);
        Debug.Log("UpgradeController boost");
        boostersOwned.text = b.amount.ToString();
    }

    /*
     
     void CheckBoardOwned(){
    
    switch (ShopManagerScript.shopItemID[]){
    case0:
    board1StateImage.enabled = false;
    }

    1 1
    2 1
    3 0
    4 0
    5 0

    case 5 == 1:
    }



    if(ShopManagerScript.itemID[1, i] == 1){

        if(i == 1){
            board1StateImage.enabled = false;
        }
        if(i == 2){
            
        }

    }




     */

    void CheckBoardOwned()
    {
        if (ShopManagerScript.shopItems[3, 1] == 1)
        {
            board1StateImage.enabled = true;
            board1StateImage.sprite = ItemOwnedImage;
            board2StateImage.enabled = false;
            board3StateImage.sprite = ItemLockedImage;
            board4StateImage.sprite = ItemLockedImage;
            board5StateImage.sprite = ItemLockedImage;
        }
        if (ShopManagerScript.shopItems[3, 2] == 1)
        {
            board1StateImage.sprite = ItemOwnedImage;
            board2StateImage.enabled = true;
            board2StateImage.sprite = ItemOwnedImage;
            board3StateImage.enabled = false;
            board4StateImage.sprite = ItemLockedImage;
            board5StateImage.sprite = ItemLockedImage;
        }
        if (ShopManagerScript.shopItems[3, 3] == 1)
        {
            board1StateImage.sprite = ItemOwnedImage;
            board2StateImage.sprite = ItemOwnedImage;
            board3StateImage.enabled = true;
            board3StateImage.sprite = ItemOwnedImage;
            board4StateImage.enabled = false;
            board5StateImage.sprite = ItemLockedImage;
        }
        if (ShopManagerScript.shopItems[3, 4] == 1)
        {
            board1StateImage.sprite = ItemOwnedImage;
            board2StateImage.sprite = ItemOwnedImage;
            board3StateImage.sprite = ItemOwnedImage;
            board4StateImage.enabled = true;
            board4StateImage.sprite = ItemOwnedImage;
            board5StateImage.enabled = false;
        }
        if (ShopManagerScript.shopItems[3, 5] == 1)
        {
            board1StateImage.sprite = ItemOwnedImage;
            board2StateImage.sprite = ItemOwnedImage;
            board3StateImage.sprite = ItemOwnedImage;
            board4StateImage.sprite = ItemOwnedImage;
            board5StateImage.enabled = true;
            board5StateImage.sprite = ItemOwnedImage;
        }
    }

    void CheckBoardLevel()
    {
        switch (boardLevel)
        {
            case 0:
                CharacterController.accSpeed = 200;
                CharacterController.maxSpeed = 30;
                board1StateImage.sprite = ItemLockedImage;
                board2StateImage.sprite = ItemLockedImage;
                board3StateImage.sprite = ItemLockedImage;
                board4StateImage.sprite = ItemLockedImage;
                board5StateImage.sprite = ItemLockedImage;
                board1IsUsingImage.enabled = false;
                board2IsUsingImage.enabled = false;
                board3IsUsingImage.enabled = false;
                board4IsUsingImage.enabled = false;
                board5IsUsingImage.enabled = false;
                break;
            case 1:
                CharacterController.accSpeed = 220;
                CharacterController.maxSpeed = 60;
                board1IsUsingImage.enabled = true;
                board2IsUsingImage.enabled = false;
                board3IsUsingImage.enabled = false;
                board4IsUsingImage.enabled = false;
                board5IsUsingImage.enabled = false;
                //board1StateImage.enabled = true;
                //board1StateImage.sprite = ItemOwnedImage;
                //board2StateImage.enabled = false;
                //board3StateImage.sprite = ItemLockedImage;
                //board4StateImage.sprite = ItemLockedImage;
                //board5StateImage.sprite = ItemLockedImage;
                break;
            case 2:
                CharacterController.accSpeed = 240;
                CharacterController.maxSpeed = 80;
                board1IsUsingImage.enabled = false;
                board2IsUsingImage.enabled = true;
                board3IsUsingImage.enabled = false;
                board4IsUsingImage.enabled = false;
                board5IsUsingImage.enabled = false;
                //board1StateImage.sprite = ItemOwnedImage;
                //board2StateImage.enabled = true;
                //board2StateImage.sprite = ItemOwnedImage;
                //board3StateImage.enabled = false;
                //board4StateImage.sprite = ItemLockedImage;
                //board5StateImage.sprite = ItemLockedImage;
                break;
            case 3:
                CharacterController.accSpeed = 400;
                CharacterController.maxSpeed = 90;
                board1IsUsingImage.enabled = false;
                board2IsUsingImage.enabled = false;
                board3IsUsingImage.enabled = true;
                board4IsUsingImage.enabled = false;
                board5IsUsingImage.enabled = false;
                //board1StateImage.sprite = ItemOwnedImage;
                //board2StateImage.sprite = ItemOwnedImage;
                //board3StateImage.enabled = true;
                //board3StateImage.sprite = ItemOwnedImage;
                //board4StateImage.enabled = false;
                //board5StateImage.sprite = ItemLockedImage;
                break;
            case 4:
                CharacterController.accSpeed = 500;
                CharacterController.maxSpeed = 120;
                board1IsUsingImage.enabled = false;
                board2IsUsingImage.enabled = false;
                board3IsUsingImage.enabled = false;
                board4IsUsingImage.enabled = true;
                board5IsUsingImage.enabled = false;
                //board1StateImage.sprite = ItemOwnedImage;
                //board2StateImage.sprite = ItemOwnedImage;
                //board3StateImage.sprite = ItemOwnedImage;
                //board4StateImage.enabled = true;
                //board5StateImage.sprite = ItemOwnedImage;
                //board5StateImage.enabled = false;
                break;
            case 5:
                CharacterController.accSpeed = 50000;
                CharacterController.maxSpeed = 400;
                board1IsUsingImage.enabled = false;
                board2IsUsingImage.enabled = false;
                board3IsUsingImage.enabled = false;
                board4IsUsingImage.enabled = false;
                board5IsUsingImage.enabled = true;
                //board1StateImage.sprite = ItemOwnedImage;
                //board2StateImage.sprite = ItemOwnedImage;
                //board3StateImage.sprite = ItemOwnedImage;
                //board4StateImage.sprite = ItemOwnedImage;
                //board5StateImage.enabled = true;
                //board5StateImage.sprite = ItemOwnedImage;
                break;
        }
    }
    void CheckJetpackLevel()
    {
        if (jetpackLevel >= 1)
        {
            jetpackOwned = true;
        }

        switch (jetpackLevel)
        {
            case 0:
                if (inShop)
                {
                    Jetpack1StateImage.enabled = false;
                    Jetpack2StateImage.sprite = ItemLockedImage;
                    Jetpack3StateImage.sprite = ItemLockedImage;
                    Jetpack4StateImage.sprite = ItemLockedImage;
                }

                break;
            case 1:
                JetpackController.moveSpeed = 3;
                JetpackController.jetpackFuel = 1;
                if (inShop == false)
                {
                    jetpackBtn.GetComponent<Image>().sprite = jetpackLvl1Ímage;
                }
                if (inShop)
                {
                    Jetpack1StateImage.enabled = true;
                    Jetpack1StateImage.sprite = ItemOwnedImage;
                    Jetpack2StateImage.enabled = false;
                    Jetpack3StateImage.sprite = ItemLockedImage;
                    Jetpack4StateImage.sprite = ItemLockedImage;
                }
                break;
            case 2:
                JetpackController.moveSpeed = 5;
                JetpackController.jetpackFuel = 2;
                if (inShop == false)
                {
                    jetpackBtn.GetComponent<Image>().sprite = jetpackLvl2Image;
                }
                if (inShop)
                {
                    Jetpack1StateImage.sprite = ItemOwnedImage;
                    Jetpack2StateImage.enabled = true;
                    Jetpack2StateImage.sprite = ItemOwnedImage;
                    Jetpack3StateImage.enabled = false;
                    Jetpack4StateImage.sprite = ItemLockedImage;
                }


                break;
            case 3:
                JetpackController.moveSpeed = 7;
                JetpackController.jetpackFuel = 3;
                if (inShop == false)
                {
                    jetpackBtn.GetComponent<Image>().sprite = jetpackLvl3Image;
                }
                if (inShop)
                {
                    Jetpack1StateImage.sprite = ItemOwnedImage;
                    Jetpack2StateImage.sprite = ItemOwnedImage;
                    Jetpack3StateImage.enabled = true;
                    Jetpack3StateImage.sprite = ItemOwnedImage;
                    Jetpack4StateImage.enabled = false; 
                }

                

                break;
            case 4:
                JetpackController.moveSpeed = 9;
                JetpackController.jetpackFuel = 4;
                if (inShop == false)
                {
                    jetpackBtn.GetComponent<Image>().sprite = jetpackLvl4Image;
                }
                if (inShop)
                {
                    Jetpack4StateImage.enabled = true;
                    Jetpack1StateImage.sprite = ItemOwnedImage;
                    Jetpack2StateImage.sprite = ItemOwnedImage;
                    Jetpack3StateImage.sprite = ItemOwnedImage;
                    Jetpack4StateImage.sprite = ItemOwnedImage;
                }
                break;
        }
    }
}
