using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : MonoBehaviour
{
    public ParticleSystem trail;
    public float theta;

    private void OnEnable()
    {
        StartCoroutine(GuidedMoveCo());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
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
        */
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
    }
    private IEnumerator GuidedMoveCo()
    {
        while (true)
        {
            yield return null;
            transform.position = Vector2.MoveTowards(transform.position, DogeController.inst.transform.position, 2f * Time.deltaTime);
            if (Vector2.Distance(DogeController.inst.transform.position, transform.position) > GameManager.inst.distance)
            {
                break;
            }
        }
        Deactivate();
    }
}
