using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour, INotifications
{
    public static GameManager instance;
    
    [Header("Variables para guardar")]
    public TutorialClass[] tutorials;
    public WalletClass[] wallet;
    public VillagerClass[] LevelStation;
    public int Cost1 = 10000;
    public int Cost2 = 55000;
    public int Cost3 = 150000;

    [Header("Villas")]
    public GameObject[] Village = new GameObject[4];
    public GameObject Casa;

    [Header("Textos")]
    //public TextMeshProUGUI money;
    public TextMeshProUGUI[] Levels;

    private void Awake()
    {
        instance = this;
    }
    private void Start() {

        wallet = SaveSystem.Instance.wallet;
        LevelStation = SaveSystem.Instance.levelStation;
        tutorials = SaveSystem.Instance.tutorial;

        MostrarVilla();


        AutoSave();
    }
    public int calcLevel(int level)
    {
        return level;
    }
    private void AutoSave()
    {
        Debug.Log("AUTO GUARDADO");
        SaveSystem.Instance.SaveAll();
        Notify();
        Invoke("AutoSave",250f);
    }

    public float CalcCost(float cost, int tipoV)
    {
        float costt = cost + (((cost* 0.5f) * LevelStation[tipoV].LevelStation) / 2);
        return costt;
    }

    public void CostStatio(float cost, int tipoV) {

        wallet[0].mon -= cost + (((cost *0.5f) * LevelStation[tipoV].LevelStation) / 2);

    }

    public void Venta(int tipoV)
    {
        if (LevelStation[tipoV].LevelStation == 0)
        {
            wallet[0].mon += LevelStation[tipoV].earning;
        }
        else
        {
            wallet[0].mon += LevelStation[tipoV].earning * ((LevelStation[tipoV].LevelStation / 10) * 2 + 1);
        }
        Notify();

    }

    void MostrarVilla()
    {
        switch (wallet[0].levelV)
        {
            case 0:
                Village[0].SetActive(true);
                Village[1].SetActive(false);
                Village[2].SetActive(false);
                Village[3].SetActive(false);
                break;
            case 1:
                Village[0].SetActive(false);
                Village[1].SetActive(true);
                Village[2].SetActive(false);
                Village[3].SetActive(false);
                break;
            case 2:
                Village[0].SetActive(false);
                Village[1].SetActive(true);
                Village[2].SetActive(true);
                Village[3].SetActive(false);
                break;
            case 3:
                Village[0].SetActive(false);
                Village[1].SetActive(false);
                Village[2].SetActive(false);
                Village[3].SetActive(true);
                Casa.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void MejoraV()
    {
        
        switch (wallet[0].levelV)
        {
            case 0:
                if (wallet[0].mon >= Cost1)
                {
                    wallet[0].mon -= Cost1;
                    wallet[0].levelV++;
                    DialogueManager.instance.TpD4();
                    MostrarVilla();
                }
                break;
            case 1:
                if (wallet[0].mon >= Cost2)
                {
                    wallet[0].mon -= Cost2;
                    wallet[0].levelV++;
                    MostrarVilla();
                }
                break;
            case 2:
                if (wallet[0].mon >= Cost3)
                {
                    wallet[0].mon -= Cost3;
                    wallet[0].levelV++;
                    MostrarVilla();
                }
                break;
            default:

                Debug.LogError("Error Mejora Villa");

                break;
        }
        Notify();
        SaveSystem.Instance.SaveAll();
    }

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
        foreach (var observer in _observers)
        {
            observer.Updated(this);
        }
    }
}
