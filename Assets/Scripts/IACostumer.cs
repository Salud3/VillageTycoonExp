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

    void Start()
    {
        CurrentState = ManagerIA.IACostumer.None;

        DestinoCompra = ManagerIA.instance.DestinosDeComprador[Random.Range(0, 3)];
        DestinoSalida = ManagerIA.instance.SalidasDeComprador[Random.Range(0, 3)];

    }

    void Update()
    {
        CheckState();
    }

    public void CheckState()
    {
        
        switch (CurrentState)
        {
            case ManagerIA.IACostumer.None:
                ChangeState(ManagerIA.IACostumer.Walk);
                break;
            case ManagerIA.IACostumer.Walk:
                agent.SetDestination(DestinoCompra.position);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LugarCompra")
        {
            ChangeState(ManagerIA.IACostumer.Waiting);
        }

        if(other.tag == "SalidaCliente")
        {
            Destroy(this);
        }

    }
}
