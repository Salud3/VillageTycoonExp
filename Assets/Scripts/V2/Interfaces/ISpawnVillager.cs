using UnityEngine;

internal interface ISpawnVillager
{
    void NewVillagerNSS(int Job);
    void SummonVSaved(int Job);
    GameObject SummonV(int Job);
}