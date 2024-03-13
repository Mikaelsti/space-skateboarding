using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirObstacleController : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_OBSTACLE = 20f;

    public GameObject coinRow;
    public GameObject speedupObject;

    public GameObject[] obstacles;

    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;

    Vector2 playerPosition;
    Vector2 obstaclePosition;
    Vector2 imaginaryPosition;

    public float minVelocityDecreaseY = -9f;
    public float minVelocityIncreaseY = 6f;

    int rng;
    int rng2;

    private void Awake()
    {
        this.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DoCheck");
    }

    private void Update()
    {
        playerPosition = player.transform.position;
        //Debug.Log($"Meidän kuolmassa {angle} on ooppeli ojassa!");
    }

    IEnumerator DoCheck()
    {
        for (; ; )
        {
            if (ProximityCheck())
            {

            }
            else
            {
                rng = Random.Range(1, 4);
                Debug.Log("RNG: " + rng);
                if (rng == 1)
                {
                    rng2 = Random.Range(1, obstacles.Length);
                    SpawnObj(obstacles[rng2]);
                }
                else if(rng == 2)
                {
                    SpawnObj(coinRow);
                }
                else if(rng == 3)
                {
                    SpawnObj(speedupObject);
                }
            }
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }
    }

    // stage height + 10

    bool ProximityCheck()
    {
        if (Vector2.Distance(obstaclePosition, playerPosition) < PLAYER_DISTANCE_SPAWN_OBSTACLE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //-15 +6
    void SpawnObj(GameObject obj)
    {
        
        
        if (rb.velocity.y <= minVelocityDecreaseY)
        {
            imaginaryPosition = new Vector2((Random.Range(10f, 15f) + playerPosition.x), (Random.Range(-20f, -15f) + playerPosition.y));
            Debug.Log("spawnataan se alas koska pelaaja likkuu alas nopeasti");
        }
        else if (rb.velocity.y >= minVelocityIncreaseY)
        {
            imaginaryPosition = new Vector2((Random.Range(10f, 15f) + playerPosition.x), (Random.Range(20f, 15f) + playerPosition.y));
            Debug.Log("spawnataan se ylös koska pelaaja likkuu ylös nopeasti");
        }
        else
        {
            imaginaryPosition = new Vector2((Random.Range(35f, 37f) + playerPosition.x), (Random.Range(-5f, 4f) + playerPosition.y));
        }
        
        Debug.Log("Imaginary position Y: " + imaginaryPosition.y + ". Level determined position: " + GroundObstacleController.levelDeterminedPosition);

        //saattaa toimia
        //Vector2 tmpAngle = playerPosition + (Vector2.right * angle);
        //Vector2 applyAngle = Quaternion.Euler(imaginaryPosition.x, imaginaryPosition.y, 0) * tmpAngle;

        if (imaginaryPosition.y <= GroundObstacleController.levelDeterminedPosition + 10)
        {
            float tmpPos = GroundObstacleController.levelDeterminedPosition;
            float tmpRng = Random.Range(11, 15);
            imaginaryPosition.y = tmpPos + tmpRng;
            Debug.Log("Ilmaobjekti oli spawnata liian alas, joten se muutettiin: " + imaginaryPosition.y);

            obj = Instantiate(obj, new Vector2(imaginaryPosition.x, imaginaryPosition.y ), Quaternion.identity);
            //obj = Instantiate(obj, new Vector2(applyAngle.x, applyAngle.y), Quaternion.identity);
        }
        else
        {
            obj = Instantiate(obj, new Vector2(imaginaryPosition.x , imaginaryPosition.y ), Quaternion.identity);
            //obj = Instantiate(obj, new Vector2(applyAngle.x, applyAngle.y), Quaternion.identity);
        }
        obstaclePosition = obj.transform.position;
        obj.transform.parent = this.transform;
    }
}