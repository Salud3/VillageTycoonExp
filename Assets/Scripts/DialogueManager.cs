using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public bool inic = true;

    private void Start()
    {
        instance = this;
        Invoke("comprobacion", 1f);
    }
    private void Update()
    {
        if (D1.GetComponent<Dialogue>().completeText && inic && !GameManager.instance.tutorials[0].T1)
        {
            GameManager.instance.wallet[0].started = true;
            GameManager.instance.tutorials[0].T1 = true;
            inic = false;
            SaveSystem.Instance.Saveall();
        }
        if (D2.GetComponent<Dialogue>().completeText && !GameManager.instance.tutorials[0].T2)
        {
            GameManager.instance.tutorials[0].T2 = true;
            SaveSystem.Instance.Saveall();
        }
        if (D3.GetComponent<Dialogue>().completeText && !GameManager.instance.tutorials[0].T3)
        {
            GameManager.instance.tutorials[0].T3 = true;
            SaveSystem.Instance.Saveall();
        }
        if (D4.GetComponent<Dialogue>().completeText && !GameManager.instance.tutorials[0].T4)
        {
            GameManager.instance.tutorials[0].T4 = true;
            SaveSystem.Instance.Saveall();
        }


    }
    void comprobacion()
    {
        if (!GameManager.instance.wallet[0].started)
        {

            D1.transform.position = this.transform.position;

        }
        else
        {
            D1.GetComponent<Dialogue>().completeText = true;
            inic = false;

            Debug.Log("aa");
        }
    }

    public void TpD2()
    {
        if (!GameManager.instance.tutorials[0].T2)
        {
            D2.transform.position = this.transform.position;
        }
        else
        {
            D2.GetComponent<Dialogue>().completeText = true;

            Debug.Log("Tuto 2 Completo anteriormente");
        }
    }

    public void TpD3()
    {
        if (!GameManager.instance.tutorials[0].T3)
        {
            D3.transform.position = this.transform.position;
        }
        else
        {
            D3.GetComponent<Dialogue>().completeText = true;

            Debug.Log("Tuto 3 Completo anteriormente");
        }
    }
    public void TpD4()
    {
        if (!GameManager.instance.tutorials[0].T4)
        {
            D4.transform.position = this.transform.position;
        }
        else
        {
            D4.GetComponent<Dialogue>().completeText = true;

            Debug.Log("Tuto 4 Completo anteriormente");
        }
    }
}
