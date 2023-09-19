using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IACostumer : MonoBehaviour
{
    [Header("Comprador IA")]
    public ManagerIA.IACostStates CurrentState;
    public ManagerIA.IACostStates oldState;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public Transform DestinoCompra; 
    public Transform DestinoSalida;

    [Header("Variables")]
    public bool comprado;
    public bool atendido;
    public int numfila;
    public int[] peticion;

    private void Awake() 
    {

        //ChangeState(ManagerIA.IACostStates.None);
    
    }

    public void Assing(Transform lugarcompra, int numfila, Transform salida)
    {
        DestinoCompra = lugarcompra;
        this.numfila = numfila;
        DestinoSalida = salida;
        ChangeState(ManagerIA.IACostStates.None);
    }

    //Asigna al estado actual una accion
    public int Walkdistance(int numfila)
    {
        switch (numfila)
        {
            case 0: return ManagerIA.instance.Clientef1.Count;
            case 1: return ManagerIA.instance.Clientef2.Count;
            case 2: return ManagerIA.instance.Clientef3.Count;
            case 3: return ManagerIA.instance.Clientef4.Count;
            case 4: return ManagerIA.instance.Clientef5.Count;
            case 5: return ManagerIA.instance.Clientef6.Count;
            case 6: return ManagerIA.instance.Clientef7.Count;
            default: return ManagerIA.instance.Clientef1.Count;
        }
    }
    
    public void CheckState(bool second, int dist)
    {
        int a = 2;
        print(a);
        print("check");
        switch (CurrentState)
        {
            case ManagerIA.IACostStates.None:
                ChangeState(ManagerIA.IACostStates.Walk);
                break;
            case ManagerIA.IACostStates.Walk:
                //Constante Guia para la fila
                //La constante se le suma la cantidad de clientes
                switch (numfila) {
                    case 0:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef1.Count;
                        }
                        else
                        {
                            a += dist;
                        }

                        break;
                    case 1:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef2.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 2:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef3.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 3:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef4.Count;
                        }
                        else
                        {
                            a += dist;
                        }

                        break;
                    case 4:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef5.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 5:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef6.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 6:
                        if (!second)
                        {
                            a += ManagerIA.instance.Clientef7.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    default:
                        print("Error");
                        break;
                }

                //se le asigna destino a todos los clientes
                if (a < 3)
                {

                    agent.SetDestination(DestinoCompra.position);

                }
                else {
                    agent.SetDestination(DestinoCompra.position - new Vector3(0, 0, (int)a));
                }

                break;
            case ManagerIA.IACostStates.Waiting:
                print("Esperanding");
                break;
            case ManagerIA.IACostStates.Buyed:
                agent.SetDestination(DestinoSalida.position);
                break;
            default:
                ChangeState(ManagerIA.IACostStates.Walk);
                break;
        }
    }

    //recibe su compra
    public void CompraLista() {

        comprado = true;
        ChangeState(ManagerIA.IACostStates.Buyed);


    }

    
    public void ChangeState(ManagerIA.IACostStates newstate)
    {
        CurrentState = newstate;
        //Debug.Log("Cambio de estado Cliente a " + newstate);
        CheckState(false,0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LugarCompra")
        {
            this.gameObject.transform.SetParent(other.gameObject.transform);

            ChangeState(ManagerIA.IACostStates.Waiting);

            ManagerIA.instance.LlegoCliente(numfila, this);
        }

        if(comprado && other.tag == "SalidaCliente")
        {
            ManagerIA.instance.MaxCostumersAvailables++;
            Destroy(this.gameObject,0.5f);

        } else if (!comprado && other.tag == "SalidaCliente")
        {
            Debug.Log("Intentado Destruir");
        }

    }
    private void OnTriggerExit(Collider other) {

        if (other.tag == "LugarCompra") {
            this.gameObject.transform.parent.SetParent(null);

            ManagerIA.instance.AvazarFila(numfila, this);
            /*for (int i = 0; i < DestinoCompra.childCount; i++) {

                DestinoCompra.transform.GetChild(i).GetComponent<IACostumer>().CheckState();

            }*/

        }

    }



}


