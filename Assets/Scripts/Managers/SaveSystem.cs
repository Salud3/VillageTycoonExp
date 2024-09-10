using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    
    [Header("Info Player")]
    public VillagerClass[] levelStation;
    public WalletClass[] wallet;
    public TutorialClass[] tutorial;
    public string namePl;
    public float moneyD;
    //Constant Files Name
    private const string VillageFileName = "/VillageInfo.json";
    private const string WalletFileName = "/WalletInfo.json";
    private const string TutorialFileName = "/Tutorial.json";


    void Awake()
    {
        Instance = this;
        ReadInfo();
    }
    private void ReadInfo()
    {
        string url = Application.streamingAssetsPath + "/villagerInfo.json";
        
        if (System.IO.File.Exists(url))
        {
            string json = File.ReadAllText(url);
            levelStation = JsonHelper.FromJson<VillagerClass>(json);
        }
        else
        {
            ReGenInfoVillage();
        }

        string urll = Application.streamingAssetsPath + "/playerInfo.json";

        if (System.IO.File.Exists(urll))
        {
            string jsonn = File.ReadAllText(urll);
            wallet = JsonHelper.FromJson<WalletClass>(jsonn);
        }
        else
        {
            RegenWallet();
        }
        
        string uurl = Application.streamingAssetsPath + "/tutorial.json";

        if (System.IO.File.Exists(uurl))
        {
            string jjson = File.ReadAllText(uurl);
            tutorial = JsonHelper.FromJson<TutorialClass>(jjson);
        }
        else
        {
            RegenTutorial();
        }

    }
    public void SaveAll()
    {
        Save();
    } 
    private void Save()
    {
        for (int i = 0; i < levelStation.Length; i++) 
        {
            levelStation[i].Unlock = GameManager.instance.LevelStation[i].Unlock;
            levelStation[i].LevelStation = GameManager.instance.LevelStation[i].LevelStation;
        }

        string json = JsonHelper.ToJson(levelStation, true);
        string url = Application.streamingAssetsPath + VillageFileName;
        File.WriteAllText(url, json);
        print("Save level stations" + json);

        wallet[0] = GameManager.instance.wallet[0];

        string jsonn = JsonHelper.ToJson(wallet, true);
        string urll = Application.streamingAssetsPath + WalletFileName;
        File.WriteAllText(urll, jsonn);

        Debug.Log("Salvado" + jsonn);

        tutorial[0] = GameManager.instance.tutorials[0];

        string jjson = JsonHelper.ToJson(tutorial, true);
        string uurl = Application.streamingAssetsPath + TutorialFileName;
        File.WriteAllText(uurl, jjson);


    }

    public void ReGenInfo()
    {
        ReGenInfoVillage();
        RegenWallet();
        RegenTutorial();
    }

    private void RegenTutorial()
    {
        tutorial = new TutorialClass[1];
        tutorial[0] = new TutorialClass(false, false, false, false);

        string jjson = JsonHelper.ToJson(tutorial, true);
        string uurl = Application.streamingAssetsPath + TutorialFileName;
        File.WriteAllText(uurl, jjson);
    }

    private void RegenWallet()
    {
        wallet = new WalletClass[1];
        wallet[0] = new WalletClass("Wally",15,false,0);

        string jsonn = JsonHelper.ToJson(wallet, true);
        string urll = Application.streamingAssetsPath + WalletFileName;
        File.WriteAllText(urll, jsonn);
    }

    private void ReGenInfoVillage()
    {
        //regen infoPlayers
        levelStation = new VillagerClass[8];
        levelStation[0] = new VillagerClass(0, 10, 15.5f, false);
        levelStation[1] = new VillagerClass(0, 55, 25, false);
        levelStation[2] = new VillagerClass(0, 75, 35, false);
        levelStation[3] = new VillagerClass(0, 105, 55, false);
        levelStation[4] = new VillagerClass(0, 255, 85, false);
        levelStation[5] = new VillagerClass(0, 285, 95, false);
        levelStation[6] = new VillagerClass(0, 315, 105, false);
        levelStation[7] = new VillagerClass(0, 450, 150, false);

        string json = JsonHelper.ToJson(levelStation, true);
        string url = Application.streamingAssetsPath + VillageFileName;
        File.WriteAllText(url, json);
    }


    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}