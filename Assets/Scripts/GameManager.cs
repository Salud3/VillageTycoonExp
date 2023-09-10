using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WalletClass[] wallet;
    public VillagerClass[] LevelStation;

    void Start()
    {
        wallet = SaveSystem.Instance.wallet;
        LevelStation = SaveSystem.Instance.LevelStation;
    }

    public void Inicio()
    {

    }
}
