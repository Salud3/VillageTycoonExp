using UnityEngine;

internal class SpawnVillager : MonoBehaviour, ISpawnVillager
{
    //Usado para desbloquear con la compra de una estacion nueva
    public void NewVillagerNSS(int Job)
    {
        switch (Job)
        {
            case 0:
                if (ManagerIA.Instance.Farm1 == null)
                {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Farm1 = clone;
                    ManagerIA.Instance.Estructuras[0].SetActive(true);
                }

                GameManager.instance.LevelStation[Job].LevelStation++;


                break;
            case 1:
                if (ManagerIA.Instance.Farm2 == null)
                {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Farm2 = clone;
                    ManagerIA.Instance.Estructuras[1].SetActive(true);
                }

                GameManager.instance.LevelStation[Job].LevelStation++;

                break;
            case 2:
                if (ManagerIA.Instance.Farm3 == null)
                {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Farm3 = clone;
                    ManagerIA.Instance.Estructuras[2].SetActive(true);

                }

                GameManager.instance.LevelStation[Job].LevelStation++;

                break;
            case 3:
                if (ManagerIA.Instance.Costureros == null)
                {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Costureros = clone;
                    ManagerIA.Instance.Estructuras[3].SetActive(true);

                }

                GameManager.instance.LevelStation[Job].LevelStation++;

                break;
            case 4:
                if (ManagerIA.Instance.Panaderos == null)
                {
                    GameObject clone = SummonV(Job);
                   ManagerIA.Instance.Panaderos = clone;
                   ManagerIA.Instance.Estructuras[4].SetActive(true);

                }

                GameManager.instance.LevelStation[Job].LevelStation++;

                break;
            case 5:
                if (ManagerIA.Instance.Pescadores == null)
                {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Pescadores = clone;
                    ManagerIA.Instance.Estructuras[5].SetActive(true);
                }

                GameManager.instance.LevelStation[Job].LevelStation++;

                break;
            case 6:
                if (ManagerIA.Instance.Molineros == null)
                {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Molineros = clone;
                    ManagerIA.Instance.Estructuras[6].SetActive(true);
                }

                GameManager.instance.LevelStation[Job].LevelStation++;

                break;
            default:
                Debug.Log("Error summon");
                break;
        }
    }

    //Usado para instanciar los villagers que ya habian sido salvados con anterioridad
    public void SummonVSaved(int Job) 
    {
        ManagerIA.Instance.Estructuras[Job].SetActive(true);

        switch (Job) {
            case 0:
                if (ManagerIA.Instance.Farm1 == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Farm1 = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 1;
                } 
                break;
            case 1:
                if (ManagerIA.Instance.Farm2 == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Farm2 = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 2;

                }
                break;
            case 2:
                if (ManagerIA.Instance.Farm3 == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Farm3 = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 3;

                }
                break;
            case 3:
                if (ManagerIA.Instance.Costureros == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Costureros = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 4;

                }

                break;
            case 4:
                if (ManagerIA.Instance.Panaderos == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Panaderos = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 5;

                }
                break;
            case 5:
                if (ManagerIA.Instance.Pescadores == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Pescadores = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 6;
                }
                break;
            case 6:
                if (ManagerIA.Instance.Molineros == null) {
                    GameObject clone = SummonV(Job);
                    ManagerIA.Instance.Molineros = clone;
                    ManagerIA.Instance.estacionesDesbloqueadas = 7;

                }
                break;
            default:
                Debug.Log("Error summon");
                break;
        }

        

    }

    public GameObject SummonV(int Job) {
        int b = Random.Range(0, ManagerIA.Instance.VillagerSpawn.Length);
        //Debug.Log(Job+" SummonV");
        GameObject clone = Instantiate(ManagerIA.Instance.Villagers[Job], ManagerIA.Instance.VillagerSpawn[b].position, 
            new Quaternion(0, 0, 0, 0));

        clone.GetComponent<IAVillager>().AssingJob(Job);
        return clone;
    }
}