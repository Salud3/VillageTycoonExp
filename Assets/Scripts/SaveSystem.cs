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


    }
    public void Saveall()
    {
        Save();
        Debug.Log("Salvado");
    }

    private void Save()
    {
        for (int i = 0; i < LevelStation.Length; i++)
        {
            LevelStation[i].LevelStation = VillagerManager.instance.Farm1.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Farm2.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Farm3.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Pescadores.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Molineros.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Mineros.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Costureros.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Panaderos.Count;
            LevelStation[i].LevelStation = VillagerManager.instance.Farm1.Count;
        }

        string jjson = JsonHelper.ToJson(LevelStation, true);
        string uurl = Application.streamingAssetsPath + "/villagerInfo.json";
        File.WriteAllText(uurl, jjson);
        print("Save level stations" + jjson);

    }

    public void ReGenInfo()
    {
        //regen infoPlayers
        LevelStation = new VillagerClass[8];
        LevelStation[0] = new VillagerClass(0, false);
        LevelStation[1] = new VillagerClass(0, false);
        LevelStation[2] = new VillagerClass(0, false);
        LevelStation[3] = new VillagerClass(0, false);
        LevelStation[4] = new VillagerClass(0, false);
        LevelStation[5] = new VillagerClass(0, false);
        LevelStation[6] = new VillagerClass(0, false);
        LevelStation[7] = new VillagerClass(0, false);

        string json = JsonHelper.ToJson(LevelStation, true);
        string url = Application.streamingAssetsPath + "/villagerInfo.json";
        File.WriteAllText(url, json);
        
        wallet = new WalletClass[1];
        wallet[0] = new WalletClass("Wally",0);

        string jsonn = JsonHelper.ToJson(wallet, true);
        string urll = Application.streamingAssetsPath + "/playerInfo.json";
        File.WriteAllText(urll, jsonn);

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