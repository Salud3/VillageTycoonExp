using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Serialization;

public class CanvasManager : MonoBehaviour,IObserver
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI precioV;
    public TextMeshProUGUI[] Precios;
    public GameObject[] EstacionesUI;    
    public TextMeshProUGUI[] Levels;
    
    private void Start()
    {
        Invoke("Init", 0.12f);
    }

    void Init()
    {
        ManagerIA.Instance.SuscribeNotification(this);
        Updated(ManagerIA.Instance);
      
        ManagerIA.Instance.BuyLogic.SuscribeNotification(this);
        Updated(ManagerIA.Instance.BuyLogic);
        
        GameManager.instance.SuscribeNotification(this);
        Updated(GameManager.instance);
    }
    public void Updated(INotifications notify)
    {
        if (notify is ManagerIA manager)
        {
            if (GameManager.instance.wallet[0].mon > 0)
            {
                money.text = GameManager.instance.wallet[0].mon.ToString("#.##");
            }
            else
            {
                money.text = "0.0";
            }
        }

        if (notify is BuyLogic buyLogic)
        {
            float cost = 0;
            int lvl = 0;
            showStations();
            for (int i = 0; i < Precios.Length; i++)
            {
                if (EstacionesUI[i].activeSelf)
                {
                     cost = GameManager.instance.CalcCost(GameManager.instance.LevelStation[i].cost, i);
                     lvl = GameManager.instance.calcLevel(GameManager.instance.LevelStation[i].LevelStation);
                     Debug.Log(lvl);
                     Debug.Log(cost);
                    Precios[i].text = cost.ToString("##.##");
                    Levels[i].text = "lvl: " + lvl.ToString();
                } 
            }
        }

        if (notify is GameManager gm)
        {
            if (precioV.gameObject.activeSelf)
            {
                switch (gm.wallet[0].levelV)
                {
                    case 0:
                        precioV.text = gm.Cost1.ToString("#.##");
                        break;
                    case 1:
                        precioV.text = gm.Cost2.ToString("#.##");
                        break;
                    case 2:
                        precioV.text = gm.Cost3.ToString("#.##");
                        break;
                    case 3:
                        precioV.text = "Maximo Nivel!!";
                        break;
                    default:
                        precioV.text = gm.Cost1.ToString("#.##");
                        break;
                }
            }
            money.text = GameManager.instance.wallet[0].mon.ToString("#.##");

        }
        
    }

    public void showStations()
    {
        if (ManagerIA.Instance.leveltotal >= 5 && ManagerIA.Instance.leveltotal <= 14)
        {
            for (int i = 0; i < 2; i++)
            {
                EstacionesUI[i].SetActive(true);
            }
        }else if (ManagerIA.Instance.leveltotal >= 15 && ManagerIA.Instance.leveltotal <= 30)
        {
            for (int i = 0; i < 5; i++)
            {
                EstacionesUI[i].SetActive(true);
            }
        }
        else if (ManagerIA.Instance.leveltotal >30)
        {
            for (int i = 0; i < EstacionesUI.Length; i++)
            {
                EstacionesUI[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < EstacionesUI.Length; i++)
            {
                if (i == 0)
                {
                    EstacionesUI[i].SetActive(true);
                }
                else
                {
                    EstacionesUI[i].SetActive(false);
                    
                }
            }
        }
    }
}
