using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FloatingObject : MonoBehaviour
{
    public Rigidbody2D rb2d;

    protected float speed;
    protected Vector2 direction;

    public void Shoot()
    {
        direction = (DogeController.inst.transform.position - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }
    public void SetSpeed(float value) => speed = value;
}
