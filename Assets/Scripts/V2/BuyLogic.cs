using System.Collections.Generic;
using UnityEngine;

internal class BuyLogic : MonoBehaviour, IBuyLogic
{
    private ISpawnVillager  spawnVillager;

    private void Start()
    {
        spawnVillager = GetComponent<SpawnVillager>();
    }

    public void BuyLvl(int Job)
    {
        GetStationUnlock();

        if (GameManager.instance.wallet[0].mon > GameManager.instance.CalcCost(GameManager.instance.LevelStation[Job].cost, Job))
        {
            GameManager.instance.CostStatio(GameManager.instance.LevelStation[Job].cost, Job);
            spawnVillager.NewVillagerNSS(Job);
            GameManager.instance.LevelStation[Job].Unlock = true;
            ManagerIA.Instance.Notify();
        }

        ManagerIA.Instance.LevelTotall();
        Notify();
    }

    public void GetStationUnlock()
    {
        ManagerIA.Instance.estacionesDesbloqueadas = 0;
        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++)
        {
            if (GameManager.instance.LevelStation[i].Unlock)
            {
                ManagerIA.Instance.estacionesDesbloqueadas += 1;

            }
        }
        Notify();

    }

    public void UnlockedInit()
    {
        for (int i = 0; i < GameManager.instance.LevelStation.Length; i++) {
            if (GameManager.instance.LevelStation[i].Unlock) {
                ManagerIA.Instance.estacionesDesbloqueadas++;
                spawnVillager.SummonVSaved(i);

            }
        }            
        Notify();

    }
    
    //ObserverLogic
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
}