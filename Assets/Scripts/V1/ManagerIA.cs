using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerIA : MonoBehaviour
{
    public enum IAStatesV {None, Walk, Working, Carry, Selling}
    public enum TipoAldeano { Farm1, Farm2, Farm3, Pescador, Molinero, Costurero, Panadero }
    public enum IACostStates {None, Walk, Waiting, Buyed}

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
    public List<GameObject> Clientef4;
    public List<GameObject> Clientef5;
    public List<GameObject> Clientef6;
    public List<GameObject> Clientef7;

    public int MaxCostumersAvailables;

    [Header("Lugares Costumer y objeto instance")]
    
    public GameObject Villager;
    public Transform[] VillagerSpawn = new Transform[3];
    public Transform[] LugarEntregas = new Transform[3];
    public Transform[] VillagerTrabajos = new Transform[8];
    public Transform[] VillagerReposo = new Transform[8];

    public GameObject Farm1;
    public GameObject Farm2;
    public GameObject Farm3;
    public GameObject Costureros;
    public GameObject Panaderos;
    public GameObject Pescadores;
    public GameObject Molineros;


    void Start()
    {
        instance = this;

        Invoke("asd",0.1f);
    }

    public void asd() {
        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++) {
            if (GameManager.instance.LevelStation[i].Unlock) {
                EstacionesDesbloqueadas++;
            }
        }


        isnt();
    }

    //Decide si spawnear o no un cliente
    public void isnt() {
        a = Random.Range(3, 5);
        //print(a);

        if ( (MaxCostumersAvailables > 0) && ((Clientef1.Count + Clientef2.Count + Clientef3.Count) <= 25)) {

            Invoke("SummonCostumer", a);
        
        } else {
    
            Invoke("NotSummonCost", 0.1f);
        
        }
    }
    //Spawnea un Cliente
    public void SummonCostumer() {
        int b = Random.Range(0, EntradasDeComprador.Length);
        GameObject cclone =  Instantiate(costumer, EntradasDeComprador[b].position , new Quaternion(0,0,0,0));      MaxCostumersAvailables--;
        int awa = Random.Range(0, EstacionesDesbloqueadas - 1);
        cclone.GetComponent<IACostumer>().numfila = awa; 
        cclone.GetComponent<IACostumer>().DestinoCompra = DestinosDeComprador[awa];
        cclone.GetComponent<IACostumer>().DestinoSalida = SalidasDeComprador[Random.Range(0, SalidasDeComprador.Length)];

        switch (awa) {
            case 0:
                Clientef1.Add(cclone);
                break;
            case 1:
                Clientef2.Add(cclone);
                break;  
            case 2:     
                Clientef3.Add(cclone);
                break;  
            case 3:     
                Clientef4.Add(cclone);
                break;  
            case 4:     
                Clientef5.Add(cclone);
                break;  
            case 5:     
                Clientef6.Add(cclone);
                break;  
            case 6:     
                Clientef7.Add(cclone);
                break;
            default:
                print("Error");
                break;
        }
        MaxCostumersAvailables--;


        Invoke("isnt", 1f);
    }
    //No spawnea Cliente
    public void NotSummonCost() {
        Invoke("isnt", 1f);
    }

    

    
    //Spawnea un Aldeano y A�ade al trabajo
    public void LevelUp(int Job) {

        if (GameManager.instance.wallet[0].mon >= GameManager.instance.LevelStation[Job].cost) 
        {
            GameManager.instance.wallet[0].mon -= GameManager.instance.LevelStation[Job].cost;

            switch (Job) {
                case 0:
                    if (Farm1 == null) {
                        GameObject clone = SummonV(Job);
                        Farm1 = clone;
                    } else {
                        Farm1.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                case 1:
                    if (Farm2 == null) {
                        GameObject clone = SummonV(Job);
                        Farm2 = clone;
                    } else {
                        Farm2.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                case 2:
                    if (Farm3 == null) {
                        GameObject clone = SummonV(Job);
                        Farm3 = clone;
                    } else {
                        Farm3.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                case 3:
                    if (Panaderos == null) {
                        GameObject clone = SummonV(Job);
                        Panaderos = clone;
                    } else {
                        Panaderos.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                case 4:
                    if (Costureros == null) {
                        GameObject clone = SummonV(Job);
                        Costureros = clone;
                    } else {
                        Costureros.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                case 5:
                    if (Pescadores == null) {
                        GameObject clone = SummonV(Job);
                        Pescadores = clone;
                    } else {
                        Pescadores.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                case 6:
                    if (Molineros == null) {
                        GameObject clone = SummonV(Job);
                        Molineros = clone;
                    } else {
                        Molineros.GetComponent<IAVillager>().levelstation++;
                    }
                    break;
                default:
                    Debug.Log("Error summon");
                    break;
            }

        }

    }

    private GameObject SummonV(int Job) {
        int b = Random.Range(0, VillagerSpawn.Length);

        GameObject clone = Instantiate(Villager, VillagerSpawn[b].position, new Quaternion(0, 0, 0, 0));

        clone.GetComponent<IAVillager>().AssingJob(Job);
        return clone;
    }
}    