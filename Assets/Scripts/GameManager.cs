using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GameObject boomEff;
    private List<GameObject> boomEffs = new List<GameObject>();

    public float distance = 10f;           // 도지와의 거리(사건의 지평선)

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    public void InstantiateBoomEffect(Vector2 position)
    {
        GameObject current = null;

        for (int i = 0; i < boomEffs.Count; i++)
        {
            if (!boomEffs[i].activeSelf)
            {
                current = boomEffs[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(boomEff, position, Quaternion.identity, transform) as GameObject;
            boomEffs.Add(current);
        }
        else
        {
            current.transform.position = position;
            current.SetActive(true);
        }
    }
}
