using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ManagerIA : MonoBehaviour
{
    public enum IAStatesV {None, Walk, Working, Carry, Selling}
    public enum TipoAldeano { Farm1, Farm2, Farm3, Costurero, Panadero, Pescador, Molinero }
    public enum IACostStates {None, Walk, Waiting, Buyed}

    public static ManagerIA Instance;
    
    private ISpawnCostumer spawnCostumer;

    [Header("Niveles")]
    public int estacionesDesbloqueadas;
    int a = 0;
    public int leveltotal;
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
    
    //Maximo de clientes en las filas iniciales
    public int _maxCostumersFI = 15;

    [Header("Lugares Costumer y objeto instance")]
    
    public GameObject Villager;
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

    public int A
    {
        set { a = value; }
        get { return a; }
    }

    void Start()
    {
        Instance = this;
        spawnCostumer = GetComponent<SpawnCostumer>();    
        
        Invoke("Inic",0.1f);
    }

    public void Inic() {
        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++) {
            if (GameManager.instance.LevelStation[i].Unlock) {
                estacionesDesbloqueadas++;
                SpawnVillagerBuyed(i);

            }
        }
        LevelTotall();

        spawnCostumer.MakeInvokeCustomer();
    }

    public void ReciveCostumer(GameObject costumer, int numfila)
    {
        switch (numfila)
        {
            case 1:
                Costumersf1.Remove(costumer);
                costumerPool.Add(costumer);
                costumer.SetActive(false);
                break;
            case 2:
                break; 
            case 3:
                break;
            case 4:
                break; 
            case 5:
                break; 
            case 6:
                break; 
            default: 
                break;
        }
    }

    public void SpawnVillagerBuyed(int Job) 
    {
        Estructuras[Job].SetActive(true);

        switch (Job) {
                case 0:
                    if (Farm1 == null) {
                        GameObject clone = SummonV(Job);
                        Farm1 = clone;
                        estacionesDesbloqueadas = 1;
                    } 
                    break;
                case 1:
                    if (Farm2 == null) {
                        GameObject clone = SummonV(Job);
                        Farm2 = clone;
                        estacionesDesbloqueadas = 2;

                    }
                    break;
                case 2:
                    if (Farm3 == null) {
                        GameObject clone = SummonV(Job);
                        Farm3 = clone;
                        estacionesDesbloqueadas = 3;

                    }
                    break;
                case 3:
                    if (Costureros == null) {
                        GameObject clone = SummonV(Job);
                        Costureros = clone;
                        estacionesDesbloqueadas = 4;

                    }

                    break;
                case 4:
                    if (Panaderos == null) {
                        GameObject clone = SummonV(Job);
                        Panaderos = clone;
                        estacionesDesbloqueadas = 5;

                    }
                    break;
                case 5:
                    if (Pescadores == null) {
                        GameObject clone = SummonV(Job);
                        Pescadores = clone;
                        estacionesDesbloqueadas = 6;
                    }
                    break;
                case 6:
                    if (Molineros == null) {
                        GameObject clone = SummonV(Job);
                        Molineros = clone;
                        estacionesDesbloqueadas = 7;

                    }
                    break;
                default:
                    Debug.Log("Error summon");
                    break;
            }

        

    }
    //Spawnea un Aldeano y Añade al trabajo
    public void LevelUp(int Job)
    {
        leveltotal = 0;

        estacionesDesbloqueadas = 0;

        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++)
        {
            if (GameManager.instance.LevelStation[i].Unlock)
            {
                estacionesDesbloqueadas += 1;

            }
        }

        if (GameManager.instance.wallet[0].mon > GameManager.instance.calccost(GameManager.instance.LevelStation[Job].cost, Job))
        {
            Debug.Log(GameManager.instance.wallet[0].mon);
            GameManager.instance.costStatio(GameManager.instance.LevelStation[Job].cost, Job);
            Debug.Log(GameManager.instance.wallet[0].mon);

            switch (Job)
            {
                case 0:
                    if (Farm1 == null)
                    {
                        GameObject clone = SummonV(Job);
                        Farm1 = clone;
                        Estructuras[0].SetActive(true);
                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;


                    break;
                case 1:
                    if (Farm2 == null)
                    {
                        GameObject clone = SummonV(Job);
                        Farm2 = clone;
                        Estructuras[1].SetActive(true);
                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;

                    break;
                case 2:
                    if (Farm3 == null)
                    {
                        GameObject clone = SummonV(Job);
                        Farm3 = clone;
                        Estructuras[2].SetActive(true);

                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;

                    break;
                case 3:
                    if (Costureros == null)
                    {
                        GameObject clone = SummonV(Job);
                        Costureros = clone;
                        Estructuras[3].SetActive(true);

                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;

                    break;
                case 4:
                    if (Panaderos == null)
                    {
                        GameObject clone = SummonV(Job);
                        Panaderos = clone;
                        Estructuras[4].SetActive(true);

                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;

                    break;
                case 5:
                    if (Pescadores == null)
                    {
                        GameObject clone = SummonV(Job);
                        Pescadores = clone;
                        Estructuras[5].SetActive(true);
                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;

                    break;
                case 6:
                    if (Molineros == null)
                    {
                        GameObject clone = SummonV(Job);
                        Molineros = clone;
                        Estructuras[6].SetActive(true);
                    }

                    GameManager.instance.LevelStation[Job].LevelStation++;

                    break;
                default:
                    Debug.Log("Error summon");
                    break;
            }

            GameManager.instance.LevelStation[Job].Unlock = true;

        }

        LevelTotall();


    }
    bool etapa2 = false; 
    int c;
    void LevelTotall()
    {
        c = 0;
        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++)
        {
            leveltotal += GameManager.instance.LevelStation[i].LevelStation;
        }

        CalcPoolSize();

    }

    private int lowLevel = 5;
    private int highLevel = 12;
    
    private void CalcPoolSize()
    {
        if (leveltotal <5)
        {
            for (int i = 0; i < lowLevel; i++)
            {
                spawnCostumer.SummonCostumer();
            }
            
        }else if (leveltotal >= 5 && !GameManager.instance.tutorials[0].T3 && !etapa2)
        {
            //Inicializa el Tutorial3
            DialogueManager.instance.TpD3();
            
           /*
            //12 es la constante para el maximo de la fila 1 niveles menores a 5
            if (c <= 12)
            {
                maxNumCostumer = 12-c;
            }
            else
            {
                maxNumCostumer = 0;
            }
            */
           
           
           
            etapa2 = true;
            
        }
    }

    public void AvazarFila(int fila, IACostumer costumer)
    {
        switch (fila)
        {
            case 0:
                Costumersf1.Remove(costumer.gameObject);
                if (Costumersf1.Count > 0)
                {
                    for (int i = 0; i < Costumersf1.Count; i++)
                    {

                        Costumersf1[i].GetComponent<IACostumer>().CheckState(true, i);
                    }
                }

                break;
            case 1:
                Costumersf2.Remove(costumer.gameObject);
                if (Costumersf2.Count > 0)
                {
                    for (int i = 0; i < Costumersf2.Count; i++)
                    {
                        Costumersf2[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 2:
                Costumersf3.Remove(costumer.gameObject);
                if (Costumersf3.Count > 0)
                {
                    for (int i = 0; i < Costumersf3.Count; i++)
                    {
                        Costumersf3[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 3:
                Costumersf4.Remove(costumer.gameObject);
                if (Costumersf4.Count > 0)
                {
                    for (int i = 0; i < Costumersf4.Count; i++)
                    {
                        Costumersf4[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 4:
                Costumersf5.Remove(costumer.gameObject);
                if (Costumersf5.Count > 0)
                {
                    for (int i = 0; i < Costumersf5.Count; i++)
                    {

                        Costumersf5[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 5:
                Costumersf6.Remove(costumer.gameObject);
                if (Costumersf6.Count > 0)
                {
                    for (int i = 0; i < Costumersf6.Count; i++)
                    {
                        Costumersf6[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 6:
                Costumersf7.Remove(costumer.gameObject);
                if (Costumersf7.Count > 0)
                {
                    for (int i = 0; i < Costumersf7.Count; i++)
                    {
                        Costumersf7[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            default:
                    Costumersf1[0].GetComponent<IACostumer>().CheckState(true, 0);

                break;
        }

    }
    public void LlegoCliente(int fila, IACostumer costumer)
    {
        switch (fila)
        {
            case 0:
                if (Farm1 != null)
                {
                    Farm1.GetComponent<IAVillager>().chambear();
                    Farm1.GetComponent<IAVillager>().costumer = costumer;
                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 1:
                if (Farm2 != null)
                {
                    Farm2.GetComponent<IAVillager>().chambear();
                    Farm2.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 2:
                if (Farm3 != null)
                {
                    Farm3.GetComponent<IAVillager>().chambear();
                    Farm3.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 3:
                if (Costureros != null)
                {
                    Costureros.GetComponent<IAVillager>().chambear();
                    Costureros.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 4:
                if (Panaderos != null)
                {
                    Panaderos.GetComponent<IAVillager>().chambear();
                    Panaderos.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 5:
                if (Pescadores != null)
                {
                    Pescadores.GetComponent<IAVillager>().chambear();
                    Pescadores.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 6:
                if (Molineros != null)
                {
                    Molineros.GetComponent<IAVillager>().chambear();
                    Molineros.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            default:
                break;
        }
    }

    private GameObject SummonV(int Job) {
        int b = Random.Range(0, VillagerSpawn.Length);

        GameObject clone = Instantiate(Villager, VillagerSpawn[b].position, new Quaternion(0, 0, 0, 0));

        clone.GetComponent<IAVillager>().AssingJob(Job);
        return clone;
    }
}