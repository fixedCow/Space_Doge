using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AntController : MonoBehaviour
{
    public GameObject doge;
    public float speed;
    public float distance;
    private float theta;

    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        transform.position = doge.transform.position + new Vector3(distance *
        Mathf.Cos(theta * Mathf.Deg2Rad), distance * Mathf.Sin(theta * Mathf.Deg2Rad));
        if (theta < 360) theta += speed * Time.deltaTime;
        else theta = 0;
    }
    public void SetTheta(float value) => theta = value;
    public void SetDoge(GameObject _doge) => doge = _doge;
}
