using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static ManagerIA;

public class IAVillager : MonoBehaviour {
    [Header("Villager IA")]
    public ManagerIA.IAStatesV CurrentState;
    public ManagerIA.IAStatesV oldState;

    [Header("Tipo de Trabajo")]
    public ManagerIA.TipoAldeano tipoAldeano;
    public Transform LugarDeTrabajo;
    public Transform Lugarentrega;
    public Transform Destino;


    [Header("NavMesh")]
    public NavMeshAgent agent;

    public void AssingJob(int index)
    {
        switch (index)
        {
            case 0:
                tipoAldeano = TipoAldeano.Farm1;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];

                break;
            case 1:
                tipoAldeano = TipoAldeano.Farm2;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 2:
                tipoAldeano = TipoAldeano.Farm3;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 3:
                tipoAldeano = TipoAldeano.Pescador;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 4:
                tipoAldeano = TipoAldeano.Molinero;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 5:
                tipoAldeano = TipoAldeano.Miner;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 6:
                tipoAldeano = TipoAldeano.Costurero;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 7:
                tipoAldeano = TipoAldeano.Panadero;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 8:
                tipoAldeano = TipoAldeano.Farm1;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            default:
                tipoAldeano = TipoAldeano.Farm1;
                LugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            
        }

        CheckState();
    }

    public void CheckState() {
        switch (CurrentState) {
            case ManagerIA.IAStatesV.None:
                break;
            case ManagerIA.IAStatesV.Walk:
                agent.SetDestination(Destino.position);
                break;
            case ManagerIA.IAStatesV.Working:
                StartCoroutine(Trabajo());
                break;
            case ManagerIA.IAStatesV.Carry:
                break;
            case ManagerIA.IAStatesV.Selling:
                break;
            default:
                break;
        }
    }

    public void ChangeState(ManagerIA.IAStatesV newstate) {

        oldState = CurrentState;
        CurrentState = newstate;
        Debug.Log("Cambio de estado Cliente a " + newstate);
        CheckState();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LugarTrabajo")
        {
            ChangeState(IAStatesV.Working);

        }
    }

    public IEnumerator Trabajo()
    {

        Debug.Log("Trabajando");
        

        yield return new WaitForSeconds(5);

        Debug.Log("Trabajo hecho");

        ChangeState(IAStatesV.Walk);
        CheckState();

    }

}
