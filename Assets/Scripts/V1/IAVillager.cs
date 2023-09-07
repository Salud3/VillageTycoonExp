using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static ManagerIA;

public class IAVillager : MonoBehaviour {
    [Header("Villager IA")]
    public ManagerIA.IAStatesV currentState;
    public ManagerIA.IAStatesV oldState;

    [Header("Tipo de Trabajo")]
    public ManagerIA.TipoAldeano tipoAldeano;
    public Transform lugarDeTrabajo;
    public Transform lugarReposo;
    public Transform Puesto;
    public Transform Destino;

    [Header("Chambeando")]
    public bool chambeando = false;
    //Detectar la peticion
    public int levelstation;
    public int VelWalk;
    public int VelWork;
    public int VelSell;

    [Header("NavMesh y cliente a que atender")]
    public NavMeshAgent agent;
    public IACostumer costumer;


    public void Start() {
        ChangeDestination(lugarReposo);
    }

    public void BuscarChamba() {
        chambeando = true;

    }

    public void AssingJob(int job) {


        switch (job) {
            case 0:
                tipoAldeano = ManagerIA.TipoAldeano.Farm1;
                break;
            case 1: 
                tipoAldeano = ManagerIA.TipoAldeano.Farm2;
                break;
            case 2:
                tipoAldeano = ManagerIA.TipoAldeano.Farm3;
                break;
            case 3:
                tipoAldeano = ManagerIA.TipoAldeano.Pescador;
                break;
            case 4:
                tipoAldeano = ManagerIA.TipoAldeano.Molinero;
                break;
            case 5:
                tipoAldeano = ManagerIA.TipoAldeano.Costurero;
                break;
            case 6:
                tipoAldeano = ManagerIA.TipoAldeano.Panadero;
                break;
            default:
                break;
        }

    }

    
    //Asigna al estado actual una accion
    public void CheckState() {
        switch (currentState) {
            case ManagerIA.IAStatesV.None:
                agent.SetDestination(Destino.position);
                break;
            case ManagerIA.IAStatesV.Walk:
                agent.SetDestination(Destino.position);
                break;
            case ManagerIA.IAStatesV.Working:
                break;
            case ManagerIA.IAStatesV.Carry:
                agent.SetDestination(Destino.position);
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
        if (other.tag == "LugarEntrega" && currentState != IAStatesV.Carry) {

            ChangeDestination(lugarDeTrabajo);

        }else if (other.tag == "LugarEntrega" && currentState == IAStatesV.Carry) {

            ChangeState(IAStatesV.Selling);
            StartCoroutine("Vendiendo");
        }

        if (other.tag == "Chamba") {
            
            ChangeState(IAStatesV.Working);
            StartCoroutine("Trabajo");
        }


    }

    public IEnumerator Trabajo() {

        //Debug.Log("Trabajando");


        yield return new WaitForSeconds(5);

        //Debug.Log("Trabajo hecho");
        ChangeDestination(Puesto);
        CheckState();

    }
    public IEnumerator Vendiendo() {

        Debug.Log("Vendiendo");
        yield return new WaitForSeconds(5);

        costumer.CompraLista();
        costumer = null;
        chambeando = false;
        ChangeState(IAStatesV.None);

    }

    private void ChangeDestination(Transform dest) {
        Destino = dest;

        if (currentState == IAStatesV.Working) {
            ChangeState(IAStatesV.Carry);
        } else {
            ChangeState(IAStatesV.Walk);

        }
    }

}
