using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Rock : FloatingObject
{
    public SpriteRenderer sr;

    private void Start()
    {
        // TESTT@@@@@@@@@@@@@@@@@@@@@@@@@
        SetSpeed(Random.Range(0.05f, 0.2f));
        Shoot();
        SetAngularVelocity(Random.Range(-10f, 10f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!sr.gameObject.activeSelf)
            return;

        DogeController doge = collision.GetComponent<DogeController>();
        if (doge != null && doge.GetState() != ECharacterState.Invincible)
        {
            GameManager.inst.InstantiateBoomEffect(collision.ClosestPoint(transform.position));
            Camera.main.GetComponent<ProCamera2DShake>().Shake(0);
            GameManager.inst.SetGameStop(0.1f);
            doge.Hit();
        }
        else if (collision.tag == "BlackHole")
            Collide();
    }
    private void Collide()
    {
        GameManager.inst.InstantiateBoomEffect(transform.position);
        Deactivate();
    }
    public override void Shoot()
    {
        direction = (DogeController.inst.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }
    public void SetAngularVelocity(float vec)
    {
        rb2d.angularVelocity = vec;
    }
}
