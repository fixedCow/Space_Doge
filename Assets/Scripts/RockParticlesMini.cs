using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class RockParticlesMini : FloatingObject
{
    public RockParticles main;
    private bool isReady;

    private void OnEnable()
    {
        Invoke("SetReadyOn", 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReady)
            return;

        DogeController doge = collision.GetComponent<DogeController>();
        if (doge != null && doge.GetState() != ECharacterState.Invincible)
        {
            Camera.main.GetComponent<ProCamera2DShake>().Shake(0);
            GameManager.inst.SetGameStop(0.06f);
            if (doge.GetState() != ECharacterState.Dash)
                doge.Hit();
            Collide();
        }
        else if (collision.tag == "BlackHole")
            Deactivate();
    }
    protected override void Deactivate()
    {
        isReady = false;
        base.Deactivate();
        main.CheckActivation();
    }
    private void Collide()
    {
        GameManager.inst.InstantiateBoomEffect(transform.position);
        Deactivate();
    }
    private void SetReadyOn()
    {
        isReady = true;
    }
}
