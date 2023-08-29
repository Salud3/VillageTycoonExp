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
    public VillagerClass[] Contador;



    void Awake()
    {
        Instance = this;
    }

    public void ReadInfo()
    {
        string url = Application.streamingAssetsPath + "/dataPlayer.json";
        string json = File.ReadAllText(url);

        Contador = JsonHelper.FromJson<VillagerClass>(json);

    }
    public void Saveall()
    {
        Save();
        Debug.Log("Salvado");
    }

    /*private void SaveUpgrades()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].mult = gmp.cpsTt[i];

        }

        string json = JsonHelper.ToJson(upgrades, true);
        string url = Application.streamingAssetsPath + "/dataUpgrades.json";
        File.WriteAllText(url, json);

        print("Save edif " + json);

    }
    private void InfoPlayerSave()
    {
        cryptos = new CryptoClass[1];
        cryptos[0] = new CryptoClass(namepl, cryptoG, clickmult);

        string json = JsonHelper.ToJson(cryptos, true);
        string url = Application.streamingAssetsPath + "/dataPlayer.json";
        File.WriteAllText(url, json);

        print("Save InfoP " + json);

    }
    public void ReGenInUp()
    {
        cryptoG = 0;
        namepl = "CryptoBro";


        //regen infoPlayers
        upgrades = new UpgradesClass[10];
        upgrades[0] = new UpgradesClass("1", 1f, 0f, 0.05f); //click
        upgrades[1] = new UpgradesClass("2", 15f, 0f, 1.0f);//mouse 
        upgrades[2] = new UpgradesClass("3", 55f, 0f, 6.0f); //chino
        upgrades[3] = new UpgradesClass("4", 165f, 0f, 12.50f);//tarjeta
        upgrades[4] = new UpgradesClass("5", 370f, 0f, 26.0f);//hacker blanco
        upgrades[5] = new UpgradesClass("6", 1050f, 0f, 54.0f);//hacker negro
        upgrades[6] = new UpgradesClass("7", 1900f, 0f, 100.230f);//Grafica 2
        upgrades[7] = new UpgradesClass("8", 8200f, 0f, 600.30f);//Wifi
        upgrades[8] = new UpgradesClass("9", 30000f, 0f, 900.030f);//Nube
        upgrades[9] = new UpgradesClass("10", ((Mathf.Pow(1000, 11) * 10f)), 0f, 7000.23f);//Servidor



        string json = JsonHelper.ToJson(upgrades, true);
        string url = Application.streamingAssetsPath + "/dataUpgrades.json";
        File.WriteAllText(url, json);

        print(json);


        //Info Player
        cryptos = new CryptoClass[1];
        cryptos[0] = new CryptoClass(namepl, cryptoG, 0.05f);

        string jsonn = JsonHelper.ToJson(cryptos, true);
        string urll = Application.streamingAssetsPath + "/dataPlayer.json";
        File.WriteAllText(urll, jsonn);

        print(jsonn);


        //OpUpClass
        opup = new OpUpClass[10];
        opup[0] = new OpUpClass(false, false, false, false, false);//click
        opup[1] = new OpUpClass(false, false, false, false, false);//mouse
        opup[2] = new OpUpClass(false, false, false, false, false);//chino
        opup[3] = new OpUpClass(false, false, false, false, false);//grafica
        opup[4] = new OpUpClass(false, false, false, false, false);//Hacker b
        opup[5] = new OpUpClass(false, false, false, false, false);//Hacker n
        opup[6] = new OpUpClass(false, false, false, false, false);//grafica 2
        opup[7] = new OpUpClass(false, false, false, false, false);// wifi
        opup[8] = new OpUpClass(false, false, false, false, false);// nube
        opup[9] = new OpUpClass(false, false, false, false, false);//servidor

        string jjson = JsonHelper.ToJson(opup, true);
        string uurl = Application.streamingAssetsPath + "/dataOpUp.json";
        File.WriteAllText(uurl, jjson);
        print(jjson);

        ReadInfo();

    }
    */
    private void Save()
    {
        for (int i = 0; i < Contador.Length; i++)
        {
            Contador[i].CountVillagers = VillagerManager.instance.Farm1.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Farm2.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Farm3.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Pescadores.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Molineros.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Mineros.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Costureros.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Panaderos.Count;
            Contador[i].CountVillagers = VillagerManager.instance.Farm1.Count;
        }

        string jjson = JsonHelper.ToJson(Contador, true);
        string uurl = Application.streamingAssetsPath + "/dataOpUp.json";
        File.WriteAllText(uurl, jjson);
        print("Save bools " + jjson);


    }
    public void ReGenVillagers()
    {
        //regen infoPlayers
        Contador = new VillagerClass[9];
        Contador[0] = new VillagerClass(0);
        Contador[1] = new VillagerClass(0);
        Contador[2] = new VillagerClass(0);
        Contador[3] = new VillagerClass(0);
        Contador[4] = new VillagerClass(0);
        Contador[5] = new VillagerClass(0);
        Contador[6] = new VillagerClass(0);
        Contador[7] = new VillagerClass(0);
        Contador[8] = new VillagerClass(0);



        string json = JsonHelper.ToJson(Contador, true);
        string url = Application.streamingAssetsPath + "/dataPlayer.json";
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