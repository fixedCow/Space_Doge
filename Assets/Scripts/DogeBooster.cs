using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeBooster : MonoBehaviour
{
    public ParticleSystem ps;

    private void Update()
    {
        Boost();
    }
    private void Boost()
    {
        if (ps.startSpeed == DogeController.inst.speed)
            return;
        ps.emissionRate = DogeController.inst.speed * 2;
        ps.startSpeed = DogeController.inst.speed * 2;
    }
}
