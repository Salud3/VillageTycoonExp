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
    public Transform Puesto;
    public Transform Destino;

    [Header("Chambeando")]
    public bool chambeando = false;
    //Detectar la peticion
    public int numfila = 0;
    public int numfilaOld = 0;
    public int cFila1 = 0;
    public int cFila2 = 0;
    public int cFila3 = 0;

    [Header("NavMesh y cliente a que atender")]
    public NavMeshAgent agent;
    public IACostumer costumer;


    public void BuscarChamba() {
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
                tipoAldeano = TipoAldeano.Costurero;
                lugarDeTrabajo = ManagerIA.instance.VillagerTrabajos[index];
                break;
                
            case 6:
                tipoAldeano = TipoAldeano.Panadero;
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
            // Debug.Log("A");
            numfila = other.GetComponent<LugarDeEntrega>().numfila;
            //AsignarChamba(numfila);
            ChangeDestination(lugarDeTrabajo);

        }else if (other.tag == "LugarEntrega" && currentState == IAStatesV.Carry) {
           // Debug.Log("B");

            ChangeState(IAStatesV.Selling);
            StartCoroutine("Vendiendo");
        } else {
          //  Debug.Log("B");

        }

        if (other.tag == "Chamba") {
            
            ChangeState(IAStatesV.Working);
            StartCoroutine("Trabajo");
        }


    }

    //conforme a la peticion asigna un lugar al cual ir a chambear, requiere un numero de fila
    /*public void AsignarChamba(int a) {

        switch (a) {
            case 0:
                if (ManagerIA.instance.Clientef1.Count != 0 && !ManagerIA.instance.Clientef1[0].GetComponent<IACostumer>().atendido) {
                    costumer = ManagerIA.instance.Clientef1[0].GetComponent<IACostumer>();
                    Invoke("AsingDestCh",0.1f);

                } else {
                    BuscarChamba();
                }
                break;
            case 1:
                if (ManagerIA.instance.Clientef2.Count != 0 && !ManagerIA.instance.Clientef2[0].GetComponent<IACostumer>().atendido) {
                    costumer = ManagerIA.instance.Clientef2[0].GetComponent<IACostumer>();
                    Invoke("AsingDestCh", 0.1f);

                } else {
                    BuscarChamba();
                }
                break;
            case 2:
                if (ManagerIA.instance.Clientef3.Count != 0 && !ManagerIA.instance.Clientef3[0].GetComponent<IACostumer>().atendido) {
                    costumer = ManagerIA.instance.Clientef3[0].GetComponent<IACostumer>();
                    Invoke("AsingDestCh", 0.1f);

                } else {
                    BuscarChamba();
                }
                break;
            default:
                break;
        }

    }
    public void AsingDestCh() {

        costumer.atendido = true;
        ChangeDestination(lugarDeTrabajo);

    }*/

    public IEnumerator Trabajo() {

        Debug.Log("Trabajando");


        yield return new WaitForSeconds(5);

        Debug.Log("Trabajo hecho");
        ChangeDestination(Puesto);
        CheckState();

    }
    public IEnumerator Vendiendo() {

        Debug.Log("Vendiendo");
        numfilaOld = numfila;
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
