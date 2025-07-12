using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerIA : MonoBehaviour, INotifications
{
    public enum IAStatesV {None, Walk, Working, Carry, Selling}
    //Depreciate: public enum TipoAldeano { Farm1, Farm2, Farm3, Costurero, Panadero, Pescador, Molinero }

    public string[] villagerType;
    public Dictionary<string, int> villagerToInt;
    public Dictionary<int,string> intToVillager;
    
    public enum IACostStates {None, Walk, Waiting, Buyed}

    public static ManagerIA Instance;
    private ISpawnCostumer spawnCostumer;    
    private ICheckoutLogic checkoutLogic;
    private IBuyLogic _buyLogic;
    public IBuyLogic BuyLogic { get { return _buyLogic; } }

    [Header("PoolSizeData")] 
    [SerializeField] private int _totalSpawned;
    private int lowLevel = 5;
    public bool etapa2 = false;

    [Header("LevelData")] 
    //Contador e Estaciones desbloqueadas
    public int estacionesDesbloqueadas;
    public int leveltotal;
    //Maximo de clientes en las filas iniciales
    public int _maxCostumersFI = 15;
    
    [Header("Lugares Costumer y objeto instance")]
    public GameObject costumer;
    public Transform[] EntradasDeComprador = new Transform[3];
    public Transform[] SalidasDeComprador = new Transform[3];
    public Transform[] DestinosDeComprador = new Transform[3];

    public List<GameObject> Costumersf1;
    public List<GameObject> Costumersf2;
    public List<GameObject> Costumersf3;
    public List<GameObject> Costumersf4;
    public List<GameObject> Costumersf5;
    public List<GameObject> Costumersf6;
    public List<GameObject> Costumersf7;
    
    public List<GameObject> costumerPool;
    

    [Header("Lugares Costumer y objeto instance")]
    
    public GameObject[] Villagers;
    public Transform[] VillagerSpawn = new Transform[3];
    public Transform[] LugarEntregas = new Transform[7];
    public Transform[] VillagerTrabajos = new Transform[7];
    public Transform[] VillagerReposo = new Transform[7];

    public GameObject Farm1;
    public GameObject Farm2;
    public GameObject Farm3;
    public GameObject Costureros;
    public GameObject Panaderos;
    public GameObject Pescadores;
    public GameObject Molineros;

    public GameObject[] Estructuras = new GameObject[7];

    
    //Observer Logic
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void SuscribeNotification(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnSuscribeNotification(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var obs in _observers)
        {
            obs.Updated(this);
        }
    }
    
    //End Observer Logic
    
    void Start()
    {
        Instance = this;
        
        villagerToInt = new Dictionary<string, int>();
        intToVillager = new Dictionary<int, string>();
        int i = 0;
        foreach (var type in villagerType)
        {
            villagerToInt.Add(type, i);
            intToVillager.Add(i,type);
            i++;
        }
        spawnCostumer = GetComponent<SpawnCostumer>();
        _buyLogic = GetComponent<BuyLogic>();
        checkoutLogic = GetComponent<CheckoutLogic>();
        
        Invoke("Inic",0.1f);
    }
    
    public void Inic() {
        _buyLogic.UnlockedInit();
        LevelTotall();

        spawnCostumer.MakeInvokeCustomer();
    }

    public void ExitC(GameObject customer)
    {
        checkoutLogic.ExitCostumer(customer);
    }

    public void LineUpdate(int fila,GameObject customer)
    {
        checkoutLogic.AvazarFila(fila, customer);
    }

    public void CheckoutC(int fila, IACostumer customer)
    {
        checkoutLogic.LlegoCliente(fila, customer);
    }


    //Spawnea un Aldeano y Añade al trabajo
    public void LevelUp(int Job)
    {
        _buyLogic.BuyLvl(Job);
    }

    public void LevelTotall()
    {
        leveltotal = 0;
        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++)
        {
            leveltotal += GameManager.instance.LevelStation[i].LevelStation;
        }
        Debug.LogWarning("Level total: "+leveltotal);
        CalcPoolSize();
    }
    
    private void CalcPoolSize()
    {
        _totalSpawned = Costumersf2.Count + Costumersf3.Count + Costumersf1.Count
                            + Costumersf4.Count + Costumersf5.Count + Costumersf6.Count
                            + Costumersf7.Count + costumerPool.Count;
        
        Debug.Log(leveltotal + "what the: " + _totalSpawned);
        
        if (leveltotal <5 && _totalSpawned <= lowLevel)
        {
            Debug.Log("Low level");
            for (int i = 0; i < lowLevel && _totalSpawned < lowLevel; i++)
            {
                spawnCostumer.SummonCostumer();
            }
            
        }else if (leveltotal >= 5 && !GameManager.instance.tutorials[0].T3 && !etapa2)
        {
            Debug.Log("High level, tutorial 3 and etapa2");
            //Inicializa el Tutorial3
            DialogueManager.instance.TpD3();

            for (int i = 0; i < _maxCostumersFI && _totalSpawned < _maxCostumersFI; i++)
            {
                spawnCostumer.SummonCostumer();
            }
           
            etapa2 = true;
            
        }
        else if (_totalSpawned < _maxCostumersFI)
        {
            Debug.Log("High level");
            for (int i = 0; i < _maxCostumersFI && _totalSpawned < _maxCostumersFI; i++)
            {
                spawnCostumer.SummonCostumer();
            }
        }
    }


}