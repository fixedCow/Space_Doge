using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Bullet : FloatingObject
{
    public SpriteRenderer sr;
    public TrailRenderer trail;
    public ParticleSystem psTrail;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!sr.gameObject.activeSelf)
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
        else if (collision.tag == "Ant")
            Collide();
        else if (collision.tag == "BlackHole")
            Deactivate();
    }
    private void Collide()
    {
        GameManager.inst.InstantiateBoomEffect(transform.position);
        Deactivate();
    }
    protected override void Deactivate()
    {
        sr.gameObject.SetActive(false);
        trail.emitting = false;
        psTrail.Stop();
        timer = 0f;
        rb2d.bodyType = RigidbodyType2D.Static;
        Invoke("ResetBullet", 1f);
    }
    protected override void GuidedCheck()
    {
        base.GuidedCheck();
        if (isGuided)
        {
            psTrail.gameObject.SetActive(true);
            trail.gameObject.SetActive(false);
        }
        else
        {
            psTrail.gameObject.SetActive(false);
            trail.gameObject.SetActive(true);
        }
    }
    private void ResetBullet()
    {
        SetIsGuided(false);
        trail.Clear();
        psTrail.Clear();
        trail.emitting = true;
        psTrail.Play();
        BulletGenerator.inst.BulletRemoved();
        gameObject.SetActive(false);
    }
    protected override void Homing()
    {
        if (rb2d.bodyType == RigidbodyType2D.Static)
            return;
        base.Homing();
    }
}
