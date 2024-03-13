using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackController : MonoBehaviour
{
    public Rigidbody2D rb;
    private float moveDirection;
    public static float moveSpeed = 5;
    public static float jetpackFuel;
    public float currentFuel;
    public float forceMultiplier;

    public Slider jetpackFuelSlider;

    //public static bool jetpackOwned;

    public bool jetpackPressed = false;

    private void Start()
    {
        currentFuel = jetpackFuel;
        SetMaxFuel(jetpackFuel);
    }

    void Update()
    {

        if (CharacterController.isFlying)
        {
            if (jetpackPressed && UpgradeController.jetpackOwned == true && currentFuel > 0)
            {
                
                JetPackUp();
            }
            else
            {
                forceMultiplier = 0;
            }
        }
    }

    void JetPackUp()
    {
        moveDirection = Input.GetAxis("Vertical");
        moveDirection = 1;
        forceMultiplier += Time.deltaTime * moveSpeed;

        rb.velocity = new Vector2(rb.velocity.x, moveDirection * forceMultiplier);

        currentFuel -= Time.deltaTime;
        SetFuel(currentFuel);
        Debug.Log("Force Multiplier " + forceMultiplier);
        Debug.Log("Current fuel" + currentFuel);
    }

    public void UseJetpack(bool canUseJetpack)
    {
        jetpackPressed = canUseJetpack;

    }

    public void SetFuel(float fuel)
    {
        jetpackFuelSlider.value = fuel;
    }

    public void SetMaxFuel(float fuel)
    {
        jetpackFuelSlider.maxValue = fuel;
        jetpackFuelSlider.value = fuel;
        Debug.Log("Jetpack fuel " + fuel);
    }
}
