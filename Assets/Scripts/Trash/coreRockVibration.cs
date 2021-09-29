using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreRockVibration : MonoBehaviour
{
    public GameObject main;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.05f)
        {
            SetRandomPosition();
            timer = 0;
        }
    }
    private void SetRandomPosition()
    {
        transform.position = main.transform.position + new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
    }
}
