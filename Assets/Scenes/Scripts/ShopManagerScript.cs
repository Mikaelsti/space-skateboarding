using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public static int[,] shopItems = new int[6, 16];
    public TMPro.TMP_Text coinsTxt;
    public static int score;
    public JSONSaveLoad jsl;
    public string path;
    public static string savedProgressDirectory = "savedprogress";

    public GameObject[] blockers;

    private void Awake()
    {
        jsl = new JSONSaveLoad();
    }

    void Start()
    {
        jsl = new JSONSaveLoad();
        //coinsTxt.text = "Coins: " + coins.ToString();

        #if UNITY_ANDROID
                path = Application.persistentDataPath;
        #endif
        #if UNITY_EDITOR
                path = Application.dataPath;
        #endif

        LoadP();

        //Item's ID

        //Boards
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        //Gadgets
        shopItems[1, 6] = 6;
        shopItems[1, 7] = 7;
        shopItems[1, 8] = 8;
        shopItems[1, 9] = 9;
        shopItems[1, 10] = 10;
        //Skins
        shopItems[1, 11] = 11;
        shopItems[1, 12] = 12;
        shopItems[1, 13] = 13;
        shopItems[1, 14] = 14;
        shopItems[1, 15] = 15;


        //Item's Price

        //Boards
        shopItems[2, 1] = 100;
        shopItems[2, 2] = 500;
        shopItems[2, 3] = 1000;
        shopItems[2, 4] = 1500;
        shopItems[2, 5] = 2000;
        //Gadgets
        shopItems[2, 6] = 100; //booster
        shopItems[2, 7] = 500; //jetpack 1
        shopItems[2, 8] = 1000; //jetpack 2
        shopItems[2, 9] = 5000; //jetpack 3
        shopItems[2, 10] = 100000; //jetpack 4
        //Skins
        shopItems[2, 11] = 0;
        shopItems[2, 12] = 0;
        shopItems[2, 13] = 0;
        shopItems[2, 14] = 0;
        shopItems[2, 15] = 0;

        //Item Owned

        //Boards
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
        //Gadgets
        shopItems[3, 6] = 0;
        shopItems[3, 7] = 0;
        shopItems[3, 8] = 0;
        shopItems[3, 9] = 0;
        shopItems[3, 10] = 0;
        //Skins
        shopItems[3, 11] = 0;
        shopItems[3, 12] = 0;
        shopItems[3, 13] = 0;
        shopItems[3, 14] = 0;
        shopItems[3, 15] = 0;

        //item level

        //Boards
        shopItems[4, 1] = 1;
        shopItems[4, 2] = 2;
        shopItems[4, 3] = 3;
        shopItems[4, 4] = 4;
        shopItems[4, 5] = 5;
        //Gadgets
        shopItems[4, 6] = 0;
        shopItems[4, 7] = 1;
        shopItems[4, 8] = 2;
        shopItems[4, 9] = 3;
        shopItems[4, 10] = 4;
        //Skins
        shopItems[4, 11] = 1;
        shopItems[4, 12] = 2;
        shopItems[4, 13] = 3;
        shopItems[4, 14] = 4;
        shopItems[4, 15] = 5;
    }
    private void Update()
    {
        LoadJ();
        ShopLevelChecker();
    }
    public void LoadJ()
    {
        GameSaves g = jsl.LoadJson();
        coinsTxt.text = "Coins: " + g.coins;
    }

    public void SaveP(int id, string name, bool isBought)
    {
        GameProgress p = new GameProgress(id, name, isBought);
        jsl.SaveProgress(p);
    }


    public void LoadP()
    {
        /*
         Pitäisi saada for loopattua kaikki filut läpi ja kattoa missä boolean on true ja sitten vaihtaa sen paikka ykköseksi tuolta tiedostosta
        */

        for (int i = 1; i < ShopManagerScript.shopItems.GetLength(1); i++)
        {
            if (i == 0)
            {
                continue;
            }
            if (i == 6 || i == 0)
            {
                continue;
            }
            GameProgress p = jsl.LoadProgress(i);

            Debug.Log($"{i} kohta on: {p.name}, {p.id}, {p.done}");

            //jos p.id mätsää i ja sen p.done on true
            if (p.id == i && p.done == true)
            {
                if (p.id >= 7 && p.id < 11 && p.done == true)
                {
                    UpgradeController.jetpackOwned = true;

                    // 7 = 1, 8 = 2, 9 = 3, 10 = 4 -6
                    UpgradeController.jetpackLevel = i - 6;
                    JetpackController.jetpackFuel = i - 6;

                }

                if (p.id >= 2 && p.id < 6 && p.done == true)
                {
                    UpgradeController.boardLevel = i;

                }

                if (p.id >= 11 && p.id < 16 && p.done == true)
                {
                    //UpgradeController.skinLevel = i;

                }
                ShopManagerScript.shopItems[3, i] = 1;
                Debug.Log($"Itemin id{i} > " + ShopManagerScript.shopItems[3, i] + ":id oli ostettu joten se muutettiin omistetuksi");
            }
        }
    }


    public void SaveB(int id, string name, int level, int amount)
    {
        SaveBoosts b = new SaveBoosts(id, name, level, amount);
        jsl.SaveBoost(b);
    }
    public void LoadB(int id)
    {
        SaveBoosts b = jsl.LoadBoost(id);
        Debug.Log(b.name + "_I: " + b.id + "_L: " + b.level + "_A: " + b.amount);
    }

    public void Buy()
    {
        LoadP();
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        GameSaves g = new GameSaves(ga.distance, coins);

        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        

        if (ga.coins >= shopItems[2, 6] && shopItems[1, ButtonRef.GetComponent<ShopItemInfo>().itemID] == 6)
        {
            Debug.Log("asd");
            g.coins = ga.coins -= shopItems[2, ButtonRef.GetComponent<ShopItemInfo>().itemID];
            g.coins = ga.coins;

            SaveBoosts b = jsl.LoadBoost(6);
            b.amount++;
            jsl.SaveBoost(b);
            jsl.SaveJson(g);

            LoadB(6);

            UpgradeController.boosterOwned = true;
            Debug.Log("Booster ostettud");
        }

        /*if (shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID] == 1 && shopItems[1, ButtonRef.GetComponent<ShopItemInfo>().itemID] != 6)
        {
            if (ButtonRef.GetComponent<ShopItemInfo>().itemID > 1 && ButtonRef.GetComponent<ShopItemInfo>().itemID < 6)
            {
                UpgradeController.boardLevel = shopItems[4, ButtonRef.GetComponent<ShopItemInfo>().itemID];
                Debug.Log($"Board level on: {ButtonRef.GetComponent<ShopItemInfo>().itemID}");
            }
        } else */if (ga.coins >= shopItems[2, ButtonRef.GetComponent<ShopItemInfo>().itemID] && 1 > shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID] && shopItems[1, ButtonRef.GetComponent<ShopItemInfo>().itemID] != 6 && shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID] != 1)
        {

            g.coins = ga.coins -= shopItems[2, ButtonRef.GetComponent<ShopItemInfo>().itemID];
            g.coins = ga.coins;
            shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID] = 1;
            
            //ID, Nimi, Level, boolean
            SaveP(shopItems[1, ButtonRef.GetComponent<ShopItemInfo>().itemID],"save" , true);
            LoadP();
            //ButtonRef.GetComponent<ShopItemInfo>().isOwnedTxt = shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID];

            //näytää onko jo ostettu
            //TODO EI TALLENNU
            TextMeshProUGUI owned = ButtonRef.GetComponentInChildren<TextMeshProUGUI>();
            //owned.text = "Owned";

            jsl.SaveJson(g);
        }
        else
        {
            Debug.Log("et voi ostaa");
            Debug.Log($"{shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID]} {ga.coins} {shopItems[2, ButtonRef.GetComponent<ShopItemInfo>().itemID]}");
        }
        Debug.Log(shopItems[1, ButtonRef.GetComponent<ShopItemInfo>().itemID] + " " + shopItems[2, ButtonRef.GetComponent<ShopItemInfo>().itemID] + " " + shopItems[3, ButtonRef.GetComponent<ShopItemInfo>().itemID]);
    }

    public void BuyJetpackLevel1()
    {
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        //GameSaves ga = jsl.LoadJson();
        //GameProgress p = jsl.LoadProgress(2, "Jetpack");
        if (UpgradeController.jetpackOwned == false && ga.coins >= shopItems[2, 7])
        {            
            //UpgradeController.jetpackOwned = true;
            UpgradeController.jetpackLevel = 1;
            Debug.Log("Jetp�kki" + UpgradeController.jetpackLevel);
        }
    }
    public void BuyJetpackLevel2()
    {
        // GameProgress p = jsl.LoadProgress(6);
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.jetpackLevel == 1 && ga.coins >= shopItems[2, 8] /*p.done == true*/)
        {
            //ShopLevelChecker(1, 2 , "Jetpack");
            //SaveP(2, "jetpack", 2, true, "Jetpack");
            UpgradeController.jetpackLevel = 2;
            Debug.Log("Jetp�kki" + UpgradeController.jetpackLevel);
        }
    }
    public void BuyJetpackLevel3()
    {
        //GameProgress p = jsl.LoadProgress(8);
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.jetpackLevel == 2 && ga.coins >= shopItems[2, 9] /*p.done == true*/)
        {
            //ShopLevelChecker(2, 3, "Jetpack");
            //SaveP(3, "jetpack", 3, true, "Jetpack");
            UpgradeController.jetpackLevel = 3;
            Debug.Log("Jetp�kki" + UpgradeController.jetpackLevel);
        }
    }
    public void BuyJetpackLevel4()
    {
        //GameProgress p = jsl.LoadProgress(9);
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.jetpackLevel == 3 && ga.coins >= shopItems[2, 10] /*p.done == true*/)
        {
           // ShopLevelChecker(3, 4, "Jetpack");
            //SaveP(4, "jetpack", 4, true, "Jetpack");
            UpgradeController.jetpackLevel = 4;
            Debug.Log("Jetp�kki" + UpgradeController.jetpackLevel);
        }
    }

    public void BuyBooster()
    {
        //Boostin ID = 6
        
        
        /*
        if (UpgradeController.boosterOwned == false)
        {
            UpgradeController.boosterOwned = true;
            Debug.Log("Booster ostettud");
        }*/
    }
    public void BuyBoardLevel2()
    {
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.boardLevel == 0 && ga.coins >= shopItems[2, 1])
        {
            UpgradeController.boardLevel = 1;

            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        } else if (/*UpgradeController.boardLevel == 0 && ga.coins >= shopItems[2, 1] ||*/ shopItems[3, 1] == 1)
        {
           // ShopLevelChecker(1, 2, "Board");
           // SaveP(1, "board", 2, true, "Board");
            UpgradeController.boardLevel = 1;
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else
        {
            Debug.Log("Board tier too low");
        }
    }
    public void BuyBoardLevel3()
    {
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.boardLevel == 1 && ga.coins >= shopItems[2, 1])
        {
            UpgradeController.boardLevel = 2;
            
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else if (/*UpgradeController.boardLevel == 1 && ga.coins >= shopItems[2, 2] ||*/ shopItems[3, 2] == 1)
        {
           // ShopLevelChecker(2, 3, "Board");
           // SaveP(2, "board", 3, true, "Board");
            UpgradeController.boardLevel = 2;
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else
        {
            Debug.Log("Board tier too low");
        }
    }
    public void BuyBoardLevel4()
    {
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.boardLevel == 2 && ga.coins >= shopItems[2, 1])
        {
            UpgradeController.boardLevel = 3;

            Debug.Log("1 Board lvl = " + UpgradeController.boardLevel);
        }
        else if (/*UpgradeController.boardLevel == 2 && ga.coins >= shopItems[2, 3] ||*/ shopItems[3, 3] == 1)
        {
            //ShopLevelChecker(3, 4, "Board");
            //SaveP(3, "board", 4, true, "Board");
            UpgradeController.boardLevel = 3;
            Debug.Log("2 Board lvl = " + UpgradeController.boardLevel);
        }
        else
        {
            Debug.Log("Board tier too low");
        }
    }
    public void BuyBoardLevel5()
    {
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.boardLevel == 3 && ga.coins >= shopItems[2, 1])
        {
            UpgradeController.boardLevel = 4;
      
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else if (/*UpgradeController.boardLevel == 3 && ga.coins >= shopItems[2, 4] || */ shopItems[3, 4] == 1)
        {
            //ShopLevelChecker(3, 4, "Board");
            //SaveP(3, "board", 4, true, "Board");
            UpgradeController.boardLevel = 4;
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else
        {
            Debug.Log("Board tier too low");
        }
    }
    public void BuyBoardLevel6()
    {
        int coins = score;
        GameSaves ga = jsl.LoadJson();
        if (UpgradeController.boardLevel == 4 && ga.coins >= shopItems[2, 1])
        {
            UpgradeController.boardLevel = 5;
 
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else if (/*UpgradeController.boardLevel == 4 && ga.coins >= shopItems[2, 5] || */ shopItems[3, 5] == 1)
        {
            //ShopLevelChecker(3, 4, "Board");
            //SaveP(3, "board", 4, true, "Board");
            UpgradeController.boardLevel = 5;
            Debug.Log("Board lvl = " + UpgradeController.boardLevel);
        }
        else
        {
            Debug.Log("Board tier too low");
        }
    }


    public void ShopLevelChecker()
    {
        /*
        for (int i = 0; i<shopItems.GetLength(1); i++)
        {
            if (shopItems[3, i] == 1) {
                Debug.Log($"itemi {i} on ostettu ja aktiivinen");
                blockers = GameObject.FindGameObjectsWithTag($"shop_item_id_{i}");
                Debug.Log($"Peliobjekti shop_item_id_{i} on l�ydetty");
            }
            for (int j = 0; j < blockers.Length; j++)
            {
                Debug.Log($"asd {blockers.Length}");
                blockers[j].SetActive(true);
            }
        }
        */
    }
}
