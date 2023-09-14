using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class WalletClass
{
    public float mon;
    public string nae;

    public WalletClass (string username,float money)
    {
        mon = money;
        nae = username;
    } 
}
