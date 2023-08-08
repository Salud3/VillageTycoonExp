using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerIA : MonoBehaviour
{
    public enum IAVillager {None, Walk, Working, Carry, Selling}
    public enum IACostumer {None, Walk, Waiting, Buyed}
    public static ManagerIA instance;

    [Header("Lugares Costumer y objeto instance")]
    public GameObject costumer;
    public Transform[] EntradasDeComprador = new Transform[3];
    public Transform[] SalidasDeComprador = new Transform[3];
    public Transform[] DestinosDeComprador = new Transform[3];

    void Start()
    {
        instance = this;
        isnt();
    }

    public void isnt() {
        int a = Random.Range(10, 25);
        print(a);
        Invoke("SummonCostumer", a);
    }

    public void SummonCostumer() {
        Instantiate(costumer, EntradasDeComprador[Random.Range(0,3)].position , new Quaternion(0,0,0,0));
        Invoke("isnt", 1f);
    }

}
