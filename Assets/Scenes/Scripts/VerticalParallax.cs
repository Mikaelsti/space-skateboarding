using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalParallax : MonoBehaviour
{

    float defaultY = 0;
    [SerializeField]
    float offsetY = 0;
    [SerializeField]
    float maxY = 0;
    [SerializeField]
    float shiftMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        defaultY = transform.parent.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        float yDifference = (Camera.main.transform.position.y - defaultY) * shiftMultiplier;
        if (defaultY + offsetY + yDifference > defaultY + offsetY + maxY)
        {
            position.y = defaultY + offsetY + maxY;
        }
        else
        {
            position.y = defaultY + offsetY  + yDifference;
        }
        transform.position = position;

    }

    private void OnValidate()
    {
        Start();
        Update();
    }
}
