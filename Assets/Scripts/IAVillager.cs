using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static ManagerIA;

public class IAVillager : MonoBehaviour {
    [Header("Villager IA")]
    public ManagerIA.IAVillager CurrentState;
    public ManagerIA.IAVillager oldState;

    [Header("NavMesh")]
    public NavMeshAgent agent;

    [Header("Tipo de Trabajo")]
    public ManagerIA.TipoAldeano tipoAldeano;


    private void Awake() {


    }

    public void CheckState() {
        switch (CurrentState) {
            case ManagerIA.IAVillager.None:
                break;
            case ManagerIA.IAVillager.Walk:
                break;
            case ManagerIA.IAVillager.Working:
                break;
            case ManagerIA.IAVillager.Carry:
                break;
            case ManagerIA.IAVillager.Selling:
                break;
            default:
                break;
        }
    }

    public void ChangeState(ManagerIA.IAVillager newstate) {

        oldState = CurrentState;
        CurrentState = newstate;
        Debug.Log("Cambio de estado Cliente a " + newstate);
        CheckState();

    }

}
