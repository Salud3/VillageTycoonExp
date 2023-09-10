using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class WalletClass
{
    public int mon;
    public string nae;

    public WalletClass (string username,int money)
    {
        mon = money;
        nae = username;
    } 
}
