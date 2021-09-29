using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntManager : MonoBehaviour
{
    public GameObject doge;
    public GameObject ant;
    public List<GameObject> ants = new List<GameObject>();
    public List<AntController> currentAnts = new List<AntController>();

    public void AddAnt()
    {
        /* TESTTTTTT@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        if (currentAnts.Count >= 7)
            return;
        */

        GameObject current = null;
        for (int i = 0; i < ants.Count; i++)
        {
            if (!ants[i].activeSelf)
            {
                current = ants[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(ant, Vector2.zero, Quaternion.identity, transform) as GameObject;
            ants.Add(current);
        }
        else
        {
            current.SetActive(true);
        }
        AntController ac = current.GetComponent<AntController>();
        ac.SetDoge(doge);
        currentAnts.Add(ac);
        SetAntTheta();
    }
    public void RemoveAnt()
    {
        if (currentAnts.Count > 0)
        {
            currentAnts[0].gameObject.SetActive(false);
            currentAnts.RemoveAt(0);
            SetAntTheta();
        }
    }
    private void SetAntTheta()
    {
        for (int i = 0; i < currentAnts.Count; i++)
        {
            currentAnts[i].SetTheta(360 / currentAnts.Count * i);
        }
    }
}
