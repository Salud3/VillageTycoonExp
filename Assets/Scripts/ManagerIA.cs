using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerIA : MonoBehaviour
{
    public enum IAVillager {None, Walk, Working, Carry, Selling}
    public enum IACostumer {None, Walk, Waiting, Buyed}
    public static ManagerIA instance;

    public Transform[] EntradasDeComprador = new Transform[3];
    public Transform[] SalidasDeComprador = new Transform[3];
    public Transform[] DestinosDeComprador = new Transform[3];

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
