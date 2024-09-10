using UnityEngine;

internal class SpawnCostumer : MonoBehaviour, ISpawnCostumer
{
    private int _leveltotal;
    public void Isnt() 
    {
        //Decide si spawnear o no un cliente
        float a;

        switch (_leveltotal)
        {
            case >= 25:
                a = Random.Range(0, 3);
                break;
            case <= 25:
                a = Random.Range(0, 8);
                break;
        }
        print("tiempo de espera"+ a);

        if ( ((ManagerIA.Instance.MaxCostumersAvailables > 0) &&
              ((ManagerIA.Instance.Clientef1.Count + ManagerIA.Instance.Clientef2.Count +
                ManagerIA.Instance.Clientef3.Count) <= 25)) && GameManager.instance.wallet[0].started)
        {

            Invoke("SummonCostumer", a);
        
        } else {
    
            Invoke("NotSummonCost", 0.1f);
        
        }
    }

    //Spawnea un Cliente
    public void SummonCostumer() {
        int b = Random.Range(0, ManagerIA.Instance.EntradasDeComprador.Length);
        GameObject cclone =  Instantiate( ManagerIA.Instance.costumer, 
            ManagerIA.Instance.EntradasDeComprador[b].position , new Quaternion(0,0,0,0));      
        int _numfila = Random.Range(0,  ManagerIA.Instance.estacionesDesbloqueadas);
        cclone.GetComponent<IACostumer>().Assing( ManagerIA.Instance.DestinosDeComprador[_numfila],
            _numfila,  ManagerIA.Instance.SalidasDeComprador[Random.Range(0,  ManagerIA.Instance.SalidasDeComprador.Length)]);

        switch (_numfila) {
            case 0:
                ManagerIA.Instance.Clientef1.Add(cclone);
                break;
            case 1:
                ManagerIA.Instance.Clientef2.Add(cclone);

                break;  
            case 2:
                ManagerIA.Instance.Clientef3.Add(cclone);
                break;  
            case 3:
                ManagerIA.Instance.Clientef4.Add(cclone);

                break;  
            case 4:
                ManagerIA.Instance.Clientef5.Add(cclone);

                break;  
            case 5:
                ManagerIA.Instance.Clientef6.Add(cclone);

                break;  
            case 6:
                ManagerIA.Instance.Clientef7.Add(cclone);

                break;
            default:
                print("Error");
                break;
        }

        ManagerIA.Instance.MaxCostumersAvailables--;


        Invoke("Isnt", 1f);
    }

    //No spawnea Cliente
    public void NotSummonCost() {
        Invoke("Isnt", 2f);
    }
}