using UnityEngine;

internal class CheckoutLogic: MonoBehaviour, ICheckoutLogic
{
    public void AvazarFila(int fila, GameObject costumer)
    {
        switch (fila)
        {
            case 0:
                ManagerIA.Instance.Costumersf1.Remove(costumer);
                
                if (ManagerIA.Instance.Costumersf1.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf1.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf1[i].GetComponent<IACostumer>().CheckState(true, i);
                    }
                }

                break;
            case 1:
                ManagerIA.Instance.Costumersf2.Remove(costumer);
                if (ManagerIA.Instance.Costumersf2.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf2.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf2[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 2:
                ManagerIA.Instance.Costumersf3.Remove(costumer);
                if (ManagerIA.Instance.Costumersf3.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf3.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf3[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 3:
                ManagerIA.Instance.Costumersf4.Remove(costumer);
                if (ManagerIA.Instance.Costumersf4.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf4.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf4[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 4:
                ManagerIA.Instance.Costumersf5.Remove(costumer);
                if (ManagerIA.Instance.Costumersf5.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf5.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf5[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 5:
                ManagerIA.Instance.Costumersf6.Remove(costumer);
                if (ManagerIA.Instance.Costumersf6.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf6.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf6[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            case 6:
                ManagerIA.Instance.Costumersf7.Remove(costumer);
                if (ManagerIA.Instance.Costumersf7.Count > 0)
                {
                    for (int i = 0; i < ManagerIA.Instance.Costumersf7.Count; i++)
                    {
                        ManagerIA.Instance.Costumersf7[i].GetComponent<IACostumer>().CheckState(true, i);

                    }
                }
                break;
            default:
                ManagerIA.Instance.Costumersf1[0].GetComponent<IACostumer>().CheckState(true, 0);

                break;
        }

    }

    public void ExitCostumer(GameObject costumer)
    {
        ManagerIA.Instance.costumerPool.Add(costumer);                
        costumer.SetActive(false);
    }

    public void LlegoCliente(int fila, IACostumer costumer)
    {
        switch (fila)
        {
            case 0:
                if (ManagerIA.Instance.Farm1 != null)
                {
                    ManagerIA.Instance.Farm1.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Farm1.GetComponent<IAVillager>().costumer = costumer;
                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 1:
                if (ManagerIA.Instance.Farm2 != null)
                {
                    ManagerIA.Instance.Farm2.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Farm2.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 2:
                if (ManagerIA.Instance.Farm3 != null)
                {
                    ManagerIA.Instance.Farm3.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Farm3.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 3:
                if (ManagerIA.Instance.Costureros != null)
                {
                    ManagerIA.Instance.Costureros.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Costureros.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 4:
                if (ManagerIA.Instance.Panaderos != null)
                {
                    ManagerIA.Instance.Panaderos.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Panaderos.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 5:
                if (ManagerIA.Instance.Pescadores != null)
                {
                    ManagerIA.Instance.Pescadores.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Pescadores.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            case 6:
                if (ManagerIA.Instance.Molineros != null)
                {
                    ManagerIA.Instance.Molineros.GetComponent<IAVillager>().chambear();
                    ManagerIA.Instance.Molineros.GetComponent<IAVillager>().costumer = costumer;

                }
                else
                {
                    Debug.Log("No hay Trabajador");
                }
                break;
            default:
                break;
        }
    }
}