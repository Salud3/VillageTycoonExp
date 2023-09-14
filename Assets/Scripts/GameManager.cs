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
    }
    private void Update() {
        
    }

    public void costStatio(float cost) {

        wallet[0].mon -= cost;

    }
    public void Inicio()
    {

    }
}
