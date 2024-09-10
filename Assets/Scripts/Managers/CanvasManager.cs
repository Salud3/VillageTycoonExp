using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public GameObject[] canvasLockedStations;
    public GameObject station1;



    public void showStations()
    {
        if (ManagerIA.Instance.leveltotal >= 5 && ManagerIA.Instance.leveltotal <= 14)
        {
            for (int i = 0; i < 2; i++)
            {
                canvasLockedStations[i].SetActive(true);
            }
        }else if (ManagerIA.Instance.leveltotal >= 15 && ManagerIA.Instance.leveltotal <= 30)
        {
            for (int i = 0; i < 5; i++)
            {
                canvasLockedStations[i].SetActive(true);
            }
        }
        else if (ManagerIA.Instance.leveltotal >30)
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
