using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacleController : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_groundObstacle = 20f;
    public float determineTilePosition;
    [SerializeField] public static float levelDeterminedPosition;

    public GameObject groundObstacleObject;

    public GameObject[] groundObstacles;

    [SerializeField] private GameObject player;

    Vector2 playerPosition;
    Vector2 groundObstaclePosition;


    int rng;

    private void Awake()
    {
        this.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DoCheck");
        levelDeterminedPosition = determineTilePosition;
    }

    private void Update()
    {
        playerPosition = player.transform.position;
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
                rng = Random.Range(1, groundObstacles.Length);
                SpawnObj(groundObstacles[rng]);
            }
            yield return new WaitForSeconds(Random.Range(4, 6));
        }
    }

    bool ProximityCheck()
    {
        if (Vector2.Distance(groundObstaclePosition, playerPosition) < PLAYER_DISTANCE_SPAWN_groundObstacle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SpawnObj(GameObject obj)
    {
        obj = Instantiate(obj, new Vector2(Random.Range(30f, 32.0f) + playerPosition.x, levelDeterminedPosition + 1), Quaternion.identity);
        groundObstaclePosition = obj.transform.position;
        obj.transform.parent = this.transform;
    }
}
