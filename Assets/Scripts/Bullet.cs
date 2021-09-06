using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FloatingObject
{
    public TrailRenderer trail;

    private void OnEnable()
    {
        StartCoroutine(CheckSelfDestroyConditionCo());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DogeController doge = collision.GetComponent<DogeController>();
        if (doge != null)
        {
            doge.Hit();
            Collide();
        }
        else if (collision.tag == "Ant")
        {
            Collide();
        }
    }
    private void Collide()
    {
        GameManager.inst.InstantiateBoomEffect(transform.position);
        Deactivate();
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
        BulletGenerator.inst.BulletRemoved();
        trail.Clear();
    }
    private IEnumerator CheckSelfDestroyConditionCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (Vector2.Distance(DogeController.inst.transform.position, transform.position) > GameManager.inst.distance)
            {
                break;
            }
        }
        Deactivate();
    }
}
