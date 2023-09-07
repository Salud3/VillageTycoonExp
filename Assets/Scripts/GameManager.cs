using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VillagerClass[] LevelStation;

    void Start()
    {
        LevelStation = SaveSystem.Instance.LevelStation;
    }

    public void Inicio()
    {

    }
}
