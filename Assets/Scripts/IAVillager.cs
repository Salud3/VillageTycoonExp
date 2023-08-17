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
    public Transform lugarentrega;
    public Transform Destino;

    [Header("Chambeando")]
    public bool chambeando = false;
    public int numpeticion = 0;
    public int numfila = 0;
    public int cFila1 = 0;
    public int cFila2 = 0;
    public int cFila3 = 0;

    [Header("NavMesh")]
    public NavMeshAgent agent;

    private void Update() {
        if (!chambeando) {
            BuscarChamba();
        }
    }
    //Va a una de las filas a Buscar chamba
    public void BuscarChamba() {
        cFila1 = ManagerIA.instance.Clientef1.Count;
        cFila2 = ManagerIA.instance.Clientef2.Count;
        cFila3 = ManagerIA.instance.Clientef3.Count;

        bool a = cFila1 > cFila2;
        bool b = cFila2 > cFila3;

        if (a) {

            numfila = 0;

        } else if (b) {

            numfila = 1;

        } else {

            numfila= 2;
        }


        Destino = ManagerIA.instance.LugarEntregas[numfila];
        lugarentrega = ManagerIA.instance.LugarEntregas[numfila];
        chambeando = true;
        ChangeState(IAStatesV.Walk);

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
        if (other.tag == "LugarEntrega" && currentState != IAStatesV.Carry) {
            Debug.Log("A");
            AsignarChamba(numfila);
            ChangeState(IAStatesV.Working);
            

        }else if (other.tag == "LugarEntrega" && currentState == IAStatesV.Carry) {
            Debug.Log("A");

            ChangeState(IAStatesV.None);
            chambeando = false;
        } else {
            Debug.Log("B");

        }

        if (other.tag == "Chamba") {

            StartCoroutine("Trabajo");
        }


    }

    //conforme a la peticion asigna un lugar al cual ir a chambear, requiere un numero de fila
    public void AsignarChamba(int a) {
        
        
        switch (a) {
            case 0:
                if (ManagerIA.instance.Clientef1[0]!=null) {
                    numpeticion = ManagerIA.instance.Clientef1[0].GetComponent<IACostumer>().peticion;
                } else {
                    BuscarChamba();
                }
                break;
            case 1:
                if (ManagerIA.instance.Clientef1[0] != null) {
                    numpeticion = ManagerIA.instance.Clientef2[0].GetComponent<IACostumer>().peticion;
                } else {
                    BuscarChamba();
                }
                break;
            case 2:
                if (ManagerIA.instance.Clientef1[0] != null) {
                    numpeticion = ManagerIA.instance.Clientef3[0].GetComponent<IACostumer>().peticion;
                } else {
                    BuscarChamba();
                }
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
