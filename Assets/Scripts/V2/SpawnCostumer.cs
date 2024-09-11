using System;
using UnityEngine;
using Random = UnityEngine.Random;

internal class SpawnCostumer : MonoBehaviour, ISpawnCostumer
{
    private int _leveltotal;

    private int b;
    //Spawnea un Cliente
    public void SummonCostumer() {
         b= Random.Range(0, ManagerIA.Instance.EntradasDeComprador.Length);
        
        GameObject _clone =  Instantiate( ManagerIA.Instance.costumer, 
            ManagerIA.Instance.EntradasDeComprador[b].position , new Quaternion(0,0,0,0));  
        
        ManagerIA.Instance.costumerPool.Add(_clone);
        
        
        _clone.gameObject.SetActive(false);
        
    }
    
    //Decidir si inicializa o no un cliente
    public void MakeInvokeCustomer() 
    {
        float temp_time;

        switch (_leveltotal)
        {
            case >= 25:
                temp_time = Random.Range(0, 3);
                break;
            case <= 25:
                temp_time = Random.Range(0, 8);
                break;
        }
        print("tiempo de espera"+ temp_time);

        //Comparar cuanto es el maximo y cuantos hay en las filas iniciales
        if ((ManagerIA.Instance._maxCostumersFI > (ManagerIA.Instance.Costumersf1.Count+ManagerIA.Instance.Costumersf2.Count+ManagerIA.Instance.Costumersf3.Count)
                ) && GameManager.instance.wallet[0].started && ManagerIA.Instance.costumerPool.Count > 0)
        {
            //invoca un cliente en a tiempo
            Invoke("InitCostumer", temp_time);
        
        } else {
            //no invoca e inicia la espera de invocacion
            Invoke("NotSummonCost", 0.1f);
        
        }
    }

    private void InitCostumer()
    {
        GameObject _clone = ManagerIA.Instance.costumerPool[0];
        b= Random.Range(0, ManagerIA.Instance.EntradasDeComprador.Length);

        _clone.gameObject.SetActive(true);
        
        int _numfila = Random.Range(0,  ManagerIA.Instance.estacionesDesbloqueadas);

        
        _clone.GetComponent<IACostumer>().Assing( ManagerIA.Instance.DestinosDeComprador[_numfila],
            _numfila,  ManagerIA.Instance.SalidasDeComprador[Random.Range(0,  ManagerIA.Instance.SalidasDeComprador.Length)],
            ManagerIA.Instance.EntradasDeComprador[b]);

        switch (_numfila) {
            case 0:
                ManagerIA.Instance.Costumersf1.Add(_clone);
                break;
            case 1:
                ManagerIA.Instance.Costumersf2.Add(_clone);

                break;  
            case 2:
                ManagerIA.Instance.Costumersf3.Add(_clone);
                break;  
            case 3:
                ManagerIA.Instance.Costumersf4.Add(_clone);

                break;  
            case 4:
                ManagerIA.Instance.Costumersf5.Add(_clone);

                break;  
            case 5:
                ManagerIA.Instance.Costumersf6.Add(_clone);

                break;  
            case 6:
                ManagerIA.Instance.Costumersf7.Add(_clone);

                break;
            default:
                print("Error");
                break;
        }
        
        ManagerIA.Instance.costumerPool.Remove(_clone);
        
        Invoke("MakeInvokeCustomer", 1f);

    }

    //No spawnea Cliente
    public void NotSummonCost() {
        Invoke("MakeInvokeCustomer", 2f);
    }
}