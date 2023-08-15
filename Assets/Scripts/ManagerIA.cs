using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerIA : MonoBehaviour
{
    public enum IAStatesV {None, Walk, Working, Carry, Selling}
    public enum TipoAldeano { Farm1, Farm2, Farm3, Pescador, Molinero, Miner, Costurero, Panadero }

    public enum IACostumer {None, Walk, Waiting, Buyed}
    public static ManagerIA instance;
    [Header("Niveles")]
    public int EstacionesDesbloqueadas;
    int a = 0;
    [Header("Lugares Costumer y objeto instance")]
    public GameObject costumer;
    public Transform[] EntradasDeComprador = new Transform[3];
    public Transform[] SalidasDeComprador = new Transform[3];
    public Transform[] DestinosDeComprador = new Transform[3];

    public List<GameObject> Clientef1;
    public List<GameObject> Clientef2;
    public List<GameObject> Clientef3;

    public int MaxCostumersAvailables;
    public int Nivel;

    [Header("Lugares Costumer y objeto instance")]
    
    public GameObject Villager;
    public Transform[] VillagerSpawn = new Transform[3];
    public Transform[] LugarEntregas = new Transform[3];
    public Transform[] VillagerTrabajos = new Transform[8];

    public List<GameObject> Farm1;
    public List<GameObject> Farm2;
    public List<GameObject> Farm3;
    public List<GameObject> Pescadores;
    public List<GameObject> Molineros;
    public List<GameObject> Mineros;
    public List<GameObject> Costureros;
    public List<GameObject> Panaderos;


    void Start()
    {
        instance = this;
        isnt();
    }
    //Decide si spawnear o no un cliente
    public void isnt() {
        a = Random.Range(3, 5);
        print(a);

        if ( (MaxCostumersAvailables > 0) && ((Clientef1.Count + Clientef2.Count + Clientef3.Count) <= 25)) {

            Invoke("SummonCostumer", a);
        
        } else {
    
            Invoke("NotSummonCost", 0.1f);
        
        }
    }
    //Spawnea un Cliente
    public void SummonCostumer() {
        int b = Random.Range(0, 3);
        Instantiate(costumer, EntradasDeComprador[b].position , new Quaternion(0,0,0,0));
        MaxCostumersAvailables--;

        Invoke("isnt", 1f);
    }
    //No spawnea Cliente
    public void NotSummonCost() {
        Invoke("isnt", 1f);
    }

    //Spawnea un Aldeano y Añade al trabajo
    public void SummonVillager(int index)
    {
        int b = Random.Range(0, 3);

        GameObject clone = Instantiate(Villager, VillagerSpawn[b].position, new Quaternion(0, 0, 0, 0));

        clone.GetComponent<IAVillager>().AssingJob(index);

        switch (index) {
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
    
}    