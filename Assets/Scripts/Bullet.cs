using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Bullet : FloatingObject
{
    public SpriteRenderer sr;
    public TrailRenderer trail;
    public ParticleSystem psTrail;

    private void OnDisable()
    {
        SetIsGuided(false);
        trail.Clear();
        psTrail.Clear();
        trail.emitting = true;
        psTrail.Play();
        turnSpeed = 0f;
        speed = 0f;
        timer = 0f;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
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
        if (!sr.gameObject.activeSelf)
            return;
        sr.gameObject.SetActive(false);
        trail.emitting = false;
        psTrail.Stop();
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
        gameObject.SetActive(false);
        BulletGenerator.inst.BulletRemoved();
    }
    protected override void GuidedMove()
    {
        if (rb2d.bodyType == RigidbodyType2D.Static)
            return;
        base.GuidedMove();
    }
}
