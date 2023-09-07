using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class VillagerClass
{
    public bool Unlock;
    public int LevelStation;
    public VillagerClass(int CVill,bool locked)
    {
        LevelStation = CVill;
        Unlock = locked;
    }

}
