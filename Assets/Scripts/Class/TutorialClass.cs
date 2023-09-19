using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TutorialClass
{
    public bool T1;
    public bool T2;
    public bool T3;
    public bool T4;

    public TutorialClass(bool completed1,bool completed2,bool completed3,bool completed4)
    {
        T1 = completed1;
        T2 = completed2;
        T3 = completed3;
        T4 = completed4;
    }
}
