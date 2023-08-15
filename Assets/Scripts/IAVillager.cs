using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static ManagerIA;

public class IAVillager : MonoBehaviour {
    [Header("Villager IA")]
    public ManagerIA.IAStatesV currentState;
    public ManagerIA.IAStatesV oldState;

    [Header("Tipo de Trabajo")]
    public ManagerIA.TipoAldeano tipoAldeano;
    public Transform lugarDeTrabajo;
    public Transform lugarentrega;
    public Transform Destino;

    [Header("Chambeando")]
    public bool chambeando = false;
    public int numpeticion = 0;
    public int numfila = 0;

    [Header("NavMesh")]
    public NavMeshAgent agent;

    public void Update() {
        if (!chambeando) {
            BuscarChamba();
            ChangeState(IAStatesV.Walk);
        }
    }
    //Va a una de las filas a Buscar chamba
    public void BuscarChamba() {

        numfila = Random.Range(0, 3);
        agent.SetDestination(ManagerIA.instance.LugarEntregas[numfila].position);
        chambeando = true;

    }
    //Asigna al villager un trabajo para trabajar en SOLO esa cosa
    public void AssingJob(int index)
    {
        switch (index)
        {
            case 0:
                tipoAldeano = TipoAldeano.Farm1;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];

                break;
            case 1:
                tipoAldeano = TipoAldeano.Farm2;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 2:
                tipoAldeano = TipoAldeano.Farm3;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 3:
                tipoAldeano = TipoAldeano.Pescador;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 4:
                tipoAldeano = TipoAldeano.Molinero;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 5:
                tipoAldeano = TipoAldeano.Miner;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 6:
                tipoAldeano = TipoAldeano.Costurero;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 7:
                tipoAldeano = TipoAldeano.Panadero;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            case 8:
                tipoAldeano = TipoAldeano.Farm1;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            default:
                tipoAldeano = TipoAldeano.Farm1;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
            
        }

        CheckState();
    }
    //Asigna al estado actual una accion
    public void CheckState() {
        switch (currentState) {
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
    //Cambia el estado del villager
    public void ChangeState(ManagerIA.IAStatesV newstate) {

        oldState = currentState;
        currentState = newstate;
        Debug.Log("Cambio de estado Cliente a " + newstate);
        CheckState();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LugarTrabajo" && (currentState == IAStatesV.Walk || currentState == IAStatesV.None))
        {
            AsignarChamba(numfila);
            ChangeState(IAStatesV.Working);

        }
        if (other.tag == "LugarTrabajo" && currentState == IAStatesV.Carry) {
            ChangeState(IAStatesV.None);

        }
    }

    public void AsignarChamba(int a) {
        switch (a) {
            case 0:
                numpeticion = ManagerIA.instance.Clientef1[0].GetComponent<IACostumer>().peticion;
                break;
            case 1:
                numpeticion = ManagerIA.instance.Clientef2[0].GetComponent<IACostumer>().peticion;
                break;
            case 2:
                numpeticion = ManagerIA.instance.Clientef3[0].GetComponent<IACostumer>().peticion;
                break;
            default:
                break;
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
