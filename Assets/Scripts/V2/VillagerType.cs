using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerType : MonoBehaviour
{
    [SerializeField] private string villagerType;
    
    public string Type { get { return villagerType; } set{value = villagerType; } }
}
