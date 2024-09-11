using System;
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
    //public ManagerIA.TipoAldeano tipoAldeano;
    VillagerType villagerType;

    public string jobAssingbyType;
    
    public Transform lugarDeTrabajo;
    public Transform lugarReposo;
    public Transform lugarEntrega;
    public Transform Destino;

    [Header("Chambeando")]
    public bool chambeando = false;
    //Detectar la peticion

    [Header("Stats")]
    public int nivel;
    public float VelWalk;
    public float VelWork;
    public float VelSell;

    [Header("NavMesh y cliente a que atender")]
    public NavMeshAgent agent;
    public IACostumer costumer;


    public void AssingJob(int job)
    {
        villagerType = GetComponent<VillagerType>();
        jobAssingbyType = villagerType.Type;
        
        lugarDeTrabajo = ManagerIA.Instance.VillagerTrabajos[job];
        lugarReposo = ManagerIA.Instance.VillagerReposo[job];
        lugarEntrega = ManagerIA.Instance.LugarEntregas[job];

        incio();

    }

    int JobToInt(string job)
    {
        
        if (!ManagerIA.Instance.villagerToInt.TryGetValue(jobAssingbyType, out var i ))
        {
            throw new Exception($"Villager {jobAssingbyType} no existe");
        }
        return i;
    }

    public void incio() {
        //Debug.Log(GameManager.instance.LevelStation[(int)tipoAldeano].LevelStation +  tipoAldeano.ToString());
        
        nivel = GameManager.instance.LevelStation[ JobToInt(jobAssingbyType) ].LevelStation;
        AssingLevelStats(nivel);
        ChangeDestination(lugarReposo);

    }
    
    public void AssingLevelStats(int level)
    {
        switch (level)
        {
            case 0:
                VelWalk= 3.8f;
                agent.speed = VelWalk;
                VelWork = 5f;
                VelSell = 7f;
                break;
            case 1:
                VelWalk = 3.9f;
                agent.speed = VelWalk;
                VelWork = 4.85f;
                VelSell = 6.8f;
                break;
            case 2:
                VelWalk = 4f;
                agent.speed = VelWalk;
                VelWork = 4.75f;
                VelSell = 6.5f;
                break;
            case 3:
                VelWalk = 4.2f;
                agent.speed = VelWalk;
                VelWork = 4.60f;
                VelSell = 6.3f;
                break;
            case 4:
                VelWalk = 4.4f;
                agent.speed = VelWalk;
                VelWork = 4.0f;
                VelSell = 5.9f;
                break;
            case 5:
                VelWalk = 4.6f;
                agent.speed = VelWalk;
                VelWork = 3.8f;
                VelSell = 5.5f;
                break;
            case 6:
                VelWalk = 4.8f;
                agent.speed = VelWalk;
                VelWork = 3.5f;
                VelSell = 4.0f;
                break;
            case 7:
                VelWalk = 4.8f;
                agent.speed = VelWalk;
                VelWork = 3.2f;
                VelSell = 4.8f;
                break;
            case 8:
                VelWalk = 4.8f;
                agent.speed = VelWalk;
                VelWork = 2.5f;
                VelSell = 4.2f;
                break;
            case 9:
                VelWalk = 4.8f;
                agent.speed = VelWalk;
                VelWork = 2f;
                VelSell = 3f;
                break;
            case >=10:
                VelWalk = 5.2f;
                agent.speed = VelWalk;
                VelWork = 1f;
                VelSell = 1f;
                break;
            default:
                VelWalk = 3.8f;
                agent.speed = VelWalk;
                VelWork = 5f;
                VelSell = 7f;
                break;
        }
    }

    public void BuscarChamba() {
        chambeando = true;

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
        //Debug.Log("Cambio de estado Cliente a " + newstate);
        CheckState();

    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "LugarEntrega" && currentState != IAStatesV.Carry) {


        }else*/ if (other.tag == "LugarEntrega" && currentState == IAStatesV.Carry) {

            ChangeState(IAStatesV.Selling);
            StartCoroutine("Vendiendo");
        }

        if (other.tag == "Chamba" && other.transform == lugarDeTrabajo) {
            
            ChangeState(IAStatesV.Working);
            StartCoroutine("Trabajo");
        }


    }

    public IEnumerator Trabajo() {

        //Debug.Log("Trabajando");


        yield return new WaitForSeconds(VelWork);

        //Debug.Log("Trabajo hecho");
        ChangeDestination(lugarEntrega);
        CheckState();

    }
    public IEnumerator Vendiendo() {

        //Debug.Log("Vendiendo");
        yield return new WaitForSeconds(VelSell);
        //Debug.Log("Venta de " + (int)tipoAldeano + " " + tipoAldeano);
        GameManager.instance.Venta(JobToInt(jobAssingbyType));
        costumer.CompraLista();
        costumer = null;
        chambeando = false;
        ChangeState(IAStatesV.None);

    }

    public void chambear()
    {
        ChangeDestination(lugarDeTrabajo);

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
