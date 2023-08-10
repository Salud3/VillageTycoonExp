using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerIA : MonoBehaviour
{
    public enum IAVillager {None, Walk, Working, Carry, Selling}
    public enum TipoAldeano { Farm1, Farm2, Farm3, Pescador, Molinero }

    public enum IACostumer {None, Walk, Waiting, Buyed}
    public static ManagerIA instance;

    int a = 0;
    [Header("Lugares Costumer y objeto instance")]
    public GameObject costumer;
    public Transform[] EntradasDeComprador = new Transform[3];
    public Transform[] SalidasDeComprador = new Transform[3];
    public Transform[] DestinosDeComprador = new Transform[3];

    public List<GameObject> Clientef1;
    public List<GameObject> Clientef2;
    public List<GameObject> Clientef3;

    void Start()
    {
        
        instance = this;
        isnt();
    }

    public void isnt() {
        a = Random.Range(3, 5);
        print(a);

        if ( (Clientef1.Count + Clientef2.Count + Clientef3.Count) <= 25) {

            Invoke("SummonCostumer", a);
        
        } else {
    
            Invoke("NotSummonCost", 0.1f);
        
        }
    }

    public void SummonCostumer() {
        int b = Random.Range(0, 3);

        Instantiate(costumer, EntradasDeComprador[b].position , new Quaternion(0,0,0,0));

        Invoke("isnt", 1f);
    }

    public void NotSummonCost() {
        Invoke("isnt", 1f);
    }

}
