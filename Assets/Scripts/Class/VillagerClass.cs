using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class VillagerClass
{
    public bool Unlock;
    public int LevelStation;
    public float earning;
    public float cost;

    public VillagerClass(int CVill, int costt,float Moneyearn, bool locked)
    {
        LevelStation = CVill;
        Unlock = locked;
        earning = Moneyearn;
        cost = costt;
    }

}
