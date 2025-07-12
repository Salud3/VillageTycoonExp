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
    public Transform EntradaCompra; 
    public Transform DestinoSalida;

    [Header("Variables")]
    public bool comprado;
    public bool atendido;
    public int numfila;
    public int[] peticion;

    public void Assing(Transform lugarcompra, int numfila, Transform salida, Transform entrada)
    {
        DestinoCompra = lugarcompra;
        this.numfila = numfila;
        DestinoSalida = salida;
        EntradaCompra = entrada;
        ChangeState(ManagerIA.IACostStates.None);
    }

    public void CheckState(bool second, int dist)
    {
        int a = 2;
        //print(a);
        //print("check");
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
                            a += ManagerIA.Instance.Costumersf1.Count;
                        }
                        else
                        {
                            a += dist;
                        }

                        break;
                    case 1:
                        if (!second)
                        {
                            a += ManagerIA.Instance.Costumersf2.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 2:
                        if (!second)
                        {
                            a += ManagerIA.Instance.Costumersf3.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 3:
                        if (!second)
                        {
                            a += ManagerIA.Instance.Costumersf4.Count;
                        }
                        else
                        {
                            a += dist;
                        }

                        break;
                    case 4:
                        if (!second)
                        {
                            a += ManagerIA.Instance.Costumersf5.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 5:
                        if (!second)
                        {
                            a += ManagerIA.Instance.Costumersf6.Count;
                        }
                        else
                        {
                            a += dist;
                        }
                        break;
                    case 6:
                        if (!second)
                        {
                            a += ManagerIA.Instance.Costumersf7.Count;
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
            //this.gameObject.transform.SetParent(other.gameObject.transform);

            ChangeState(ManagerIA.IACostStates.Waiting);

            ManagerIA.Instance.CheckoutC(numfila, this);
        }

        if(comprado && other.tag == "SalidaCliente")
        {
            //Mandar a quitarme y desactivarme
            this.transform.position = EntradaCompra.position;
            ManagerIA.Instance.ExitC(this.gameObject);

        } else if (!comprado && other.tag == "SalidaCliente")
        {
            Debug.Log("Intentado Destruir");
        }

    }
    private void OnTriggerExit(Collider other) {

        if (other.tag == "LugarCompra") {
            //this.gameObject.transform.parent.SetParent(null);

            ManagerIA.Instance.LineUpdate(numfila, this.gameObject);


        }

    }



}


