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

    private void Awake() {
    
        DestinoCompra = ManagerIA.instance.DestinosDeComprador[Random.Range(0, 3)];
        DestinoSalida = ManagerIA.instance.SalidasDeComprador[Random.Range(0, 3)];

    
    }
    private void Start()
    {
        ChangeState(ManagerIA.IACostumer.None);
    }

    private void Update()
    {
        if (CurrentState == ManagerIA.IACostumer.Waiting) {

        }
    }

    public void CheckState()
    {
        
        switch (CurrentState)
        {
            case ManagerIA.IACostumer.None:
                ChangeState(ManagerIA.IACostumer.Walk);
                break;
            case ManagerIA.IACostumer.Walk:

                int a = DestinoCompra.childCount+2;

                if (DestinoCompra.childCount > 0) {

                    agent.SetDestination(DestinoCompra.position + new Vector3(0, 0, (int)a));

                } else {
                    agent.SetDestination(DestinoCompra.position);
                }

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
        CheckState();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LugarCompra")
        {
            
            this.gameObject.transform.parent.SetParent(other.transform);
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


