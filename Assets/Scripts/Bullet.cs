using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FloatingObject
{
    public bool isGuided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DogeController doge = collision.GetComponent<DogeController>();
        if (doge != null)
        {
            doge.Hit();
            gameObject.SetActive(false);
        }
        else if (collision.tag == "Ant")
        {
            gameObject.SetActive(false);
        }
    }
}
