using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    public JSONSaveLoad jsl;
    public Rigidbody2D rb;
    public float boostMultiplier;
    public static int boostNumberOwned;
    bool boostIsUsed;
    public int boostsLeftInt;
    public TMPro.TMP_Text boostsLeftTxt;
    public GameObject boostBtn;
    Vector2 absolute;

    //public static bool boosterOwned;

    private void Start()
    {
        boostIsUsed = false;
        jsl = new JSONSaveLoad();

        SaveBoosts b = jsl.LoadBoost(6);

        if (b.amount > 3)
        {
            boostsLeftInt = 3;
        }
        else
        {
            boostsLeftInt = b.amount;
        }

        boostsLeftTxt.text = boostsLeftInt.ToString();
    }
    public void Update()
    {

    }

    public void Boost()
    {
        //Lataa boostin ja miinustaa
        SaveBoosts b = jsl.LoadBoost(6);

        if (b.amount > 0 && boostsLeftInt > 0)
        {
            //Mathf.Abs() saa absoluuttisen arvon
            absolute = new Vector2(Mathf.Abs(rb.transform.right.x), Mathf.Abs(rb.transform.right.y));
            //rb.AddForce(rb.transform.right * absolute * boostMultiplier * 100);

            rb.AddForce(Mathf.Abs(rb.transform.right.x)* absolute * boostMultiplier * 100);
            rb.AddForce(Mathf.Abs(rb.transform.right.y) * absolute * boostMultiplier * 100);


            b.amount--;
            jsl.SaveBoost(b);
            boostsLeftInt--;
            boostsLeftTxt.text = boostsLeftInt.ToString();
            Debug.Log("Boost");
        }else if (b.amount <= 0)
        {
            Debug.Log("kaikki meni");
            boostIsUsed = true;
            UpgradeController.boosterOwned = false;
        }
        if (boostsLeftInt == 0)
        {
            Debug.Log("Boostit loppu");
            boostBtn.SetActive(false);
            //boostsLeftTxt.gameObject.SetActive(false);
        }

        /*
        if (CharacterController.isFlying && boostIsUsed == false && UpgradeController.boosterOwned)
        {
            rb.AddForce(rb.transform.right * boostMultiplier * 100);
            boostIsUsed = true;
            Debug.Log("Boost");
        }
        */
    }
}
