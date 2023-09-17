using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public WalletClass[] wallet;
    public VillagerClass[] LevelStation;

    private void Awake()
    {
        instance = this;
    }
    private void Start() {

        wallet = SaveSystem.Instance.wallet;
        LevelStation = SaveSystem.Instance.LevelStation;

        AutoSave();
    }

    private void AutoSave()
    {
        Debug.Log("AUTO GUARDADO");
        SaveSystem.Instance.Saveall();
        Invoke("AutoSave",250f);
    }

    private void Update() {
        
    }

    public void costStatio(float cost) {

        wallet[0].mon -= cost;

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


        /*switch (tipoV)
                {
            case 0:
                if (LevelStation[tipoV].LevelStation == 0)
                {
                    wallet[0].mon += LevelStation[tipoV].earning;
                }
                else
                {
                    wallet[0].mon += LevelStation[tipoV].earning * ((LevelStation[tipoV].LevelStation/10)*2 + 1);
                }
                break;
            case 1:
                if (LevelStation[tipoV].LevelStation == 0)
                {
                    wallet[0].mon += LevelStation[tipoV].earning;
                }
                else
                {
                    wallet[0].mon += LevelStation[tipoV].earning * ((LevelStation[tipoV].LevelStation / 10) * 2 + 1);
                }
                break;
            case 2:
                if (LevelStation[tipoV].LevelStation == 0)
                {
                    wallet[0].mon += LevelStation[tipoV].earning;
                }
                else
                {
                    wallet[0].mon += LevelStation[tipoV].earning * ((LevelStation[tipoV].LevelStation / 10) * 2 + 1);
                }
                break;
            case 3:
                if (LevelStation[tipoV].LevelStation == 0)
                {
                    wallet[0].mon += LevelStation[tipoV].earning;
                }
                else
                {
                    wallet[0].mon += LevelStation[tipoV].earning * ((LevelStation[tipoV].LevelStation / 10) * 2 + 1);
                }
                break;
            case 4:
                wallet[0].mon += LevelStation[tipoV].earning;
                break;
            case 5:
                wallet[0].mon += LevelStation[tipoV].earning;
                break;
            case 6:
                wallet[0].mon += LevelStation[tipoV].earning;
                break;
            case 7:
                wallet[0].mon += LevelStation[tipoV].earning;
                break;
            default:
                break;
        }*/
        
    }

    public void Inicio()
    {

    }
}
