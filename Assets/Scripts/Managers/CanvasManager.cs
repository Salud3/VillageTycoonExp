using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public GameObject[] canvasLockedStations;
    public GameObject station1;



    public void showStations()
    {
        if (ManagerIA.instance.leveltotal >= 5 && ManagerIA.instance.leveltotal <= 14)
        {
            for (int i = 0; i < 2; i++)
            {
                canvasLockedStations[i].SetActive(true);
            }
        }else if (ManagerIA.instance.leveltotal >= 15 && ManagerIA.instance.leveltotal <= 30)
        {
            for (int i = 0; i < 5; i++)
            {
                canvasLockedStations[i].SetActive(true);
            }
        }
        else if (ManagerIA.instance.leveltotal >30)
        {
            for (int i = 0; i < canvasLockedStations.Length; i++)
            {
                canvasLockedStations[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < canvasLockedStations.Length; i++)
            {
                canvasLockedStations[i].SetActive(false);
            }
        }
    }
}
