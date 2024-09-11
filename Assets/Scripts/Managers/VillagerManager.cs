using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public static VillagerManager instance;
    public enum IAStatesV { None, Walk, Working, Carry, Selling }
    public enum TipoAldeano
    {
        Farm1, Farm2, Farm3, Pescador,
        Molinero, Miner, Costurero, Panadero
    }

    [Header("Villager Prefab, Spawns, Lugares de Entrega" +
            " y Lugares de chambas")]
    public GameObject Prefab;
    public Transform[] VSpawns = new Transform[3];
    public Transform[] LugaresEntregas = new Transform[3];
    public Transform[] LugaresTrabajos = new Transform[8];


    [Header("listas de Villagers")]
    public List<GameObject> Farm1;
    public List<GameObject> Farm2;
    public List<GameObject> Farm3;
    public List<GameObject> Pescadores;
    public List<GameObject> Molineros;
    public List<GameObject> Mineros;
    public List<GameObject> Costureros;
    public List<GameObject> Panaderos;

    private void Awake()
    {
        instance = this;
    }

    
}
/*
    //Version Primitiva Spawnea un Aldeano y Añade al trabajo
 public void SummonVillager(int index)
    {
        int b = Random.Range(0, 3);

        GameObject clone = Instantiate(Prefab, VSpawns[b].position, new Quaternion(0, 0, 0, 0));

        clone.GetComponent<IAVillager>().AssingJob(index);

        switch (index)
        {
            case 0:
                Farm1.Add(clone);
                break;
            case 1:
                Farm2.Add(clone);
                break;
            case 2:
                Farm3.Add(clone);
                break;
            case 3:
                Pescadores.Add(clone);
                break;
            case 4:
                Molineros.Add(clone);
                break;
            case 5:
                Mineros.Add(clone);
                break;
            case 6:
                Costureros.Add(clone);
                break;
            case 7:
                Panaderos.Add(clone);
                break;
            default:
                Farm1.Add(clone);
                break;
        }


    }
 */