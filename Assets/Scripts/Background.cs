using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public MeshRenderer mrd;

    private void Update()
    {
        MoveBackground();
    }
    private void MoveBackground()
    {
        if (DogeController.inst == null)
            return;
        mrd.material.mainTextureOffset += new Vector2(0, DogeController.inst.speed * Time.deltaTime / 6);
    }
}