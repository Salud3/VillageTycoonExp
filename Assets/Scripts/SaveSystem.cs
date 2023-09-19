using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    [Header("Info Player")]
    public VillagerClass[] LevelStation;
    public WalletClass[] wallet;
    public TutorialClass[] Tutorial;
    public string namepl;
    public float moneyD;


    void Awake()
    {
        Instance = this;
        ReadInfo();
    }

    public void ReadInfo()
    {
        string url = Application.streamingAssetsPath + "/villagerInfo.json";
        string json = File.ReadAllText(url);

        LevelStation = JsonHelper.FromJson<VillagerClass>(json);

        string urll = Application.streamingAssetsPath + "/playerInfo.json";
        string jsonn = File.ReadAllText(urll);

        wallet = JsonHelper.FromJson<WalletClass>(jsonn);

        string uurl = Application.streamingAssetsPath + "/tutorial.json";
        string jjson = File.ReadAllText(uurl);

        Tutorial = JsonHelper.FromJson<TutorialClass>(jjson);

    }
    public void Saveall()
    {
        Save();
    }

    private void Save()
    {
        for (int i = 0; i < LevelStation.Length; i++) 
        {
            LevelStation[i].Unlock = GameManager.instance.LevelStation[i].Unlock;
            LevelStation[i].LevelStation = GameManager.instance.LevelStation[i].LevelStation;
        }

        string json = JsonHelper.ToJson(LevelStation, true);
        string url = Application.streamingAssetsPath + "/villagerInfo.json";
        File.WriteAllText(url, json);
        print("Save level stations" + json);

        wallet[0] = GameManager.instance.wallet[0];

        string jsonn = JsonHelper.ToJson(wallet, true);
        string urll = Application.streamingAssetsPath + "/playerInfo.json";
        File.WriteAllText(urll, jsonn);

        Debug.Log("Salvado" + jsonn);

        Tutorial[0] = GameManager.instance.tutorials[0];

        string jjson = JsonHelper.ToJson(Tutorial, true);
        string uurl = Application.streamingAssetsPath + "/tutorial.json";
        File.WriteAllText(uurl, jjson);


    }

    public void ReGenInfo()
    {
        //regen infoPlayers
        LevelStation = new VillagerClass[8];
        LevelStation[0] = new VillagerClass(0, 10, 15.5f, false);
        LevelStation[1] = new VillagerClass(0, 55, 25, false);
        LevelStation[2] = new VillagerClass(0, 75, 35, false);
        LevelStation[3] = new VillagerClass(0, 105, 55, false);
        LevelStation[4] = new VillagerClass(0, 255, 85, false);
        LevelStation[5] = new VillagerClass(0, 285, 95, false);
        LevelStation[6] = new VillagerClass(0, 315, 105, false);
        LevelStation[7] = new VillagerClass(0, 450, 150, false);

        string json = JsonHelper.ToJson(LevelStation, true);
        string url = Application.streamingAssetsPath + "/villagerInfo.json";
        File.WriteAllText(url, json);
        
        wallet = new WalletClass[1];
        wallet[0] = new WalletClass("Wally",15,false,0);

        string jsonn = JsonHelper.ToJson(wallet, true);
        string urll = Application.streamingAssetsPath + "/playerInfo.json";
        File.WriteAllText(urll, jsonn);

        Tutorial = new TutorialClass[1];
        Tutorial[0] = new TutorialClass(false, false, false, false);

        string jjson = JsonHelper.ToJson(Tutorial, true);
        string uurl = Application.streamingAssetsPath + "/tutorial.json";
        File.WriteAllText(uurl, jjson);


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