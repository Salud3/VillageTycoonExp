using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("a")]
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
    public TextMeshProUGUI money;
    public GameObject[] canvasEstacionesDes;
    public TextMeshProUGUI[] Precios;
    public TextMeshProUGUI PrecioV;

    private void Awake()
    {
        instance = this;
    }
    private void Start() {

        wallet = SaveSystem.Instance.wallet;
        LevelStation = SaveSystem.Instance.LevelStation;
        tutorials = SaveSystem.Instance.Tutorial;

        MostrarVilla();


        AutoSave();
    }
    private void Update() 
    {
        if (wallet[0].mon > 0)
        {
            money.text = wallet[0].mon.ToString("#.##");
        }
        else
        {
            money.text = "0.0";
        }

        for (int i = 0; i < Precios.Length; i++)
        {
            if (canvasEstacionesDes[i].activeSelf)
            {
                Precios[i].text = calccost(LevelStation[i].cost,i).ToString("##.##");
            }                           //(cost * (1+(cost/10)) * (1+(LevelStation[tipoV].LevelStation / 2)) 
        }

        if (PrecioV.gameObject.activeSelf)
        {
            switch (wallet[0].levelV)
            {
                case 0:
                    PrecioV.text = Cost1.ToString("#.##");
                    break;
                case 1:
                    PrecioV.text = Cost2.ToString("#.##");
                    break;
                case 2:
                    PrecioV.text = Cost3.ToString("#.##");
                    break;
                case 3:
                    PrecioV.text = "Maximo Nivel!!";
                    break;
                default:
                    PrecioV.text = Cost1.ToString("#.##");
                    break;
            }
        }

    }
    

    
    private void AutoSave()
    {
        Debug.Log("AUTO GUARDADO");
        SaveSystem.Instance.Saveall();
        Invoke("AutoSave",250f);
    }

    public float calccost(float cost, int tipoV)
    {
        float costt = cost + (((cost* 0.5f) * LevelStation[tipoV].LevelStation) / 2);
        return costt;
    }

    public void costStatio(float cost, int tipoV) {

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

                print("Error Compra Villa");

                break;
        }
        SaveSystem.Instance.Saveall();
    }
}
