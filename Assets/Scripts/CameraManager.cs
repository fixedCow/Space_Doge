using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager inst;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
}
