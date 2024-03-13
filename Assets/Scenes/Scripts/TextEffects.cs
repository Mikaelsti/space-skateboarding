using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour
{
    public float DestroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
