using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IACostumer : MonoBehaviour
{
    [Header("Comprador IA")]
    public ManagerIA.IACostumer CurrentState;
    public ManagerIA.IACostumer oldState;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public Transform DestinoCompra; 
    public Transform DestinoSalida;

    [Header("Variables")]
    public bool comprado;
    int numDeFila = 0;

    private void Awake() {

        numDeFila = Random.Range(0, 3);
        DestinoCompra = ManagerIA.instance.DestinosDeComprador[numDeFila];

        switch (numDeFila) {
            case 0:
                ManagerIA.instance.Clientef1.Add(this.gameObject);
                break;
            case 1:
                ManagerIA.instance.Clientef2.Add(this.gameObject);
                break; 
            case 2:
                ManagerIA.instance.Clientef3.Add(this.gameObject);
                break;
            default:
                break;
        }

        DestinoSalida = ManagerIA.instance.SalidasDeComprador[Random.Range(0, 3)];

        ChangeState(ManagerIA.IACostumer.None);
    
    }

    public void CheckState()
    {
        
        switch (CurrentState)
        {
            case ManagerIA.IACostumer.None:
                ChangeState(ManagerIA.IACostumer.Walk);
                break;
            case ManagerIA.IACostumer.Walk:
                //Constante Guia para la fila
                int a = 2;
                //La constante se le suma la cantidad de clientes
                switch (numDeFila) {
                    case 0:
                        a += ManagerIA.instance.Clientef1.Count;
                        Debug.Log(this.gameObject + "Pos en fila: " + a);
                        break;
                    case 1:
                        a += ManagerIA.instance.Clientef2.Count;
                        Debug.Log(this.gameObject + "Pos en fila: " + a);
                        break;
                    case 2:
                        a += ManagerIA.instance.Clientef3.Count ;
                        Debug.Log(this.gameObject +"Pos en fila: " +a);
                        break;
                    default:
                        break;
                }

                //se le asigna destino a todos los clientes
                if (a > 3) {

                    agent.SetDestination(DestinoCompra.position - new Vector3(0, 0, (int)a));

                } else {
                    agent.SetDestination(DestinoCompra.position);
                }

                break;
            case ManagerIA.IACostumer.Waiting:
                print("Esperanding");
                break;
            case ManagerIA.IACostumer.Buyed:
                agent.SetDestination(DestinoSalida.position);
                break;
            default:
                ChangeState(ManagerIA.IACostumer.Walk);
                break;
        }
    }


    public void ChangeState(ManagerIA.IACostumer newstate)
    {
        CurrentState = newstate;
        Debug.Log("Cambio de estado Cliente a " + newstate);
        CheckState();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LugarCompra")
        {
            this.gameObject.transform.SetParent(other.gameObject.transform);

            ChangeState(ManagerIA.IACostumer.Waiting);
            
        }

        if(comprado && other.tag == "SalidaCliente")
        {
            Destroy(this,0.5f);
        } else {
            Debug.Log("Intentado Destruir");
        }

    }
    private void OnTriggerExit(Collider other) {

        if (other.tag == "LugarCompra") {
            for (int i = 0; i < DestinoCompra.childCount; i++) {

                DestinoCompra.transform.GetChild(i).GetComponent<IACostumer>().CheckState();

            }
            this.transform.parent.SetParent(null);

        }

    }



}


