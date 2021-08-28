using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGaugeController : MonoBehaviour
{
    private int value { get; set; }
    [SerializeField] private int hello;

    public void hi(int number)
    {
        hello = 12;
        value = number;
        hello = value;
    }
    private void Start()
    {
        Debug.Log("prestart");
        hi(8);
        Debug.Log("Start");
    }
}
