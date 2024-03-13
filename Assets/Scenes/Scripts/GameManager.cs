using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject buttonController;
    public GameObject coinController;
    public GameObject airObstacleController;
    public GameObject flightSpeedupController;
    public GameObject groundObstacleController;
    public GameObject tile;
    public GameObject ads1;
    public GameObject ads2;
    public GameObject adsButton;
    public GameObject runEndMenu;

    [SerializeField]
    private TMPro.TMP_Text currentDistanceText;
    [SerializeField]
    private TMPro.TMP_Text endDistance;
    [SerializeField]
    private TMPro.TMP_Text longestDistance;
    [SerializeField]
    private TMPro.TMP_Text totalCoins;
    [SerializeField]
    private TMPro.TMP_Text currentCoins;

    public TMPro.TMP_Text coinsCollected;
    public TMPro.TMP_Text coinsFromDistance;
    public TMPro.TMP_Text coinsFromFlips;
    public TMPro.TMP_Text coinsTotalGained;
    public TMPro.TMP_Text currentHeightText;

    public static bool distanceSaved = true;
    public GameObject player;
    private float startPos;
    public static float currentDist;
    public static int score;
    bool isTracking = false;

    public GameObject rampObstacle;
    public Transform[] rampObstacleSP;
    private int rampObstacleCount;

    public GameObject rampBoost;
    public Transform[] rampBoostSP;
    private int rampBoostCount;

    public bool gsf;
    public JSONSaveLoad jsl;
    public string path;

    private static bool initializeGame = false;
    public static bool adsMultiplier = false;

    public string dirName = "savedprogress";

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        ads1.SetActive(false);
        ads2.SetActive(false);

        #if UNITY_ANDROID
        path = Application.persistentDataPath;
        #endif
        #if UNITY_EDITOR
        path = Application.dataPath;
        #endif

        jsl = new JSONSaveLoad();

        adsMultiplier = false;

        if (File.Exists( path + "/" + "gamesaves" + ".json"))
        {
            LoadJ();
        }
        else
        {
            ResetJ();
            LoadJ();
        }

        if (!Directory.Exists(Path.Combine(path, dirName)))
        {
            Directory.CreateDirectory(Path.Combine(path, dirName));
            Debug.Log("Tehtiin: " + dirName);
        }

        if (!initializeGame)
        {
            InitializeProgress();
            LoadP();
            
            initializeGame = true;
        }
        LoadB(6);

        SpawnRampObstacle();
        SpawnRampBoost();
    }

    // Update is called once per frame
    void Update()
    {
        //gsf = ButtonController.gameStateFroze;

        if (isTracking && ButtonController.gameStateFroze == false)
        {
            TrackDistance();
        }
        if (ButtonController.gameStateFroze)
        {

            coinsFromDistance.text = "Distance: " + (float)Mathf.Round((currentDist) * 1f) / 1; // /5
            coinsCollected.text = "Collected: " + score.ToString();
            int FlipCoins = CharacterController.flips * 10;
            coinsFromFlips.text = "Flips: " + FlipCoins.ToString();
            coinsTotalGained.text = "Total: " + (float)Mathf.Round((currentDist + score + FlipCoins) * 1f) / 1;
            Debug.Log("gamemanager gameStateFroze  on" + ButtonController.gameStateFroze);

            //PYÖRISTYS (float)Mathf.Round(g.distance * 100f) / 100f

            if (distanceSaved)
            {

            }else
            {
                SaveJ();
                LoadJ();
                distanceSaved = true;
            }

        }
    }

    void SpawnRampObstacle()
    {
        for (int i = 0; i <= 2; i++)
        {
            Instantiate(rampObstacle, rampObstacleSP[Random.Range(0, rampObstacleSP.Length)].position, rampObstacle.transform.rotation);
        }
    }

    void SpawnRampBoost()
    {
        for (int i = 0; i <= 2; i++)
        {
            Instantiate(rampBoost, rampBoostSP[Random.Range(0, rampBoostSP.Length)].position, rampBoost.transform.rotation);
        }
    }

    private void TrackDistance()
    {
        currentDist = (player.transform.position.x - startPos) / 3;
        currentDistanceText.text = /*"distance: " +*/ currentDist.ToString("#.00") + "m";
        endDistance.text = "distance: " + currentDist.ToString("#.00") + "m";
        currentCoins.text = "Coins: " + score.ToString();
       
        currentHeightText.text = ((player.transform.position.y - GroundObstacleController.levelDeterminedPosition) / 3).ToString("#.00") + "m";
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        startPos = player.transform.position.x;
        isTracking = true;
        buttonController.GetComponent<ButtonController>().enabled = true;
        airObstacleController.GetComponent<AirObstacleController>().enabled = true;
        groundObstacleController.GetComponent<GroundObstacleController>().enabled = true;

        ButtonController.gameStateFroze = false;
        CharacterController.isFlying = true;
    }

    public void SaveJ()
    {
        int coins = score;
        int coinsFromDistance = (int)currentDist;
        float distance = currentDist;
        int coinsFromFlips = CharacterController.flips * 10;
        GameSaves g = new GameSaves(distance, coins);
        GameSaves ga = jsl.LoadJson();
        g.coins = ga.coins + coins + coinsFromDistance + coinsFromFlips;

        Debug.Log($"score saved {score}");
        
        if (ga.distance > g.distance)
        {
            g.distance = ga.distance;
            jsl.SaveJson(g);
        }
        else
        {
            //g.coins = ga.coins + coins + coinsFromDistance + coinsFromFlips;
            
        }
        jsl.SaveJson(g);
    }

    public void ResetJ()
    {
        int coins = 0;
        float distance = 0;
        GameSaves g = new GameSaves(distance, coins);

        jsl.SaveJson(g);
    }

    public void LoadJ()
    {
        GameSaves g = jsl.LoadJson();

        float ga = (float)Mathf.Round(g.distance * 100f) / 100f;

        longestDistance.text = "Longest Distance: " + ga + "m";
        totalCoins.text = "Coins: " + g.coins;
    }
    public void LoadF()
    {
        GameSaves g = jsl.LoadFromResources();
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
    public void SaveP(int id, string name, bool isBought)
    {
        GameProgress p = new GameProgress(id, name, isBought);
        jsl.SaveProgress(p);
    }

    public void SaveB(int id, string name, int level, int amount)
    {
        SaveBoosts b = new SaveBoosts(id, name, level, amount);
        jsl.SaveBoost(b);
    }
    public void LoadB(int id)
    {
        SaveBoosts b = jsl.LoadBoost(id);
        if (b.amount > 0)
        {
            UpgradeController.boosterOwned = true;
        }
        Debug.Log(b.name + "_I: " + b.id + "_L: " + b.level + "_A: " + b.amount);
    }

    public void InitializeProgress()
    {
        Debug.Log("Initialisointi aloitettu");
        #if UNITY_ANDROID
                path = Application.persistentDataPath;
        #endif
        #if UNITY_EDITOR
                path = Application.dataPath;
        #endif

        for (int i = 1; i < ShopManagerScript.shopItems.GetLength(1); i++)
        {
            Debug.Log("For loopataan initialisointia");
            if (i == 6)
            {
                continue;
            }
            if (File.Exists(path + "/" + ShopManagerScript.savedProgressDirectory + "/" + i + "_" + "save" + ".json"))
            {
            }
            else
            {
                SaveP(i, "save", false);
            }
        }

        if (File.Exists(path + "/" + ShopManagerScript.savedProgressDirectory + "/" + "6" + "_" + "save" + ".json"))
        {

        }
        else
        {
            SaveB(6, "save", 1, 0);
        }

        Debug.Log("Initialisointi valmis");
    }

    public void ResetAllItems()
    {
        for (int i = 0; i < ShopManagerScript.shopItems.GetLength(1); i++)
        {
            if (i == 0)
            {
                continue;
            }
            if (i == 6 || i == 0)
            {
                continue;
            }
            SaveP(i, "save", false);
            ShopManagerScript.shopItems[3, i] = 0;
            Debug.Log($"{i} {ShopManagerScript.shopItems[3, i]} pitäisi olla nollattu");
        }
        UpgradeController.jetpackOwned = false;
        SaveB(6, "save", 1, 0);
        LoadB(6);
        LoadP();
    }

    public void WatchAds()
    {
        /*
        int coins = score;
        int coinsFromDistance = (int)currentDist;
        float distance = currentDist;
        int coinsFromFlips = CharacterController.flips * 10;
        GameSaves g = new GameSaves(distance, coins);
        GameSaves ga = jsl.LoadJson();
        g.coins = ga.coins + coins + coinsFromDistance + coinsFromFlips; 
        */
        runEndMenu.SetActive(false);
        int adsRng = Random.Range(1, 100);
        int coins = score;
        int coinsFromDistance = (int)currentDist;
        int coinsFromFlips = CharacterController.flips * 10;
        GameSaves ga = jsl.LoadJson();
        GameSaves g = new GameSaves(ga.distance, coins);
        g.coins = ga.coins + coins + coinsFromDistance + coinsFromFlips;
        jsl.SaveJson(g);
        if (adsRng >= 50)
        {
            ads1.SetActive(true);
            StartCoroutine(WaitForTrailer(30));
        }
        else
        {
            ads2.SetActive(true);
            StartCoroutine(WaitForTrailer(33));
        }
    }

    IEnumerator WaitForTrailer(int secs)
    {
        adsButton.SetActive(false);
        yield return new WaitForSecondsRealtime(33);
        //Tarvii oikeanmallista ratkaisua
        runEndMenu.SetActive(true);
    }
}
