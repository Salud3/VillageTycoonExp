using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class WalletClass
{
    public float mon;
    public string nae;
    public bool started;
    public int levelV;
    public WalletClass (string username,float money, bool start, int levelv)
    {
        mon = money;
        nae = username;
        started = start;
        levelV = levelv;
    } 
}
