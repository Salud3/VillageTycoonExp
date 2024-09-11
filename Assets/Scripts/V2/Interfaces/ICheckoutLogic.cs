using UnityEngine;

internal interface ICheckoutLogic
{
    void AvazarFila(int fila, GameObject costumer);
    void ExitCostumer(GameObject costumer);
    void LlegoCliente(int fila, IACostumer costumer);
}