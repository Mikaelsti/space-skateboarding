using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopItemInfo : MonoBehaviour
{

    public int itemID;
    public TMPro.TMP_Text priceTxt;

    public static int isOwnedInt;
    public TMPro.TMP_Text quantityTxt;

    public static bool isOwnedBool;
    public GameObject shopManager;

    public JSONSaveLoad jsl;

    private void Start()
    {
        jsl = new JSONSaveLoad();
    }


    void Update()
    {
        //for (int i = 0;i < ShopManagerScript.shopItems.GetLength(1);i++)
        //{
        //    Debug.Log("voi perse");
        //    GameProgress p = jsl.LoadProgress(i);
        //    Debug.Log(("p.done = ") + p.done.ToString());
        //    if (p.done == true)
        //    {
        //        ShopManagerScript.shopItems[3, i] = 1;
        //        Debug.Log("Ui juma ku toimii");
                
        //    }
        //}
        priceTxt.text = "Price: " + ShopManagerScript.shopItems[2, itemID].ToString();
        //quantityTxt.text = isOwnedInt.ToString();
    }
}
