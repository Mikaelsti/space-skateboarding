using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{

    public GameObject menuShopUI;
    public GameObject shopBoardUI;
    public GameObject shopGadgetUI;
    public GameObject shopSkinUI;
    public GameObject returnBtn;
    public GameObject returnToGame;

    public void MenuBoard()
    {
        menuShopUI.SetActive(false);
        shopBoardUI.SetActive(true);
        returnBtn.SetActive(true);
        returnToGame.SetActive(false);
    }

    public void MenuGadget()
    {
        menuShopUI.SetActive(false);
        shopGadgetUI.SetActive(true);
        returnBtn.SetActive(true);
        returnToGame.SetActive(false);
    }

    public void MenuSkin()
    {
        menuShopUI.SetActive(false);
        shopSkinUI.SetActive(true);
        returnBtn.SetActive(true);
        returnToGame.SetActive(false);
    }

    public void ReturnToShop()
    {
        menuShopUI.SetActive(true);
        shopBoardUI.SetActive(false);
        shopGadgetUI.SetActive(false);
        shopSkinUI.SetActive(false);
        returnBtn.SetActive(false);
        returnToGame.SetActive(true);
    }

    public void ReturnToGame()
    {
        SceneManager.LoadScene("AltoScene");
        Debug.Log("ReturnToGame");
    }
}
