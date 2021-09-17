using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GameObject boomEff;
    private List<GameObject> boomEffs = new List<GameObject>();
    public GameObject sonicEff;
    private List<GameObject> sonicEffs = new List<GameObject>();

    // TEST
    public AntManager am;

    public float distance = 15f;           // 도지와의 거리(사건의 지평선)

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    private void Update()
    {
        // FOR TEST @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DogeController.inst.Dash();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DogeController.inst.SetShield(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            am.AddAnt();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            am.RemoveAnt();
        }
    }
    public void SetGameStop(float time)
    {
        StartCoroutine("SetGameStopCo", time);
    }
    private IEnumerator SetGameStopCo(float time)
    {
        Time.timeScale = 0.0001f;
        yield return new WaitForSeconds(0.0001f * time);
        Time.timeScale = 1f;
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
    public void InstantiateSonicboomEffect(Vector2 position)
    {
        GameObject current = null;

        for (int i = 0; i < sonicEffs.Count; i++)
        {
            if (!sonicEffs[i].activeSelf)
            {
                current = sonicEffs[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(sonicEff, position, Quaternion.identity, transform) as GameObject;
            sonicEffs.Add(current);
        }
        else
        {
            current.transform.position = position;
            current.SetActive(true);
        }
    }
}
