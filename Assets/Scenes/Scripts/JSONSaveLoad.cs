using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaveLoad
{
    // Start is called before the first frame update
    public string mobilePath;
    public string path;
    public string savedProgressDirectory = "savedprogress";
    public string boardDir = "Boards";
    public string jetpackDir = "Jetpacks";
    public string skinsDir = "Skins";
    public string boostsDir = "Boosts";

    public void SaveJson(GameSaves g)
    {
        
        #if UNITY_ANDROID
                path = Application.persistentDataPath;
        #endif
        #if UNITY_EDITOR
                path = Application.dataPath;
        #endif
        
        string json = JsonUtility.ToJson(g);
        File.WriteAllText(path + "/" + "gamesaves" + ".json", json);
    }
    public GameSaves LoadJson()
    {
        
        #if UNITY_ANDROID
                path = Application.persistentDataPath;
        #endif
        #if UNITY_EDITOR
                path = Application.dataPath;
        #endif
        
        //path = Application.dataPath;
        GameSaves g = new GameSaves();
        string json = File.ReadAllText(path + "/" + "gamesaves" + ".json");
        JsonUtility.FromJsonOverwrite(json, g);

        return g;
    }
    public GameSaves LoadFromResources()
    {
        path = "JSONFiles/Saves/";
        GameSaves g = new GameSaves();
        path = path + "gamesaves" + ".json";
        string newPath = path.Replace(".json", "");
        TextAsset ta = Resources.Load<TextAsset>(newPath);
        string json = ta.text;
        JsonUtility.FromJsonOverwrite(json, g);

        return g;
    }

    public void SaveProgress(GameProgress p)
    {
    #if UNITY_ANDROID
            path = Application.persistentDataPath;
    #endif
    #if UNITY_EDITOR
            path = Application.dataPath;
    #endif
        /*
         
        tallennus pit‰isi olla muotoa <p.asian nimi muuttuja + p.level + p.id>
        t‰ll‰hetkell‰ meill‰ on k‰ytˆss‰ vaan ID
        esim jetpack_2_2.json
        {true}

         */
        string json = JsonUtility.ToJson(p);
        File.WriteAllText(path + "/" + savedProgressDirectory + "/" + p.id  + "_" + "save" + ".json", json);
        Debug.Log("JIISONI TALLENNETTU> " + p.id);
    }



    public GameProgress LoadProgress(int id)
    {
    #if UNITY_ANDROID
            path = Application.persistentDataPath;
    #endif
    #if UNITY_EDITOR
            path = Application.dataPath;
    #endif

        /*
        Lataaminen kans samalla tavalla pit‰isi ladata tiedoston <p.asian nimi muuttuja + p.level + p.id>
        for loopilla onnistuisi se tehd‰.
        */
        
        GameProgress p = new GameProgress();
        string json = File.ReadAllText(path + "/" + savedProgressDirectory + "/" + id + "_" + "save" + ".json");
        JsonUtility.FromJsonOverwrite(json, p);
        return p;
    }

    public void SaveBoost(SaveBoosts b)
    {
    #if UNITY_ANDROID
            path = Application.persistentDataPath;
    #endif
    #if UNITY_EDITOR
            path = Application.dataPath;
    #endif

        string json = JsonUtility.ToJson(b);
        File.WriteAllText(path + "/" + savedProgressDirectory + "/" + b.id + "_" + "save" + ".json", json);
    }

    public SaveBoosts LoadBoost(int id)
    {
    #if UNITY_ANDROID
            path = Application.persistentDataPath;
    #endif
    #if UNITY_EDITOR
            path = Application.dataPath;
    #endif

        SaveBoosts b = new SaveBoosts();
        string json = File.ReadAllText(path + "/" + savedProgressDirectory + "/" + id + "_" + "save" + ".json");
        JsonUtility.FromJsonOverwrite(json, b);
        return b;
    }
    public void SaveTutorial (SaveTutorial t)
    {
    #if UNITY_ANDROID
            path = Application.persistentDataPath;
    #endif
    #if UNITY_EDITOR
            path = Application.dataPath;
    #endif

        string json = JsonUtility.ToJson(t);
        File.WriteAllText(path + "/" + savedProgressDirectory + "/" + "1" + "tutorialdata" + ".json", json);

    }

    public SaveTutorial LoadTutorial(int id)
    {
    #if UNITY_ANDROID
            path = Application.persistentDataPath;
    #endif
    #if UNITY_EDITOR
            path = Application.dataPath;
    #endif

        SaveTutorial t = new SaveTutorial();
        string json = File.ReadAllText(path + "/" + savedProgressDirectory + "/" + id + "tutorialdata" + ".json");
        JsonUtility.FromJsonOverwrite(json, t);

        return t;
    }
}

/*
 
Upgraden kun ostaa niin se tallentuu erilliselle JSON tiedostolle sen nimell‰ ja siell‰ tiedostossa lukee joko "true" tai "false"
defaultilla me halutaan generoida kaikki tiedostot ensin falsella ja p‰ivitt‰‰ ne niiden nimen annettua arvoksi true
kun shopin p‰iviys tapahtuu, me halutaan katsoa jokainen tiedosto l‰pi, eli tarvitaanko niille sitten uusi kansio?
miten saamme tiedostoille nimet kun kaikki ovat pit‰neet tehd‰ numeroilla? Mill‰‰n asialla ei ole varsinaisesti nime‰.

 */
public class GameSaves
{
    public float distance;
    public int coins;
    public GameSaves(float d, int c)
    {
        distance = d;
        coins = c;
    }public GameSaves()
    {

    }
}

public class GameProgress
{
    public int id;
    public string name;
    public bool done;

    public GameProgress(int i, string n, bool d)
    {
        id = i;
        name = n;
        done = d;
    }public GameProgress()
    {

    }
}

public class SaveBoosts
{
    public int id;
    public string name;
    public int level;
    public int amount;

    public SaveBoosts(int i, string n, int l, int a)
    {
        id = i;
        level = l;
        name = n;
        amount = a;
    }public SaveBoosts()
    {

    }
}

public class SaveTutorial
{
    public int id;
    public string name;
    public bool done;

    public SaveTutorial(int i, string n,bool d)
    {
        id = i;
        name = n;
        done = d;
    }public SaveTutorial()
    {

    }
}
